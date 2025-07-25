﻿using SiliFish.Database;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using SiliFish.Repositories;
using SiliFish.Services;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace SiliFish.ModelUnits.Architecture
{
    public class RunningModel : ModelBase
    {
        public double DeltaT { get; private set; }
        public int SkipDuration { get; private set; }
        public int MaxTime { get; private set; }
        public Random randomNumGenerator { get; private set; }

        protected bool db_mode = false;
        protected double[] Time;
        protected List<CellPool> neuronPools = [];
        protected List<CellPool> musclePools = [];

        [JsonIgnore]
        [Browsable(false)]
        public List<StimulusTemplate> StimulusTemplates { get; set; }//used in creating stats data

        [JsonIgnore]
        [Browsable(false)]
        public List<CellPool> CellPools
        {
            get
            {
                return neuronPools.Union(musclePools).ToList();
            }
        }
        [Browsable(false)]
        [JsonPropertyOrder(2)]
        public List<CellPool> NeuronPools
        {
            get
            {
                return neuronPools;
            }
            set
            {
                //called by JSON
                neuronPools = value;
            }
        }
        [Browsable(false)]
        [JsonPropertyOrder(2)]
        public List<CellPool> MusclePools
        {
            get
            {
                return musclePools;
            }
            set
            {
                //called by JSON
                musclePools = value;
            }
        }
        [Browsable(false)]
        [JsonIgnore]
        public List<CellPool> MotoNeuronPools
        {
            get
            {
                return MotoNeurons.Select(c => c.CellPool).Distinct().ToList();
            }
        }
        [Browsable(false)]
        [JsonIgnore]
        public List<CellPool> InterNeuronPools
        {
            get
            {
                return neuronPools.Except(MotoNeuronPools).ToList();
            }
        }
        [JsonIgnore]
        [Browsable(false)]
        public List<Cell> MotoNeurons //MotoNeurons are the neurons projecting to the muscle cells
        {
            get
            {
                List<Cell> MuscleCells = MusclePools.SelectMany(mp => mp.GetCells()).ToList();
                List<Cell> MNs = MuscleCells.SelectMany(c => ((MuscleCell)c).EndPlates.Select(ep => ep.PreNeuron as Cell)).ToList();
                return MNs.Distinct().ToList();
            }
        }

        /// <summary>
        /// Brings a summary of gap junctions between cell pools
        /// </summary>
        [JsonIgnore]
        public List<InterPool> GapPoolJunctions
        {
            get
            {
                List<InterPool> interPools = [];
                foreach (CellPool sourcePool in CellPools)
                {
                    string source = sourcePool.ID;
                    foreach (CellPool targetPool in CellPools)
                    {
                        string target = targetPool.ID;
                        IEnumerable<GapJunction> list = sourcePool.GetCells().SelectMany(c => c.GapJunctions.Where(jnc => jnc.Cell1.CellPool == sourcePool && jnc.Cell2.CellPool == targetPool));
                        int count = list.Count();
                        if (count > 0)
                        {
                            interPools.Add(new InterPool(list)
                            {
                                SourcePool = source,
                                TargetPool = target,
                                Mode = CellOutputMode.Electrical
                            });
                        }
                    }
                }
                return interPools;
            }
        }

        /// <summary>
        /// Brings a summary of chem junctions between cell pools
        /// </summary>
        [JsonIgnore]
        public List<InterPool> ChemPoolJunctions
        {
            get
            {
                List<InterPool> interPools = [];
                foreach (CellPool sourcePool in NeuronPools)
                {
                    string source = sourcePool.ID;
                    foreach (CellPool targetPool in CellPools)
                    {
                        string target = targetPool.ID;
                        IEnumerable<ChemicalSynapse> list = sourcePool.GetCells().SelectMany(c => (c as Neuron).Terminals.Where(jnc => jnc.PreNeuron.CellPool == sourcePool && jnc.PostCell.CellPool == targetPool));
                        int count = list.Count();
                        if (count > 0)
                        {
                            interPools.Add(new InterPool(list)
                            {
                                SourcePool = source,
                                TargetPool = target,
                                Mode = sourcePool.CellOutputMode
                            });
                        }
                    }
                }
                return interPools;
            }
        }

        [JsonIgnore]
        [Browsable(false)]
        public double[] TimeArray { get { return Time; } }

        [JsonIgnore]
        [Browsable(false)]
        public SwimmingEpisodes SwimmingEpisodes { get; internal set; }

        public RunningModel()
        {
        }

        public RunningModel(ModelTemplate swimmingModelTemplate)
        {
            if (swimmingModelTemplate == null) return;

            ModelName = swimmingModelTemplate.ModelName;
            ModelDescription = $"Generated on {DateTime.Now:g}\r\n{swimmingModelTemplate.ModelDescription}";
            ModelDimensions = swimmingModelTemplate.ModelDimensions.Clone();
            Settings = swimmingModelTemplate.Settings.Clone();
            KinemParam = swimmingModelTemplate.KinemParam.Clone();
            DynamicsParam = swimmingModelTemplate.DynamicsParam.Clone();
            SetParameters(swimmingModelTemplate.Parameters);
            randomNumGenerator = new Random(Settings.Seed);

            #region Generate pools and cells
            foreach (CellPoolTemplate cellPoolTemplate in swimmingModelTemplate.CellPoolTemplates.Where(cp => cp.Active))
            {
                GenerateCellPoolsFrom(cellPoolTemplate);
            }
            #endregion

            #region Generate Gap Junctions and Chemical Synapses
            foreach (InterPoolTemplate jncTemp in swimmingModelTemplate.InterPoolTemplates.Where(ip => ip.JncActive))
            {
                CellPool leftSource = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == jncTemp.SourcePool && np.PositionLeftRight == SagittalPlane.Left);
                CellPool rightSource = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == jncTemp.SourcePool && np.PositionLeftRight == SagittalPlane.Right);

                CellPool leftTarget = neuronPools.Union(musclePools).FirstOrDefault(mp => mp.CellGroup == jncTemp.TargetPool && mp.PositionLeftRight == SagittalPlane.Left);
                CellPool rightTarget = neuronPools.Union(musclePools).FirstOrDefault(mp => mp.CellGroup == jncTemp.TargetPool && mp.PositionLeftRight == SagittalPlane.Right);

                if (jncTemp.JunctionType == JunctionType.Synapse || jncTemp.JunctionType == JunctionType.NMJ)
                {
                    if (jncTemp.AxonReachMode == AxonReachMode.Ipsilateral || jncTemp.AxonReachMode == AxonReachMode.Bilateral)
                    {
                        PoolToPoolChemSynapse(leftSource, leftTarget, jncTemp);
                        PoolToPoolChemSynapse(rightSource, rightTarget, jncTemp);
                    }
                    if (jncTemp.AxonReachMode == AxonReachMode.Contralateral || jncTemp.AxonReachMode == AxonReachMode.Bilateral)
                    {
                        PoolToPoolChemSynapse(leftSource, rightTarget, jncTemp);
                        PoolToPoolChemSynapse(rightSource, leftTarget, jncTemp);
                    }
                }
                else if (jncTemp.JunctionType == JunctionType.Gap)
                {
                    if (jncTemp.AxonReachMode == AxonReachMode.Ipsilateral || jncTemp.AxonReachMode == AxonReachMode.Bilateral)
                    {
                        PoolToPoolGapJunction(leftSource, leftTarget, jncTemp);
                        PoolToPoolGapJunction(rightSource, rightTarget, jncTemp);
                    }
                    if (jncTemp.AxonReachMode == AxonReachMode.Contralateral || jncTemp.AxonReachMode == AxonReachMode.Bilateral)
                    {
                        PoolToPoolGapJunction(leftSource, rightTarget, jncTemp);
                        PoolToPoolGapJunction(rightSource, leftTarget, jncTemp);
                    }
                }
            }
            #endregion

            #region Generate Stimuli
            StimulusTemplates = [];
            if (swimmingModelTemplate.StimulusTemplates?.Count > 0)
            {
                foreach (StimulusTemplate stimulus in swimmingModelTemplate.StimulusTemplates.Where(stim => stim.Active))
                {
                    StimulusTemplates.AddRange(stimulus.CreateSpreadedCopy());

                    if (stimulus.LeftRight.Contains("Left") || stimulus.LeftRight == "Both")
                    {
                        CellPool target = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == stimulus.TargetPool && np.PositionLeftRight == SagittalPlane.Left);
                        target?.ApplyStimulus(stimulus, stimulus.TargetSomite, stimulus.TargetCell);
                    }
                    if (stimulus.LeftRight.Contains("Right") || stimulus.LeftRight == "Both")
                    {
                        CellPool target = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == stimulus.TargetPool && np.PositionLeftRight == SagittalPlane.Right);
                        target?.ApplyStimulus(stimulus, stimulus.TargetSomite, stimulus.TargetCell);
                    }
                }
            }
            #endregion
        }

        public RunningModel CreateCopy()
        {
            string jsonstring = JsonSerializer.Serialize(this, JsonUtil.SerializerOptions);
            RunningModel newModel = (RunningModel)ModelFile.ReadFromJson(jsonstring, out List<string> issues);
            if (issues?.Count > 0)
                ExceptionHandler.ExceptionHandling("Running Model copy creation unsuccessful", new Exception(string.Join(";", issues)));
            newModel.StimulusTemplates = StimulusTemplates;
            return newModel;
        }
        /// <summary>
        ///Needs to be run after created from JSON 
        /// </summary>
        public override void LinkObjects()
        {
            foreach (CellPool pool in CellPools)
            {
                pool.LinkObjects(this);
            }
        }

        public void LinkPlotObjects(PlotDefinition plot)
        {
            if (plot == null) return;
            if (plot.Selection is PlotSelectionUnits plotSelection)
            {
                PlotSelectionUnits linkedSelection = new(plotSelection);
                foreach (Tuple<string, string> unitTag in plotSelection.UnitTags)
                {
                    ModelUnitBase unit = null;
                    if (unitTag.Item1 == "CellPool")
                        unit = GetCellPools().FirstOrDefault(cp => cp.ID == unitTag.Item2);
                    else if (unitTag.Item1 == "Neuron" || unitTag.Item1 == "MuscleCell")
                        unit = GetCells().FirstOrDefault(cp => cp.ID == unitTag.Item2);
                    else if (unitTag.Item1 == "ChemicalSynapse")
                        unit = GetChemicalJunctions().FirstOrDefault(cp => cp.ID == unitTag.Item2);
                    else if (unitTag.Item1 == "GapJunction")
                        unit = GetGapJunctions().FirstOrDefault(cp => cp.ID == unitTag.Item2);
                    else
                    { }
                    if (unit != null)
                        linkedSelection.AddUnit(unit);
                }
                if (linkedSelection.Units?.Count > 0)
                    plot.Selection = linkedSelection;
                else plot.Selection = null;
            }

        }

        public override List<Difference> DiffersFrom(ModelBase other)
        {
            List<Difference> differences = [];
            if (other is RunningModel om)
            {
                differences = base.DiffersFrom(other) ?? [];
                List<Difference> diffs = ModelUnitBase.ListDiffersFrom(CellPools.Select(c => c as ModelUnitBase).ToList(),
                    om.CellPools.Select(c => c as ModelUnitBase).ToList());
                if (diffs != null)
                    differences.AddRange(diffs);
            }
            else
            {
                differences.Add(new Difference("Models not comparable."));
            }
            if (differences.Count != 0)
                return differences;
            return null;
        }
        public void BackwardCompatibility_Stimulus()
        {
            try
            {
                foreach (Cell cell in GetCells().Where(c => c.Stimuli != null && c.Stimuli.HasStimulus))
                {
                    List<Stimulus> stimList = cell.Stimuli.ListOfStimulus.Where(s => s.Settings.Mode is StimulusMode.Sinusoidal or StimulusMode.Pulse).ToList();
                    foreach (Stimulus stim in stimList)
                    {
                        if (stim.Settings.Frequency == null || stim.Settings.Frequency == 0)
                        {
                            stim.Settings.Frequency = stim.Settings.Value2;
                            if (stim.Settings.Mode == StimulusMode.Pulse)
                                stim.Settings.Value2 = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public void BackwardCompatibility_AxonLength()//has to be called after linkObjects
        {
            try
            {
                if (GetCells().Any(c => c.AscendingAxonLength > 0 || c.DescendingAxonLength > 0))
                    return;

                foreach (Cell cell in GetCells())
                {
                    List<GapJunction> descendingGapList = cell.GapJunctions.
                        Where(jnc => jnc.Cell1.ID == cell.ID && cell.Coordinate.X < jnc.Cell2.Coordinate.X ||
                            jnc.Cell2.ID == cell.ID && cell.Coordinate.X < jnc.Cell1.Coordinate.X)?.ToList();
                    double descGap = descendingGapList.Count != 0 ?
                        descendingGapList.Max(jnc => Util.Distance(jnc.Cell1.Coordinate, jnc.Cell2.Coordinate, jnc.DistanceMode)) : 0;
                    double descChem = 0;
                    if (cell is Neuron neuron)
                    {
                        List<ChemicalSynapse> descendingChemList = neuron.Terminals.Where(jnc => cell.Coordinate.X < jnc.PostCell.Coordinate.X).ToList();
                        if (descendingChemList.Count != 0)
                            descChem = descendingChemList.Max(jnc => Util.Distance(cell.Coordinate, jnc.PostCell.Coordinate, jnc.DistanceMode));
                    }
                    cell.DescendingAxonLength = Math.Max(descChem, descGap);

                    List<GapJunction> ascendingGapList = cell.GapJunctions.
                        Where(jnc => jnc.Cell1 == cell && cell.Coordinate.X > jnc.Cell2.Coordinate.X ||
                            jnc.Cell2 == cell && cell.Coordinate.X > jnc.Cell1.Coordinate.X).ToList();
                    double ascGap = ascendingGapList.Count != 0 ?
                        ascendingGapList.Max(jnc => Util.Distance(jnc.Cell1.Coordinate, jnc.Cell2.Coordinate, jnc.DistanceMode)) : 0;
                    double ascChem = 0;
                    if (cell is Neuron neuron2)
                    {
                        List<ChemicalSynapse> ascendingChemList = neuron2.Terminals.Where(jnc => cell.Coordinate.X < jnc.PostCell.Coordinate.X).ToList();
                        if (ascendingChemList.Count != 0)
                            ascChem = ascendingChemList.Max(jnc => Util.Distance(cell.Coordinate, jnc.PostCell.Coordinate, jnc.DistanceMode));
                    }
                    cell.AscendingAxonLength = Math.Max(ascChem, ascGap);

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public override void BackwardCompatibility()
        {
            base.BackwardCompatibility();

            if (string.Compare(Version, "2.2.4") < 0)//frequency to stimulus is added on 2.2.4
            {
                BackwardCompatibility_Stimulus();
            }
        }
        public override void BackwardCompatibilityAfterLinkObjects()
        {
            base.BackwardCompatibilityAfterLinkObjects();
            if (string.Compare(Version, "2.4.0") < 0)//axon legths are added on 2.4.0
            {
                BackwardCompatibility_AxonLength();
            }
        }

        public override bool CheckValues(ref List<string> errors, ref List<string> warnings)
        {
            errors ??= [];
            warnings ??= [];
            int preCount = errors.Count + warnings.Count;
            base.CheckValues(ref errors, ref warnings);
            foreach (CellPool cp in CellPools)
                cp.CheckValues(ref errors, ref warnings);
            return errors.Count + warnings.Count == preCount;
        }

        #region Cells
        public List<Cell> GetCells()
        {
            List<Cell> MuscleCells = MusclePools.SelectMany(mp => mp.GetCells()).ToList();
            List<Cell> Neurons = NeuronPools.SelectMany(np => np.GetCells()).ToList();
            return MuscleCells.Union(Neurons).ToList();

        }

        public Cell GetCell(string id)
        {
            Cell cell = MusclePools.FirstOrDefault(mp => mp.GetCell(id) != null)?.GetCell(id);
            cell ??= NeuronPools.FirstOrDefault(np => np.GetCell(id) != null)?.GetCell(id);
            return cell;
        }
        public (List<Cell> LeftMNs, List<Cell> RightMNs) GetMotoNeurons(int SomiteSeq)
        {
            List<Cell> motoNeurons = MotoNeurons;
            List<Cell> leftMNs = motoNeurons
                                .Where(c => c.PositionLeftRight == SagittalPlane.Left &&
                                        c.Somite == SomiteSeq)
                                .ToList();
            List<Cell> rightMNs = motoNeurons
                                .Where(c => c.PositionLeftRight == SagittalPlane.Right &&
                                        c.Somite == SomiteSeq)
                                .ToList();
            return (leftMNs, rightMNs);
        }
        public List<Cell> GetNeurons()
        {
            return NeuronPools.SelectMany(np => np.GetCells()).ToList();
        }

        public void ClearCells()
        {
            foreach (CellPool cellPool in CellPools)
            {
                cellPool.Cells.Clear();
            }
        }

        public bool AddCell(Cell cell)
        {
            CellPool cellPool = CellPools.FirstOrDefault(cp => cp.CellGroup == cell.CellGroup && cp.PositionLeftRight == cell.PositionLeftRight);
            if (cellPool == null) return false;
            cellPool.AddCell(cell);
            return true;
        }

        public bool HasCells()
        {
            return GetCells().Count != 0;
        }
        #endregion
        #region Cell Pool

        public List<CellPool> GenerateCellPoolsFrom(CellPoolTemplate cellPoolTemplate)
        {
            List<CellPool> cellPoolList = [];
            if (cellPoolTemplate.PositionLeftRight == SagittalPlane.Both || cellPoolTemplate.PositionLeftRight == SagittalPlane.Left)
            {
                CellPool pool = new(this, cellPoolTemplate, SagittalPlane.Left);
                if (cellPoolTemplate.CellType == CellType.Neuron)
                    neuronPools.Add(pool);
                else if (cellPoolTemplate.CellType == CellType.MuscleCell)
                    musclePools.Add(pool);
                cellPoolList.Add(pool);
            }
            if (cellPoolTemplate.PositionLeftRight == SagittalPlane.Both || cellPoolTemplate.PositionLeftRight == SagittalPlane.Right)
            {
                CellPool pool = new(this, cellPoolTemplate, SagittalPlane.Right);
                if (cellPoolTemplate.CellType == CellType.Neuron)
                    neuronPools.Add(pool);
                else if (cellPoolTemplate.CellType == CellType.MuscleCell)
                    musclePools.Add(pool);
                cellPoolList.Add(pool);
            }
            return cellPoolList;
        }
        public override List<CellPoolTemplate> GetCellPools()
        {
            return CellPools.Select(cp => (CellPoolTemplate)cp).ToList();
        }
        public override bool AddCellPool(CellPoolTemplate cellPool)
        {
            if (cellPool is CellPool cp)
            {
                cp.Model = this;
                if (cellPool.CellType == CellType.Neuron)
                    neuronPools.Add(cp);
                else if (cellPool.CellType == CellType.MuscleCell)
                    musclePools.Add(cp);
                else return false;
                return true;
            }
            return false;
        }

        public override bool RemoveCellPool(CellPoolTemplate cellPool)
        {
            if (cellPool is CellPool cp)
                cp.DeleteCells();
            if (cellPool.CellType == CellType.Neuron)
                return neuronPools.RemoveAll(cp => cp.ID == cellPool.ID) > 0;
            if (cellPool.CellType == CellType.MuscleCell)
                return musclePools.RemoveAll(cp => cp.ID == cellPool.ID) > 0;
            return false;
        }

        public override bool UpdateCellPool(CellPoolTemplate cellPoolNew)
        {
            if (cellPoolNew is not CellPool cp)
                return false;
            CellPool cellPoolOld = CellPools.FirstOrDefault(cp => cp.ID == cellPoolNew.ID);
            if (cellPoolOld.CellType == CellType.Neuron)
                neuronPools.RemoveAll(cp => cp.ID == cellPoolNew.ID);
            else if (cellPoolOld.CellType == CellType.MuscleCell)
                musclePools.RemoveAll(cp => cp.ID == cellPoolNew.ID);
            foreach (Cell c in cellPoolOld.Cells)
                cp.AddCell(c);
            CellPools.Add(cp);
            return true;
        }


        #endregion

        #region Junctions
        public bool HasJunctions()
        {
            return CellPools.Any(cp => cp.HasJunctions());
        }
        public override List<InterPoolBase> GetGapJunctions()
        {
            List<InterPoolBase> listJunctions = [];
            foreach (Cell cell in CellPools.SelectMany(cp => cp.Cells))
            {
                foreach (GapJunction jnc in cell.GapJunctions.Where(j => j.Cell1 == cell))
                    listJunctions.Add(jnc);
            }
            return listJunctions;
        }
        public override List<InterPoolBase> GetChemicalJunctions()
        {
            List<InterPoolBase> listJunctions = [];
            foreach (Cell cell in CellPools.SelectMany(cp => cp.Cells))
            {
                if (cell is Neuron neuron)
                    foreach (ChemicalSynapse syn in neuron.Terminals)
                        listJunctions.Add(syn);
            }
            return listJunctions;
        }

        public void RemoveJunctionsOf(CellPool cp, Cell cell, bool gap, bool chemin, bool chemout)
        {
            cp?.DeleteJunctions(gap, chemin, chemout);
            cell?.DeleteJunctions(gap, chemin, chemout);
            if (cp == null && cell == null)
            {
                foreach (CellPool cellPool in CellPools)
                {
                    cellPool.DeleteJunctions(gap, chemin, chemout);
                }
            }
        }

        #endregion

        #region Stimuli

        public bool HasStimulus()
        {
            return GetCells().Any(c => c.HasStimulus());
        }
        public override List<StimulusBase> GetStimuli()
        {
            List<StimulusBase> listStimuli = [];
            foreach (CellPool cellPool in CellPools)
            {
                listStimuli.AddRange(cellPool.GetStimuli());
            }
            return listStimuli;
        }

        public void ClearStimuli()
        {
            foreach (CellPool cellPool in CellPools)
            {
                cellPool.ClearStimuli();
            }
        }

        public override void AddStimulus(StimulusBase stim)
        {
            Stimulus stimulus = stim as Stimulus;
            stimulus.TargetCell.Stimuli.Add(stimulus);
        }

        public override void RemoveStimulus(StimulusBase stim)
        {
            Stimulus stimulus = stim as Stimulus;
            stimulus.TargetCell.Stimuli.Remove(stimulus);
        }

        public override void UpdateStimulus(StimulusBase stim, StimulusBase stim2)
        {
            RemoveStimulus(stim);
            AddStimulus(stim2);
        }
        #endregion
        public virtual ((double, double), (double, double), (double, double), int) GetSpatialRange()
        {
            double minX = 999;
            double maxX = -999;
            double minY = 999;
            double maxY = -999;
            double minZ = 999;
            double maxZ = -999;
            int rangeY1D = 0;

            foreach (CellPool pool in neuronPools.Union(musclePools))
            {
                if (pool.ColumnIndex2D > rangeY1D)
                    rangeY1D = pool.ColumnIndex2D;
                (double localMin, double localMax) = pool.XRange();
                if (localMin < minX) minX = localMin;
                if (localMax > maxX) maxX = localMax;
                (localMin, localMax) = pool.YRange();
                if (localMin < minY) minY = localMin;
                if (localMax > maxY) maxY = localMax;
                (localMin, localMax) = pool.ZRange();
                if (localMin < minZ) minZ = localMin;
                if (localMax > maxZ) maxZ = localMax;
            }
            return ((minX, maxX), (minY, maxY), (minZ, maxZ), rangeY1D);
        }

        public virtual (double, double) GetJunctionRange()
        {
            double maxWeight = 0;
            double minWeight = 999;
            foreach (CellPool pool in neuronPools.Union(musclePools))
            {
                (double localMin, double localMax) = pool.GetJunctionRange();
                if (localMin < minWeight)
                    minWeight = localMin;
                if (localMax > maxWeight)
                    maxWeight = localMax;
            }
            return (minWeight, maxWeight);
        }
        public virtual int GetNumberOfCells()
        {
            return CellPools.Sum(p => p.Cells.Count);
        }
        public virtual int GetNumberOfCells(int startSomite, int endSomite)
        {
            return CellPools.Sum(p => p.Cells.Count(c => c.Somite >= startSomite && c.Somite <= endSomite));
        }
        public virtual int GetNumberOfJunctions()
        {
            int gapJunctions = CellPools.SelectMany(p => p.Cells).Sum((Cell c) => c.GapJunctions.Count);
            int chemJunctions = CellPools.SelectMany(p => p.Cells).Sum(c => ((c as MuscleCell)?.EndPlates.Count ?? 0) + ((c as Neuron)?.Synapses.Count ?? 0));
            return gapJunctions / 2 + chemJunctions; //gap junctions are counted twice
        }

        public virtual int GetMemoryRequirementPerDeltaT()
        {
            return GetNumberOfCells() * 8; //8 bytes for double 
        }
        public virtual (int RollingWindow, int Capacity) CheckMemory()
        {
            double maxDuration = CellPools.SelectMany(p => p.Cells).Max(c => c.TimeDistance());
            int rollingWindow = (int)Math.Ceiling(maxDuration / (100 * DeltaT)) * 100;
            GCMemoryInfo memoryInfo = GC.GetGCMemoryInfo();
            long availableMemory = memoryInfo.TotalAvailableMemoryBytes - memoryInfo.MemoryLoadBytes;
            double rollingWindowMemory = rollingWindow * GetMemoryRequirementPerDeltaT();
            int capacity = (int)Math.Floor(availableMemory / (rollingWindowMemory * 2));
            return (rollingWindow, capacity);
        }

        /// <summary>
        /// Parameter Desc keeps some descriptive text for the relevant parameters
        /// for easy user entry
        /// </summary>
        /// <returns>The tooltip values for the model parameters</returns>
        public virtual Dictionary<string, object> GetParameterDesc()
        {
            return null;
        }

        /// <summary>
        /// Ensures all JSON files with missing parameters are brought up to date 
        /// if there are new parameters added to the code
        /// </summary>
        /// <param name="paramDict"></param>
        protected virtual void FillMissingParameters(Dictionary<string, object> paramDict)
        {
        }

        public virtual void SetAnimationParameters(KinemParam kinemParam)
        {
            this.KinemParam = kinemParam;
        }
        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            FillMissingParameters(paramExternal);
        }


        public (List<Cell> Cells, List<CellPool> Pools) GetSubsetCellsAndPools(string poolIdentifier, PlotSelectionInterface cellSelection, int iStart = 0, int iEnd = -1)
        {
            List<CellPool> pools = null;

            if (cellSelection is PlotSelectionUnits unitsSelection)
            {
                if (unitsSelection.Units?.FirstOrDefault() is Cell)
                {
                    List<Cell> cells = unitsSelection.Units.Where(c => c is Cell).Cast<Cell>().ToList();
                    return (cells, null);
                }
                if (unitsSelection.Units?.FirstOrDefault() is CellPool)
                {
                    pools = unitsSelection.Units.Where(c => c is CellPool).Cast<CellPool>().ToList();
                }
                if (unitsSelection.Units?.FirstOrDefault() is InterPool)
                {
                    List<InterPool> interpools = unitsSelection.Units.Where(x => x is InterPool).Cast<InterPool>().ToList();
                    if (interpools.Count != 0)
                    {
                        pools = [];
                        foreach (InterPool ip in interpools)
                            pools.AddRange(CellPools.Where(c => c.ID == ip.SourcePool || c.ID == ip.TargetPool).ToList());
                        pools = pools.Distinct().ToList();
                    }
                }
            }
            if (cellSelection is PlotSelectionMultiCells mcs)
            {
                if (pools == null) //not loaded from the units above
                {
                    if (poolIdentifier == Const.AllPools)
                        pools = neuronPools.Union(musclePools).Where(p => p.OnSide(mcs.SagittalPlane)).ToList();
                    else if (poolIdentifier == Const.Neurons)
                        pools = neuronPools.Where(p => p.OnSide(mcs.SagittalPlane)).ToList();
                    else if (poolIdentifier == Const.MuscleCells)
                        pools = musclePools.Where(p => p.OnSide(mcs.SagittalPlane)).ToList();
                    else if (poolIdentifier == Const.Motoneurons)
                        pools = MotoNeuronPools.Where(p => p.OnSide(mcs.SagittalPlane)).ToList();
                    else if (poolIdentifier == Const.Interneurons)
                        pools = InterNeuronPools.Where(p => p.OnSide(mcs.SagittalPlane)).ToList();
                    else if (poolIdentifier == Const.ExcitatoryNeurons)
                        pools = neuronPools.Where(p => p.NTMode == NeuronClass.Glutamatergic &&
                            p.OnSide(mcs.SagittalPlane)).ToList();
                    else if (poolIdentifier == Const.InhibitoryNeurons)
                        pools = neuronPools.Where(p => (p.NTMode == NeuronClass.GABAergic || p.NTMode == NeuronClass.Glycinergic) &&
                            p.OnSide(mcs.SagittalPlane)).ToList();
                    else if (poolIdentifier == Const.SupraSpinal)
                        pools = neuronPools.Union(musclePools).Where(p => p.BodyLocation == BodyLocation.SupraSpinal &&
                            p.OnSide(mcs.SagittalPlane)).ToList();
                    else if (poolIdentifier == Const.Spinal)
                        pools = neuronPools.Union(musclePools).Where(p => p.BodyLocation == BodyLocation.SpinalCord &&
                            p.OnSide(mcs.SagittalPlane)).ToList();
                    else if (poolIdentifier == Const.MusculoSkeletal)
                        pools = neuronPools.Union(musclePools).Where(p => p.BodyLocation == BodyLocation.MusculoSkeletal &&
                            p.OnSide(mcs.SagittalPlane)).ToList();
                    else
                        pools = neuronPools.Union(musclePools).Where(p => p.CellGroup == poolIdentifier
                                        && p.OnSide(mcs.SagittalPlane)).ToList();
                }
                List<Cell> cells = pools.SelectMany(p => p.GetCells(mcs, iStart, iEnd)).Where(c => c.Active).ToList();
                if (cells.Count != 0)
                    return (cells, pools);
                return (null, pools);
            }
            return (null, pools);
        }
        public List<Cell> GetSubsetCells(string poolIdentifier, PlotSelectionInterface cellSelection, int iStart = 0, int iEnd = -1)
        {
            (List<Cell> Cells, List<CellPool> Pools) = GetSubsetCellsAndPools(poolIdentifier, cellSelection, iStart, iEnd);
            Cells ??= [];
            if (Cells.Count == 0 && Pools != null)
                foreach (CellPool pool in Pools)
                    Cells.AddRange(pool.Cells);
            return Cells;
        }

        private bool MemoryAllocation(RunParam runParam, SimulationDBLink dBLink)
        {
            foreach (Cell cell in GetCells())
                cell.MemoryAllocation(runParam, dBLink);
            return true;
        }

        /// <summary>
        /// Clears the allocated memory used for plotting purposes
        /// </summary>
        /// <returns></returns>
        public double MemoryFlush()//Currently only done by the user
        {
            double cleared = 0;
            foreach (Cell cell in GetCells())
                cleared += cell.MemoryFlush();
            return cleared;
        }

        public virtual bool InitForSimulation(RunParam runParam, ref SimulationDBLink dbLink, Random random)
        {
            try
            {
                if (runParam.iMax <= 0) return false;
                DeltaT = runParam.DeltaT;
                SkipDuration = runParam.SkipDuration;
                MaxTime = runParam.MaxTime;
                randomNumGenerator = random;
                if (!neuronPools.Any(p => p.GetCells().Any()) &&
                    !musclePools.Any(p => p.GetCells().Any())) //No cells
                    return false;
                Time = new double[runParam.iMax];
                for (int i = 0; i < runParam.iMax; i++)
                    Time[i] = runParam.GetTimeOfIndex(i);

                int uniqueID = 1;

                foreach (CellPool neurons in neuronPools)
                    foreach (Neuron neuron in neurons.GetCells().Cast<Neuron>())
                        neuron.InitForSimulation(runParam, ref uniqueID);

                foreach (CellPool muscleCells in musclePools)
                    foreach (MuscleCell mc in muscleCells.GetCells().Cast<MuscleCell>())
                        mc.InitForSimulation(runParam, ref uniqueID);

                if (dbLink != null)
                {
                    (dbLink.RollingWindow, dbLink.WindowMultiplier) = CheckMemory();
                    if (dbLink.RollingWindow * dbLink.WindowMultiplier > runParam.iMax)
                        dbLink = null;
                }
                SwimmingEpisodes = null;
                return MemoryAllocation(runParam, dbLink);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public virtual bool ResumeSimulation(RunParam runParam)
        {
            SwimmingEpisodes = null;

            foreach (CellPool neurons in neuronPools)
                foreach (Neuron neuron in neurons.GetCells().Cast<Neuron>())
                    neuron.ResumeSimulation(runParam);

            foreach (CellPool muscleCells in musclePools)
                foreach (MuscleCell mc in muscleCells.GetCells().Cast<MuscleCell>())
                    mc.ResumeSimulation(runParam);
            return true;
        }
        /// <summary>
        /// Extra steps required after simulation if DB is used for memory
        /// </summary>
        /// <param name="runParam"></param>
        /// <param name="dBLink"></param>
        /// <returns></returns>
        public virtual bool FinalizeSimulation(RunParam runParam, SimulationDBLink dBLink)
        {
            try
            {
                foreach (CellPool neurons in neuronPools)
                    foreach (Neuron neuron in neurons.GetCells().Cast<Neuron>())
                        neuron.FinalizeSimulation(runParam, dBLink);

                foreach (CellPool muscleCells in musclePools)
                    foreach (MuscleCell mc in muscleCells.GetCells().Cast<MuscleCell>())
                        mc.FinalizeSimulation(runParam, dBLink);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        protected void PoolToPoolGapJunction(CellPool pool1, CellPool pool2, InterPoolTemplate template)
        {
            if (pool1 == null || pool2 == null) return;
            TimeLine timeline = template.TimeLine_ms;
            pool1.ReachToCellPoolViaGapJunction(pool2, template, timeline);
        }

        protected void PoolToPoolChemSynapse(CellPool pool1, CellPool pool2, InterPoolTemplate template)
        {
            if (pool1 == null || pool2 == null) return;
            TimeLine timeline = template.TimeLine_ms;
            pool1.ReachToCellPoolViaChemSynapse(pool2, template, timeline);
        }

        public void KinemParamsChanged()
        {
            SwimmingEpisodes = null; //reset swimming episodes
        }

    }

}
