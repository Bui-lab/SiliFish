using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits
{
    public class CellPoolTemplate : ModelUnitBase
    {
        public string CellGroup { get; set; }
        public string Description { get; set; }
        public CellType CellType { get; set; }
        public BodyLocation BodyLocation { get; set; }
        public string CoreType { get; set; }
        public NeuronClass NTMode { get; set; }//relevant only if CellType==Neuron
        public Color Color { get; set; } = Color.Red;
        public Dictionary<string, object> Parameters
        {
            get { return parameters; }
            set
            {
                parameters = value?.ToDictionary(kvp => kvp.Key,
                    kvp => Distribution.CreateDistributionObject(kvp.Value) as object);
            }
        }
        public SagittalPlane PositionLeftRight { get; set; } = SagittalPlane.Both;
        public int ColumnIndex2D { get; set; }
        public int NumOfCells { get; set; } = 1;
        public CountingMode PerSomiteOrTotal { get; set; } = CountingMode.Total;

        public string SomiteRange { get; set; }

        private SpatialDistribution SpatialDistribution = new();

        public object XDistribution
        {
            get { return SpatialDistribution.XDistribution; }
            set
            {
                SpatialDistribution.XDistribution = Distribution.CreateDistributionObject(value);
            }
        }
        public object Y_AngleDistribution
        {
            get { return SpatialDistribution.Y_AngleDistribution; }
            set
            {
                SpatialDistribution.Y_AngleDistribution = Distribution.CreateDistributionObject(value);
            }
        }
        public object Z_RadiusDistribution
        {
            get { return SpatialDistribution.Z_RadiusDistribution; }
            set
            {
                SpatialDistribution.Z_RadiusDistribution = Distribution.CreateDistributionObject(value);
            }
        }

        private Distribution _ConductionVelocity;
        private Dictionary<string, object> parameters;

        public object ConductionVelocity
        {
            get { return _ConductionVelocity; }
            set
            {
                _ConductionVelocity = Distribution.CreateDistributionObject(value);
            }
        }
        [JsonIgnore]
        public string Position
        {
            get
            {
                string FTS =
                    //FUTURE_IMPROVEMENT
                    //(PositionDorsalVentral == FrontalPlane.Ventral ? "V" : PositionDorsalVentral == FrontalPlane.Dorsal ? "D" : "") +
                    //(PositionAnteriorPosterior == TransversePlane.Posterior ? "P" : PositionAnteriorPosterior == TransversePlane.Anterior ? "A" : PositionAnteriorPosterior == TransversePlane.Central ? "C" : "") +
                    (PositionLeftRight == SagittalPlane.Left ? "L" : PositionLeftRight == SagittalPlane.Right ? "R" : "LR");
                return FTS;
            }
        
        }
        public override string ToString()
        {
            return CellGroup + (Active ? "" : " (inactive)");
        }

        [JsonIgnore]
        public override string Distinguisher { get { return CellGroup; } }

        [JsonIgnore]
        public override string Tooltip
        {
            get
            {
                string ntmode = CellType == CellType.Neuron && NTMode != NeuronClass.NotSet ?
                    $"Neurotransmitter: {NTMode}\r\n" : "";
                string persomite = PerSomiteOrTotal == CountingMode.PerSomite ? "//somite" : "";
                return $"{CellGroup}\r\n" +
                    $"{Description}\r\n" +
                    $"{ntmode}" +
                    $"Position: {Position}\r\n" +
                    $"# of cells: {NumOfCells}{persomite}\r\n" +
                    $"Spatial Distribution:\r\n{SpatialDistribution.GetTooltip()}\r\n" +
                    $"TimeLine: {TimeLine_ms}\r\n" +
                    $"Active: {Active}";
            }
        }

        [JsonIgnore]
        public double? VThreshold
        {
            get
            {
                string Threshold_property = CellCoreUnit.CreateCore(CoreType, null)?.VThresholdParamName;
                if (Parameters.ContainsKey(Threshold_property))
                    return Parameters.ReadDouble(Threshold_property);
                return null;
            }
        }
        [JsonIgnore]
        public double? VReversal
        {
            get
            {
                string Reversal_property = CellCoreUnit.CreateCore(CoreType, null)?.VReversalParamName;

                if (Parameters.ContainsKey(Reversal_property))
                    return Parameters.ReadDouble(Reversal_property);
                return null;
            }
        }

        public override int CompareTo(ModelUnitBase otherbase)
        {
            CellPoolTemplate other = otherbase as CellPoolTemplate;
            return CellGroup.CompareTo(other.CellGroup);
        }

        public CellPoolTemplate()
        { }
        public CellPoolTemplate(CellPoolTemplate cpl)
        {
            if (cpl == null)
                return;
            CellGroup = cpl.CellGroup + " copy";
            Description = cpl.Description;
            CellType = cpl.CellType;
            NTMode = cpl.NTMode;
            Color = cpl.Color;
            Parameters = new Dictionary<string, object>(cpl.Parameters);
            PositionLeftRight = cpl.PositionLeftRight;
            ColumnIndex2D = cpl.ColumnIndex2D;
            NumOfCells = cpl.NumOfCells;
            PerSomiteOrTotal = cpl.PerSomiteOrTotal;
            SomiteRange = cpl.SomiteRange;
            SpatialDistribution = new SpatialDistribution(cpl.SpatialDistribution);
            _ConductionVelocity = cpl._ConductionVelocity?.CreateCopy();
            TimeLine_ms = new TimeLine(cpl.TimeLine_ms);
        }

    }

}
