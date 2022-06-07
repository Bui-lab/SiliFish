using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SiliFish.DataTypes;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits;
using SiliFish.Services;
//using SixLabors.ImageSharp.Drawing.Processing;

namespace SiliFish
{

    public struct RunParam
    {
        public int tSkip { get; set; } = 0;
        public int tMax { get; set; } = 1000;
        public static double dt { get; set; } = 0.1;//The step size
        public RunParam() { }

    }

    public class SwimmingModel
    {
        public RunParam runParam;
        public double cv = -1; //the transmission speed

        protected double E_glu = 0; //the reversal potential of glutamate
        protected double E_gly = -70; //the reversal potential of glycine
        protected double E_gaba = -70; //the reversal potential of GABA
        protected double E_ach = 120; //reversal potential for ACh receptors

        public double khi = 3.0; //#damping constant , high khi =0.5/ low = 0.1
        public double w0 = 2.5; //20Hz = 125.6
        public double alpha = 0.1;


        public static Random rand = new(0);

        private string modelName;
        public string ModelName { get => modelName; set => modelName = value; }
        public string ModelDescription { get; set; }
        public double SpinalRostralCaudalDistance { get; set; }
        public double SpinalDorsalVentralDistance { get; set; }
        public double SpinalMedialLateralDistance { get; set; }
        public double SpinalBodyPosition { get; set; }
        public double BodyMedialLateralDistance{ get; set; }
        public double BodyDorsalVentralDistance { get; set; }

        public int NumberOfSomites { get; set; } = 15;

        private int iProgress = 0;
        private int iMax = 1;

        protected bool model_run = false;
        protected bool initialized = false;
        protected double[] Time;

        protected double taur, taud, vth; //synapse parameters

        protected List<CellPool> NeuronPools = new();
        protected List<CellPool> MuscleCellPools = new();
        protected List<InterPool> PoolConnections = new();

        public bool ModelRun { get { return model_run; } }
        public int MaxIndex { get { return iMax; } }
        public double[] TimeArray { get { return Time; } }

        public SwimmingModel()
        {
            modelName = this.GetType().Name;
        }

        public List<CellPool> CellPools
        {
            get
            {
                if (!initialized) 
                    InitStructures(1);
                return NeuronPools.Union(MuscleCellPools).ToList();
            }
        }
        public List<CellPool> MusclePools
        {
            get
            {
                if (!initialized)
                    InitStructures(1);
                return MuscleCellPools;
            }
        }
        public List<InterPool> ChemPoolConnections { get { return PoolConnections.Where(con => con.IsChemical).ToList(); } }
        public List<InterPool> GapPoolConnections { get { return PoolConnections.Where(con => !con.IsChemical).ToList(); } }
        public virtual ((double, double), (double, double), (double, double), int) GetSpatialRange()
        {
            double minX = 999;
            double maxX = -999;
            double minY = 999;
            double maxY = -999;
            double minZ = 999;
            double maxZ = -999;
            int rangeY1D = 0;

            foreach (CellPool pool in NeuronPools.Union(MuscleCellPools))
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
            foreach (CellPool pool in NeuronPools.Union(MuscleCellPools))
            {
                (double localMin, double localMax) = pool.GetConnectionRange();
                if (localMin < minWeight)
                    minWeight = localMin;
                if (localMax > maxWeight)
                    maxWeight = localMax;
            }
            return (minWeight, maxWeight);
        }




        public virtual Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = new();

            paramDict.Add("Dynamic.cv", cv);
            paramDict.Add("Dynamic.E_ach", E_ach);
            paramDict.Add("Dynamic.E_glu", E_glu);
            paramDict.Add("Dynamic.E_gly", E_gly);
            paramDict.Add("Dynamic.E_gaba", E_gaba);
            paramDict.Add("Dynamic.DampingCoef", khi);
            paramDict.Add("Dynamic.w0", w0);
            paramDict.Add("Dynamic.ConversionCoef", alpha);

