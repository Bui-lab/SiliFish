using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace SiliFish.DataTypes
{

    public class SpatialDistribution
    {
        public Distribution XDistribution { get; set; }
        public Distribution Y_AngleDistribution { get; set; }
        public Distribution Z_RadiusDistribution { get; set; }

        [JsonIgnore, Browsable(false)]
        public static string CSVExportColumnNames => $"XDistribution,YDistribution,ZDistribution";

        [JsonIgnore, Browsable(false)]
        internal static int CSVExportColumCount => CSVExportColumnNames.Split(',').Length;
        [JsonIgnore, Browsable(false)]
        public string CSVExportValues
        {
            get => $"{XDistribution.CSVCellExportValues},{Y_AngleDistribution.CSVCellExportValues},{Z_RadiusDistribution.CSVCellExportValues}";
            set 
            {
                string[] values = value.Split(',');
                if (values.Length != CSVExportColumCount) return;
                XDistribution = Distribution.CreateDistributionObjectFromCSVCell(values[0]);
                Y_AngleDistribution= Distribution.CreateDistributionObjectFromCSVCell(values[1]);
                Z_RadiusDistribution= Distribution.CreateDistributionObjectFromCSVCell(values[2]);
            }
        }

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
