using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Cells
{
    public class CellPool: CellPoolTemplate
    {
        [JsonIgnore]
        public RunningModel Model { get; set; }

        [JsonPropertyOrder(2)]
        public List<Cell> Cells { get; set; }

        [JsonIgnore]
        public IEnumerable<JunctionBase> Projections
        {
            get
            {
                List<JunctionBase> jncs = [];
                foreach (Cell cell in Cells)
                {
                    if (cell is Neuron neuron)
                    {
                        jncs.AddRange(neuron.Terminals);
                        jncs.AddRange(neuron.Synapses);
                    }
                    else if (cell is MuscleCell muscleCell)
                        jncs.AddRange(muscleCell.EndPlates);
                    jncs.AddRange(cell.GapJunctions);
                }
                return jncs;
            }
        }

        public override bool Active
        {
            get => base.Active;
            set
            {
                if (Cells != null)
                {
                    foreach (Cell c in Cells)
                        c.Active = value;
                }
                base.Active = value;
            }
        }
        public CellPool()
        {
            Cells = [];
            PlotType = "CellPool";
        }

        public CellPool(CellPool cellPool):base(cellPool) 
        {
            PlotType = "CellPool";
            Cells = [];
        }

        public CellPool(RunningModel model, CellPoolTemplate template, SagittalPlane leftright)
        {
            PlotType = "CellPool";
            Model = model;
            CellGroup = template.CellGroup;
            Description = template.Description;
            CellType = template.CellType;
            CoreType = template.CoreType;
            NTMode = template.NTMode;
            Color = template.Color;
            BodyLocation = template.BodyLocation;
            PositionLeftRight = leftright;
            ColumnIndex2D = template.ColumnIndex2D;
            NumOfCells = template.NumOfCells;
            XDistribution = template.XDistribution?.Clone();
            Y_AngleDistribution = template.Y_AngleDistribution?.Clone();
            Y_AngleDistribution = Y_AngleDistribution?.ReviewYDistribution(leftright);
            Z_RadiusDistribution = template.Z_RadiusDistribution?.Clone();
            AscendingAxonLength = template.AscendingAxonLength?.Clone();
            DescendingAxonLength = template.DescendingAxonLength?.Clone();
            Parameters = template.Parameters.ToDictionary(entry => entry.Key, entry => entry.Value);
            PerSomiteOrTotal = template.PerSomiteOrTotal;
            SomiteRange = template.SomiteRange;
            ConductionVelocity = template.ConductionVelocity?.Clone();
            TimeLine_ms = new TimeLine(template.TimeLine_ms);
            Active = template.Active;
            Cells = [];
            GenerateCells();
        }

        /// <summary>
        /// Called from predefined models
        /// </summary>
        /// <param name="placement">columnIndex2D</param>
        public CellPool(RunningModel model, CellType cellType, BodyLocation position, string group, SagittalPlane pos, int placement, Color color)
        {
            Model = model;
            CellGroup = group;
            CellType = cellType;
            BodyLocation = position;
            PositionLeftRight = pos;
            ColumnIndex2D = placement;
            Color = color;
            Cells = [];
        }

        public override CellPoolTemplate CreateTemplateCopy()
        {
            return new CellPoolTemplate(this);
        }

        public override ModelUnitBase CreateCopy()
        {
            return new CellPool(Model, CellType, BodyLocation, CellGroup, PositionLeftRight, ColumnIndex2D, Color);
        }
        public void LinkObjects(RunningModel model)
        {
            Model = model;
            foreach (Cell cell in Cells)
            {
                cell.CellPool = this;
                cell.LinkObjects(model, this);
            }
        }

        public override List<Difference> DiffersFrom(ModelUnitBase other)
        {
            if (other is not CellPool ocp) 
                return [new Difference("Incompatible classes", $"{ID}({GetType()}", $"{other.ID}{other.GetType()}")]; 
            List<Difference> differences = base.DiffersFrom(other) ?? [];
            List<Difference> diffs = ListDiffersFrom(Cells.Select(c => c as ModelUnitBase).ToList(),
                ocp.Cells.Select(c => c as ModelUnitBase).ToList());
            if (diffs != null)
                differences.AddRange(diffs);
            if (differences.Count != 0)
                return differences;
            return null;
        }
        public override string ToString()
        {
            return ID + (Active ? "" : " (inactive)");
        }
        public override bool CheckValues(ref List<string> errors)
        {
            int preCount = errors?.Count ?? 0;
            base.CheckValues(ref errors);
            foreach (Cell cell in Cells)
                cell.CheckValues(ref errors);
            return errors.Count == preCount;
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
                List<Cell> cells = [.. Cells.OrderBy(c => c.Sequence)];
                midCell = cells[Cells.Count / 2];
            }
            else
            {
                double avgSomite = (minSomite + maxSomite) / 2;
                double minDistance = Cells.Min(c => Math.Abs(c.Somite - avgSomite));
                List<Cell> midSomiteCells = [.. Cells.Where(c => c.Somite - avgSomite == minDistance).OrderBy(c => c.Sequence)];
                if (midSomiteCells.Count == 0)
                    return (0, 0, 0);
                midCell = midSomiteCells[midSomiteCells.Count / 2];
            }
            int mult = 1;
            if (ColumnIndex2D > 0)
                mult = ColumnIndex2D;
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

        #region Cell Functions
        public void AddCell(Cell c)
        {
            Cells.Add(c);
            c.CellPool = this;
            c.CellGroup = CellGroup;//as cell group holds the ID until the link is created
            c.PositionLeftRight = PositionLeftRight;
        }

        public void RemoveCell(Cell c)
        {
            Cells.Remove(c);
        }

        public Cell GetCell(string id)
        {
            return Cells.FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Cell> GetCells()
        {
            return Cells.AsEnumerable();
        }

        public IEnumerable<int> GetSomites(PlotSelectionMultiCells somiteSelection)
        {
            if (somiteSelection.SomiteSelection == PlotSomiteSelection.All)
                return Cells.Select(c => c.Somite).Distinct().AsEnumerable();

            List<int> som = [];
            if (somiteSelection.SomiteSelection == PlotSomiteSelection.Single)
            {
                som.Add(somiteSelection.NSomite);
            }
            else if (somiteSelection.SomiteSelection == PlotSomiteSelection.FirstMiddleLast)
            {
                int minSom = Cells.Select(c => c.Somite).Min();
                int maxSom = Cells.Select(c => c.Somite).Max();
                som.Add(minSom);
                som.Add((minSom + maxSom) / 2);
                som.Add(maxSom);
            }
            else if (somiteSelection.SomiteSelection == PlotSomiteSelection.Random)
            {
                IEnumerable<int> somites = Cells.Select(c => c.Somite).Distinct();
                if (somites.Count() > somiteSelection.NSomite)
                    som.AddRange(somites.OrderBy(s => Model.randomNumGenerator.Next()).Select(s => s).Take(somiteSelection.NSomite));
            }
            else if (somiteSelection.SomiteSelection == PlotSomiteSelection.RostralTo)
            {
                som.AddRange(Enumerable.Range(1, somiteSelection.NSomite));
            }
            else if (somiteSelection.SomiteSelection == PlotSomiteSelection.CaudalTo)
            {
                som.AddRange(Enumerable.Range(somiteSelection.NSomite, Model.ModelDimensions.NumberOfSomites - somiteSelection.NSomite + 1));
            }
            return som;
        }


        public IEnumerable<Cell> GetCells(PlotSelectionMultiCells cellSelection, int iStart, int iEnd)
        {
            if (cellSelection.SomiteSelection == PlotSomiteSelection.All && cellSelection.CellSelection == PlotCellSelection.All)
                return Cells.AsEnumerable();

            if (Cells.Count == 0)
                return Cells.AsEnumerable();

            List<int> som = GetSomites(cellSelection).ToList();
            List<int> seq = [];

            if (cellSelection.CellSelection == PlotCellSelection.Single)
            {
                seq.Add(cellSelection.NCell);
            }
            else if (cellSelection.CellSelection == PlotCellSelection.FirstMiddleLast)
            {
                int minSeq = Cells.Select(c => c.Sequence).Min();
                int maxSeq = Cells.Select(c => c.Sequence).Max();
                seq.Add(minSeq);
                seq.Add((minSeq + maxSeq) / 2);
                seq.Add(maxSeq);
            }
            else if (cellSelection.CellSelection == PlotCellSelection.Random)
            {
                IEnumerable<int> seqs = Cells.Select(c => c.Sequence).Distinct();
                if (seqs.Count() > cellSelection.NCell)
                    seq.AddRange(seqs.OrderBy(s => Model.randomNumGenerator.Next()).Select(s => s).Take(cellSelection.NCell));
            }
            List<Cell> subList = Cells
                .Where(c =>
                (cellSelection.CellSelection != PlotCellSelection.Spiking || c.IsSpiking(iStart, iEnd)) &&
                (cellSelection.CellSelection != PlotCellSelection.NonSpiking || !c.IsSpiking(iStart, iEnd)) &&
                (c.Somite < 0 || som.Count == 0 || som.Contains(c.Somite)) && (seq.Count == 0 || seq.Contains(c.Sequence)))
                .OrderBy(c => c.Somite)
                .ThenBy(c => c.Sequence)
                .Distinct()
                .ToList();
            return subList;
        }

   
        public void GenerateCells()
        {
            int n = NumOfCells;
            bool neuron = CellType == CellType.Neuron;
            
            ModelDimensions MD = Model.ModelDimensions;
            List<int> somites = PerSomiteOrTotal == CountingMode.PerSomite ? 
                Util.ParseRange(SomiteRange, defMin: 1, defMax: MD.NumberOfSomites) :
                [-1];

            double somiteLength = 0;
            if (PerSomiteOrTotal == CountingMode.Total)
            {
                somiteLength = MD.SpinalRostralCaudalDistance / MD.NumberOfSomites;
            }
            foreach (int somite in somites)
            {
                Coordinate[] coordinates = GenerateCoordinates(Model.randomNumGenerator, MD, n, somite);
                Dictionary<string, double[]> paramValues = Parameters.GenerateMultipleInstanceValues(n, ordered: false);

                double defaultCV = Model.Settings.cv;
                double[] cv = ConductionVelocity != null ?
                    ConductionVelocity.GenerateNNumbers(n, 0, ordered: false) :
                    Enumerable.Repeat(defaultCV, n).ToArray();
                double[] ascendingAxonLength = AscendingAxonLength != null ?
                    AscendingAxonLength.GenerateNNumbers(n, 0, ordered: false) :
                    Enumerable.Repeat(0.0, n).ToArray();
                double[] descendingAxonLength = DescendingAxonLength != null ?
                    DescendingAxonLength.GenerateNNumbers(n, 0, ordered: false) :
                    Enumerable.Repeat(0.0, n).ToArray();

                foreach (int i in Enumerable.Range(0, n))
                {
                    int actualSomite = somite;
                    //if the distribution is based on the total length, calculate the somite index
                    if (somite < 0)
                    {
                        double x = coordinates[i].X;
                        actualSomite = 0;
                        while (x > 0)
                        {
                            x -= somiteLength;
                            actualSomite++;
                        }
                    }
                    Dictionary<string, double> cellParams = paramValues.ToDictionary(kvp => kvp.Key, kvp => kvp.Value[i]);
                    Cell cell = neuron ? new Neuron(Model, this, actualSomite, i + 1, cellParams, cv[i], ascendingAxonLength[i], descendingAxonLength[i]) :
                        new MuscleCell(Model, this, actualSomite, i + 1, cellParams, cv[i], ascendingAxonLength[i], descendingAxonLength[i]);
                    cell.PositionLeftRight = PositionLeftRight;
                    cell.Coordinate = coordinates[i];
                    
                    AddCell(cell);
                }
            }
        }

        public void DeleteCells()
        {
            while (Cells.Count != 0)
            {
                Cell c = Cells.First();
                c.ClearLinks();
                Cells.Remove(c);
            }
        }
        public int GetMaxCellSomite()
        {
            if (Cells != null && Cells.Count != 0)
                return Cells.Max(c => c.Somite);
            return 0;
        }

        public int GetMaxCellSequence()
        {
            if (Cells != null && Cells.Count != 0)
                return Cells.Max(c => c.Sequence);
            return 0;
        }

        public int GetMaxCellSequence(int somite)
        {
            if (Cells != null && Cells.Count != 0)
                return Cells.Where(c => c.Somite == somite).Max(c => c.Sequence);
            return 0;
        }

        public bool HasCells()
        {
            return Cells != null && Cells.Count != 0;
        }
        #endregion

        #region Projection Functions
        public void ReachToCellPoolViaGapJunction(CellPool target, 
            CellReach reach, 
            TimeLine timeline,
            Dictionary<string, Distribution> synParams,
            double probability, 
            DistanceMode distanceMode,
            double? delay_ms,
            double? fixedduration_ms)
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
            foreach (Cell pre in this.GetCells())
            {
                int counter = 0;

                //To prevent recursive gap junctions in self projecting pools
                IEnumerable<Cell> targetcells = this == target ? target.GetCells()
                    .Where(c => c.Somite > pre.Somite || (c.Somite == pre.Somite && c.Sequence > pre.Sequence)) : target.GetCells();
                foreach (Cell post in targetcells)
                {
                    if (maxIncoming > 0) //check whether the target cell already has connections from the same pool
                    {
                        int existing = post.GapJunctions.Count(gj => gj.Cell1.CellPool == this);
                        if (existing >= maxIncoming)
                            continue;
                    }
                    double r = Model.randomNumGenerator.NextDouble();
                    if (probability < r)
                        continue;
                    if (reach.WithinReach(pre, post))
                    {
                        Dictionary<string, double> paramValues = synParams.GenerateSingleInstanceValues();

                        GapJunction jnc = pre.CreateGapJunction(post, paramValues, distanceMode);
                        jnc.SetDelay(delay_ms);
                        jnc.SetTimeLine(timeline);
                        if (fixedduration_ms != null)
                            jnc.SetFixedDuration((double)fixedduration_ms);
                        counter++;
                        if (maxOutgoing > 0 && counter >= maxOutgoing)
                            break;
                    }
                }
            }
        }

        public void ReachToCellPoolViaChemSynapse(CellPool target, 
            CellReach reach,
            string coreType,
            Dictionary<string, Distribution> synParams, 
            TimeLine timeline, 
            double probability, 
            DistanceMode distanceMode, 
            double? delay_ms,
            double? fixedduration_ms)
        {
            int maxIncoming = reach.MaxIncoming;
            int maxOutgoing = reach.MaxOutgoing;
            if (maxIncoming > 0 && maxOutgoing == 0)
            {//if target pool has a limit, prevent the scenario where one source cell fulfills that limit
                int numSource = GetCells().Count();
                int numTarget = target.GetCells().Count();
                maxOutgoing = (int)Math.Ceiling((double)maxIncoming * numTarget / numSource);
            }
            else if (maxIncoming == 0 && maxOutgoing > 0)
            {//if the source pool has a limit, prevent the scenario where one target cell fulfills that limit
                int numSource = GetCells().Count();
                int numTarget = target.GetCells().Count();
                maxIncoming = (int)Math.Ceiling((double)maxOutgoing * numSource / numTarget);
            }
            foreach (Neuron pre in GetCells().Cast<Neuron>())
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
                    double r = Model.randomNumGenerator.NextDouble();
                    if (probability < r)
                        continue;
                    if (reach.WithinReach(pre, post))
                    {
                        Dictionary<string, double> paramValues = synParams.GenerateSingleInstanceValues();
                        ChemicalSynapse syn = pre.CreateChemicalSynapse(post, coreType, paramValues, distanceMode);
                        syn.SetDelay(delay_ms);
                        if (fixedduration_ms != null)
                            syn.SetFixedDuration((double)fixedduration_ms);
                        syn.SetTimeLine(timeline);
                        counter++;
                        if (maxOutgoing > 0 && counter >= maxOutgoing)
                            break;
                    }
                }
            }
        }

        public bool HasConnections(bool gap = true, bool chemin = true, bool chemout = true)
        {
            if (!HasCells()) return false;
            return Cells.Any(c=>c.HasConnections(gap, chemin, chemout));
        }

        public void DeleteJunctions(bool gap = true, bool chemin = true, bool chemout = true)
        {
            foreach (Cell c in  Cells)
                c.DeleteJunctions(gap, chemin, chemout);
        }

        public List<JunctionBase> GetJunctionsTo(string targetPool)
        {
            return Cells?.SelectMany(c => c.Projections.Where(gj => gj.SourcePool == targetPool || gj.TargetPool == targetPool)).Cast<JunctionBase>().ToList();
        }

        #endregion

        #region Stimulus functions

        public override bool HasStimulus()
        {
            return Cells.Any(c => c.HasStimulus());
        }
        public void ApplyStimulus(StimulusTemplate stimulus, string TargetSomite, string TargetCell)
        {
            if (stimulus == null)
                return;
            List<int> somites = [];
            List<int> seqs = [];
            if (!TargetSomite.StartsWith("All"))
                somites = Util.ParseRange(TargetSomite, 1, Model.ModelDimensions.NumberOfSomites);
            if (!TargetCell.StartsWith("All"))
                seqs = Util.ParseRange(TargetCell, 1, this.Cells.Max(c=>c.Sequence));

            foreach (Cell cell in GetCells()
                .Where(c => (somites.Count == 0 || somites.Contains(c.Somite)) && (seqs.Count == 0 || seqs.Contains(c.Sequence))))
            {
                TimeLine timeLine = new(stimulus.TimeLine_ms);
                double rc_delay = stimulus.DelayPerSomite * (cell.Somite - 1);
                double sg_delay = cell.PositionLeftRight == SagittalPlane.Right ? stimulus.DelaySagittal : 0;
                timeLine.AddOffset(rc_delay + sg_delay);
                Stimulus stimulus_ms = new(stimulus.Settings, cell, timeLine);
                cell.Stimuli.Add(stimulus_ms);
            }
        }

        public List<StimulusBase> GetStimuli()
        {
            List<StimulusBase> listStimuli = [];
            foreach (Cell cell in Cells)
            {
                foreach (Stimulus stim in cell.Stimuli.ListOfStimulus)
                    listStimuli.Add(stim);
            }
            return listStimuli;
        }

        public void ClearStimuli()
        {
            foreach (Cell cell in Cells)
            {
                cell.ClearStimuli();
            }
        }

        #endregion

        #region Sort Functions
        public virtual void SortJunctions()
        {
            Cells.ForEach(c => c.SortJunctions());
        }
        public virtual void SortJunctionsBySource()
        {
            Cells.ForEach(c => c.SortJunctionsBySource());
        }
        public virtual void SortJunctionsByTarget()
        {
            Cells.ForEach(c => c.SortJunctionsByTarget());
        }

        public virtual void SortStimuli()
        {
            Cells.ForEach(c => c.SortStimuli());
        }

        #endregion
    }

}
