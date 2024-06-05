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
        /// Looking at the earliest episode, starts with the earliest somite, and goes caudal and rostral to find the episodes 
        /// that belong to the same undulatory movement
        /// </summary>
        /// <param name="TimeArray"></param>
        /// <param name="cells">of a single cell pool and sagittal plane</param>
        /// <returns></returns>
        public static List<TrainOfBurstCars> GenerateColumnsOfBursts(DynamicsParam settings, double dt, List<Cell> cells, int iStart, int iEnd)
        {
            List<TrainOfBurstCars> trueTrains = [];
            List<TrainOfBurstCars> ungroupedTrains = [];
            foreach (Cell cell in cells)
            {
                List<int> spikes = cell.GetSpikeIndices(iStart, iEnd);
                if (spikes.Count == 0) continue;
                List<BurstOrSpike> burstOrSpikes = BurstOrSpike.SpikesToBursts(settings, dt, spikes, out double _);
                TrainOfBurstCars train = new(burstOrSpikes, "Somite and Pool", "burst1",  cell.Somite, cell.CellPool.ID);
                ungroupedTrains.Add(train);
            }
            ungroupedTrains = [.. ungroupedTrains.OrderBy(ut => ut.EarliestSpike.BurstOrSpike.SpikeTimeList[0])];
            int curIndex = 0;
            double posDelay = settings.RCPositiveDelay;
            double negDelay = Math.Abs(settings.RCNegativeDelay);
            while (ungroupedTrains.Count != 0)
            {
                TrainOfBurstCars keyTrain = ungroupedTrains.First();
                BurstOrSpikeCar keyBurstsCar = keyTrain.BurstCarList[0];
                keyTrain.BurstCarList.Remove(keyBurstsCar);
                if (keyTrain.BurstCarList.Count == 0)
                    ungroupedTrains.Remove(keyTrain);
                //going downward caudally
                TrainOfBurstCars curTrain = keyTrain;
                BurstOrSpikeCar curBurst = new(keyBurstsCar.BurstOrSpike, keyBurstsCar.iCarID, keyBurstsCar.sCarID);
                TrainOfBurstCars trueTrain = new([], description: "Train Seq and Pool", burstDescription: "Somite and Pool", curIndex++, keyBurstsCar.sCarID);
                trueTrain.BurstCarList.Add(curBurst);
                //curTrain.iID is the somite. 
                while (ungroupedTrains.Count != 0)
                {
                    TrainOfBurstCars caudalTrain = ungroupedTrains.FirstOrDefault(t => t.iTrainID == curTrain.iTrainID + 1);
                    if (caudalTrain == null)
                        break;
                    double start = curBurst.BurstOrSpike.SpikeTimeList[0];
                    double end = curBurst.BurstOrSpike.SpikeTimeList[^1];
                    BurstOrSpikeCar caudalBurst = caudalTrain.BurstCarList
                        .FirstOrDefault(b =>
                            start - negDelay <= b.BurstOrSpike.SpikeTimeList[0] && b.BurstOrSpike.SpikeTimeList[0] <= end + posDelay
                            );
                    if (caudalBurst == null)
                        break;
                    trueTrain.BurstCarList.Add(caudalBurst);
                    caudalTrain.BurstCarList.RemoveAll(b =>
                                start - negDelay <= b.BurstOrSpike.SpikeTimeList[0] && b.BurstOrSpike.SpikeTimeList[0] <= end + posDelay);
                    curTrain = caudalTrain;
                    curBurst = caudalBurst;
                }
                //going upward rostrally
                curTrain = keyTrain;
                curBurst = keyBurstsCar;
                while (ungroupedTrains.Count != 0)
                {
                    TrainOfBurstCars rostralTrain = ungroupedTrains.FirstOrDefault(t => t.iTrainID == curTrain.iTrainID - 1);
                    if (rostralTrain == null)
                        break;
                    double start = curBurst.BurstOrSpike.SpikeTimeList[0];
                    double end = curBurst.BurstOrSpike.SpikeTimeList[^1];
                    BurstOrSpikeCar rostralBurst = rostralTrain.BurstCarList
                        .FirstOrDefault(b =>
                            start - posDelay <= b.BurstOrSpike.SpikeTimeList[0] && b.BurstOrSpike.SpikeTimeList[0] <= end + negDelay
                            );
                    if (rostralBurst == null)
                        break;
                    trueTrain.BurstCarList.Add(rostralBurst);
                    rostralTrain.BurstCarList.RemoveAll(b =>
                                start - posDelay <= b.BurstOrSpike.SpikeTimeList[0] && b.BurstOrSpike.SpikeTimeList[0] <= end + negDelay);
                    curTrain = rostralTrain;
                    curBurst = rostralBurst;
                }
                ungroupedTrains.RemoveAll(t => t.BurstCarList.Count == 0);
                ungroupedTrains = [.. ungroupedTrains.OrderBy(ut => ut.EarliestSpike.BurstOrSpike.SpikeTimeList[0])];
                trueTrains.Add(trueTrain);
            }

            return trueTrains;
        }

        /// <summary>
        /// Looking at the earliest episode, starts with the earliest somite, and goes caudal and rostral to find the episodes that belong to the same undulatory movement
        /// </summary>
        /// <param name="TimeArray"></param>
        /// <param name="cells">of a single cell pool and sagittal plane</param>
        /// <returns></returns>
        public static List<TrainOfBurstCars> GenerateLeftRightAlternations(DynamicsParam settings, double dt, List<Cell> cells, int iStart, int iEnd)
        {
            List<TrainOfBurstCars> trueTrains = [];
            List<TrainOfBurstCars> ungroupedTrains = [];
            
            foreach (Cell cell in cells)
            {
                List<int> spikes = cell.GetSpikeIndices(iStart, iEnd);
                if (spikes.Count == 0) continue;
                List<BurstOrSpike> burstOrSpikes = BurstOrSpike.SpikesToBursts(settings, dt, spikes, out double _);
                TrainOfBurstCars train = new(burstOrSpikes, "Somite and Pool", "burst3", cell.Somite, cell.CellPool.ID);
                ungroupedTrains.Add(train);
            }
            ungroupedTrains = [.. ungroupedTrains.OrderBy(ut => ut.EarliestSpike.BurstOrSpike.SpikeTimeList[0])];
            int curIndex = 0;
            double posDelay = settings.RCPositiveDelay;
            double negDelay = Math.Abs(settings.RCNegativeDelay);
            while (ungroupedTrains.Count != 0)
            {
                TrainOfBurstCars keyTrain = ungroupedTrains.First();
                BurstOrSpikeCar keyBursts = keyTrain.BurstCarList[0];
                keyTrain.BurstCarList.Remove(keyBursts);
                if (keyTrain.BurstCarList.Count == 0)
                    ungroupedTrains.Remove(keyTrain);
                //going downward caudally
                TrainOfBurstCars curTrain = keyTrain;
                BurstOrSpikeCar curBurst = keyBursts;
                TrainOfBurstCars trueTrain = new([], "? and ?", "burst4", curIndex++, null);
                trueTrain.BurstCarList.Add(curBurst);
                //curTrain.iID is the somite. 
                while (ungroupedTrains.Count != 0)
                {
                    TrainOfBurstCars caudalTrain = ungroupedTrains.FirstOrDefault(t => t.iTrainID == curTrain.iTrainID + 1);
                    if (caudalTrain == null)
                        break;
                    double start = curBurst.BurstOrSpike.SpikeTimeList[0];
                    double end = curBurst.BurstOrSpike.SpikeTimeList[^1];
                    BurstOrSpikeCar caudalBurst = caudalTrain.BurstCarList
                        .FirstOrDefault(b =>
                            start - negDelay <= b.BurstOrSpike.SpikeTimeList[0] && b.BurstOrSpike.SpikeTimeList[0] <= end + posDelay
                            );
                    if (caudalBurst == null)
                        break;
                    trueTrain.BurstCarList.Add(caudalBurst);
                    caudalTrain.BurstCarList.RemoveAll(b =>
                                start - negDelay <= b.BurstOrSpike.SpikeTimeList[0] && b.BurstOrSpike.SpikeTimeList[0] <= end + posDelay);
                    curTrain = caudalTrain;
                    curBurst = caudalBurst;
                }
                //going upward rostrally
                curTrain = keyTrain;
                curBurst = keyBursts;
                while (ungroupedTrains.Count != 0)
                {
                    TrainOfBurstCars rostralTrain = ungroupedTrains.FirstOrDefault(t => t.iTrainID == curTrain.iTrainID - 1);
                    if (rostralTrain == null)
                        break;
                    double start = curBurst.BurstOrSpike.SpikeTimeList[0];
                    double end = curBurst.BurstOrSpike.SpikeTimeList[^1];
                    BurstOrSpikeCar rostralBurst = rostralTrain.BurstCarList
                        .FirstOrDefault(b =>
                            start - posDelay <= b.BurstOrSpike.SpikeTimeList[0] && b.BurstOrSpike.SpikeTimeList[0] <= end + negDelay
                            );
                    if (rostralBurst == null)
                        break;
                    trueTrain.BurstCarList.Add(rostralBurst);
                    rostralTrain.BurstCarList.RemoveAll(b =>
                                start - posDelay <= b.BurstOrSpike.SpikeTimeList[0] && b.BurstOrSpike.SpikeTimeList[0] <= end + negDelay);
                    curTrain = rostralTrain;
                    curBurst = rostralBurst;
                }
                ungroupedTrains.RemoveAll(t => t.BurstCarList.Count == 0);
                ungroupedTrains = [.. ungroupedTrains.OrderBy(ut => ut.EarliestSpike.BurstOrSpike.SpikeTimeList[0])];
                trueTrains.Add(trueTrain);
            }

            return trueTrains;
        }

    }
}
