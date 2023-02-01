using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Stim;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Cells
{
    public class CellPool: CellPoolTemplate
    {
        public static Func<double> gapWeightNoiseMultiplier;//TODO incorporate noise
        public static Func<double> synWeightNoiseMultiplier;
        public static Func<double> rangeNoiseMultiplier;

        [JsonIgnore]
        public RunningModel Model { get; set; }

        [JsonPropertyOrder(2)]
        public List<Cell> Cells { get; set; }

        [JsonIgnore]
        public IEnumerable<JunctionBase> Projections
        {
            get
            {
                List<JunctionBase> jncs = new();
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
            Cells = new List<Cell>();
        }

        public CellPool(CellPool cellPool):base(cellPool) 
        {
            Cells = new List<Cell>();
        }

        public CellPool(RunningModel model, CellPoolTemplate template, SagittalPlane leftright)
        {
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
            XDistribution = template.XDistribution.Clone();
            Y_AngleDistribution = template.Y_AngleDistribution?.Clone();
            Y_AngleDistribution = Y_AngleDistribution?.ReviewYDistribution(leftright);
            Z_RadiusDistribution = template.Z_RadiusDistribution?.Clone();
            Parameters = template.Parameters.ToDictionary(entry => entry.Key, entry => entry.Value);
            PerSomiteOrTotal = template.PerSomiteOrTotal;
            SomiteRange = template.SomiteRange;
            ConductionVelocity = template.ConductionVelocity?.Clone();
            TimeLine_ms = new TimeLine(template.TimeLine_ms);
            Active = template.Active;
            Cells = new List<Cell>();
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
            Cells = new List<Cell>();
        }

        public override CellPoolTemplate CreateTemplateCopy()
        {
            return new CellPoolTemplate(this);
        }

        public void LinkObjects(RunningModel model)
        {
            foreach (Cell cell in Cells)
            {
                cell.CellPool = this;
                cell.LinkObjects(model, this);
            }
        }

        public override string ToString()
        {
            return ID + (Active ? "" : " (inactive)");
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
        }

        public Cell GetCell(string id)
        {
            return Cells.FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Cell> GetCells()
        {
            return Cells.AsEnumerable();
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

   
        public void GenerateCells()
        {
            int n = NumOfCells;
            bool neuron = CellType == CellType.Neuron;
            
            ModelDimensions MD = Model.ModelDimensions;
            List<int> somites = PerSomiteOrTotal == CountingMode.PerSomite ? 
                Util.ParseRange(SomiteRange, defMin: 1, defMax: MD.NumberOfSomites) :
                new List<int>() { -1 };

            double somiteLength = 0;
            if (PerSomiteOrTotal == CountingMode.Total && MD.NumberOfSomites > 0)
            {
                somiteLength = MD.SpinalRostralCaudalDistance / MD.NumberOfSomites;
            }
            foreach (int somite in somites)
            {
                Coordinate[] coordinates = GenerateCoordinates(Model.rand, MD, n, somite);
                Dictionary<string, double[]> paramValues = Parameters.GenerateMultipleInstanceValues(n, ordered: false);

                double defaultCV = CurrentSettings.Settings.cv;
                double[] cv = ConductionVelocity != null ?
                    ((Distribution)ConductionVelocity).GenerateNNumbers(n, 0, ordered: false) :
                    Enumerable.Repeat(defaultCV, n).ToArray();

                foreach (int i in Enumerable.Range(0, n))
                {
                    int actualSomite = somite;
                    //if the model is somite based but the distribution is based on the total length, calculate the somite index
                    if (somite < 0 && MD.NumberOfSomites > 0)
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
                    Cell cell = neuron ? new Neuron(Model, this, actualSomite, i + 1, cellParams, cv[i]) :
                        new MuscleCell(Model, this, actualSomite, i + 1, cellParams, cv[i]);
                    cell.PositionLeftRight = PositionLeftRight;
                    cell.Coordinate = coordinates[i];
                    
                    AddCell(cell);
                }
            }
        }

        public void DeleteCells()
        {
            while (Cells.Any())
            {
                Cell c = Cells.First();
                c.ClearLinks();
                Cells.Remove(c);
            }
        }
        public int GetMaxCellSomite()
        {
            if (Cells != null && Cells.Any())
                return Cells.Max(c => c.Somite);
            return 0;
        }

        public int GetMaxCellSequence()
        {
            if (Cells != null && Cells.Any())
                return Cells.Max(c => c.Sequence);
            return 0;
        }

        #endregion

        #region Projection Functions
        public void ReachToCellPoolViaGapJunction(CellPool target, 
            CellReach reach, 
            TimeLine timeline, 
            double weight,
            double probability, 
            DistanceMode distanceMode,
            double delay_ms,
            double? fixedduration_ms)
        {
            foreach (Cell pre in this.GetCells())
            {
                //To prevent recursive gap junctions in self projecting pools
                IEnumerable<Cell> targetcells = this == target ? target.GetCells().Where(c => c.Somite != pre.Somite || c.Sequence > pre.Sequence) : target.GetCells();
                foreach (Cell post in targetcells)
                {
                    if (probability < Model.rand.Next(1))
                        continue;
                    double mult = 1;
                    if (CellPool.rangeNoiseMultiplier != null)
                        mult = rangeNoiseMultiplier();
                    if (reach.WithinReach(pre, post, mult))
                    {
                        mult = 1;
                        if (CellPool.gapWeightNoiseMultiplier != null)
                            mult = gapWeightNoiseMultiplier();
                        GapJunction jnc = pre.CreateGapJunction(post, weight * mult, distanceMode);
                        jnc.SetDelay(delay_ms);
                        jnc.SetTimeSpan(timeline);
                        if (fixedduration_ms != null)
                            jnc.SetFixedDuration((double)fixedduration_ms);
                    }
                }
            }
        }

        public void ReachToCellPoolViaChemSynapse(CellPool target, 
            CellReach reach, 
            SynapseParameters param, 
            TimeLine timeline, 
            double weight,
            double probability, 
            DistanceMode distanceMode, 
            double delay_ms,
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
                    if (probability < Model.rand.Next(1))
                        continue;
                    double mult = 1;
                    if (CellPool.rangeNoiseMultiplier != null)
                        mult = rangeNoiseMultiplier();
                    if (reach.WithinReach(pre, post, mult))
                    {
                        mult = 1;
                        if (CellPool.synWeightNoiseMultiplier != null)
                            mult = synWeightNoiseMultiplier();
                        ChemicalSynapse syn = pre.CreateChemicalSynapse(post, param, weight * mult, distanceMode);
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
        #endregion
        public void ApplyStimulus(StimulusTemplate stimulus, string TargetSomite, string TargetCell)
        {
            if (stimulus == null)
                return;
            List<int> somites = new();
            List<int> seqs = new();
            if (!TargetSomite.StartsWith("All"))
                somites = Util.ParseRange(TargetSomite, 1, Model.ModelDimensions.NumberOfSomites);
            if (!TargetCell.StartsWith("All"))
                seqs = Util.ParseRange(TargetCell, 1, this.Cells.Max(c=>c.Sequence));

            foreach (Cell cell in GetCells()
                .Where(c => (!somites.Any() || somites.Contains(c.Somite)) && (!seqs.Any() || seqs.Contains(c.Sequence))))
            {
                Stimulus stimulus_ms = new(stimulus.Settings, cell, stimulus.TimeLine_ms);
                cell.Stimuli.Add(stimulus_ms);
            }
        }
    }

}
