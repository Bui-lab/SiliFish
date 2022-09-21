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
