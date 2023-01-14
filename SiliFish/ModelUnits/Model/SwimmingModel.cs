using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Model
{
    public class SwimmingModel
    {
        public static Random rand = new(0);

        private string modelName;
        public string ModelName { get => modelName; set => modelName = value; }
        public string ModelDescription { get; set; }

        public ModelDimensions ModelDimensions { get; set; }
        public RunParam RunParam { get; set; } = new();
        public KinemParam KinemParam { get; set; }


        private int iRunCounter = 0;
        private int iProgress = 0;
        private int iMax = 1;

        protected bool model_run = false;
        protected bool initialized = false;
        protected double[] Time;

        protected double taur, taud, vth; //synapse parameters

        private UnitOfMeasure uom = UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad;
        public UnitOfMeasure UoM
        {
            get { return uom; }
            set { CurrentSettings.Settings.UoM = uom = value; }
        }

        protected List<CellPool> neuronPools = new();
        protected List<CellPool> musclePools = new();
        protected List<InterPool> gapPoolConnections = new();
        protected List<InterPool> chemPoolConnections = new();

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


        public SwimmingModel()
        {
            modelName = this.GetType().Name;
            KinemParam = new();
        }

        public SwimmingModel(SwimmingModelTemplate swimmingModelTemplate)
        {
            neuronPools.Clear();
            musclePools.Clear();
            gapPoolConnections.Clear();
            chemPoolConnections.Clear();

            initialized = false;

            if (swimmingModelTemplate == null) return;

            ModelName = swimmingModelTemplate.ModelName;
            ModelDescription = swimmingModelTemplate.ModelDescription;
            ModelDimensions = swimmingModelTemplate.ModelDimensions;
            CurrentSettings.Settings = swimmingModelTemplate.Settings;
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
                    Stimulus stim = new(stimulus.StimulusSettings, stimulus.TimeLine_ms);
                    if (stimulus.LeftRight.Contains("Left"))
                    {
                        CellPool target = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == stimulus.TargetPool && np.PositionLeftRight == SagittalPlane.Left);
                        target?.ApplyStimulus(stim, stimulus.TargetSomite, stimulus.TargetCell);
                    }
                    if (stimulus.LeftRight.Contains("Right"))
                    {
                        CellPool target = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == stimulus.TargetPool && np.PositionLeftRight == SagittalPlane.Right);
                        target?.ApplyStimulus(stim, stimulus.TargetSomite, stimulus.TargetCell);
                    }
                }
            }
            #endregion
        }

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
        public List<CellPool> MotoNeuronPools //MotoNeurons are the neurons projecting to the muscle cells
        {
            get
            {
                return chemPoolConnections
                    .Where(conn => conn.TargetPool.CellType == CellType.MuscleCell)
                    .Select(conn => conn.SourcePool)
                    .Distinct()
                    .ToList();
            }
        }

        public (List<Cell> LeftMNs, List<Cell> RightMNs) GetMotoNeurons(int numSomites)
        {
            List<CellPool> motoNeurons = MotoNeuronPools;
            int NumberOfSomites = ModelDimensions.NumberOfSomites;
            int maxSeq = NumberOfSomites - numSomites;
            if (NumberOfSomites <= 0)
                maxSeq = CellPools.Max(cp => cp.GetCells().Max(c => c.Sequence)) - numSomites;
            List<Cell> leftMNs = motoNeurons
                                .Where(pool => pool.PositionLeftRight == SagittalPlane.Left)
                                .SelectMany(pool => pool.Cells)
                                .Where(c => NumberOfSomites > 0 && c.Somite > maxSeq
                                                || NumberOfSomites <= 0 && c.Sequence > maxSeq)
                                .ToList();
            List<Cell> rightMNs = motoNeurons
                                .Where(pool => pool.PositionLeftRight == SagittalPlane.Right)
                                .SelectMany(pool => pool.Cells)
                                .Where(c => NumberOfSomites > 0 && c.Somite > maxSeq
                                                || NumberOfSomites <= 0 && c.Sequence > maxSeq)
                                .ToList();
            return (leftMNs, rightMNs);
        }

        [JsonIgnore, Browsable(false)]
        public List<InterPool> ChemPoolConnections
        {
            get
            {
                return chemPoolConnections;
            }
            set
            {
                //called by JSON
                chemPoolConnections = value;
            }
        }

        [JsonIgnore, Browsable(false)]
        public List<InterPool> GapPoolConnections
        {
            get
            {
                return gapPoolConnections;
            }
            set
            {
                //called by JSON
                gapPoolConnections = value;
            }
        }

        //Needs to be run after created from JSON
        public void LinkObjects()
        {
            foreach (CellPool pool in CellPools)
            {
                pool.LinkObjects(this);
            }
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
                foreach (Neuron neuron in neurons.GetCells())
                    neuron.InitDataVectors(nmax);

            foreach (CellPool muscleCells in musclePools)
                foreach (MuscleCell mc in muscleCells.GetCells())
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

        protected void PoolToPoolGapJunction(CellPool pool1, CellPool pool2, CellReach cr, TimeLine timeline = null, double probability = 1)
        {
            if (pool1 == null || pool2 == null) return;
            gapPoolConnections.Add(new InterPool(pool1, pool2, cr, null, timeline));
            pool1.ReachToCellPoolViaGapJunction(pool2, cr, timeline, probability);
        }
        protected void PoolToPoolChemSynapse(CellPool pool1, CellPool pool2, CellReach cr, SynapseParameters synParam, TimeLine timeline = null, double probability = 1)
        {
            if (pool1 == null || pool2 == null) return;
            chemPoolConnections.Add(new InterPool(pool1, pool2, cr, synParam, timeline));
            pool1.ReachToCellPoolViaChemSynapse(pool2, cr, synParam, timeline, probability);
        }

        protected void PoolToPoolGapJunction(CellPool pool1, CellPool pool2, InterPoolTemplate template)
        {
            if (pool1 == null || pool2 == null) return;
            CellReach cr = template.CellReach;
            cr.SomiteBased = ModelDimensions.NumberOfSomites > 0;
            TimeLine timeline = template.TimeLine_ms;
            gapPoolConnections.Add(new InterPool(pool1, pool2, cr, null, timeline));
            pool1.ReachToCellPoolViaGapJunction(pool2, cr, timeline, template.Probability);
        }

        protected void PoolToPoolChemSynapse(CellPool pool1, CellPool pool2, InterPoolTemplate template)
        {
            if (pool1 == null || pool2 == null) return;
            CellReach cr = template.CellReach;
            cr.SomiteBased = ModelDimensions.NumberOfSomites > 0;
            SynapseParameters synParam = template.SynapseParameters;
            TimeLine timeline = template.TimeLine_ms;
            chemPoolConnections.Add(new InterPool(pool1, pool2, cr, synParam, timeline));
            pool1.ReachToCellPoolViaChemSynapse(pool2, cr, synParam, timeline, template.Probability);
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
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
        protected virtual void RunModelLoop(int? seed, RunParam rp)
        {
            try
            {
                iProgress = 0;
                model_run = false;
                if (seed != null || rand == null)
                    rand = new Random(seed != null ? (int)seed : 0);
                RunParam = rp;
                RunParam.static_dt = rp.dt;
                RunParam.static_dt_Euler = rp.dtEuler;
                RunParam.static_Skip = rp.tSkip_ms;
                iMax = rp.iMax;
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
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void RunModel(int? seed, RunParam rp, int count = 1)
        {
            string filename = $"{modelName}_{DateTime.Now:yyMMdd-HHmm}";
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
                RunModelLoop(seed, rp);
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

        public static void ExceptionHandling(string name, Exception ex)
        {
            Console.WriteLine(name + ex.ToString());
        }

    }

}
