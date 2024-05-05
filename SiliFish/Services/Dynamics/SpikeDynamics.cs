using SiliFish.DynamicUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Services.Dynamics
{
    public class SpikeDynamics
    {
        public static (DynamicsStats, (double AvgV, double AvgVPos, double AvgVNeg)) GenerateSpikeStats(DynamicsParam settings, double dt, Cell cell, int iStart, int iEnd)
        {
            List<int> spikes = cell.GetSpikeIndices(iStart, iEnd, (int)(settings.BurstBreak / dt));
            if (spikes.Count == 0) return (null, (0, 0, 0));
            if (spikes[^1] > iEnd)
                iEnd = spikes[^1];
            double[] V = cell.V.AsArray();
            if (iEnd >= V.Length)
                iEnd = V.Length - 1;
            DynamicsStats dyn = new(settings, V[0..iEnd], dt, cell.Core.VSpikeThreshold, iStart, iEnd);
            double avgV = V[iStart..iEnd].Average();
            double avgVPos = V[iStart..iEnd].Any(v => v >= cell.Core.Vr) ?
                V[iStart..iEnd].Where(v => v >= cell.Core.Vr).Average() : 0;
            double avgVNeg = V[iStart..iEnd].Any(v => v < cell.Core.Vr) ?
                V[iStart..iEnd].Where(v => v < cell.Core.Vr).Average() : 0;
            return (dyn, (avgV, avgVPos, avgVNeg));
        }

        /// <summary>
        /// Looking at the earliest episode, starts with the earliest somite, and goes caudal and rostral to find the episodes that belong to the same undulatory movement
        /// </summary>
        /// <param name="TimeArray"></param>
        /// <param name="cells">of a single cell pool and sagittal plane</param>
        /// <returns></returns>
        public static List<TrainOfBursts> GenerateColumnsOfBursts(DynamicsParam settings, double dt, List<Cell> cells, int iStart, int iEnd)
        {
            List<TrainOfBursts> trueTrains = [];
            List<TrainOfBursts> ungroupedTrains = [];
            foreach (Cell cell in cells)
            {
                List<int> spikes = cell.GetSpikeIndices(iStart, iEnd);
                if (spikes.Count == 0) continue;
                List<BurstOrSpike> burstOrSpikes = BurstOrSpike.SpikesToBursts(settings, dt, spikes, out double _);
                TrainOfBursts train = new(burstOrSpikes, cell.CellPool.ID, cell.Somite);
                ungroupedTrains.Add(train);
            }
            ungroupedTrains = [.. ungroupedTrains.OrderBy(ut => ut.EarliestSpike.burst.SpikeTimeList[0])];
            int curIndex = 0;
            double posDelay = settings.RCPositiveDelay;
            double negDelay = Math.Abs(settings.RCNegativeDelay);
            while (ungroupedTrains.Count != 0)
            {
                TrainOfBursts keyTrain = ungroupedTrains.First();
                (int iID, string sID, BurstOrSpike Bursts) keyBursts = keyTrain.BurstList[0];
                keyTrain.BurstList.Remove(keyBursts);
                if (keyTrain.BurstList.Count == 0)
                    ungroupedTrains.Remove(keyTrain);
                //going downward caudally
                TrainOfBursts curTrain = keyTrain;
                BurstOrSpike curBurst = keyBursts.Bursts;
                TrainOfBursts trueTrain = new([], null, curIndex++);
                trueTrain.BurstList.Add((keyBursts.iID, keyBursts.sID, curBurst));
                //curTrain.iID is the somite. 
                while (ungroupedTrains.Count != 0)
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
                while (ungroupedTrains.Count != 0)
                {
                    TrainOfBursts rostralTrain = ungroupedTrains.FirstOrDefault(t => t.iID == curTrain.iID - 1);
                    if (rostralTrain == null)
                        break;
                    double start = curBurst.SpikeTimeList[0];
                    double end = curBurst.SpikeTimeList[^1];
                    (int rostraliID, string rostralsID, BurstOrSpike rostralBurst) = rostralTrain.BurstList
                        .FirstOrDefault(b =>
                            start - posDelay <= b.Bursts.SpikeTimeList[0] && b.Bursts.SpikeTimeList[0] <= end + negDelay
                            );
                    if (rostralBurst == null)
                        break;
                    trueTrain.BurstList.Add((rostraliID, rostralsID, rostralBurst));
                    rostralTrain.BurstList.RemoveAll(b =>
                                start - posDelay <= b.Bursts.SpikeTimeList[0] && b.Bursts.SpikeTimeList[0] <= end + negDelay);
                    curTrain = rostralTrain;
                    curBurst = rostralBurst;
                }
                ungroupedTrains.RemoveAll(t => t.BurstList.Count == 0);
                ungroupedTrains = [.. ungroupedTrains.OrderBy(ut => ut.EarliestSpike.burst.SpikeTimeList[0])];
                trueTrains.Add(trueTrain);
            }

            return trueTrains;
        }

    }
}
