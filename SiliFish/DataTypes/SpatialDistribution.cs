﻿namespace SiliFish.DataTypes
{

    public class SpatialDistribution
    {
        public Distribution XDistribution { get; set; }
        public Distribution Y_AngleDistribution { get; set; }
        public Distribution Z_RadiusDistribution { get; set; }

        public SpatialDistribution()
        { }
        public SpatialDistribution(SpatialDistribution sd)
        {
            XDistribution = sd.XDistribution.CreateCopy();
            Y_AngleDistribution = sd.Y_AngleDistribution.CreateCopy();
            Z_RadiusDistribution = sd.Z_RadiusDistribution.CreateCopy();
        }
        public string GetTooltip()
        {
            if (Y_AngleDistribution != null && Y_AngleDistribution.Angular)
                return $"X: {XDistribution}\r\nAngle: {Y_AngleDistribution}\r\nRadius:{Z_RadiusDistribution}";
            else
                return $"X: {XDistribution}\r\nY: {Y_AngleDistribution}\r\nZ: {Z_RadiusDistribution}";
        }
    }
}