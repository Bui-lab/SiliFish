using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.DynamicUnits
{
    public class Cluster
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

        public void MergeCluster(Cluster c)
        {
            centroid = (centroid * numMember + c.centroid * c.numMember) / (numMember + c.numMember);
            numMember += c.numMember;
            if (clusterMax < c.clusterMin)
                clusterMax = c.clusterMax;
            else
                clusterMin = c.clusterMin;

        }

    }
}
