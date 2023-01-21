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
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Cells
{
    public class CellPool: CellPoolTemplate
    {
        public static Func<double> gapWeightNoiseMultiplier;
        public static Func<double> synWeightNoiseMultiplier;
        public static Func<double> rangeNoiseMultiplier;

        private readonly RunningModel Model;

        [JsonPropertyOrder(2)]
        public List<Cell> Cells { get; set; }
        

        public CellPool()
        {
            Cells = new List<Cell>();
        }

        public CellPool(CellPool cellPool):base(cellPool) 
        {
        }
        public override CellPoolTemplate CreateCopy()
        {
            return new(this);
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
            XDistribution = (Distribution)template.XDistribution;
            Y_AngleDistribution = ((Distribution)template.Y_AngleDistribution)?.CreateCopy();
            Y_AngleDistribution = Y_AngleDistribution?.ReviewYDistribution(leftright);
            Z_RadiusDistribution = (Distribution)template.Z_RadiusDistribution;
            Parameters = template.Parameters;
            PerSomiteOrTotal = template.PerSomiteOrTotal;
            SomiteRange = template.SomiteRange;
            ConductionVelocity = template.ConductionVelocity?.CreateCopy();
            TimeLine_ms = new TimeLine(template.TimeLine_ms);
            Cells = new List<Cell>();
            GenerateCells(template, leftright);
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

        public void GenerateCells(CellPoolTemplate template, SagittalPlane leftright)
        {
            int n = template.NumOfCells;
            bool neuron = template.CellType == CellType.Neuron;
            
            ModelDimensions MD = Model.ModelDimensions;
            List<int> somites = template.PerSomiteOrTotal == CountingMode.PerSomite ? 
                Util.ParseRange(template.SomiteRange, defMin: 1, defMax: MD.NumberOfSomites) :
                new List<int>() { -1 };

            double somiteLength = 0;
            if (template.PerSomiteOrTotal == CountingMode.Total && MD.NumberOfSomites > 0)
            {
                somiteLength = MD.SpinalRostralCaudalDistance / MD.NumberOfSomites;
            }
            foreach (int somite in somites)
            {
                Coordinate[] coordinates = GenerateCoordinates(MD, n, somite);
                Dictionary<string, double[]> paramValues = template.Parameters.GenerateMultipleInstanceValues(n, ordered: false);

                double defaultCV = CurrentSettings.Settings.cv;
                double[] cv = template.ConductionVelocity != null ?
                    ((Distribution)template.ConductionVelocity).GenerateNNumbers(n, 0, ordered: false) :
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
                    Cell cell = neuron ? new Neuron(template, actualSomite, i + 1, cellParams, cv[i]) :
                        new MuscleCell(template, actualSomite, i + 1, cellParams);
                    cell.PositionLeftRight = leftright;
                    cell.coordinate = coordinates[i];
                    
                    AddCell(cell);
                }
            }
        }

        public void FormKernel(double weight)
        {
            foreach (Neuron ic in GetCells().Cast<Neuron>())
            {
                //IC to IC kernel junctions
                foreach (Neuron ic2 in GetCells().Where(c => c.Sequence > ic.Sequence).Cast<Neuron>()) //sequence check is added to prevent recursive or self junctions
                {
                    //no distance check as ICs form a kernel
                    double mult = 1;
                    if (CellPool.gapWeightNoiseMultiplier != null)
                        mult = gapWeightNoiseMultiplier();
                    ic.CreateGapJunction(ic2, weight * mult, DistanceMode.Euclidean);
                }
            }
        }
        public void ReachToCellPoolViaGapJunction(CellPool target, CellReach reach, TimeLine timeline, double probability, DistanceMode distanceMode)
        {
            foreach (Cell pre in this.GetCells())
            {
                //To prevent recursive gap junctions in self projecting pools
                IEnumerable<Cell> targetcells = this == target ? target.GetCells().Where(c => c.Somite != pre.Somite || c.Sequence > pre.Sequence) : target.GetCells();
                foreach (Cell post in targetcells)
                {
                    if (probability < RunningModel.rand.Next(1))
                        continue;
                    double mult = 1;
                    if (CellPool.rangeNoiseMultiplier != null)
                        mult = rangeNoiseMultiplier();
                    if (reach.WithinReach(pre, post, mult))
                    {
                        mult = 1;
                        if (CellPool.gapWeightNoiseMultiplier != null)
                            mult = gapWeightNoiseMultiplier();
                        GapJunction jnc = pre.CreateGapJunction(post, reach.Weight * mult, distanceMode);
                        jnc.SetDelay(reach.Delay_ms);
                        jnc.SetTimeSpan(timeline);
                        if (reach.FixedDuration_ms != null)
                            jnc.SetFixedDuration((int)reach.FixedDuration_ms);
                    }
                }
            }
        }

        public void ReachToCellPoolViaChemSynapse(CellPool target, CellReach reach, SynapseParameters param, TimeLine timeline, double probability, DistanceMode distanceMode)
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
                    if (probability < RunningModel.rand.Next(1))
                        continue;
                    double mult = 1;
                    if (CellPool.rangeNoiseMultiplier != null)
                        mult = rangeNoiseMultiplier();
                    if (reach.WithinReach(pre, post, mult))
                    {
                        mult = 1;
                        if (CellPool.synWeightNoiseMultiplier != null)
                            mult = synWeightNoiseMultiplier();
                        ChemicalSynapse syn = pre.CreateChemicalSynapse(post, param, reach.Weight * mult, distanceMode);
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
