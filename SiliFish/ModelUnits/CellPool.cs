using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using SiliFish.DataTypes;

namespace SiliFish.ModelUnits
{
    public class CellPoolTemplate: IComparable<CellPoolTemplate>
    {  
        public string CellGroup { get; set; }
        public string Description { get; set; }
        public CellType CellType { get; set; }
        public NeuronClass NTMode { get; set; }//valid if CellType==Neuron
        public Color Color { get; set; } = Color.Red;
        public BodyLocation BodyLocation { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public SagittalPlane PositionLeftRight { get; set; } = SagittalPlane.NotSet;
        public int ColumnIndex2D { get; set; }
        public int NumOfCells { get; set; } = 1;

        private SpatialDistribution SpatialDistribution = new();

        public object? XDistribution
        {
            get { return SpatialDistribution.XDistribution; }
            set
            {
                SpatialDistribution.XDistribution = value is JsonElement element ? Distribution.GetOfDerivedType(element.GetRawText()) : (Distribution)value;
            }
        }
        public object? Y_AngleDistribution
        {
            get { return SpatialDistribution.Y_AngleDistribution; }
            set
            {
                SpatialDistribution.Y_AngleDistribution = value is JsonElement element ? Distribution.GetOfDerivedType(element.GetRawText()) : (Distribution)value;
            }
        }
        public object? Z_RadiusDistribution
        {
            get { return SpatialDistribution.Z_RadiusDistribution; }
            set
            {
                SpatialDistribution.Z_RadiusDistribution = value is JsonElement element ? Distribution.GetOfDerivedType(element.GetRawText()) : (Distribution)value;
            }
        }

        private Distribution _ConductionVelocity;
        public object? ConductionVelocity
        {
            get { return _ConductionVelocity; }
            set
            {
                _ConductionVelocity = value is JsonElement element ? Distribution.GetOfDerivedType(element.GetRawText()) : (Distribution)value;
            }
        }
        public bool Active { get; set; } = true;
        public TimeLine TimeLine { get; set; } = new TimeLine();
        public string Position
        {
            get
            {
                string FTS =
                    //                    (PositionDorsalVentral == FrontalPlane.Ventral ? "V" : PositionDorsalVentral == FrontalPlane.Dorsal ? "D" : "") +
                    //                    (PositionAnteriorPosterior == TransversePlane.Posterior ? "P" : PositionAnteriorPosterior == TransversePlane.Anterior ? "A" : PositionAnteriorPosterior == TransversePlane.Central ? "C" : "") +
                    (PositionLeftRight == SagittalPlane.Left ? "L" : PositionLeftRight == SagittalPlane.Right ? "R" : "LR")
                    ;
                return FTS;
            }
        }
        public override string ToString()
        {
            return CellGroup;
        }

        
        public string GetTooltip()
        {
            string ntmode = CellType == CellType.Neuron && NTMode != NeuronClass.NotSet ?
                $"Neurotransmitter: {NTMode}\r\n"   : "";
            return $"{CellGroup}\r\n" +
                $"{Description}\r\n" +
                $"{ntmode}" +
                $"Location: {BodyLocation}\r\n" +
                $"Position: {Position}\r\n" +
                $"# of cells: {NumOfCells}\r\n" +
                $"Spatial Distribution:\r\n{SpatialDistribution.GetTooltip()}\r\n" +
                $"TimeLine: {TimeLine}";
        }

        public int CompareTo(CellPoolTemplate other)
        {
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
            BodyLocation = cpl.BodyLocation;
            Parameters = new Dictionary<string, object>(cpl.Parameters);
            PositionLeftRight= cpl.PositionLeftRight;
            ColumnIndex2D = cpl.ColumnIndex2D;
            NumOfCells = cpl.NumOfCells;
            SpatialDistribution = new SpatialDistribution(cpl.SpatialDistribution);
            _ConductionVelocity = cpl._ConductionVelocity?.CreateCopy();
            TimeLine = new TimeLine(cpl.TimeLine);            
        }

    }
    public class CellPool
    {
        public static Func<double> gapWeightNoiseMultiplier;
        public static Func<double> synWeightNoiseMultiplier;
        public static Func<double> rangeNoiseMultiplier;

        private SwimmingModel Model;
        public string CellGroup { get; }
        public CellType CellType { get; }
        public Color Color { get; }
        public BodyLocation BodyPosition { get; }

        public SagittalPlane PositionLeftRight = SagittalPlane.NotSet;

