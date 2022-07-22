using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int tSkip_ms { get; set; } = 0;
        public int tMax { get; set; } = 1000;
        public double dt { get; set; } = 0.1;//The step size

        public int iIndex(double t)
        {
            int i = (int)((t+tSkip_ms)/ dt);
            if (i < 0) i = 0;
            if (i >= iMax) i = iMax - 1;
            return i;
        }
        public int iMax { get { return Convert.ToInt32(((tMax + tSkip_ms) / dt) + 1); } }
        public static double static_dt { get; set; } = 0.1;//Used from data structures that don't have direct access to the model
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
        public double BodyMedialLateralDistance { get; set; }
        public double BodyDorsalVentralDistance { get; set; }

        public int NumberOfSomites { get; set; } = 0;

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
            Dictionary<string, object> paramDict = new()
            {
                { "General.Name", ModelName },
                { "General.Description", ModelDescription },
                { "General.NumberOfSomites", NumberOfSomites },
                { "General.SpinalRostralCaudalDistance", SpinalRostralCaudalDistance },
                { "General.SpinalDorsalVentralDistance", SpinalDorsalVentralDistance },
                { "General.SpinalMedialLateralDistance", SpinalMedialLateralDistance },
                { "General.SpinalBodyPosition", SpinalBodyPosition },
                { "General.BodyDorsalVentralDistance", BodyDorsalVentralDistance },
                { "General.BodyMedialLateralDistance", BodyMedialLateralDistance },

                { "Dynamic.cv", cv },
                { "Dynamic.E_ach", E_ach },
                { "Dynamic.E_glu", E_glu },
                { "Dynamic.E_gly", E_gly },
                { "Dynamic.E_gaba", E_gaba },

                { "Animation.DampingCoef", khi },
                { "Animation.w0", w0 },
                { "Animation.ConversionCoef", alpha }
            };

            return paramDict;
        }

        public void FillMissingParameters(Dictionary<string, object> paramDict)
        {
            paramDict.AddObject("Dynamic.cv", cv, skipIfExists: true);
            paramDict.AddObject("Dynamic.E_ach", E_ach, skipIfExists: true);
            paramDict.AddObject("Dynamic.E_glu", E_glu, skipIfExists: true);
            paramDict.AddObject("Dynamic.E_gly", E_gly, skipIfExists: true);
            paramDict.AddObject("Dynamic.E_gaba", E_gaba, skipIfExists: true);
            paramDict.AddObject("Animation.DampingCoef", khi, skipIfExists: true);
            paramDict.AddObject("Animation.w0", w0, skipIfExists: true);
            paramDict.AddObject("Animation.ConversionCoef", alpha, skipIfExists: true);

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

        public virtual void SetAnimationParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            khi = paramExternal.Read("Animation.DampingCoef", khi);
            w0 = paramExternal.Read("Animation.w0", w0);
            alpha = paramExternal.Read("Animation.ConversionCoef", alpha);
        }
        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            ModelName = paramExternal.Read("General.Name", ModelName);
            ModelDescription = paramExternal.Read("General.Description", ModelDescription);
            NumberOfSomites = paramExternal.ReadInteger("General.NumberOfSomites");
            SpinalRostralCaudalDistance = paramExternal.ReadDouble("General.SpinalRostralCaudalDistance");
            SpinalDorsalVentralDistance = paramExternal.ReadDouble("General.SpinalDorsalVentralDistance");
            SpinalMedialLateralDistance = paramExternal.ReadDouble("General.SpinalMedialLateralDistance");
            SpinalBodyPosition = paramExternal.ReadDouble("General.SpinalBodyPosition");
            BodyDorsalVentralDistance = paramExternal.ReadDouble("General.BodyDorsalVentralDistance");
            BodyMedialLateralDistance = paramExternal.ReadDouble("General.BodyMedialLateralDistance");

            cv = paramExternal.Read("Dynamic.cv", cv);
            E_ach = paramExternal.Read("Dynamic.E_ach", E_ach);
            E_glu = paramExternal.Read("Dynamic.E_glu", E_glu);
            E_gly = paramExternal.Read("Dynamic.E_gly", E_gly);
            E_gaba = paramExternal.Read("Dynamic.E_gaba", E_gaba);

            khi = paramExternal.Read("Animation.DampingCoef", khi);
            w0 = paramExternal.Read("Animation.w0", w0);
            alpha = paramExternal.Read("Animation.ConversionCoef", alpha);
        }

        public (List<Cell> Cells, List<CellPool> Pools) GetSubsetCellsAndPools(PlotExtend plotMode, string subset, CellSelectionStruct cellSelection)
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
                    return (cellpool.GetCells(cellSelection).ToList(), null);
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

        public virtual string SummarizeModel()
        {
            StringBuilder strBuilder = new();
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

        private void CalculateNeuronalOutputs(int t)
        {
            try
            {
                /*Simple multithreading as below makes it less efficient
                Parallel.ForEach(NeuronPools, neurons =>
                {
                    foreach (Neuron neuron in neurons.GetCells())
                        neuron.CalculateNeuronalOutputs(t);
                });
                /*/
                foreach (CellPool neurons in NeuronPools)
                {
                    foreach (Neuron neuron in neurons.GetCells())
                        neuron.CalculateNeuronalOutputs(t);
                }
                
            }
            catch (Exception ex)
            {
                ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        

        private void CalculateMembranePotentialsFromCurrents(int t)
        {
            foreach (CellPool neurons in NeuronPools)
            {
                foreach (Neuron neuron in neurons.GetCells())
                    neuron.CalculateMembranePotential(t);
            }

            foreach (CellPool muscleCells in MuscleCellPools)
                foreach (MuscleCell mc in muscleCells.GetCells())
                    mc.CalculateMembranePotential(t);
        }


        public virtual void MainLoop(double seed, RunParam rp)
        {
            try
            {
                iProgress = 0;
                model_run = false;
                rand = new Random((int)seed);
                runParam = rp;
                RunParam.static_dt = rp.dt;
                iMax = rp.iMax;
                Stimulus.nMax = iMax;

                InitStructures(iMax);
                //# This loop is the main loop where we solve the ordinary differential equations at every time point
                Time[0] = Math.Round((double)-1 * runParam.tSkip_ms, 2);
                foreach (var index in Enumerable.Range(1, iMax - 1))
                {
                    iProgress = index;
                    Time[index] = Math.Round(runParam.dt * index - runParam.tSkip_ms, 2);
                    if (CancelRun)
                    {
                        CancelRun = false;
                        return;
                    }
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

        private (double[,] vel, double[,] angle) GenerateSpineVelAndAngleNoSomite(int startIndex, int endIndex)
        {
            if (!model_run) return (null, null);
            List<Cell> LeftMuscleCells = MusclePools.Where(mp => mp.PositionLeftRight == SagittalPlane.Left).SelectMany(mp => mp.GetCells()).ToList();
            List<Cell> RightMuscleCells = MusclePools.Where(mp => mp.PositionLeftRight == SagittalPlane.Right).SelectMany(mp => mp.GetCells()).ToList();

            int nmax = endIndex - startIndex + 1;
            //TODO: left and right muscle cell count can be different
            int nMuscle = LeftMuscleCells.Count;
            if (nMuscle == 0) return (null, null);
            // Allocating arrays for velocity and position
            double[,] vel = new double[nMuscle, nmax];
            double[,] angle = new double[nMuscle, nmax];
            // Setting constants and initial values for vel. and pos.
            double vel0 = 0.0;
            double angle0 = 0.0;
            double dt = runParam.dt;
            int k = 0;

            foreach (MuscleCell leftMuscle in LeftMuscleCells.OrderBy(c => c.Sequence))
            {
                MuscleCell rightMuscle = (MuscleCell)RightMuscleCells.FirstOrDefault(c => c.Sequence == leftMuscle.Sequence);
                vel[k, 0] = vel0;
                angle[k, 0] = angle0;
                angle[nMuscle - 1, 0] = 0.0;
                foreach (var i in Enumerable.Range(1, nmax - 1))
                {
                    double R = (leftMuscle.R + rightMuscle.R) / 2;
                    //alpha is the conversion coefficient: "Animation.ConversionCoef"
                    double coef = alpha * (1 - 0.2 * R);//The formula in the paper. 0.1 is used in the python code
                    double voltDiff = rightMuscle.V[startIndex + i - 1] - leftMuscle.V[startIndex + i - 1];
                    //khi is the damping coefficient: "Animation.DampingCoef"
                    double acc = -Math.Pow(w0, 2) * angle[k, i - 1] - 2 * vel[k, i - 1] * khi * w0 + coef * voltDiff;
                    vel[k, i] = vel[k, i - 1] + acc * dt;
                    angle[k, i] = angle[k, i - 1] + vel[k, i - 1] * dt;
                }
                k++;
            }

            return (vel, angle);
        }

        private (double[,] vel, double[,] angle) GenerateSpineVelAndAngle(int startIndex, int endIndex)
        {
            if (!model_run) return (null, null);
            if (NumberOfSomites <= 0)
                return GenerateSpineVelAndAngleNoSomite(startIndex, endIndex);

            int nmax = endIndex - startIndex + 1;
            int nSomite = NumberOfSomites;

            // Allocating arrays for velocity and position
            double[,] vel = new double[nSomite, nmax];
            double[,] angle = new double[nSomite, nmax];
            // Setting constants and initial values for vel. and pos.
            double vel0 = 0.0;
            double angle0 = 0.0;
            double dt = runParam.dt;
            int k = 0;

            for (int somite = 0; somite < nSomite; somite++)
            {
                List<Cell> LeftMuscleCells = MusclePools
                    .Where(mp => mp.PositionLeftRight == SagittalPlane.Left)
                    .SelectMany(mp => mp.GetCells().Where(c => c.Somite == somite)).ToList();
                List<Cell> RightMuscleCells = MusclePools
                    .Where(mp => mp.PositionLeftRight == SagittalPlane.Right)
                    .SelectMany(mp => mp.GetCells().Where(c => c.Somite == somite)).ToList();
                double R = LeftMuscleCells.Sum(c => (c as MuscleCell).R) + RightMuscleCells.Sum(c => (c as MuscleCell).R);
                R /= (LeftMuscleCells.Count + RightMuscleCells.Count);
                double coef = alpha * (1 - 0.2 * R);//The formula in the paper
                vel[k, 0] = vel0;
                angle[k, 0] = angle0;
                angle[nSomite - 1, 0] = 0.0;
                foreach (var i in Enumerable.Range(1, nmax - 1))
                {
                    double voltDiff = RightMuscleCells.Sum(c => c.V[startIndex + i - 1]) - LeftMuscleCells.Sum(c => c.V[startIndex + i - 1]);
                    double acc = -Math.Pow(w0, 2) * angle[k, i - 1] - 2 * vel[k, i - 1] * khi * w0 + coef * voltDiff;
                    vel[k, i] = vel[k, i - 1] + acc * dt;
                    angle[k, i] = angle[k, i - 1] + vel[k, i - 1] * dt;
                }
          
            }

            return (vel, angle);
        }

        public Dictionary<string, Coordinate[]> GenerateSpineCoordinates(int startIndex, int endIndex)
        {
            (double[,] vel, double[,] angle) = GenerateSpineVelAndAngle(startIndex, endIndex);

            if (vel?.GetLength(0) != angle?.GetLength(0) ||
                vel?.GetLength(1) != angle?.GetLength(1))
                return null;
            int nSpineNode = vel.GetLength(0);
            int nMax = vel.GetLength(1);
            double[,] x = new double[nSpineNode, nMax + 1];
            double[,] y = new double[nSpineNode, nMax + 1];
            foreach (var i in Enumerable.Range(0, nMax))
            {
                x[0, i] = 0;
                y[0, i] = 0;
                angle[0, i] = 0;
                foreach (int l in Enumerable.Range(1, nSpineNode - 1))
                {
                    angle[l, i] = angle[l - 1, i] + angle[l, i];
                    x[l, i] = x[l - 1, i] + Math.Sin(angle[l, i]);
                    y[l, i] = y[l - 1, i] - Math.Cos(angle[l, i]);
                }
            }

            Dictionary<string, Coordinate[]> somiteCoordinates = new();
            foreach (int somiteIndex in Enumerable.Range(0, nSpineNode))
            {
                string somite = "S" + somiteIndex.ToString("000"); //for proper sorting
                somiteCoordinates.Add(somite, new Coordinate[nMax + 1]);
                somiteCoordinates[somite][0] = (0, 0);
                foreach (var i in Enumerable.Range(0, nMax))
                {
                    somiteCoordinates[somite][i] = (x[somiteIndex, i], y[somiteIndex, i]);
                }
            }
            return somiteCoordinates;
        }

        public (Coordinate[] , List<SwimmingEpisode>) GetSwimmingEpisodes(double left_bound, double right_bound, int delay)
        {
            /*Converted from the code written by Yann Roussel and Tuan Bui
            This function calculates tail beat frequency based upon crossings of y = 0 as calculated from the body angles calculated
            by VRMuscle and VLMuscle
            :param dt: float, time step
            :param lower_bound: int, bound used to discriminate swimming tail beats from noise
            :param upper_bound: int, bound used to discriminate swimming tail beats from noise
            :param delay: float, defines the time window during which we consider tail beats
            :return: Four 1-D numpy arrays for number of tail beats, interbeat time intervals, start times and beat times
            */

            Dictionary<string, Coordinate[]> spineCoordinates = GenerateSpineCoordinates(0, TimeArray.Length - 1);

            //We will only use the tip of the tail to determine tail beats (if the x coordinate of the tip is smaller (or more negative)
            //than the left bound or if the x coordinate of the tip is greater than the right bound, then detect as a tail beat

            Coordinate[] tail_tip_coord = spineCoordinates.Last().Value;

            int side = 0;
            const int LEFT = -1;
            const int RIGHT = 1;
            int nMax = TimeArray.Length;
            double dt = runParam.dt;
            double offset = runParam.tSkip_ms;
            List<SwimmingEpisode> episodes = new();
            SwimmingEpisode lastEpisode = null;
            int i = (int)(offset/dt);
            while (i < nMax)
            {
                int iMax = Math.Min(i + delay, nMax);
                Coordinate[] window = tail_tip_coord[i..iMax];
                double t = TimeArray[i];
                if (!window.Any(coor => coor.X < left_bound || coor.X > right_bound))
                {
                    side = 0;
                    lastEpisode?.EndEpisode(t);
                    lastEpisode = null;
                    i = iMax;
                    continue;
                }

                if (lastEpisode == null)
                {
                    if (tail_tip_coord[i].X < left_bound)//beginning an episode on the left
                    {
                        lastEpisode = new SwimmingEpisode(t);
                        episodes.Add(lastEpisode);
                        side = LEFT;
                    }
                    else if (tail_tip_coord[i].X > right_bound) //beginning an episode on the right
                    {
                        lastEpisode = new SwimmingEpisode(t);
                        episodes.Add(lastEpisode);
                        side = RIGHT;
                    }
                }
                else // During an episode
                {
                    if (tail_tip_coord[i].X < left_bound && side == RIGHT)
                    {
                        side = LEFT;
                        lastEpisode.EndBeat(t);
                    }

                    else if (tail_tip_coord[i].X > right_bound && side == LEFT)
                    {
                        side = RIGHT;
                        lastEpisode.EndBeat(t);
                    }
                }
                i++;
            }

            return (tail_tip_coord, episodes.Where(e => e.End > 0).ToList());
        }

        public bool CancelRun { get; set; } = false;
        public double GetProgress() => iMax > 0 ? (double)iProgress / iMax : 0;

        public static void ExceptionHandling(string name, Exception ex)
        {
            Console.WriteLine(name + ex.ToString());
        }

    }

}
