using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish
{
    public class KinemParam
    {
        public double kinemZeta = 3.0; //#damping constant , high zeta =0.5/ low = 0.1
        public double kinemW0 = 2.5; //20Hz = 125.6
        public double kinemAlpha = 0;
        public double kinemBeta = 0;
        public double kinemConvCoef = 0.1;
        public double kinemBound = 0.5;
        public int kinemDelay = 1000;
    }
    public static class SwimmingModelKinematics
    {
        private static (double[,] vel, double[,] angle) GenerateSpineVelAndAngleNoSomite(SwimmingModel model, int startIndex, int endIndex)
        {
            if (!model.ModelRun) return (null, null);
            List<Cell> LeftMuscleCells = model.MusclePools.Where(mp => mp.PositionLeftRight == SagittalPlane.Left).SelectMany(mp => mp.GetCells()).ToList();
            List<Cell> RightMuscleCells = model.MusclePools.Where(mp => mp.PositionLeftRight == SagittalPlane.Right).SelectMany(mp => mp.GetCells()).ToList();

            int nmax = endIndex - startIndex + 1;
            //TODO: left and right muscle cell count can be different
            int nMuscle = LeftMuscleCells.Count;
            if (nMuscle == 0) return (null, null);
            // Allocating arrays for velocity and position
            double[,] vel = new double[nMuscle, nmax];
            double[,] angle = new double[nMuscle, nmax];
            // Setting constants and initial values for vel. and pos.
            double vel0 = 0.0;
            double angle0 = 0.0;
            double dt = model.runParam.dt;
            int k = 0;

            double kinemAlpha = model.kinemParam.kinemAlpha;
            double kinemBeta = model.kinemParam.kinemBeta;
            double kinemConvCoef = model.kinemParam.kinemConvCoef;
            double kinemW0 = model.kinemParam.kinemW0;
            double kinemZeta = model.kinemParam.kinemZeta;

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
                    //khi is the damping coefficient: "Kinematics.Damping Coef"
                    double acc = -Math.Pow(kinemW0, 2) * angle[k, i - 1] - 2 * vel[k, i - 1] * kinemZeta * kinemW0 + coef * voltDiff;
                    vel[k, i] = vel[k, i - 1] + acc * dt;
                    angle[k, i] = angle[k, i - 1] + vel[k, i - 1] * dt;
                }
                k++;
            }

            return (vel, angle);
        }

        private static (double[,] vel, double[,] angle) GenerateSpineVelAndAngle(SwimmingModel model, int startIndex, int endIndex)
        {
            if (!model.ModelRun) return (null, null);
            if (model.NumberOfSomites <= 0)
                return GenerateSpineVelAndAngleNoSomite(model, startIndex, endIndex);

            int nmax = endIndex - startIndex + 1;
            int nSomite = model.NumberOfSomites;

            // Allocating arrays for velocity and position
            double[,] vel = new double[nSomite, nmax];
            double[,] angle = new double[nSomite, nmax];
            // Setting constants and initial values for vel. and pos.
            double vel0 = 0.0;
            double angle0 = 0.0;
            double dt = model.runParam.dt;

            double kinemAlpha = model.kinemParam.kinemAlpha;
            double kinemBeta = model.kinemParam.kinemBeta;
            double kinemConvCoef = model.kinemParam.kinemConvCoef;
            double kinemW0 = model.kinemParam.kinemW0;
            double kinemZeta = model.kinemParam.kinemZeta;

            for (int somite = 0; somite < nSomite; somite++)
            {
                List<Cell> LeftMuscleCells = model.MusclePools
                    .Where(mp => mp.PositionLeftRight == SagittalPlane.Left)
                    .SelectMany(mp => mp.GetCells().Where(c => c.Somite == somite)).ToList();
                List<Cell> RightMuscleCells = model.MusclePools
                    .Where(mp => mp.PositionLeftRight == SagittalPlane.Right)
                    .SelectMany(mp => mp.GetCells().Where(c => c.Somite == somite)).ToList();
                double R = LeftMuscleCells.Sum(c => (c as MuscleCell).R) + RightMuscleCells.Sum(c => (c as MuscleCell).R);
                R /= (LeftMuscleCells.Count + RightMuscleCells.Count);
                double coef = kinemAlpha + kinemBeta * R;
                if (Math.Abs(coef) < 0.0001)
                    coef = kinemConvCoef;
                vel[somite, 0] = vel0;
                angle[somite, 0] = angle0;
                angle[nSomite - 1, 0] = 0.0;
                foreach (var i in Enumerable.Range(1, nmax - 1))
                {
                    double voltDiff = RightMuscleCells.Sum(c => c.V[startIndex + i - 1]) - LeftMuscleCells.Sum(c => c.V[startIndex + i - 1]);
                    double acc = -Math.Pow(kinemW0, 2) * angle[somite, i - 1] - 2 * vel[somite, i - 1] * kinemZeta * kinemW0 + coef * voltDiff;
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
        public static Dictionary<string, Coordinate[]> GenerateSpineCoordinates(SwimmingModel model, int startIndex, int endIndex)
        {
            (double[,] vel, double[,] angle) = GenerateSpineVelAndAngle(model, startIndex, endIndex);

            if (vel?.GetLength(0) != angle?.GetLength(0) ||
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
        public static (Coordinate[], List<SwimmingEpisode>) GetSwimmingEpisodesUsingMuscleCells(SwimmingModel model)
        {
            /*Converted from the code written by Yann Roussel and Tuan Bui
            This function calculates tail beat frequency based upon crossings of y = 0 as calculated from the body angles calculated
            by VRMuscle and VLMuscle
            :param dt: float, time step
            :param lower_bound: int, bound used to discriminate swimming tail beats from noise
            :param upper_bound: int, bound used to discriminate swimming tail beats from noise
            :param delay: float, defines the time window during which we consider tail beats
            :return: Four 1-D numpy arrays for number of tail beats, interbeat time intervals, start times and beat times
            */

            Dictionary<string, Coordinate[]> spineCoordinates = GenerateSpineCoordinates(model, 0, model.TimeArray.Length - 1);

            //We will only use the tip of the tail to determine tail beats (if the x coordinate of the tip is smaller (or more negative)
            //than the left bound or if the x coordinate of the tip is greater than the right bound, then detect as a tail beat

            Coordinate[] tail_tip_coord = spineCoordinates.Last().Value;
            double left_bound = - model.kinemParam.kinemBound;
            double right_bound = model.kinemParam.kinemBound;
            int side = 0;
            const int LEFT = -1;
            const int RIGHT = 1;
            int nMax = model.TimeArray.Length;
            double dt = model.runParam.dt;
            double offset = model.runParam.tSkip_ms;
            int delay = (int)(model.kinemParam.kinemDelay / dt);
            List<SwimmingEpisode> episodes = new();
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
                        episodes.Add(lastEpisode);
                        side = LEFT;
                    }
                    else if (tail_tip_coord[i].X > right_bound) //beginning an episode on the right
                    {
                        lastEpisode = new SwimmingEpisode(t);
                        episodes.Add(lastEpisode);
                        side = RIGHT;
                    }
                }
                else // During an episode
                {
                    if (tail_tip_coord[i].X < left_bound && side == RIGHT)
                    {
                        side = LEFT;
                        lastEpisode.EndBeat(t);
                        lastEpisode.StartBeat(t);
                    }

                    else if (tail_tip_coord[i].X > right_bound && side == LEFT)
                    {
                        side = RIGHT;
                        lastEpisode.EndBeat(t);
                        lastEpisode.StartBeat(t);
                    }
                }
                i++;
            }

            return (tail_tip_coord, episodes.Where(e => e.End > 0).ToList());
        }


        public static (List<SwimmingEpisode>, List<SwimmingEpisode>) GetSwimmingEpisodesUsingMotoNeurons(SwimmingModel model, List<Cell> leftMNs, List<Cell> rightMNs, 
            double burstBreak,  double episodeBreak)
        {
            List<int> leftSpikes = new();
            List<int> rightSpikes = new();
            int iSkip = (int)(model.runParam.tSkip_ms / model.runParam.dt);
            foreach (Cell c in leftMNs)
                leftSpikes.AddRange(c.GetSpikeIndices(iSkip));
            foreach (Cell c in rightMNs)
                rightSpikes.AddRange(c.GetSpikeIndices(iSkip));
            leftSpikes.Sort();
            rightSpikes.Sort();

            List<SwimmingEpisode> leftEpisodes = SwimmingEpisode.GenerateEpisodes(model.TimeArray, leftSpikes, burstBreak, episodeBreak);
            List<SwimmingEpisode> rightEpisodes = SwimmingEpisode.GenerateEpisodes(model.TimeArray, rightSpikes, burstBreak, episodeBreak);

            return (leftEpisodes, rightEpisodes);
        }

    }
}
