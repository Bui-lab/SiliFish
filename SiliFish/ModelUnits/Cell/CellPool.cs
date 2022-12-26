using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits
{
    public struct CellSelectionStruct
    {
        public string Pools = "All";
        public SagittalPlane SagittalPlane = SagittalPlane.Both;
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

        private readonly SwimmingModel Model;
        public string CellGroup { get; }
        public CellType CellType { get; }

        [JsonIgnore]
        public Color Color { get; }
        public BodyLocation BodyLocation { get; }

        public SagittalPlane PositionLeftRight = SagittalPlane.Both;

        public readonly int columnIndex2D = 1; //the multiplier to differentiate the positions of different cellpools while plotting 2D model
        public List<Cell> Cells { get; }
        private SpatialDistribution SpatialDistribution = new();
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
        public CellPool()
        {
            Cells = new List<Cell>();
        }
        public CellPool(SwimmingModel model, CellPoolTemplate template, SagittalPlane leftright)
        {
            Model = model;
            CellGroup = template.CellGroup;
            CellType = template.CellType;
            Color = template.Color;
            BodyLocation = template.BodyLocation;
            PositionLeftRight = leftright;
            columnIndex2D = template.ColumnIndex2D;
            Cells = new List<Cell>();
            GenerateCells(template, leftright);
        }

        /// <summary>
        /// Called from predefined models
        /// </summary>
        /// <param name="placement">columnIndex2D</param>
        public CellPool(SwimmingModel model, CellType cellType, BodyLocation position, string group, SagittalPlane pos, int placement, Color color)
        {
            Model = model;
            CellGroup = group;
            CellType = cellType;
            BodyLocation = position;
            PositionLeftRight = pos;
            columnIndex2D = placement;
            Color = color;
            Cells = new List<Cell>();
        }

        public bool OnSide(SagittalPlane sagittal)
        {
            if (sagittal == SagittalPlane.Both)
                return true;
            if (sagittal == SagittalPlane.Left && PositionLeftRight == SagittalPlane.Left ||
                sagittal == SagittalPlane.Right && PositionLeftRight == SagittalPlane.Right)
                return true;
            return false;
        }
        public virtual (double, double, double) XYZMiddle()
        {
            if (Cells == null || Cells.Count == 0) return (0, 0, 0);
            int minSomite = Cells.Min(c => c.Somite);
            int maxSomite = Cells.Max(c => c.Somite);
            Cell midCell;
            if (minSomite == maxSomite) //check the cell with middle sequence
            {
                List<Cell> cells = Cells.OrderBy(c => c.Sequence).ToList();
                midCell = cells[Cells.Count / 2];
            }
            else
            {
                double avgSomite = (minSomite + maxSomite) / 2;
                double minDistance = Cells.Min(c => Math.Abs(c.Somite - avgSomite));
                List<Cell> midSomiteCells = Cells.Where(c => c.Somite - avgSomite == minDistance).OrderBy(c => c.Sequence).ToList();
                midCell = midSomiteCells[midSomiteCells.Count / 2];
            }
            int mult = 1;
            if (columnIndex2D > 0)
                mult = columnIndex2D;
            return (midCell.X, mult * midCell.Y, midCell.Z);
        }
        public virtual (double, double) XRange()
        {
            if (Cells == null || Cells.Count == 0) return (999, -999);
            return (Cells.Min(c => c.X), Cells.Max(c => c.X));
        }

        public virtual (double, double) YRange()
        {
            if (Cells == null || Cells.Count == 0) return (999, -999);
            return (Cells.Min(c => c.Y), Cells.Max(c => c.Y));
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

            double x_length = 0, x_offset = 0;
            double y_length = 0, y_offset = 0;
            double z_length = 0, z_offset = 0;
            double radius = 0;
            switch (BodyLocation)
            {
                case BodyLocation.SpinalCord:
                    x_length = somite < 0 ? Model.SpinalRostralCaudalDistance : Model.SpinalRostralCaudalDistance / Model.NumberOfSomites;
                    x_offset = Model.SupraSpinalRostralCaudalDistance + (somite >= 0 ? somite * Model.SpinalRostralCaudalDistance / Model.NumberOfSomites : 0);
                    y_length = Model.SpinalMedialLateralDistance;
                    z_length = Model.SpinalDorsalVentralDistance;
                    z_offset = Model.SpinalBodyPosition;
                    radius = Math.Sqrt(Math.Pow(Model.SpinalMedialLateralDistance, 2) + Math.Pow(Model.SpinalDorsalVentralDistance / 2, 2));
                    break;
                case BodyLocation.MusculoSkeletal:
                    x_length = somite < 0 ? Model.SpinalRostralCaudalDistance : Model.SpinalRostralCaudalDistance / Model.NumberOfSomites;
                    x_offset = Model.SupraSpinalRostralCaudalDistance + (somite >= 0 ? somite * Model.SpinalRostralCaudalDistance / Model.NumberOfSomites : 0);
                    y_length = Model.BodyMedialLateralDistance;
                    //y_offset = Model.SpinalMedialLateralDistance;
                    z_length = Model.BodyDorsalVentralDistance;
                    radius = Math.Sqrt(Math.Pow(Model.BodyMedialLateralDistance, 2) + Math.Pow(Model.BodyDorsalVentralDistance / 2, 2));
                    break;
                case BodyLocation.SupraSpinal:
                    x_length = Model.SupraSpinalRostralCaudalDistance;
                    y_length = Model.SupraSpinalMedialLateralDistance;
                    z_length = Model.SupraSpinalDorsalVentralDistance;
                    radius = Math.Sqrt(Math.Pow(Model.SupraSpinalMedialLateralDistance, 2) + Math.Pow(Model.SupraSpinalDorsalVentralDistance / 2, 2));
                    break;
            }
            double[] x = XDistribution?.GenerateNNumbers(n, x_length) ?? Distribution.GenerateNRandomNumbers(n, x_length);
            double[] y = new double[n];
            double[] z = new double[n];
            
            if (Y_AngleDistribution != null && Y_AngleDistribution.Angular)
            {
                double[] angles = Y_AngleDistribution?.GenerateNNumbers(n, 180) ?? Distribution.GenerateNRandomNumbers(n, 180);
                double centerZ = z_offset + z_length / 2;
                double[] radii = Z_RadiusDistribution?.GenerateNNumbers(n, radius) ?? Distribution.GenerateNRandomNumbers(n, radius);
                foreach (int i in Enumerable.Range(0, n))
                {
                    double radian = angles[i] * Math.PI / 180;
                    y[i] = Math.Sin(radian) * radii[i];
                    z[i] = centerZ - Math.Cos(radian) * radii[i];
                }
            }
            else
            {
                y = Y_AngleDistribution?.GenerateNNumbers(n, y_length) ?? Distribution.GenerateNRandomNumbers(n, y_length);
                z = Z_RadiusDistribution?.GenerateNNumbers(n, z_length) ?? Distribution.GenerateNRandomNumbers(n, z_length);
            }

            foreach (int i in Enumerable.Range(0, n))
            {
                coordinates[i].X = x[i] + x_offset;
                coordinates[i].Y = y[i] + y_offset;
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

            if (!Cells.Any())
                return Cells.AsEnumerable<Cell>();

            Random random = new();
            List<int> som = new();
            List<int> seq = new();
            if (cellSelection.somiteSelection == PlotSelection.Single)
            {
                som.Add(cellSelection.nSomite);
            }
            else if (cellSelection.somiteSelection == PlotSelection.FirstMiddleLast)
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

            if (cellSelection.cellSelection == PlotSelection.Single)
            {
                seq.Add(cellSelection.nCell);
            }
            else if (cellSelection.cellSelection == PlotSelection.FirstMiddleLast)
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


        public void GenerateCells(CellPoolTemplate template, SagittalPlane leftright)
        {
            int n = template.NumOfCells;
            bool neuron = template.CellType == CellType.Neuron;
            XDistribution = (Distribution)template.XDistribution;
            Y_AngleDistribution = ((Distribution)template.Y_AngleDistribution)?.CreateCopy();
            Y_AngleDistribution?.ReviewYDistribution(leftright);
            Z_RadiusDistribution = (Distribution)template.Z_RadiusDistribution;

            List<int> somites;
            (int firstSomite, int lastSomite) = Util.ParseRange(template.SomiteRange, defMin: 1, defMax: Model.NumberOfSomites);

            if (template.PerSomiteOrTotal == CountingMode.PerSomite)
                somites = Enumerable.Range(firstSomite, lastSomite - firstSomite + 1).ToList();
            else somites = new List<int>() { -1 };

            double somiteLength = 0;
            if (template.PerSomiteOrTotal == CountingMode.Total && SwimmingModelTemplate.SomiteBased)
            {
                somiteLength = Model.SpinalRostralCaudalDistance/ Model.NumberOfSomites;
            }
            foreach (int somite in somites)
            {
                Coordinate[] coordinates = GetCoordinates(n, somite);
                Dictionary<string, double[]> paramValues = template.Parameters.GenerateMultipleInstanceValues(n);

                double[] cv = template.ConductionVelocity != null ?
                    ((Distribution)template.ConductionVelocity).GenerateNNumbers(n, 0) :
                    Enumerable.Repeat(Model.cv, n).ToArray();

                foreach (int i in Enumerable.Range(0, n))
                {
                    int actualSomite = somite;
                    //if the model is somite based but the distribution is based on the total length, calculate the somite index
                    if (somite < 0 && SwimmingModelTemplate.SomiteBased)
                    {
                        double x = coordinates[i].X;
                        actualSomite = 0;
                        while (x > 0)
                        {
                            x -= somiteLength;
                            actualSomite++;
                        }
                    }
                    Cell cell = neuron ? new Neuron(template, actualSomite, i + 1, cv[i]) :
                        new MuscleCell(template, actualSomite, i + 1);
                    cell.PositionLeftRight = leftright;
                    cell.coordinate = coordinates[i];
                    cell.Parameters = paramValues.ToDictionary(kvp => kvp.Key, kvp => kvp.Value[i]);
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
        public void ReachToCellPoolViaGapJunction(CellPool target, CellReach reach, TimeLine timeline, double probabibility)
        {
            foreach (Cell pre in this.GetCells())
            {
                //To prevent recursive gap junctions in self projecting pools
                IEnumerable<Cell> targetcells = this == target ? target.GetCells().Where(c => c.Somite != pre.Somite || c.Sequence > pre.Sequence) : target.GetCells();
                foreach (Cell post in targetcells)
                {
                    if (probabibility < SwimmingModel.rand.Next(1))
                        continue;
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

        public void ReachToCellPoolViaChemSynapse(CellPool target, CellReach reach, SynapseParameters param, TimeLine timeline, double probability)
        {
            int maxIncoming = reach.MaxIncoming;
            int maxOutgoing = reach.MaxOutgoing;
            if (maxIncoming > 0 && maxOutgoing == 0)
            {//if target pool has a limit, prevent the scenario where one source cell fulfills that limit
                int numSource = this.GetCells().Count();
                int numTarget = target.GetCells().Count();
                maxOutgoing = (int)Math.Ceiling((double)maxIncoming * numTarget / numSource);
            }
            else if (maxIncoming == 0 && maxOutgoing > 0)
            {//if the source pool has a limit, prevent the scenario where one target cell fulfills that limit
                int numSource = this.GetCells().Count();
                int numTarget = target.GetCells().Count();
                maxIncoming = (int)Math.Ceiling((double)maxOutgoing * numSource / numTarget);
            }
            foreach (Neuron pre in this.GetCells())
            {
                int counter = 0;
                foreach (Cell post in target.GetCells())
                {
                    if (maxIncoming > 0) //check whether the target cell already has connections from the same pool
                    {
                        if (post is MuscleCell muscleCell)
                        {
                            int existing = muscleCell.EndPlates.Count(ep => ep.PreNeuron.CellPool == this);
                            if (existing >= maxIncoming)
                                continue;
                        }
                        else if (post is Neuron neuron)
                        {
                            int existing = neuron.Synapses.Count(syn => syn.PreNeuron.CellPool == this);
                            if (existing >= maxIncoming)
                                continue;
                        }
                    }
                    if (probability < SwimmingModel.rand.Next(1))
                        continue;
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
                        counter++;
                        if (maxOutgoing > 0 && counter >= maxOutgoing)
                            break;
                    }
                }
            }
        }

        public void ApplyStimulus(Stimulus stimulus_ms, string TargetSomite, string TargetCell)
        {
            if (stimulus_ms == null)
                return;
            int minSom = -1;
            int maxSom = int.MaxValue;
            int minSeq = -1;
            int maxSeq = int.MaxValue;
            if (!TargetSomite.StartsWith("All"))
                (minSom, maxSom) = Util.ParseRange(TargetSomite);
            if (!TargetCell.StartsWith("All"))
                (minSeq, maxSeq) = Util.ParseRange(TargetCell);

            foreach (Cell cell in GetCells().Where(c => c.Somite >= minSom && c.Somite <= maxSom && c.Sequence >= minSeq && c.Sequence <= maxSeq))
            {
                cell.Stimuli.Add(stimulus_ms);
            }
        }
    }

}
