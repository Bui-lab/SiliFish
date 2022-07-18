using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using SiliFish.DataTypes;

namespace SiliFish.ModelUnits
{
    public struct CellSelectionStruct
    {
        public PlotSelection somiteSelection = PlotSelection.All;
        public int nSomite = -1;
        public PlotSelection cellSelection = PlotSelection.All;
        public int nCell = -1;
        public CellSelectionStruct()
        {
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

        public virtual double XAvg()
        {
            if (Cells == null || Cells.Count == 0) return 0;
            return Cells.Average(c => c.X);
        }
        public virtual (double, double) XRange()
        {
            if (Cells == null || Cells.Count == 0) return (999, -999);
            return (Cells.Min(c => c.X), Cells.Max(c => c.X));
        }

        public virtual double YAvg()
        {
            if (Cells == null || Cells.Count == 0) return 0;
            int mult = 1;
            if (PositionLeftRight == SagittalPlane.Left)
                mult = -1;
            if (columnIndex2D == 0)
                return mult * Cells.Average(c => Math.Abs(c.Y));
            return mult * columnIndex2D;
        }
        public virtual (double, double) YRange()
        {
            if (Cells == null || Cells.Count == 0) return (999, -999);
            return (Cells.Min(c => c.Y), Cells.Max(c => c.Y));
        }

        public virtual double ZAvg()
        {
            if (Cells == null || Cells.Count == 0) return 0;
            return Cells.Average(c => c.Z);
        }

        public virtual (double, double) ZRange()
        {
            if (Cells == null || Cells.Count == 0) return (999, -999);
            return (Cells.Min(c => c.Z), Cells.Max(c => c.Z));
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
        private Coordinate[] GetCoordinates(int n, int somite = -1)
        {
            if (n <= 0) return null;
            Coordinate[] coordinates = new Coordinate[n];
            Distribution.Random = SwimmingModel.rand;

            double dist = somite < 0 ? Model.SpinalRostralCaudalDistance :
                Model.SpinalRostralCaudalDistance / Model.NumberOfSomites;
            double x_offset = somite >= 0 ?
                somite * Model.SpinalRostralCaudalDistance / Model.NumberOfSomites : 0;
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
                coordinates[i].X = x[i] + x_offset;
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

        public IEnumerable<Cell> GetCells()
        {
            return Cells.AsEnumerable<Cell>();
        }

        public IEnumerable<Cell> GetCells(CellSelectionStruct cellSelection)
        {
            if (cellSelection.somiteSelection == PlotSelection.All && cellSelection.cellSelection == PlotSelection.All)
                return Cells.AsEnumerable<Cell>();

            Random random = new();
            List<int> som = new();
            List<int> seq = new();
            if (cellSelection.somiteSelection == PlotSelection.FirstMiddleLast)
            {
                int minSom = Cells.Select(c => c.Somite).Min();
                int maxSom = Cells.Select(c => c.Somite).Max();
                som.Add(minSom);
                som.Add(maxSom);
                som.Add((int)((minSom + maxSom) / 2));
            }
            else if (cellSelection.somiteSelection == PlotSelection.Random)
            {
                IEnumerable<int> somites = Cells.Select(c => c.Somite).Distinct();
                if (somites.Count() > cellSelection.nSomite)
                    som.AddRange(somites.OrderBy(s => random.Next()).Select(s => s).Take(cellSelection.nSomite));
            }

            if (cellSelection.cellSelection == PlotSelection.FirstMiddleLast)
            {
                int minSeq = Cells.Select(c => c.Sequence).Min();
                int maxSeq = Cells.Select(c => c.Sequence).Max();
                seq.Add(minSeq);
                seq.Add(maxSeq);
                seq.Add((int)((minSeq + maxSeq) / 2));
            }
            else if (cellSelection.cellSelection == PlotSelection.Random)
            {
                IEnumerable<int> seqs = Cells.Select(c => c.Sequence).Distinct();
                if (seqs.Count() > cellSelection.nCell)
                    seq.AddRange(seqs.OrderBy(s => random.Next()).Select(s => s).Take(cellSelection.nCell));
            }
            return Cells
                .Where(c => (!som.Any() || som.Contains(c.Somite)) && (!seq.Any() || seq.Contains(c.Sequence)))
                .OrderBy(c => c.Somite)
                .ThenBy(c => c.Sequence)
                .Distinct()
                .AsEnumerable<Cell>();
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

            List<int> somites;
            if (template.PerSomiteOrTotal== CountingMode.PerSomite)
                somites = Enumerable.Range(0, Model.NumberOfSomites).ToList();
            else somites = new List<int>() { -1 };

            foreach (int somite in somites)
            {
                Coordinate[] coordinates = GetCoordinates(n, somite);

                double[] cv = template.ConductionVelocity != null ?
                    ((Distribution)template.ConductionVelocity).GenerateNNumbers(n, 0) :
                    Enumerable.Repeat(Model.cv, n).ToArray();

                foreach (int i in Enumerable.Range(0, n))
                {
                    Cell cell = neuron ? new Neuron(template, somite, i, cv[i]) :
                        new MuscleCell(template, somite, i);
                    cell.PositionLeftRight = leftright;
                    cell.coordinate = coordinates[i];
                    AddCell(cell);
                }
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

        public void ReachToCellPoolViaChemSynapse(CellPool target, CellReach reach, SynapseParameters param, TimeLine timeline)
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
                        ChemicalSynapse syn = pre.CreateChemicalSynapse(post, param, reach.Weight * mult, reach.DistanceMode);
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
