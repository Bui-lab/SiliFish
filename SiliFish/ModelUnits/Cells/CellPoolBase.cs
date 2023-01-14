using SiliFish.DataTypes;
using SiliFish.Definitions;
using System.Drawing;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Cells
{
    public class CellPoolBase: ModelUnitBase //TODO for cell pool, this is not necessary
    {
        public string CellGroup { get; set; }
        public CellType CellType { get; set; }
        public string Description { get; set; }
        public string CoreType { get; set; }

        public BodyLocation BodyLocation { get; set; }
        public Color Color { get; set; } = Color.Red;

        public SagittalPlane PositionLeftRight { get; set; } = SagittalPlane.Both;
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
        protected SpatialDistribution SpatialDistribution = new();

        public Distribution XDistribution
        {
            get { return SpatialDistribution.XDistribution; }
            set { SpatialDistribution.XDistribution = value; }
        }
        public Distribution Y_AngleDistribution
        {
            get { return SpatialDistribution.Y_AngleDistribution; }
            set { SpatialDistribution.Y_AngleDistribution = value; }
        }
        public Distribution Z_RadiusDistribution
        {
            get { return SpatialDistribution.Z_RadiusDistribution; }
            set { SpatialDistribution.Z_RadiusDistribution = value; }
        }

        public int ColumnIndex2D { get; set; } = 1; //the multiplier to differentiate the positions of different cellpools while plotting 2D model

        [JsonIgnore]
        public string ID { get { return Position + "_" + CellGroup; } set { } }

        public CellPoolBase() { }
        public CellPoolBase(CellPoolBase cellPoolBase)
        {
            CellGroup= cellPoolBase.CellGroup;
            CellType = cellPoolBase.CellType;
            Description= cellPoolBase.Description;
            CoreType = cellPoolBase.CoreType;
            BodyLocation= cellPoolBase.BodyLocation;
            Color = cellPoolBase.Color;
            PositionLeftRight = cellPoolBase.PositionLeftRight;
            SpatialDistribution = new(cellPoolBase.SpatialDistribution);
        }

    }
}
