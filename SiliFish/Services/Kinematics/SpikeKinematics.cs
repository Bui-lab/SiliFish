using SiliFish.DynamicUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
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
        /// <returns></returns>
        public static List<TrainOfBursts> GenerateColumnsOfBursts(KinemParam settings, double dt, List<Cell> cells, int iStart, int iEnd)
        {
            //TODO works for only somite based models
            List<TrainOfBursts> trueTrains = new();
            List<TrainOfBursts> ungroupedTrains = new();
            foreach (Cell cell in cells)
            {
                List<int> spikes = cell.GetSpikeIndices(iStart, iEnd);
                if (!spikes.Any()) continue;
                List<BurstOrSpike> burstOrSpikes = BurstOrSpike.SpikesToBursts(settings, dt, spikes, out double _);
                TrainOfBursts train = new(burstOrSpikes, cell.CellPool.ID, cell.Somite);
                ungroupedTrains.Add(train);
            }
            ungroupedTrains = ungroupedTrains.OrderBy(ut => ut.EarliestSpike.burst.SpikeTimeList[0]).ToList();
            int curIndex = 0;
            double posDelay = settings.RCPositiveDelay;
            double negDelay = Math.Abs(settings.RCNegativeDelay);
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
                            start - negDelay <= b.Bursts.SpikeTimeList[0] && b.Bursts.SpikeTimeList[0] <= end + posDelay
                            );
                    if (caudalBurst == null)
                        break;
                    trueTrain.BurstList.Add((caudaliID, caudalsID, caudalBurst));
                    caudalTrain.BurstList.RemoveAll(b =>
                                start - negDelay <= b.Bursts.SpikeTimeList[0] && b.Bursts.SpikeTimeList[0] <= end + posDelay);
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
                            end - negDelay <= b.Bursts.SpikeTimeList[0] && b.Bursts.SpikeTimeList[0] <= start + posDelay
                            );
                    if (rostralBurst == null)
                        break;
                    trueTrain.BurstList.Add((rostraliID, rostralsID, rostralBurst));
                    rostralTrain.BurstList.RemoveAll(b =>
                                end - negDelay <= b.Bursts.SpikeTimeList[0] && b.Bursts.SpikeTimeList[0] <= start + posDelay);
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
