using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Services.Dynamics
{
    public static class SwimmingKinematics
    {
        private static (double[,] vel, double[,] angle) GenerateSpineVelAndAngle(Simulation simulation, int startIndex, int endIndex)
        {
            if (!simulation.SimulationRun) return (null, null);
            RunningModel model = simulation.Model;
            bool useMuscleTension = model.KinemParam.UseMuscleTension;
            double halfBodyWidth = model.ModelDimensions.BodyMedialLateralDistance / 2;

            int nmax = endIndex - startIndex + 1;
            int nSomite = model.ModelDimensions.NumberOfSomites;

            // Allocating arrays for velocity and position
            double[,] vel = new double[nSomite, nmax];
            double[,] angle = new double[nSomite, nmax];
            // Setting constants and initial values for vel. and pos.
            double vel0 = 0.0;
            double angle0 = 0.0;
            double dt = model.DeltaT;

            double kinemAlpha = model.KinemParam.Alpha;
            double kinemBeta = model.KinemParam.Beta;
            double kinemConvCoef = model.KinemParam.ConvCoef;
            double kinemW0 = model.KinemParam.w0;
            double kinemZeta = model.KinemParam.Zeta;

            for (int somite = 0; somite < nSomite; somite++)
            {
                List<MuscleCell> LeftMuscleCells = model.MusclePools
                    .Where(mp => mp.PositionLeftRight == SagittalPlane.Left)
                    .SelectMany(mp => mp.GetCells().Where(c => c.Somite == somite + 1 && c.Coordinate.Y != 0)).Cast<MuscleCell>().ToList();
                List<MuscleCell> RightMuscleCells = model.MusclePools
                    .Where(mp => mp.PositionLeftRight == SagittalPlane.Right)
                    .SelectMany(mp => mp.GetCells().Where(c => c.Somite == somite + 1 && c.Coordinate.Y != 0)).Cast<MuscleCell>().ToList();
                double R = LeftMuscleCells.Sum(c => c.R) + RightMuscleCells.Sum(c => c.R);
                R /= LeftMuscleCells.Count + RightMuscleCells.Count;
                double coef = kinemAlpha + kinemBeta * R;
                if (Math.Abs(coef) < 0.0001)
                    coef = kinemConvCoef;
                vel[somite, 0] = vel0;
                angle[somite, 0] = angle0;
                angle[nSomite - 1, 0] = 0.0;
                foreach (var i in Enumerable.Range(1, nmax - 1))
                {
                    double tensDiff =
                        useMuscleTension ?
                        coef / halfBodyWidth * (RightMuscleCells.Sum(c => c.Tension[startIndex + i - 1] * Math.Abs(c.Coordinate.Y)) -
                                LeftMuscleCells.Sum(c => c.Tension[startIndex + i - 1] * Math.Abs(c.Coordinate.Y))) :
                        coef * (RightMuscleCells.Sum(c => c.V[startIndex + i - 1]) - LeftMuscleCells.Sum(c => c.V[startIndex + i - 1]));
                    double acc = -Math.Pow(kinemW0, 2) * angle[somite, i - 1] - 2 * vel[somite, i - 1] * kinemZeta * kinemW0 + tensDiff;
                    vel[somite, i] = vel[somite, i - 1] + acc * dt;
                    angle[somite, i] = angle[somite, i - 1] + vel[somite, i - 1] * dt;
                }
            }

            return (vel, angle);
        }

        /// <summary>
        /// This function needs rewriting as it does not consider:
        /// - Muscle extension/flexion - different instrinsic properties of the muscle other than its resistance
        /// - Inertia of the body (assumes it is a stick model with no resistance - exciting the most rostral muscle cell is enough for a tail beat)
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public static Dictionary<string, Coordinate[]> GenerateSpineCoordinates(Simulation simulation, int startIndex, int endIndex)
        {
            //Converted and modified from the code written by Yann Roussel and Tuan Bui
            if (!simulation.SimulationRun) return null;
            (double[,] vel, double[,] angle) = GenerateSpineVelAndAngle(simulation, startIndex, endIndex);

            if (vel == null || angle == null ||
                vel?.GetLength(0) != angle?.GetLength(0) ||
                vel?.GetLength(1) != angle?.GetLength(1))
                return null;
            int nSpineNode = vel.GetLength(0);
            int nMax = vel.GetLength(1);

            double[,] x = new double[nSpineNode, nMax + 1];
            double[,] y = new double[nSpineNode, nMax + 1];
            foreach (var i in Enumerable.Range(0, nMax))
            {
                x[0, i] = 0;
                y[0, i] = 0;
                angle[0, i] = 0;
                foreach (int l in Enumerable.Range(1, nSpineNode - 1))
                {
                    angle[l, i] = angle[l - 1, i] + angle[l, i];
                    x[l, i] = x[l - 1, i] + Math.Sin(angle[l, i]);
                    y[l, i] = y[l - 1, i] - Math.Cos(angle[l, i]);
                }
            }

            Dictionary<string, Coordinate[]> somiteCoordinates = [];
            foreach (int somiteIndex in Enumerable.Range(0, nSpineNode))
            {
                string somite = "S" + somiteIndex.ToString("000"); //for proper sorting
                somiteCoordinates.Add(somite, new Coordinate[nMax + 1]);
                somiteCoordinates[somite][0] = (0, 0);
                foreach (var i in Enumerable.Range(0, nMax))
                {
                    somiteCoordinates[somite][i] = (x[somiteIndex, i], y[somiteIndex, i]);
                }
            }
            return somiteCoordinates;
        }
        public static SwimmingEpisodes GetSwimmingEpisodesUsingMuscleCells(Simulation simulation)
        {
            if (simulation == null || !simulation.SimulationRun)
                return null;
            RunningModel model = simulation.Model;
            model.SwimmingEpisodes ??= new(simulation, GenerateSpineCoordinates(simulation, 0, model.TimeArray.Length - 1));
            return model.SwimmingEpisodes;
        }


        public static (double[], SwimmingEpisodes) GetSwimmingEpisodesUsingMotoNeurons(Simulation simulation, int somite)
        {
            RunningModel model = simulation.Model;
            (List<Cell> leftMNs, List<Cell> rightMNs) = model.GetMotoNeurons(somite);
            double episodeBreak = model.KinemParam.EpisodeBreak;
            List<int> leftSpikes = [];
            List<int> rightSpikes = [];
            int iSkip = (int)(simulation.RunParam.SkipDuration / simulation.RunParam.DeltaT);
            foreach (Cell c in leftMNs)
                leftSpikes.AddRange(c.GetSpikeIndices(iSkip));
            foreach (Cell c in rightMNs)
                rightSpikes.AddRange(c.GetSpikeIndices(iSkip));
            leftSpikes.Sort();
            rightSpikes.Sort();

            SwimmingEpisodes Episodes = SwimmingEpisode.GenerateEpisodesByMNSpikes(model.TimeArray, model.DynamicsParam, simulation.RunParam.DeltaT,
                leftSpikes, rightSpikes, episodeBreak);
            double[] range = new double[model.TimeArray.Length - iSkip];
            foreach (SwimmingEpisode episode in Episodes.Episodes)
            {
                foreach (Beat beat in episode.Beats)
                {
                    int startInd = simulation.RunParam.iIndex(beat.BeatStart);
                    int endInd = simulation.RunParam.iIndex(beat.BeatEnd);
                    int middleInd = (startInd + endInd) / 2;
                    if (beat.Direction == SagittalPlane.Left)
                    {
                        double value = leftMNs.Max(MN => MN.V.MaxValue(startInd, endInd));
                        range[middleInd - iSkip] = -value;
                    }
                    else //if (beat.Direction == SagittalPlane.Right)
                    {
                        double value = rightMNs.Max(MN => MN.V.MaxValue(startInd, endInd));
                        range[middleInd - iSkip] = value;
                    }
                }

            }

            return (range, Episodes);
        }

    }
}