            paramDict.Add("General.Name", ModelName);
            paramDict.Add("General.Description", ModelDescription);
            paramDict.Add("General.NumberOfSomites", NumberOfSomites);
            paramDict.Add("General.SpinalRostralCaudalDistance", SpinalRostralCaudalDistance);
            paramDict.Add("General.SpinalDorsalVentralDistance", SpinalDorsalVentralDistance);
            paramDict.Add("General.SpinalMedialLateralDistance", SpinalMedialLateralDistance);
            paramDict.Add("General.SpinalBodyPosition", SpinalBodyPosition);
            paramDict.Add("General.BodyDorsalVentralDistance", BodyDorsalVentralDistance);
            paramDict.Add("General.BodyMedialLateralDistance", BodyMedialLateralDistance);

            return paramDict;
        }

        public void FillMissingParameters(Dictionary<string, object> paramDict)
        {
            paramDict.AddObject("Dynamic.cv", cv, skipIfExists: true);
            paramDict.AddObject("Dynamic.E_ach", E_ach, skipIfExists: true);
            paramDict.AddObject("Dynamic.E_glu", E_glu, skipIfExists: true);
            paramDict.AddObject("Dynamic.E_gly", E_gly, skipIfExists: true);
            paramDict.AddObject("Dynamic.E_gaba", E_gaba, skipIfExists: true);
            paramDict.AddObject("Dynamic.DampingCoef", khi, skipIfExists: true);
            paramDict.AddObject("Dynamic.w0", w0, skipIfExists: true);
            paramDict.AddObject("Dynamic.ConversionCoef", alpha, skipIfExists: true);

            paramDict.AddObject("General.Name", ModelName, skipIfExists: true);
            paramDict.AddObject("General.Description", ModelDescription, skipIfExists: true);
            paramDict.AddObject("General.NumberOfSomites", NumberOfSomites, skipIfExists: true);
            paramDict.AddObject("General.SpinalRostralCaudalDistance", SpinalRostralCaudalDistance, skipIfExists: true);
            paramDict.AddObject("General.SpinalDorsalVentralDistance", SpinalDorsalVentralDistance, skipIfExists: true);
            paramDict.AddObject("General.SpinalMedialLateralDistance", SpinalMedialLateralDistance, skipIfExists: true);
            paramDict.AddObject("General.SpinalBodyPosition", SpinalBodyPosition, skipIfExists: true);
            paramDict.AddObject("General.BodyDorsalVentralDistance", BodyDorsalVentralDistance, skipIfExists: true);
            paramDict.AddObject("General.BodyMedialLateralDistance", BodyMedialLateralDistance, skipIfExists: true);
        }

        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            initialized = false;
            cv = paramExternal.Read("Dynamic.cv", cv);
            E_ach = paramExternal.Read("Dynamic.E_ach", E_ach);
            E_glu = paramExternal.Read("Dynamic.E_glu", E_glu);
            E_gly = paramExternal.Read("Dynamic.E_gly", E_gly);
            E_gaba = paramExternal.Read("Dynamic.E_gaba", E_gaba);
            khi = paramExternal.Read("Dynamic.DampingCoef", khi);
            w0 = paramExternal.Read("Dynamic.w0", w0);
            alpha = paramExternal.Read("Dynamic.ConversionCoef", alpha);

