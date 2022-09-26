using HdbscanSharp.Distance;
using HdbscanSharp.Hdbscanstar;
using HdbscanSharp.Runner;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.DynamicUnits
{
    /// <summary>
    /// The class that is used to show how a cell behaves to a certain stimulus
    /// </summary>
    public class DynamicsStats
    {
        /// <summary>
        /// The list of tau values for each spike (by time)
        /// </summary>
        public Dictionary<double, double> TauDecay, TauRise;

        /// <summary>
        /// The time points of each spike
        /// </summary>
        public List<double> SpikeList;
        private Dictionary<double, double> intervals = null;

        /// <summary>
        /// The input current (stimulus)
        /// </summary>
        public double[] IList;

        /// <summary>
        /// The membrane potential
        /// </summary>
        public double[] VList;

        /// <summary>
        /// u for neurons and rel. tension for muscle cells
        /// </summary>
        public double[] SecList;

        public Dictionary<double, double> Intervals
        {
            get
            {
                if (intervals == null && SpikeList.Count > 1)
                {
                    intervals = new();
                    foreach (int i in Enumerable.Range(0, SpikeList.Count - 1))
                        intervals.Add(RunParam.static_dt * SpikeList[i], RunParam.static_dt * (SpikeList[i + 1] - SpikeList[i]));
                }
                return intervals;
            }
        }

        public Dictionary<double, double> FiringFrequency
        {
            get 
            {
                return Intervals?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value > 0 ? 1000 / kvp.Value : 0);
            }
        }

        public void Cluster()
        {
            double[][] dataset = new double[Intervals.Count][];
            for (int i = 0; i < dataset.Length; i++)
                dataset[i] = new double[] { Intervals.Values.ToArray()[i] };
            var result = HdbscanRunner.Run(new HdbscanParameters<double[]>
            {
                DataSet = dataset, // double[][] for normal matrix or Dictionary<int, int>[] for sparse matrix
                MinPoints = 2,
                MinClusterSize = 10,
                DistanceFunction = new EuclideanDistance() // See HdbscanSharp/Distance folder for more distance function
            });
        }
        public DynamicsStats(double[] stimulus)
        {
            int iMax = stimulus.Length;
            IList = stimulus;
            VList = new double[iMax];
            SecList = new double[iMax];
            TauDecay = new();
            TauRise = new();
            SpikeList = new();
        }
    }
}
