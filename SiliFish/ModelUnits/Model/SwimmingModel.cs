﻿using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Model
{

    public struct RunParam
    {
        public int tSkip_ms { get; set; } = 0;
        public int tMax { get; set; } = 1000;
        public double dt { get; set; } = 0.1;//The step size
        public double dtEuler { get; set; } = 0.01;//The step size for Euler method

        public int iIndex(double t)
        {
            int i = (int)((t + tSkip_ms) / dt);
            if (i < 0) i = 0;
            if (i >= iMax) i = iMax - 1;
            return i;
        }
        [JsonIgnore]
        public int iMax { get { return Convert.ToInt32(((tMax + tSkip_ms) / dt) + 1); } }
        [JsonIgnore]
        public static double static_Skip { get; set; } = 0;//Used from data structures that don't have direct access to the model
        [JsonIgnore]
        public static double static_dt { get; set; } = 0.1;//Used from data structures that don't have direct access to the model
        [JsonIgnore]
        public static double static_dt_Euler { get; set; } = 0.1;//Used by the Euler method differential equation solutions

        public static double GetTimeOfIndex(int index)
        { return Math.Round(static_dt * index - static_Skip, 2); }
        public RunParam() { }
    }

    public class SwimmingModel
    {
        public RunParam runParam;
        public KinemParam kinemParam;
        public static Random rand = new(0);

        private string modelName;
        public string ModelName { get => modelName; set => modelName = value; }

        public string ModelDescription { get; set; }

        public ModelDimensions ModelDimensions;

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
            kinemParam = new();
        }

        [JsonIgnore]
        [Browsable(false)]
        public List<CellPool> CellPools
        {
            get
            {
                if (!initialized)
                    InitStructures(1);
                return neuronPools.Union(musclePools).ToList();
            }
        }
        [Browsable(false)]
        public List<CellPool> NeuronPools
        {
            get
            {
                if (!initialized)
                    InitStructures(1);
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
                if (!initialized)
                    InitStructures(1);
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
                if (!initialized)
                    InitStructures(1);
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

        [Browsable(false)]
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

        [Browsable(false)]
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
                if (pool.columnIndex2D > rangeY1D)
                    rangeY1D = pool.columnIndex2D;
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
            Dictionary<string, object> paramDict = new()
            {
                { "Kinematics.Damping Coef", kinemParam.kinemZeta },
                { "Kinematics.w0", kinemParam.kinemW0 },
                { "Kinematics.Conversion Coef", kinemParam.kinemConvCoef },
                { "Kinematics.Alpha", kinemParam.kinemAlpha },
                { "Kinematics.Beta", kinemParam.kinemBeta },
                { "Kinematics.Boundary", kinemParam.kinemBound },
                { "Kinematics.Delay", kinemParam.kinemDelay }
            };

            return paramDict;
        }

        /// <summary>
        /// Parameter Desc keeps some descriptive text for the relevant parameters
        /// for easy user entry
        /// </summary>
        /// <returns>The tooltip values for the model parameters</returns>
        public virtual Dictionary<string, object> GetParameterDesc()
        {
            Dictionary<string, object> paramDescDict = new()
            {
                { "Kinematics.w0", "Natural oscillation frequency" },
                { "Kinematics.Alpha", "If non-zero, (α + β * R) is used as 'Conversion Coefficient') " },
                { "Kinematics.Beta", "If non-zero, (α + β * R) is used as 'Conversion Coefficient') " },
                { "Kinematics.Conversion Coef", "Coefficient to convert membrane potential to driving force for the oscillation" },
                { "Kinematics.Boundary", "The distance considered as a move from the center line." },
                { "Kinematics.Delay", "The time range that will be looked ahead to detect motion." }
            };

            return paramDescDict;
        }

        /// <summary>
        /// Ensures all JSON files with missing parameters are brought up to date 
        /// if there are new parameters added to the code
        /// </summary>
        /// <param name="paramDict"></param>
        protected virtual void FillMissingParameters(Dictionary<string, object> paramDict)
        {
            paramDict.AddObject("Kinematics.Damping Coef", kinemParam.kinemZeta, skipIfExists: true);
            paramDict.AddObject("Kinematics.w0", kinemParam.kinemW0, skipIfExists: true);
            paramDict.AddObject("Kinematics.Conversion Coef", kinemParam.kinemConvCoef, skipIfExists: true);
            paramDict.AddObject("Kinematics.Alpha", kinemParam.kinemAlpha, skipIfExists: true);
            paramDict.AddObject("Kinematics.Beta", kinemParam.kinemBeta, skipIfExists: true);
            paramDict.AddObject("Kinematics.Boundary", kinemParam.kinemBound, skipIfExists: true);
            paramDict.AddObject("Kinematics.Delay", kinemParam.kinemDelay, skipIfExists: true);
        }

        public virtual void SetAnimationParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            kinemParam.kinemZeta = paramExternal.Read("Kinematics.Damping Coef", kinemParam.kinemZeta);
            kinemParam.kinemW0 = paramExternal.Read("Kinematics.w0", kinemParam.kinemW0);
            kinemParam.kinemConvCoef = paramExternal.Read("Kinematics.Conversion Coef", kinemParam.kinemConvCoef);
            kinemParam.kinemAlpha = paramExternal.Read("Kinematics.Alpha", kinemParam.kinemAlpha);
            kinemParam.kinemBeta = paramExternal.Read("Kinematics.Beta", kinemParam.kinemBeta);
            kinemParam.kinemBound = paramExternal.Read("Kinematics.Boundary", kinemParam.kinemBound);
            kinemParam.kinemDelay = paramExternal.Read("Kinematics.Delay", kinemParam.kinemDelay);
        }
        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            FillMissingParameters(paramExternal);
            SetAnimationParameters(paramExternal);
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

        protected virtual void InitNeurons()
        {
            throw (new NotImplementedException());
        }
        protected virtual void InitSynapsesAndGapJunctions()
        {
            throw (new NotImplementedException());
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
            throw (new NotImplementedException());
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
                runParam = rp;
                RunParam.static_dt = rp.dt;
                RunParam.static_dt_Euler = rp.dtEuler;
                RunParam.static_Skip = rp.tSkip_ms;
                iMax = rp.iMax;
                Stimulus.nMax = iMax;

                InitStructures(iMax);
                if (!initialized)
                    return;
                //# This loop is the main loop where we solve the ordinary differential equations at every time point
                Time[0] = Math.Round((double)-1 * runParam.tSkip_ms, 2);
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
                    (Coordinate[] tail_tip_coord, List<SwimmingEpisode> episodes) = SwimmingModelKinematics.GetSwimmingEpisodesUsingMuscleCells(this);
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