            ModelName = paramExternal.Read("General.Name", ModelName);
            ModelDescription = paramExternal.Read("General.Description", ModelDescription);
            NumberOfSomites = paramExternal.ReadInteger("General.NumberOfSomites");
            SpinalRostralCaudalDistance = paramExternal.ReadDouble("General.SpinalRostralCaudalDistance");
            SpinalDorsalVentralDistance = paramExternal.ReadDouble("General.SpinalDorsalVentralDistance");
            SpinalMedialLateralDistance = paramExternal.ReadDouble("General.SpinalMedialLateralDistance");
            SpinalBodyPosition = paramExternal.ReadDouble("General.SpinalBodyPosition");
            BodyDorsalVentralDistance = paramExternal.ReadDouble("General.BodyDorsalVentralDistance");
            BodyMedialLateralDistance = paramExternal.ReadDouble("General.BodyMedialLateralDistance");
        }

        private (List<Cell> Cells, List<CellPool> Pools) GetSubsetCellsAndPools(PlotExtend plotMode, string subset)
        {
            if (plotMode == PlotExtend.SinglePool)
            {
                return (null, NeuronPools.Union(MuscleCellPools).Where(p => p.ID == subset).ToList());
            }
            else if (plotMode == PlotExtend.OppositePools)
            {
                return (null, NeuronPools.Union(MuscleCellPools).Where(p => p.CellGroup == subset).ToList());
            }
            else if (plotMode == PlotExtend.CellsInAPool)
            {
                CellPool cellpool = CellPools.Where(p => p.ID == subset).FirstOrDefault(p => p != null);
                if (cellpool != null)
                    return (cellpool.GetCells().ToList(), null);
                else
                    return (null, null);
            }
            else if (plotMode == PlotExtend.SingleCell)
            {
                Cell cell = CellPools.Select(p => p.GetCell(subset)).FirstOrDefault(c => c != null);
                if (cell != null)
                    return (new List<Cell> { cell }, null);
                else
                    return (null, null);
            }
            else //FullModel
                return (null, NeuronPools.Union(MuscleCellPools).ToList());
        }

        //plots the membrane potentials between tstart(by default 0) and tend (by default the end)
        //if plotall is set to True, all cell membrane potentials are plotted
        //   otherwise, only motoneuron membrane potentials are plotted
        //if offset is given, it will be subtracted from Time array
        
        //TODO Plotting functions for membrane potentials, currents, and stimuli are very similar to each other. Simplify
        //TODO plotting functions should not be in SwimmingModel - reorganize
        protected virtual string PlotMembranePotentials(int iStart = 0, int iEnd = -1, List<Cell> cells = null, List<CellPool> pools = null, int offset = 0, string filename = "", int nSample = 0)
        {
            if (iStart < 0 || iStart >= Time.Length)
                iStart = 0;
            if (iEnd < iStart || iEnd >= Time.Length)
                iEnd = Time.Length - 1;
            double[] TimeOffset = offset > 0 ? (Time.Select(t => t - offset).ToArray()) : Time;
            LineChartGenerator lc = new();
            if (cells != null)
                return lc.CreatePotentialsMultiPlot(filename, "Potentials", cells, TimeOffset, iStart, iEnd, numColumns: 2);
            if (pools != null)
                return lc.CreatePotentialsMultiPlot(filename, "Potentials", pools, TimeOffset, iStart, iEnd, numColumns: 2, nSample);
            return "";
        }

        public string PlotMembranePotentials(PlotExtend plotMode, string subset, int tStart = 0, int tEnd = -1, int nSample = 0)
        {
            int tSkip = runParam.tSkip;
            double dt = RunParam.dt;
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);
            int offset = tSkip;
            (List<Cell> Cells, List<CellPool> Pools) = GetSubsetCellsAndPools(plotMode, subset);

            return PlotMembranePotentials(iStart, iEnd, cells: Cells, pools: Pools, offset, nSample: nSample);
        }
        protected virtual string PlotCurrents(int iStart = 0, int iEnd = -1, List<Cell> cells = null, List<CellPool> pools = null, int offset = 0,
            bool includeGap = true, bool includeChem = true, string filename = "", int nSample = 0)
        {
            if (iEnd < iStart || iEnd >= Time.Length)
                iEnd = Time.Length - 1;

            double[] TimeOffset = offset > 0 ? (Time.Select(t => t - offset).ToArray()) : Time;

            LineChartGenerator lc = new();
            if (cells != null)
                return lc.CreateCurrentsMultiPlot(filename, "Currents", cells, TimeOffset, iStart, iEnd, numColumns: 2, includeGap: includeGap, includeChem: includeChem);
            if (pools != null)
                return lc.CreateCurrentsMultiPlot(filename, "Currents", pools, TimeOffset, iStart, iEnd, numColumns: 2, includeGap: includeGap, includeChem: includeChem, nSample: nSample);
            return "";
        }
        public string PlotCurrents(PlotExtend plotMode, string subset, int tStart = 0, int tEnd = -1, bool includeGap = true, bool includeChem = true, int nSample = 0)
        {
            int tSkip = runParam.tSkip;
            double dt = RunParam.dt;
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);
            int offset = tSkip;
            (List<Cell> Cells, List<CellPool> Pools) = GetSubsetCellsAndPools(plotMode, subset);

            return PlotCurrents(iStart, iEnd, cells: Cells, pools: Pools, offset, includeGap, includeChem, nSample: nSample);
        }

        protected virtual string PlotStimuli(int iStart = 0, int iEnd = -1, List<Cell> cells = null, List<CellPool> pools = null, int offset = 0,
            string filename = "", int nSample = 0)
        {
            if (iEnd < iStart || iEnd >= Time.Length)
                iEnd = Time.Length - 1;

            double[] TimeOffset = offset > 0 ? (Time.Select(t => t - offset).ToArray()) : Time;

            LineChartGenerator lc = new();
            if (cells != null)
                return lc.CreateStimuliMultiPlot(filename, "Stimuli", cells, TimeOffset, iStart, iEnd, numColumns: 2);
            if (pools != null)
                return lc.CreateStimuliMultiPlot(filename, "Stimuli", pools, TimeOffset, iStart, iEnd, numColumns: 2,nSample: nSample);
            return "";
        }

        public string PlotStimuli(PlotExtend plotMode, string subset, int tStart = 0, int tEnd = -1, int nSample = 0)
        {
            int tSkip = runParam.tSkip;
            double dt = RunParam.dt;
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);
            int offset = tSkip;
            (List<Cell> Cells, List<CellPool> Pools) = GetSubsetCellsAndPools(plotMode, subset);

            return PlotStimuli(iStart, iEnd, cells: Cells, pools: Pools, offset, nSample: nSample);
        }

        public virtual string SummarizeModel()
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (CellPool pool in NeuronPools.Union(MuscleCellPools).OrderBy(p => p.PositionLeftRight).ThenBy(p => p.CellGroup))
            {
                strBuilder.AppendLine("#Cell Pool#");
                strBuilder.AppendLine(pool.ID);
                foreach (Cell cell in pool.GetCells())
                {
                    strBuilder.AppendLine(cell.ID);
                    if (cell is Neuron neuron)
                    {
                        strBuilder.AppendLine("##Gap Junctions##");
                        foreach (GapJunction jnc in neuron.GapJunctions)
                            strBuilder.AppendLine(jnc.ID);
                        strBuilder.AppendLine("##Terminals##");
                        foreach (ChemicalSynapse jnc in neuron.Terminals)
                            strBuilder.AppendLine(jnc.ID);
                        strBuilder.AppendLine("##Synapses##");
                        foreach (ChemicalSynapse jnc in neuron.Synapses)
                            strBuilder.AppendLine(jnc.ID);
                    }
                    else if (cell is MuscleCell muscle)
                    {
                        strBuilder.AppendLine("##EndPlates##");
                        foreach (ChemicalSynapse jnc in muscle.EndPlates)
                            strBuilder.AppendLine(jnc.ID);
                    }
                }
            }
            return strBuilder.ToString();
        }
        public virtual void SaveToFile(string filename = null)
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
            cell_names.AddRange(NeuronPools.OrderBy(np => np.CellGroup).Select(np => np.CellGroup).Distinct());
            cell_names.AddRange(MuscleCellPools.Select(k => k.CellGroup).Distinct());

            Dictionary<string, double[]> Vdata_list = new();
            Dictionary<string, double[]> Gapdata_list = new();
            Dictionary<string, double[]> Syndata_list = new();
            foreach (CellPool pool in NeuronPools.Union(MuscleCellPools).OrderBy(p => p.PositionLeftRight).ThenBy(p => p.CellGroup))
            {
                //pool.GetCells().Select(c => Vdata_list.TryAdd(c.ID, c.V));
                foreach (Cell c in pool.GetCells())
                {
                    Vdata_list.Add(c.ID, c.V);
                    if (c is MuscleCell)
                    {
                        (c as MuscleCell).EndPlates.ForEach(jnc => Syndata_list.TryAdd(jnc.ID, jnc.InputCurrent));
                    }
                    else if (c is Neuron)
                    {
                        (c as Neuron).Synapses.ForEach(jnc => Syndata_list.TryAdd(jnc.ID, jnc.InputCurrent));
                        (c as Neuron).GapJunctions.Where(jnc => jnc.Cell2 == c).ToList().ForEach(jnc => Gapdata_list.TryAdd(jnc.ID, jnc.InputCurrent));
                    }
                }

            }
            Util.SaveToCSV(filename: Vfilename, Time: this.Time, Values: Vdata_list);
            Util.SaveToCSV(filename: Gapfilename, Time: this.Time, Values: Gapdata_list);
            Util.SaveToCSV(filename: Synfilename, Time: this.Time, Values: Syndata_list);
            Util.SaveToJSON(filenamejson, GetParameters());
        }



        protected virtual void InitNeurons()
        {
            throw (new NotImplementedException());
        }
        protected virtual void InitSynapsesAndGapJunctions()
        {
            throw (new NotImplementedException());
        }

        protected virtual void InitMembranePotentials(int nmax)
        {
            foreach (CellPool neurons in NeuronPools)
                foreach (Neuron neuron in neurons.GetCells())
                    neuron.InitVectors(nmax);

            foreach (CellPool muscleCells in MuscleCellPools)
                foreach (MuscleCell mc in muscleCells.GetCells())
                    mc.InitVectors(nmax);
        }

        protected virtual void InitStructures(int nmax)
        {
            this.Time = new double[nmax];
            NeuronPools.Clear();
            MuscleCellPools.Clear();
            PoolConnections.Clear();
            this.InitNeurons();
            this.InitSynapsesAndGapJunctions();
            this.InitMembranePotentials(nmax);
            initialized = true;
        }
        protected void PoolToPoolGapJunction(CellPool pool1, CellPool pool2, CellReach cr, TimeLine timeline = null)
        {
            if (pool1 == null || pool2 == null) return;
            PoolConnections.Add(new InterPool { poolSource = pool1, poolTarget = pool2, reach = cr, timeLine = timeline });
            pool1.ReachToCellPoolViaGapJunction(pool2, cr, timeline);
        }
        protected void PoolToPoolGapJunction(CellPool pool1, CellPool pool2, InterPoolTemplate template)
        {
            if (pool1 == null || pool2 == null) return;
            CellReach cr = template.CellReach;
            TimeLine timeline = template.TimeLine;
            PoolConnections.Add(new InterPool { poolSource = pool1, poolTarget = pool2, reach = cr, timeLine = timeline });
            pool1.ReachToCellPoolViaGapJunction(pool2, cr, timeline);
        }

        protected void PoolToPoolChemSynapse(CellPool pool1, CellPool pool2, CellReach cr, SynapseParameters synParam, TimeLine timeline = null)
        {
            if (pool1 == null || pool2 == null) return;
            PoolConnections.Add(new InterPool { poolSource = pool1, poolTarget = pool2, reach = cr, timeLine = timeline, synapseParameters = synParam });
            pool1.ReachToCellPoolViaChemSynapse(pool2, cr, synParam, timeline);
        }

        protected void PoolToPoolChemSynapse(CellPool pool1, CellPool pool2, InterPoolTemplate template)
        {
            if (pool1 == null || pool2 == null) return;
            CellReach cr = template.CellReach;
            SynapseParameters synParam = template.SynapseParameters;
            TimeLine timeline = template.TimeLine;
            PoolConnections.Add(new InterPool { poolSource = pool1, poolTarget = pool2, reach = cr, timeLine = timeline, synapseParameters = synParam });
            pool1.ReachToCellPoolViaChemSynapse(pool2, cr, synParam, timeline);
        }

        private void CalculateNeuronalOutputs(Neuron n, int t)
        {
            try
            {
                foreach (ChemicalSynapse syn in n.Terminals)
                {
                    syn.NextStep(t);
                }

                foreach (GapJunction jnc in n.GapJunctions.Where(j => j.Cell1 == n)) //to prevent double call
                {
                    jnc.NextStep(t);
                }
            }
            catch (Exception ex) 
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }

        private void CalculateNeuronalOutputs(int t)
        {
            try
            {
                foreach (CellPool neurons in NeuronPools)
                {
                    foreach (Neuron neuron in neurons.GetCells())
                        CalculateNeuronalOutputs(neuron, t);
                }
            }
            catch (Exception ex) 
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void CalculateMembranePotentialOfNeuron(Neuron n, int t)
        {
            try
            {
                double ISyn = 0, IGap = 0, stim = 0;
                if (n.IsAlive(t))
                {
                    foreach (ChemicalSynapse syn in n.Synapses)
                    {
                        ISyn += syn.GetSynapticCurrent(t);
                    }
                    foreach (GapJunction jnc in n.GapJunctions)
                    {
                        IGap += jnc.GetGapCurrent(n, t);
                    }
                    stim = n.GetStimulus(t, rand);
                }

                n.NextStep(t, stim + ISyn + IGap);
            }
            catch (Exception ex)
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void CalculateMembranePotentialOfMuscleCell(MuscleCell n, int t)
        {
            double ISyn = 0, stim = 0;
            if (n.IsAlive(t))
            {
                foreach (ChemicalSynapse syn in n.EndPlates)
                {
                    ISyn += syn.GetSynapticCurrent(t);
                }
                stim = n.GetStimulus(t, rand);
            }
            n.NextStep(t, stim + ISyn);
        }
        private void CalculateMembranePotentialsFromCurrents(int t)
        {
            foreach (CellPool neurons in NeuronPools)
            {
                foreach (Neuron neuron in neurons.GetCells())
                    CalculateMembranePotentialOfNeuron(neuron, t);
            }

            foreach (CellPool muscleCells in MuscleCellPools)
                foreach (MuscleCell mc in muscleCells.GetCells())
                    CalculateMembranePotentialOfMuscleCell(mc, t);
        }


        // This function sets up the connectome and runs a simulation.
        // rand is a seed for a random function
        // tmax: the time period the simulation will be run for
        // tskip: the initial time period that is added to the simulation to cover the initial conditions to dissipate
        // tplot_interval: the time period that a plot of membrane potential(s) will be displayed. These plots will include the tskip region.
        // plotProgressOnly: a flag that shows whether at each tplot_interval, the plot will display the potential change from the zero point, or since the end of the previous plot.
        //   Having this parameter True will make it easier to compare each period to each other. Caution: x-axis scales will be the same, but y-axis scales will not.
        // plotAllPotentials: whether the plotting should plot only MN membrane potentials, or all cells (IC, V0a, etc)
        // printParam: a flag that will display the parameters used in the run.
        // plotResult: a flag to display the final plot from 0 to tmax (after the tskip region is removed)
        // saveCSV: if True, the membrane potentials of all of the cells will be saved as a CSV file, and parameters will be saved as a JSON file.
        //   The file name will be the same as the class name.
        // saveAnim: if True, an animation of the muscle movements will be saved as an mp4 file.
        //   The file name will be the same as the class name.
        public virtual void MainLoop(double seed, RunParam rp)
        {
            try
            {
                model_run = false;
                rand = new Random((int)seed);
                runParam = rp;
                iMax = Convert.ToInt32((rp.tMax + rp.tSkip) / RunParam.dt);
                Stimulus.nMax = iMax;
                
                InitStructures(iMax);
                //# This loop is the main loop where we solve the ordinary differential equations at every time point
                foreach (var index in Enumerable.Range(1, iMax - 1))
                {
                    iProgress = index;
                    Time[index] = Math.Round(RunParam.dt * index, 2);

                    CalculateNeuronalOutputs(index);
                    CalculateMembranePotentialsFromCurrents(index);
                }
                model_run = true;
            }
            catch (Exception ex) 
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public double GetProgress() => iMax > 0 ? (double)iProgress / iMax : 0;

        private void ExceptionHandling(string name, Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

}
