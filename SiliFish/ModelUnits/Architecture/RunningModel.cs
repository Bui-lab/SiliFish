﻿using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Architecture
{
    public class RunningModel: ModelBase
    {
        public static Random rand = new(0);

        private int iRunCounter = 0;
        private int iProgress = 0;
        private int iMax = 1;

        protected bool model_run = false;
        protected bool initialized = false;
        protected double[] Time;

        protected double taur, taud, vth; //synapse parameters

        protected List<CellPool> neuronPools = new();
        protected List<CellPool> musclePools = new();

        [JsonIgnore]
        [Browsable(false)]
        public double[] TimeArray { get { return Time; } }

        [JsonIgnore]
        [Browsable(false)]
        public bool ModelRun { get { return model_run; } }

        [JsonIgnore]
        [Browsable(false)]
        public bool CancelRun { get; set; } = false;
        private bool CancelLoop { get; set; } = false;
        public double GetProgress() => iMax > 0 ? (double)iProgress / iMax : 0;
        public int GetRunCounter() => iRunCounter;


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
            ModelDescription = swimmingModelTemplate.ModelDescription;
            ModelDimensions = swimmingModelTemplate.ModelDimensions;
            Settings = swimmingModelTemplate.Settings;
            KinemParam = swimmingModelTemplate.KinemParam;
            SetParameters(swimmingModelTemplate.Parameters);

            #region Generate pools and cells
            foreach (CellPoolTemplate pool in swimmingModelTemplate.CellPoolTemplates.Where(cp => cp.Active))
            {
                if (pool.PositionLeftRight == SagittalPlane.Both || pool.PositionLeftRight == SagittalPlane.Left)
                {
                    if (pool.CellType == CellType.Neuron)
                        neuronPools.Add(new CellPool(this, pool, SagittalPlane.Left));
                    else if (pool.CellType == CellType.MuscleCell)
                        musclePools.Add(new CellPool(this, pool, SagittalPlane.Left));
                }
                if (pool.PositionLeftRight == SagittalPlane.Both || pool.PositionLeftRight == SagittalPlane.Right)
                {
                    if (pool.CellType == CellType.Neuron)
                        neuronPools.Add(new CellPool(this, pool, SagittalPlane.Right));
                    else if (pool.CellType == CellType.MuscleCell)
                        musclePools.Add(new CellPool(this, pool, SagittalPlane.Right));
                }
            }
            #endregion

            #region Generate Gap Junctions and Chemical Synapses
            foreach (InterPoolTemplate jncTemp in swimmingModelTemplate.InterPoolTemplates.Where(ip => ip.Active))
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
            if (swimmingModelTemplate.AppliedStimuli?.Count > 0)
            {
                foreach (StimulusTemplate stimulus in swimmingModelTemplate.AppliedStimuli.Where(stim => stim.Active))
                {
                    if (stimulus.LeftRight.Contains("Left"))
                    {
                        CellPool target = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == stimulus.TargetPool && np.PositionLeftRight == SagittalPlane.Left);
                        target?.ApplyStimulus(stimulus, stimulus.TargetSomite, stimulus.TargetCell);
                    }
                    if (stimulus.LeftRight.Contains("Right"))
                    {
                        CellPool target = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == stimulus.TargetPool && np.PositionLeftRight == SagittalPlane.Right);
                        target?.ApplyStimulus(stimulus, stimulus.TargetSomite, stimulus.TargetCell);
                    }
                }
            }
            #endregion
        }

        public override void BackwardCompatibility()
        { }
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

        public (List<Cell> LeftMNs, List<Cell> RightMNs) GetMotoNeurons(int numSomites)
        {
            List<Cell> motoNeurons = MotoNeurons;
            int NumberOfSomites = ModelDimensions.NumberOfSomites;
            int maxSeq = NumberOfSomites - numSomites;
            if (NumberOfSomites <= 0)
                maxSeq = CellPools.Max(cp => cp.GetCells().Max(c => c.Sequence)) - numSomites;
            List<Cell> leftMNs = motoNeurons
                                .Where(c => c.PositionLeftRight == SagittalPlane.Left &&
                                        (NumberOfSomites > 0 && c.Somite > maxSeq
                                                || NumberOfSomites <= 0 && c.Sequence > maxSeq))
                                .ToList();
            List<Cell> rightMNs = motoNeurons
                                .Where(c => c.PositionLeftRight == SagittalPlane.Right && 
                                        (NumberOfSomites > 0 && c.Somite > maxSeq
                                                || NumberOfSomites <= 0 && c.Sequence > maxSeq))
                                .ToList();
            return (leftMNs, rightMNs);
        }

        //Needs to be run after created from JSON
        public override void LinkObjects()
        {
            foreach (CellPool pool in CellPools)
            {
                pool.LinkObjects(this);
            }
        }

        public override List<CellPoolTemplate> GetCellPools() 
        { 
            return CellPools.Select(cp=>(CellPoolTemplate)cp).ToList(); 
        }
        public override bool AddCellPool(CellPoolTemplate cellPool)
        {
            if (cellPool is CellPool cp)
            {
                CellPools.Add(cp);
                return true;
            }
            return false;
        }

        public override bool RemoveCellPool(CellPoolTemplate cellPool)
        {
            if (cellPool is CellPool cp)
            {
                return CellPools.Remove(cp);
            }
            return false;
        }
        public override void SortCellPools()
        {
            CellPools.Sort();
        }
        public override void CopyConnectionsOfCellPool(CellPoolTemplate poolSource, CellPoolTemplate poolCopyTo)
        {
            //TODO
        }
        public override List<object> GetProjections()
        {
            List<object> listProjections = new();
            foreach (Cell cell in CellPools.SelectMany(cp => cp.Cells))
            {
                foreach (GapJunction jnc in cell.GapJunctions.Where(j => j.Cell1 == cell))
                    listProjections.Add(jnc);
                if (cell is Neuron neuron)
                    foreach (ChemicalSynapse syn in neuron.Terminals.Where(j => j.PreNeuron == cell))
                        listProjections.Add(syn);
            }
            return listProjections;
        }

        public override List<StimulusBase> GetStimuli()
        {
            List<StimulusBase> listStimuli= new();
            foreach (Cell cell in CellPools.SelectMany(cp => cp.Cells))
            {
                foreach (Stimulus stim in cell.Stimuli.stimuli)
                    listStimuli.Add(stim);
            }
            return listStimuli;
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

        public virtual Dictionary<string, object> GetParameters()
        {
            return null;
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

        public (List<Cell> Cells, List<CellPool> Pools) GetSubsetCellsAndPools(string poolIdentifier, CellSelectionStruct cellSelection)
        {
            List<CellPool> pools = neuronPools.Union(musclePools).Where(p => (poolIdentifier == "All" || p.CellGroup == poolIdentifier)
                            && p.OnSide(cellSelection.SagittalPlane)).ToList();
            if (cellSelection.cellSelection == PlotSelection.Summary)
                return (null, pools);
            List<Cell> cells = pools.SelectMany(p => p.GetCells(cellSelection)).ToList();
            if (cells.Any())
                return (cells, null);
            return (null, pools);
        }

        public virtual void SaveToFile(string filename)
        {
            if (!this.model_run)
            {
                return;
            }
            string Vfilename = Path.ChangeExtension(filename, "V.csv");
            string Gapfilename = Path.ChangeExtension(filename, "Gap.csv");
            string Synfilename = Path.ChangeExtension(filename, "Syn.csv");
            string filenamejson = Path.ChangeExtension(filename, "json");

            List<string> cell_names = new();
            cell_names.AddRange(neuronPools.OrderBy(np => np.CellGroup).Select(np => np.CellGroup).Distinct());
            cell_names.AddRange(musclePools.Select(k => k.CellGroup).Distinct());

            Dictionary<string, double[]> Vdata_list = new();
            Dictionary<string, double[]> Gapdata_list = new();
            Dictionary<string, double[]> Syndata_list = new();
            foreach (CellPool pool in neuronPools.Union(musclePools).OrderBy(p => p.PositionLeftRight).ThenBy(p => p.CellGroup))
            {
                //pool.GetCells().Select(c => Vdata_list.TryAdd(c.ID, c.V));
                foreach (Cell c in pool.GetCells())
                {
                    Vdata_list.Add(c.ID, c.V);
                    c.GapJunctions.Where(jnc => jnc.Cell2 == c).ToList().ForEach(jnc => Gapdata_list.TryAdd(jnc.ID, jnc.InputCurrent));
                    if (c is MuscleCell)
                    {
                        (c as MuscleCell).EndPlates.ForEach(jnc => Syndata_list.TryAdd(jnc.ID, jnc.InputCurrent));
                    }
                    else if (c is Neuron)
                    {
                        (c as Neuron).Synapses.ForEach(jnc => Syndata_list.TryAdd(jnc.ID, jnc.InputCurrent));
                    }
                }

            }
            FileUtil.SaveModelDynamicsToCSV(filename: Vfilename, Time: this.Time, Values: Vdata_list);
            FileUtil.SaveModelDynamicsToCSV(filename: Gapfilename, Time: this.Time, Values: Gapdata_list);
            FileUtil.SaveModelDynamicsToCSV(filename: Synfilename, Time: this.Time, Values: Syndata_list);
            JsonUtil.SaveToJsonFile(filenamejson, GetParameters());
        }

        protected virtual void InitDataVectors(int nmax)
        {
            foreach (CellPool neurons in neuronPools)
                foreach (Neuron neuron in neurons.GetCells().Cast<Neuron>())
                    neuron.InitDataVectors(nmax);

            foreach (CellPool muscleCells in musclePools)
                foreach (MuscleCell mc in muscleCells.GetCells().Cast<MuscleCell>())
                    mc.InitDataVectors(nmax);
        }

        protected virtual void InitStructures(int nmax)
        {
            this.Time = new double[nmax];
            InitDataVectors(nmax);
            if (!neuronPools.Any(p => p.GetCells().Any()) &&
                !musclePools.Any(p => p.GetCells().Any()))
                return;
            initialized = true;
        }

        protected void PoolToPoolGapJunction(CellPool pool1, CellPool pool2, InterPoolTemplate template)
        {
            if (pool1 == null || pool2 == null) return;
            CellReach cr = template.CellReach;
            cr.SomiteBased = ModelDimensions.NumberOfSomites > 0;
            TimeLine timeline = template.TimeLine_ms;
            pool1.ReachToCellPoolViaGapJunction(pool2, cr, timeline, template.Probability, template.DistanceMode);
        }

        protected void PoolToPoolChemSynapse(CellPool pool1, CellPool pool2, InterPoolTemplate template)
        {
            if (pool1 == null || pool2 == null) return;
            CellReach cr = template.CellReach;
            cr.SomiteBased = ModelDimensions.NumberOfSomites > 0;
            SynapseParameters synParam = template.SynapseParameters;
            TimeLine timeline = template.TimeLine_ms;
            pool1.ReachToCellPoolViaChemSynapse(pool2, cr, synParam, timeline, template.Probability, template.DistanceMode);
        }

        private void CalculateCellularOutputs(int t)
        {
            try
            {
                foreach (CellPool pools in CellPools)
                {
                    foreach (Cell cell in pools.GetCells())
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
            foreach (CellPool pool in CellPools)
            {
                foreach (Cell cell in pool.GetCells())
                    cell.CalculateMembranePotential(timeIndex);
            }
        }
        protected virtual void RunModelLoop(int? seed)
        {
            try
            {
                iProgress = 0;
                model_run = false;
                if (seed != null || rand == null)
                    rand = new Random(seed != null ? (int)seed : 0);
                RunParam.static_dt = RunParam.dt;
                RunParam.static_dt_Euler = RunParam.dtEuler;
                RunParam.static_Skip = RunParam.tSkip_ms;
                iMax = RunParam.iMax;
                Stimulus.nMax = iMax;

                InitStructures(iMax);
                if (!initialized)
                    return;
                //# This loop is the main loop where we solve the ordinary differential equations at every time point
                Time[0] = Math.Round((double)-1 * RunParam.tSkip_ms, 2);
                foreach (var index in Enumerable.Range(1, iMax - 1))
                {
                    iProgress = index;
                    Time[index] = RunParam.GetTimeOfIndex(index);
                    if (CancelRun)
                    {
                        CancelRun = false;
                        CancelLoop = true;
                        return;
                    }
                    CalculateCellularOutputs(index);
                    CalculateMembranePotentialsFromCurrents(index);
                }
                model_run = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void RunModel(int? seed, int count = 1)
        {
            string filename = $"{ModelName}_{DateTime.Now:yyMMdd-HHmm}";
            string outputFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\Output";
            filename = Path.Combine(outputFolder, filename);
            for (int i = 0; i < count; i++)
            {
                iRunCounter = i + 1;
                if (CancelLoop)
                {
                    CancelLoop = false;
                    return;
                }
                RunModelLoop(seed);
                if (count > 1 && ModelRun)
                {
                    (Coordinate[] tail_tip_coord, List<SwimmingEpisode> episodes) = SwimmingKinematics.GetSwimmingEpisodesUsingMuscleCells(this);
                    string runfilename = $"{filename}_Run{iRunCounter}";
                    runfilename = Path.Combine(outputFolder, runfilename);
                    //Util.SaveTailMovementToCSV(runfilename + ".csv", Time, tail_tip_coord);
                    FileUtil.SaveEpisodesToCSV(filename + ".csv", iRunCounter, episodes);
                    JsonUtil.SaveToJsonFile(runfilename + ".json", this);
                    seed = null;
                }
                if (CancelLoop)
                {
                    CancelLoop = false;
                    return;
                }
            }
        }

    }

}
