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
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Architecture
{
    public class RunningModel : ModelBase
    {      
        private int iProgress = 0;
        private int iMax = 1;

        protected bool model_run = false;
        protected bool initialized = false;
        protected double[] Time;

        protected List<CellPool> neuronPools = new();
        protected List<CellPool> musclePools = new();

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
        public List<InterPool> GapPoolConnections
        {
            get
            {
                List<InterPool> interPools = new();
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
                            double minConductance = list.Min(c => c.Weight);
                            double maxConductance = list.Max(c => c.Weight);
                            interPools.Add(new InterPool()
                            {
                                SourcePool = source,
                                TargetPool = target,
                                CountJunctions = count,
                                MinConductance = minConductance,
                                MaxConductance = maxConductance,
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
        public List<InterPool> ChemPoolConnections
        {
            get
            {
                List<InterPool> interPools = new();
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
                            double minConductance = list.Min(c => c.Weight);
                            double maxConductance = list.Max(c => c.Weight);

                            interPools.Add(new InterPool()
                            {
                                SourcePool = source,
                                TargetPool = target,
                                CountJunctions = count,
                                MinConductance = minConductance,
                                MaxConductance = maxConductance,
                                Mode = sourcePool.CellOutputMode
                            });
                        }
                    }
                }
                return interPools;
            }
        }
        [JsonPropertyOrder(2)]
        public RunParam RunParam { get; set; } = new();

        [JsonIgnore]
        [Browsable(false)]
        public double[] TimeArray { get { return Time; } }

        [JsonIgnore]
        [Browsable(false)]
        public bool ModelRun { get { return model_run; } }

        [JsonIgnore]
        [Browsable(false)]
        public bool CancelRun { get; set; } = false;
        public double GetProgress() => iMax > 0 ? (double)iProgress / iMax : 0;

        [JsonIgnore]
        [Browsable(false)]
        public bool JunctionCurrentTrackingOn = true;

        public RunningModel()
        {
        }

        public RunningModel(ModelTemplate swimmingModelTemplate)
        {
            neuronPools.Clear();
            musclePools.Clear();

            initialized = false;

            if (swimmingModelTemplate == null) return;

            ModelName = swimmingModelTemplate.ModelName;
            ModelDescription = $"Generated from {ModelName} on {DateTime.Now:g}\r\n{swimmingModelTemplate.ModelDescription}";
            ModelDimensions = swimmingModelTemplate.ModelDimensions.Clone();
            Settings = swimmingModelTemplate.Settings.Clone();
            KinemParam = swimmingModelTemplate.KinemParam.Clone();
            SetParameters(swimmingModelTemplate.Parameters);

            #region Generate pools and cells
            foreach (CellPoolTemplate cellPoolTemplate in swimmingModelTemplate.CellPoolTemplates.Where(cp => cp.Active))
            {
                GenerateCellPoolsFrom(cellPoolTemplate);
            }
            #endregion

            #region Generate Gap Junctions and Chemical Synapses
            foreach (InterPoolTemplate jncTemp in swimmingModelTemplate.InterPoolTemplates.Where(ip => ip.JncActive))
            {
                CellPool leftSource = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == jncTemp.PoolSource && np.PositionLeftRight == SagittalPlane.Left);
                CellPool rightSource = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == jncTemp.PoolSource && np.PositionLeftRight == SagittalPlane.Right);

                CellPool leftTarget = neuronPools.Union(musclePools).FirstOrDefault(mp => mp.CellGroup == jncTemp.PoolTarget && mp.PositionLeftRight == SagittalPlane.Left);
                CellPool rightTarget = neuronPools.Union(musclePools).FirstOrDefault(mp => mp.CellGroup == jncTemp.PoolTarget && mp.PositionLeftRight == SagittalPlane.Right);

                if (jncTemp.ConnectionType == ConnectionType.Synapse || jncTemp.ConnectionType == ConnectionType.NMJ)
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
                else if (jncTemp.ConnectionType == ConnectionType.Gap)
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
            if (swimmingModelTemplate.StimulusTemplates?.Count > 0)
            {
                foreach (StimulusTemplate stimulus in swimmingModelTemplate.StimulusTemplates.Where(stim => stim.Active))
                {
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

        public void BackwardCompatibility_Stimulus()
        {
            try
            {
                foreach(Cell cell in GetCells().Where(c=>c.Stimuli!=null && c.Stimuli.HasStimulus))
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
                    double descGap = descendingGapList.Any() ? 
                        descendingGapList.Max(jnc => Util.Distance(jnc.Cell1.Coordinate, jnc.Cell2.Coordinate, jnc.DistanceMode)) : 0;
                    double descChem = 0;
                    if ( cell is Neuron neuron)
                    {
                        List<ChemicalSynapse> descendingChemList = neuron.Terminals.Where(jnc => cell.Coordinate.X < jnc.PostCell.Coordinate.X).ToList();
                        if (descendingChemList.Any())
                            descChem = descendingChemList.Max(jnc => Util.Distance(cell.Coordinate, jnc.PostCell.Coordinate, jnc.DistanceMode));
                    }
                    cell.DescendingAxonLength = Math.Max(descChem, descGap);

                    List<GapJunction> ascendingGapList = cell.GapJunctions.
                        Where(jnc => jnc.Cell1 == cell && cell.Coordinate.X > jnc.Cell2.Coordinate.X ||
                            jnc.Cell2 == cell && cell.Coordinate.X > jnc.Cell1.Coordinate.X).ToList();
                    double ascGap = ascendingGapList.Any() ?
                        ascendingGapList.Max(jnc => Util.Distance(jnc.Cell1.Coordinate, jnc.Cell2.Coordinate, jnc.DistanceMode)) : 0;
                    double ascChem = 0;
                    if (cell is Neuron neuron2)
                    {
                        List<ChemicalSynapse> ascendingChemList = neuron2.Terminals.Where(jnc => cell.Coordinate.X < jnc.PostCell.Coordinate.X).ToList();
                        if (ascendingChemList.Any())
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

        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            foreach (CellPool cp in CellPools)
                cp.CheckValues(ref errors);
            return errors.Count == 0;
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
            int NumberOfSomites = ModelDimensions.NumberOfSomites;
            List<Cell> leftMNs = motoNeurons
                                .Where(c => c.PositionLeftRight == SagittalPlane.Left &&
                                        (NumberOfSomites > 0 && c.Somite == SomiteSeq
                                                || NumberOfSomites <= 0 && c.Sequence == SomiteSeq))
                                .ToList();
            List<Cell> rightMNs = motoNeurons
                                .Where(c => c.PositionLeftRight == SagittalPlane.Right &&
                                        (NumberOfSomites > 0 && c.Somite == SomiteSeq
                                                || NumberOfSomites <= 0 && c.Sequence == SomiteSeq))
                                .ToList();
            return (leftMNs, rightMNs);
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
            CellPool cellPool = CellPools.FirstOrDefault(cp => cp.ID == cell.CellGroup);
            if (cellPool == null) return false;
            cellPool.AddCell(cell);
            return true;
        }

        public bool HasCells()
        {
            return GetCells().Any();
        }
        #endregion
        #region Cell Pool

        public List<CellPool> GenerateCellPoolsFrom(CellPoolTemplate cellPoolTemplate)
        {
            List<CellPool> cellPoolList = new();
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
        public override void SortCellPools()
        {
            CellPools.Sort();
        }
        public override void CopyConnectionsOfCellPool(CellPoolTemplate poolSource, CellPoolTemplate poolCopyTo)
        {
        }
        #endregion

        #region Projections
        public bool HasConnections()
        {
            return CellPools.Any(cp => cp.HasConnections());
        }
        public override List<InterPoolBase> GetGapProjections()
        {
            List<InterPoolBase> listProjections = new();
            foreach (Cell cell in CellPools.SelectMany(cp => cp.Cells))
            {
                foreach (GapJunction jnc in cell.GapJunctions.Where(j => j.Cell1 == cell))
                    listProjections.Add(jnc);
            }
            return listProjections;
        }
        public override List<InterPoolBase> GetChemicalProjections()
        {
            List<InterPoolBase> listProjections = new();
            foreach (Cell cell in CellPools.SelectMany(cp => cp.Cells))
            {
                if (cell is Neuron neuron)
                    foreach (ChemicalSynapse syn in neuron.Terminals)
                        listProjections.Add(syn);
            }
            return listProjections;
        }

        public void RemoveJunctionsOf(CellPool cp, Cell cell, bool gap, bool chemin, bool chemout)
        {
            cp?.DeleteJunctions(gap, chemin, chemout);
            cell?.DeleteJunctions(gap, chemin, chemout);
            if (cp == null && cell == null)
            {
                foreach(CellPool cellPool in CellPools)
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
            List<StimulusBase> listStimuli = new();
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

        public virtual (double, double) GetConnectionRange()
        {
            double maxWeight = 0;
            double minWeight = 999;
            foreach (CellPool pool in neuronPools.Union(musclePools))
            {
                (double localMin, double localMax) = pool.GetConnectionRange();
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
        public virtual int GetNumberOfConnections()
        {
            return CellPools.Sum(p => p.Cells.Sum(c => c.GapJunctions.Count + ((c as MuscleCell)?.EndPlates.Count ?? 0) + ((c as Neuron)?.Synapses.Count ?? 0)));
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


        public (List<Cell> Cells, List<CellPool> Pools) GetSubsetCellsAndPools(string poolIdentifier, PlotSelectionInterface cellSelection)
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
                List<Cell> cells = pools.SelectMany(p => p.GetCells(mcs)).ToList();
                if (cells.Any())
                    return (cells, null);
                return (null, pools);
            }
            return (null, pools);
        }



        protected virtual void InitForSimulation()
        {
            if (RunParam.iMax <= 0) return;
            if (!neuronPools.Any(p => p.GetCells().Any()) &&
                !musclePools.Any(p => p.GetCells().Any())) //No cells
                return;
            Time = new double[RunParam.iMax];
            foreach (CellPool neurons in neuronPools)
                foreach (Neuron neuron in neurons.GetCells().Cast<Neuron>())
                    neuron.InitForSimulation(RunParam);

            foreach (CellPool muscleCells in musclePools)
                foreach (MuscleCell mc in muscleCells.GetCells().Cast<MuscleCell>())
                    mc.InitForSimulation(RunParam);
            initialized = true;
        }

        protected void PoolToPoolGapJunction(CellPool pool1, CellPool pool2, InterPoolTemplate template)
        {
            if (pool1 == null || pool2 == null) return;
            CellReach cr = template.CellReach;
            cr.SomiteBased = ModelDimensions.NumberOfSomites > 0;
            TimeLine timeline = template.TimeLine_ms;
            pool1.ReachToCellPoolViaGapJunction(pool2, cr, timeline, template.Weight, template.Probability, template.DistanceMode, template.Delay_ms, template.FixedDuration_ms);
        }

        protected void PoolToPoolChemSynapse(CellPool pool1, CellPool pool2, InterPoolTemplate template)
        {
            if (pool1 == null || pool2 == null) return;
            CellReach cr = template.CellReach;
            cr.SomiteBased = ModelDimensions.NumberOfSomites > 0;
            SynapseParameters synParam = template.SynapseParameters;
            TimeLine timeline = template.TimeLine_ms;
            pool1.ReachToCellPoolViaChemSynapse(pool2, cr, synParam, timeline, template.Weight, template.Probability, template.DistanceMode, template.Delay_ms, template.FixedDuration_ms);
        }

        private void CalculateCellularOutputs(int t)
        {
            try
            {
                foreach (CellPool pool in CellPools.Where(cp => cp.Active))
                {
                    foreach (Cell cell in pool.GetCells().Where(c => c.Active))
                        cell.CalculateCellularOutputs(t);
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void CalculateMembranePotentialsFromCurrents(int timeIndex)
        {
            foreach (CellPool pool in CellPools.Where(cp => cp.Active))
            {
                foreach (Cell cell in pool.GetCells().Where(c => c.Active))
                    cell.CalculateMembranePotential(timeIndex);
            }
        }
        public virtual void RunModel()
        {
            try
            {
                iProgress = 0;
                model_run = false;
                rand ??= new Random(Settings.Seed);
                iMax = RunParam.iMax;

                InitForSimulation();
                if (CancelRun)
                    return;
                if (!initialized)
                    return;
                //# This loop is the main loop where we solve the ordinary differential equations at every time point
                Time[0] = Math.Round((double)-1 * RunParam.SkipDuration, 2);
                foreach (var index in Enumerable.Range(1, iMax - 1))
                {
                    iProgress = index;
                    Time[index] = RunParam.GetTimeOfIndex(index);
                    if (CancelRun)
                    {
                        CancelRun = false;
                        return;
                    }
                    CalculateCellularOutputs(index);
                    CalculateMembranePotentialsFromCurrents(index);
                }
                model_run = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
            }
        }


    }

}
