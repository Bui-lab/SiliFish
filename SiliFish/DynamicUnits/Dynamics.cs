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

        internal class Cluster
        {
            public double clusterMin, clusterMax, centroid;
            public int numMember = 0;
            public Cluster(double center)
            {
                clusterMin = clusterMax = centroid = center;
                numMember++;
            }
            public Cluster(List<double> intervals)
            {
                clusterMin = intervals.Min();
                clusterMax = intervals.Max();
                centroid = intervals.Average();
                numMember = intervals.Count;
            }
            public void AddMember(double d)
            {
                centroid = (d + centroid * numMember) / ++numMember;
                if (clusterMin > d)
                    clusterMin = d;
                if (clusterMax < d)
                    clusterMax = d;
            }
        }
        private (Cluster, Cluster) DoubleCluster(List<double> intervals)
        {
            if (intervals == null || intervals.Count() <= 1)
                return (null, null);
            
            double centroid1 = intervals.Min();
            double centroid2 = intervals.Max();
            if (centroid2 - centroid1 < centroid1 * 0.1)//single cluster
                return (new Cluster(intervals), null);
            intervals.Remove(centroid1);
            intervals.Remove(centroid2);
            Cluster cluster1 = new(centroid1);
            Cluster cluster2 = new(centroid2);
            double center = (centroid1 + centroid2) / 2;
            foreach (double d in intervals)
            {
                if (d < center) //add to cluster 1
                    cluster1.AddMember(d);
                else //add to cluster 2
                    cluster2.AddMember(d);
                center = (cluster1.centroid + cluster2.centroid) / 2;
            }
            return (cluster1, cluster2);
        }

        public void CreateClusters()
        {
            if (Intervals == null || Intervals.Count() <= 1) return;
            List<double> intervals = Intervals.Select(kvp => kvp.Value).ToList();
            (Cluster cluster1, Cluster cluster2) = DoubleCluster(intervals);
            if (cluster2 == null) return;
            intervals = Intervals.Select(kvp => kvp.Value).ToList(); //reset as min and max are removed during cluster generation
            bool[] home = new bool[intervals.Count];
            int ind = 0;
            foreach (double d in intervals)
            {
                home[ind++] = d <= cluster1.clusterMax;
            }
            List<List<double>> Bursts = new();
            bool newBurst = true;
            ind = 0;
            foreach (bool b in home)
            {
                if (b)
                {
                    if (newBurst)
                    {
                        Bursts.Add(new List<double>());
                        newBurst = false;
                    }
                    Bursts.Last().Add(ind);
                }
                else
                    newBurst = true;
                ind++;
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