        public readonly int columnIndex2D = 1; //the multiplier to differentiate the positions of different cellpools while plotting 2D model
        readonly List<Cell> Cells;
        private SpatialDistribution SpatialDistribution = new();
        public Distribution XDistribution { 
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
        public string ID { get { return Position + "_" + CellGroup; } set { } }

        public string Position
        {
            get
            {
                string FTS =
                    //                    (PositionDorsalVentral == FrontalPlane.Ventral ? "V" : PositionDorsalVentral == FrontalPlane.Dorsal ? "D" : "") +
                    //                    (PositionAnteriorPosterior == TransversePlane.Posterior ? "P" : PositionAnteriorPosterior == TransversePlane.Anterior ? "A" : PositionAnteriorPosterior == TransversePlane.Central ? "C" : "") +
                    (PositionLeftRight == SagittalPlane.Left ? "L" : PositionLeftRight == SagittalPlane.Right ? "R" : "")
                    ;
                return FTS;
            }
        }

        public CellPool(SwimmingModel model, CellPoolTemplate template, SagittalPlane leftright)
        {
            Model = model;
            CellGroup = template.CellGroup;
            CellType = template.CellType;
            Color = template.Color;
            BodyPosition = template.BodyLocation;
            PositionLeftRight = leftright;
            columnIndex2D = template.ColumnIndex2D;
            Cells = new List<Cell>();
            GenerateCells(template, leftright);
        }
        public CellPool(SwimmingModel model, CellType cellType, BodyLocation position, string group, SagittalPlane pos, int placement, Color color)
        {
            Model = model;
            CellGroup = group;
            CellType = cellType;
            BodyPosition = position;
            PositionLeftRight = pos;
            columnIndex2D = placement;
            Color = color;
            Cells = new List<Cell>();
        }

        public virtual (double, double) XRange()
        {
            if (Cells == null || Cells.Count == 0) return (999, -999);
            return (Cells.Min(c => c.x), Cells.Max(c => c.x));
        }
        public virtual (double, double) YRange()
        {
            if (Cells == null || Cells.Count == 0) return (999, -999);
            return (Cells.Min(c => c.y), Cells.Max(c => c.y));
        }
        public virtual (double, double) ZRange()
        {
            if (Cells == null || Cells.Count == 0) return (999, -999);
            return (Cells.Min(c => c.z), Cells.Max(c => c.z));
        }
        public virtual (double, double) GetConnectionRange()
        {
            double maxWeight = 0;
            double minWeight = 999;
            foreach (Cell cell in Cells)
            {
                (double localMin, double localMax) = cell.GetConnectionRange();
                if (localMin < minWeight)
                    minWeight = localMin;
                if (localMax > maxWeight)
                    maxWeight = localMax;
            }
            return (minWeight, maxWeight);
        }
        private Coordinate[] GetCoordinates(int n)
        {
            if (n <= 0) return null;
            Coordinate[] coordinates = new Coordinate[n];
            Distribution.Random = SwimmingModel.rand;

            //TODO use body location 
            double dist = Model.SpinalRostralCaudalDistance;
            double[] x = XDistribution?.GenerateNNumbers(n, dist) ?? Distribution.GenerateNRandomNumbers(n, dist);
            double[] y = new double[n];
            double[] z = new double[n];
            double z_offset = BodyPosition == BodyLocation.SpinalCord ? Model.SpinalBodyPosition : 0;

            if (Y_AngleDistribution != null && Y_AngleDistribution.Angular)
            {
                double[] angles = Y_AngleDistribution?.GenerateNNumbers(n, 180) ?? Distribution.GenerateNRandomNumbers(n, 180);

                dist = BodyPosition == BodyLocation.SpinalCord ?
                    Math.Sqrt(Math.Pow(Model.SpinalMedialLateralDistance, 2) + Math.Pow(Model.SpinalDorsalVentralDistance / 2, 2)) :
                    Math.Sqrt(Math.Pow(Model.BodyMedialLateralDistance, 2) + Math.Pow(Model.BodyDorsalVentralDistance / 2, 2));
                double centerZ = BodyPosition == BodyLocation.SpinalCord ? Model.SpinalDorsalVentralDistance / 2 : Model.BodyDorsalVentralDistance / 2;
                double[] radii = Z_RadiusDistribution?.GenerateNNumbers(n, dist) ?? Distribution.GenerateNRandomNumbers(n, dist);
                foreach (int i in Enumerable.Range(0, n))
                {
                    double radian = angles[i] * Math.PI / 180;
                    y[i] = Math.Sin(radian) * radii[i];
                    z[i] = centerZ - Math.Cos(radian) * radii[i];
                }
            }
            else
            {
                dist = BodyPosition == BodyLocation.SpinalCord ? Model.SpinalMedialLateralDistance : Model.BodyMedialLateralDistance;
                y = Y_AngleDistribution?.GenerateNNumbers(n, dist) ?? Distribution.GenerateNRandomNumbers(n, dist);
                dist = BodyPosition == BodyLocation.SpinalCord ? Model.SpinalDorsalVentralDistance : Model.BodyDorsalVentralDistance;
                z = Z_RadiusDistribution?.GenerateNNumbers(n, dist) ?? Distribution.GenerateNRandomNumbers(n, dist);
            }

            foreach (int i in Enumerable.Range(0, n))
            {
                coordinates[i].X = x[i];
                coordinates[i].Y = y[i];
                coordinates[i].Z = z[i] + z_offset;
            }
            return coordinates;
        }
        public void AddCell(Cell c)
        {
            Cells.Add(c);
            c.CellPool = this;
        }

        public Cell GetCell(string id)
        {
            return Cells.FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Cell> GetCells(int nSample = 0)
        {
            if (nSample <= 0 || Cells.Count <= nSample)
                return Cells.AsEnumerable<Cell>();
            List<int> seq = new();
            Random random = new();
            seq.Add(Cells.Select(c => c.Sequence).Min());
            if (nSample > 1)
                seq.Add(Cells.Select(c => c.Sequence).Max());
            if (nSample > 2)
                seq.AddRange(Cells.Where(c => !seq.Contains(c.Sequence)).OrderBy(c => random.Next()).Select(c => c.Sequence).Take(nSample - 2));
            return Cells.Where(c => seq.Contains(c.Sequence)).OrderBy(c => c.Sequence).Distinct().AsEnumerable<Cell>();
        }
        public Cell GetNthCell(int nSeq)
        {
            if (nSeq < 1 || Cells.Count < nSeq)
                return null;
            return Cells.ToArray()[nSeq - 1];
        }

        public void GenerateCells(CellPoolTemplate template, SagittalPlane leftright)
        {
            int n = template.NumOfCells;
            bool neuron = template.CellType == CellType.Neuron;
            XDistribution = (Distribution)template.XDistribution;
            Y_AngleDistribution = ((Distribution)template.Y_AngleDistribution).CreateCopy();
            Y_AngleDistribution.ReviewYDistribution(leftright);
            Z_RadiusDistribution = (Distribution)template.Z_RadiusDistribution;
            Coordinate[] coordinates = GetCoordinates(n);

            double[] cv = template.ConductionVelocity != null ?
                ((Distribution)template.ConductionVelocity).GenerateNNumbers(n, null) :
                Enumerable.Repeat(SwimmingModel.cv, n).ToArray();
            
            foreach (int i in Enumerable.Range(0, n))
            {
                Cell cell = neuron ? new Neuron(template, i, cv[i]) :
                    new MuscleCell(template, i);
                cell.PositionLeftRight = leftright;
                cell.coordinate = coordinates[i];
                AddCell(cell);
            }
        }

        public void FormKernel(double weight)
        {
            foreach (Neuron ic in GetCells())
            {
                //IC to IC kernel junctions
                foreach (Neuron ic2 in GetCells().Where(c => c.Sequence > ic.Sequence)) //sequence check is added to prevent recursive or self junctions
                {
                    //no distance check as ICs form a kernel
                    double mult = 1;
                    if (CellPool.gapWeightNoiseMultiplier != null)
                        mult = gapWeightNoiseMultiplier();
                    ic.CreateGapJunction(ic2, weight * mult, DistanceMode.Euclidean);
                }
            }
        }
        public void ReachToCellPoolViaGapJunction(CellPool target, CellReach reach, TimeLine timeline)
        {

            foreach (Neuron pre in this.GetCells())
            {
                //To prevent recursive gap junctions in self projecting pools
                IEnumerable<Cell> targetcells = this == target ? target.GetCells().Where(c => c.Sequence > pre.Sequence) : target.GetCells();
                foreach (Neuron post in targetcells)
                {
                    double mult = 1;
                    if (CellPool.rangeNoiseMultiplier != null)
                        mult = rangeNoiseMultiplier();
                    if (reach.WithinReach(pre, post, mult))
                    {
                        mult = 1;
                        if (CellPool.gapWeightNoiseMultiplier != null)
                            mult = gapWeightNoiseMultiplier();
                        GapJunction jnc = pre.CreateGapJunction(post, reach.Weight * mult, reach.DistanceMode);
                        jnc.SetDelay(reach.Delay_ms);
                        jnc.SetTimeSpan(timeline);
                        if (reach.FixedDuration_ms != null)
                            jnc.SetFixedDuration((int)reach.FixedDuration_ms);
                    }
                }
            }
        }

        public void ReachToCellPoolViaChemJunction(CellPool target, CellReach reach, SynapseParameters param, TimeLine timeline)
        {
            foreach (Neuron pre in this.GetCells())
            {
                foreach (Cell post in target.GetCells())
                {
                    double mult = 1;
                    if (CellPool.rangeNoiseMultiplier != null)
                        mult = rangeNoiseMultiplier();
                    if (reach.WithinReach(pre, post, mult))
                    {
                        mult = 1;
                        if (CellPool.synWeightNoiseMultiplier != null)
                            mult = synWeightNoiseMultiplier();
                        ChemicalJunction syn = pre.CreateChemicalJunction(post, param, reach.Weight * mult, reach.DistanceMode);
                        syn.SetDelay(reach.Delay_ms);
                        if (reach.FixedDuration_ms != null)
                            syn.SetFixedDuration((double)reach.FixedDuration_ms);
                        syn.SetTimeLine(timeline);
                    }
                }
            }
        }

        //FUTURE_IMPROVEMENT apply stimulus to a subset of cells
        public void ApplyStimulus(Stimulus stimulus_ms)
        {
            foreach (Cell cell in GetCells())
                cell.Stimulus = stimulus_ms;
        }
    }

}
