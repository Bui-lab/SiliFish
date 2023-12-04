using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using SiliFish.ModelUnits.Parameters;
using SiliFish.DynamicUnits;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace SiliFish.Services.Dynamics
{
    public static class SwimmingKinematics
    {
        private static (double[,] vel, double[,] angle) GenerateSpineVelAndAngleNoSomite(RunningModel model, int startIndex, int endIndex)
        {
            if (!model.ModelRun) return (null, null);
            List<Cell> LeftMuscleCells = model.MusclePools.Where(mp => mp.PositionLeftRight == SagittalPlane.Left).SelectMany(mp => mp.GetCells()).ToList();
            List<Cell> RightMuscleCells = model.MusclePools.Where(mp => mp.PositionLeftRight == SagittalPlane.Right).SelectMany(mp => mp.GetCells()).ToList();

            int nmax = endIndex - startIndex + 1;
            int nMuscle = Math.Min(LeftMuscleCells.Count, RightMuscleCells.Count);
            if (nMuscle == 0) return (null, null);
            // Allocating arrays for velocity and position
            double[,] vel = new double[nMuscle, nmax];
            double[,] angle = new double[nMuscle, nmax];
            // Setting constants and initial values for vel. and pos.
            double vel0 = 0.0;
            double angle0 = 0.0;
            double dt = model.RunParam.DeltaT;
            int k = 0;

            double kinemAlpha = model.KinemParam.Alpha;
            double kinemBeta = model.KinemParam.Beta;
            double kinemConvCoef = model.KinemParam.ConvCoef;
            double kinemW0 = model.KinemParam.w0;
            double kinemZeta = model.KinemParam.Zeta;

            foreach (MuscleCell leftMuscle in LeftMuscleCells.OrderBy(c => c.Sequence))
            {
                MuscleCell rightMuscle = (MuscleCell)RightMuscleCells.FirstOrDefault(c => c.Sequence == leftMuscle.Sequence);
                if (rightMuscle == null)
                    continue;
                vel[k, 0] = vel0;
                angle[k, 0] = angle0;
                angle[nMuscle - 1, 0] = 0.0;
                double R = (leftMuscle.R + rightMuscle.R) / 2;
                double coef = kinemAlpha + kinemBeta * R;
                if (Math.Abs(coef) < 0.0001)
                    coef = kinemConvCoef;
                foreach (var i in Enumerable.Range(1, nmax - 1))
                {
                    double voltDiff = rightMuscle.V[startIndex + i - 1] - leftMuscle.V[startIndex + i - 1];
                    double acc = -Math.Pow(kinemW0, 2) * angle[k, i - 1] - 2 * vel[k, i - 1] * kinemZeta * kinemW0 + coef * voltDiff;
                    vel[k, i] = vel[k, i - 1] + acc * dt;
                    angle[k, i] = angle[k, i - 1] + vel[k, i - 1] * dt;
                }
                k++;
            }

            return (vel, angle);
        }

        private static (double[,] vel, double[,] angle) GenerateSpineVelAndAngle(RunningModel model, int startIndex, int endIndex)
        {
            if (!model.ModelRun) return (null, null);
            if (model.ModelDimensions.NumberOfSomites <= 0)
                return GenerateSpineVelAndAngleNoSomite(model, startIndex, endIndex);
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
            double dt = model.RunParam.DeltaT;

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
        public static Dictionary<string, Coordinate[]> GenerateSpineCoordinates(RunningModel model, int startIndex, int endIndex)
        {
            (double[,] vel, double[,] angle) = GenerateSpineVelAndAngle(model, startIndex, endIndex);

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

            Dictionary<string, Coordinate[]> somiteCoordinates = new();
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
        public static (Coordinate[], SwimmingEpisodes) GetSwimmingEpisodesUsingMuscleCells(RunningModel model)
        {
            //Converted from the code written by Yann Roussel and Tuan Bui
            
            Dictionary<string, Coordinate[]> spineCoordinates = GenerateSpineCoordinates(model, 0, model.TimeArray.Length - 1);

            //We will only use the tip of the tail to determine tail beats (if the x coordinate of the tip is smaller (or more negative)
            //than the left bound or if the x coordinate of the tip is greater than the right bound, then detect as a tail beat

            Coordinate[] tail_tip_coord = spineCoordinates.Last().Value;
            double left_bound = -model.KinemParam.Boundary;
            double right_bound = model.KinemParam.Boundary;
            int side = 0;
            const int LEFT = -1;
            const int RIGHT = 1;
            int nMax = model.TimeArray.Length;
            double dt = model.RunParam.DeltaT;
            double offset = model.RunParam.SkipDuration;
            int delay = (int)(model.KinemParam.EpisodeBreak / dt);
            SwimmingEpisodes episodes = new();
            SwimmingEpisode lastEpisode = null;
            int i = (int)(offset / dt);
            while (i < nMax)
            {
                int iMax = Math.Min(i + delay, nMax);
                Coordinate[] window = tail_tip_coord[i..iMax];
                double t = model.TimeArray[i];
                if (!window.Any(coor => coor.X < left_bound || coor.X > right_bound))
                {
                    side = 0;
                    lastEpisode?.EndEpisode(t);
                    lastEpisode = null;
                    i = iMax;
                    continue;
                }

                if (lastEpisode == null)
                {
                    if (tail_tip_coord[i].X < left_bound)//beginning an episode on the left
                    {
                        lastEpisode = new SwimmingEpisode(t);
                        episodes.AddEpisode(lastEpisode);
                        side = LEFT;
                    }
                    else if (tail_tip_coord[i].X > right_bound) //beginning an episode on the right
                    {
                        lastEpisode = new SwimmingEpisode(t);
                        episodes.AddEpisode(lastEpisode);
                        side = RIGHT;
                    }
                }
                else // During an episode
                {
                    if (tail_tip_coord[i].X < left_bound && side == RIGHT)
                    {
                        side = LEFT;
                        lastEpisode.EndBeat(t);
                        lastEpisode.StartBeat(t, SagittalPlane.Left);
                    }

                    else if (tail_tip_coord[i].X > right_bound && side == LEFT)
                    {
                        side = RIGHT;
                        lastEpisode.EndBeat(t);
                        lastEpisode.StartBeat(t, SagittalPlane.Right);
                    }
                }
                i++;
            }
            episodes.Smooth(model.TimeArray[^1]);

            return (tail_tip_coord, episodes);
        }


        public static (double[], SwimmingEpisodes) GetSwimmingEpisodesUsingMotoNeurons(RunningModel model, int somite)
        {
            (List<Cell> leftMNs, List<Cell> rightMNs) = model.GetMotoNeurons(somite);
            double episodeBreak = model.KinemParam.EpisodeBreak;
            List<int> leftSpikes = new();
            List<int> rightSpikes = new();
            int iSkip = (int)(model.RunParam.SkipDuration / model.RunParam.DeltaT);
            foreach (Cell c in leftMNs)
                leftSpikes.AddRange(c.GetSpikeIndices(iSkip));
            foreach (Cell c in rightMNs)
                rightSpikes.AddRange(c.GetSpikeIndices(iSkip));
            leftSpikes.Sort();
            rightSpikes.Sort();

            SwimmingEpisodes Episodes = SwimmingEpisode.GenerateEpisodes(model.TimeArray, model.DynamicsParam, model.RunParam.DeltaT, 
                leftSpikes, rightSpikes, episodeBreak);
            double[] range = new double[model.TimeArray.Length - iSkip];
            foreach (SwimmingEpisode episode in Episodes.Episodes)
            {
                foreach (Beat beat in episode.Beats)
                {
                    int startInd = model.RunParam.iIndex(beat.BeatStart);
                    int endInd = model.RunParam.iIndex(beat.BeatEnd);
                    int middleInd = (startInd + endInd) / 2;
                    if (beat.Direction == SagittalPlane.Left)
                    {
                        double value = leftMNs.Max(MN => MN.V.Skip(startInd).Take(endInd - startInd + 1).Max());
                        range[middleInd - iSkip] = -value;
                    }
                    else //if (beat.Direction == SagittalPlane.Right)
                    {
                        double value = rightMNs.Max(MN => MN.V.Skip(startInd).Take(endInd - startInd + 1).Max());
                        range[middleInd - iSkip] = value;
                    }
                }

            }

            return (range, Episodes);
        }

    }
}
