using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SiliFish.DataTypes
{

    public class SpatialDistribution
    {
        public Distribution XDistribution { get; set; }
        public Distribution Y_AngleDistribution { get; set; }
        public Distribution Z_RadiusDistribution { get; set; }

        [JsonIgnore, Browsable(false)]
        public static string CSVExportColumnNames => $"X{Distribution.CSVExportColumnNames},Y{Distribution.CSVExportColumnNames},Z{Distribution.CSVExportColumnNames}";

        [JsonIgnore, Browsable(false)]
        private static int CSVExportColumCount => CSVExportColumnNames.Split(',').Length;
        [JsonIgnore, Browsable(false)]
        public string CSVExportValues => $"{XDistribution.CSVExportValues},{Y_AngleDistribution.CSVExportValues},{Z_RadiusDistribution.CSVExportValues}";

        public SpatialDistribution()
        { }
        public SpatialDistribution Clone()
        {
            return new SpatialDistribution()
            {
                XDistribution = XDistribution.Clone(),
                Y_AngleDistribution = Y_AngleDistribution.Clone(),
                Z_RadiusDistribution = Z_RadiusDistribution.Clone()
            };
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
