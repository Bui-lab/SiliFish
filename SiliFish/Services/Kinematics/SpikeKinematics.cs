using SiliFish.DynamicUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Swimming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Kinematics
{
    public class SpikeKinematics
    {
        /// <summary>
        /// Looking at the earliest episode, starts with the earliest somite, and goes caudal and rostral to find the episodes that belong to the same undulatory movement
        /// </summary>
        /// <param name="TimeArray"></param>
        /// <param name="cells">of a single cell pool and sagittal plane</param>
        /// <param name="episodeBreak"></param>
        /// <returns></returns>
        public static List<TrainOfBursts> GenerateColumnsOfBursts(List<Cell> cells, int iStart, int iEnd, double episodeBreak)
        {
            //TODO works for only somite based models
            List<TrainOfBursts> trueTrains = new();
            List<TrainOfBursts> ungroupedTrains = new();
            foreach (Cell cell in cells)
            {
                List<int> spikes = cell.GetSpikeIndices(iStart, iEnd);
                if (!spikes.Any()) continue;
                List<BurstOrSpike> burstOrSpikes = BurstOrSpike.SpikesToBursts(new ModelSettings(), 0.1, spikes, out double _);//TODO send the current model settings and dt
                TrainOfBursts train = new(burstOrSpikes, cell.CellPool.ID, cell.Somite);
                ungroupedTrains.Add(train);
            }
            ungroupedTrains = ungroupedTrains.OrderBy(ut => ut.EarliestSpike.burst.SpikeTimeList[0]).ToList();
            int curIndex = 0;
            while (ungroupedTrains.Any())
            {
                TrainOfBursts keyTrain = ungroupedTrains.First();
                (int iID, string sID, BurstOrSpike Bursts) keyBursts = keyTrain.BurstList[0];
                keyTrain.BurstList.Remove(keyBursts);
                if (keyTrain.BurstList.Count == 0)
                    ungroupedTrains.Remove(keyTrain);
                //going downward caudally
                TrainOfBursts curTrain = keyTrain;
                BurstOrSpike curBurst = keyBursts.Bursts;
                TrainOfBursts trueTrain = new(new List<BurstOrSpike>(), null, curIndex++);
                trueTrain.BurstList.Add((keyBursts.iID, keyBursts.sID, curBurst));
                //curTrain.iID is the somite. 
                while (ungroupedTrains.Any())
                {
                    TrainOfBursts caudalTrain = ungroupedTrains.FirstOrDefault(t => t.iID == curTrain.iID + 1);
                    if (caudalTrain == null)
                        break;
                    double start = curBurst.SpikeTimeList[0];
                    double end = curBurst.SpikeTimeList[^1];
                    (int caudaliID, string caudalsID, BurstOrSpike caudalBurst) = caudalTrain.BurstList
                        .FirstOrDefault(b =>
                            start - episodeBreak <= b.Bursts.SpikeTimeList[0] && b.Bursts.SpikeTimeList[0] <= end + episodeBreak ||
                            start - episodeBreak <= b.Bursts.SpikeTimeList[^1] && b.Bursts.SpikeTimeList[^1] <= end + episodeBreak
                            );
                    if (caudalBurst == null)
                        break;
                    trueTrain.BurstList.Add((caudaliID, caudalsID, caudalBurst));
                    caudalTrain.BurstList.RemoveAll(b =>
                                start - episodeBreak <= b.Bursts.SpikeTimeList[0] && b.Bursts.SpikeTimeList[0] <= end + episodeBreak ||
                                start - episodeBreak <= b.Bursts.SpikeTimeList[^1] && b.Bursts.SpikeTimeList[^1] <= end + episodeBreak);
                    curTrain = caudalTrain;
                    curBurst = caudalBurst;
                }
                //going upward rostrally
                curTrain = keyTrain;
                curBurst = keyBursts.Bursts;
                while (ungroupedTrains.Any())
                {
                    TrainOfBursts rostralTrain = ungroupedTrains.FirstOrDefault(t => t.iID == curTrain.iID - 1);
                    if (rostralTrain == null)
                        break;
                    double start = curBurst.SpikeTimeList[0];
                    double end = curBurst.SpikeTimeList[^1];
                    (int rostraliID, string rostralsID, BurstOrSpike rostralBurst) = rostralTrain.BurstList
                        .FirstOrDefault(b =>
                            start - 10 <= b.Bursts.SpikeTimeList[0] && b.Bursts.SpikeTimeList[0] <= end + 10 ||//TODO hardcoded
                            start - 10 <= b.Bursts.SpikeTimeList[^1] && b.Bursts.SpikeTimeList[^1] <= end + 10
                            );
                    if (rostralBurst == null)
                        break;
                    trueTrain.BurstList.Add((rostraliID, rostralsID, rostralBurst));
                    rostralTrain.BurstList.RemoveAll(b =>
                                start - 10 <= b.Bursts.SpikeTimeList[0] && b.Bursts.SpikeTimeList[0] <= end + 10 ||//TODO hardcoded
                                start - 10 <= b.Bursts.SpikeTimeList[^1] && b.Bursts.SpikeTimeList[^1] <= end + 10);
                    curTrain = rostralTrain;
                    curBurst = rostralBurst;
                }
                ungroupedTrains.RemoveAll(t => t.BurstList.Count == 0);
                ungroupedTrains = ungroupedTrains.OrderBy(ut => ut.EarliestSpike.burst.SpikeTimeList[0]).ToList();
                trueTrains.Add(trueTrain);
            }

            return trueTrains;
        }

    }
}
