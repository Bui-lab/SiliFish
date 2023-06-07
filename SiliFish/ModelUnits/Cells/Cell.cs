using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Cells
{
    [JsonDerivedType(typeof(Cell), typeDiscriminator: "cell")]
    [JsonDerivedType(typeof(Neuron), typeDiscriminator: "neuron")]
    [JsonDerivedType(typeof(MuscleCell), typeDiscriminator: "musclecell")]
    public class Cell : ModelUnitBase, IDataExporterImporter
    {
        private static int csvExportCoreParamCount = 10;
        private static Type GetTypeOfCell(string discriminator)
        {
            return discriminator switch
            {
                "cell" => typeof(Cell),
                "neuron" => typeof(Neuron),
                "musclecell" => typeof(MuscleCell),
                _ => typeof(Cell),
            };
        }
        public static Cell CreateCell(string cellType)
        {
            return (Cell)Activator.CreateInstance(GetTypeOfCell(cellType));
        }
        protected virtual string Discriminator => "cell";

        public CellPool CellPool;
        public Coordinate Coordinate;

        [JsonIgnore]
        public RunningModel Model { get; set; }
        public CellCoreUnit Core { get; set; }
        public string CellGroup { get; set; }
        public int Sequence { get; set; }
        public int Somite { get; set; } = -1;
        public override string ID
        {
            get
            {
                string s = Somite > 0 ? "_" + Somite.ToString() : "";
                return $"{Position}_{CellGroup}{s}_{Sequence}";
            }
        }


        public double ConductionVelocity { get; set; }
        public double AscendingAxonLength { get; set; }
        public double DescendingAxonLength { get; set; }

        [JsonPropertyOrder(3)]
        public Stimuli Stimuli { get; set; } = new();

        #region Position Properties
        /*public FrontalPlane PositionDorsalVentral { get; set; } = FrontalPlane.NotSet;
        public TransversePlane PositionAnteriorPosterior { get; set; } = TransversePlane.NotSet;
        */
        public SagittalPlane PositionLeftRight { get; set; } = SagittalPlane.Both;

        [JsonIgnore]
        public string Position
        {
            get
            {
                string FTS =
                    /*(PositionDorsalVentral == FrontalPlane.Ventral ? "V" : PositionDorsalVentral == FrontalPlane.Dorsal ? "D" : "") +
                    (PositionAnteriorPosterior == TransversePlane.Posterior ? "P" : PositionAnteriorPosterior == TransversePlane.Anterior ? "A" : PositionAnteriorPosterior == TransversePlane.Central ? "C" : "") +
                    */
                    (PositionLeftRight == SagittalPlane.Left ? "L" : PositionLeftRight == SagittalPlane.Right ? "R" : "");
                return FTS;
            }
        }
        public double X { get => Coordinate.X; set => Coordinate.X = value; }
        public double Y { get => Coordinate.Y; set => Coordinate.Y = value; }
        public double Z { get => Coordinate.Z; set => Coordinate.Z = value; }
        #endregion

        #region Connection Properties
        [JsonPropertyOrder(1)]
        public List<GapJunction> LeavingGapJunctions
        {
            get
            {
                return GapJunctions.Where(jnc => jnc.Cell1 == this).ToList();
            }
            set => GapJunctions = value;
        }
        [JsonIgnore]
        public List<GapJunction> GapJunctions { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<JunctionBase> Projections
        {
            get
            {
                return GapJunctions;
            }
        }

        #endregion

        #region Simulation Values
        [JsonIgnore]
        public double[] V; //Membrane potential vector
        #endregion
        [JsonIgnore]
        public virtual double RestingMembranePotential { get {
                Exception exception = new NotImplementedException();
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
                throw exception;
            }
        }

        [JsonIgnore, Browsable(false)]
        public static List<string> ColumnNames { get; } =
            ListBuilder.Build<string>("CellType", "CellPool", "Somite", "Sequence", Coordinate.ColumnNames,
            "Conduction Velocity", "CoreType",
            Enumerable.Range(1, CellCoreUnit.CoreParamMaxCount).SelectMany(i => new[] { $"Param{i}", $"Value{i}" }),
            "Active",
            TimeLine.ColumnNames);

         [JsonIgnore, Browsable(false)]
        private List<string> csvExportCoreValues
        {
            get
            {
                List<string> paramValues = Core.Parameters
                    .Take(CellCoreUnit.CoreParamMaxCount)
                    .OrderBy(kv => kv.Key)
                    .SelectMany(kv => new[] { kv.Key, kv.Value.ToString()}).ToList();
                for (int i = Core.Parameters.Count; i < CellCoreUnit.CoreParamMaxCount; i++)
                {
                    paramValues.Add(string.Empty);
                    paramValues.Add(string.Empty);
                }
                return paramValues;
            }
        }

        public List<string> ExportValues()
        {
            return ListBuilder.Build<string>(Discriminator, CellPool.ID, Somite, Sequence, Coordinate.ExportValues(),
                ConductionVelocity, Core.CoreType, csvExportCoreValues, Active, TimeLine_ms.ExportValues());
        }
        public void ImportValues(List<string> values)
        {
            int iter = 1; //discriminitor field will be already used when this object is created
            if (values.Count < ColumnNames.Count - 1) return;//Timeline may be blank
            CellGroup = values[iter++];//this is the ID of the cell pool
            Somite = int.Parse(values[iter++]);
            Sequence = int.Parse(values[iter++]);
            Coordinate.ImportValues(values.Take(new Range(iter, iter + 3)).ToList());
            iter += 3;
            ConductionVelocity = double.Parse(values[iter++]);
            string coreType = values[iter++].Trim();
            Dictionary<string, double> parameters = new();
            for (int i = 1; i <= csvExportCoreParamCount; i++)
            {
                if (iter > values.Count - 2) break;
                string paramkey = values[iter++].Trim();
                double.TryParse(values[iter++].Trim(), out double paramvalue);
                if (!string.IsNullOrEmpty(paramkey))
                {
                    parameters.Add(paramkey, paramvalue);
                }
            }
            Core = CellCoreUnit.CreateCore(coreType, parameters);
            Active = bool.Parse(values[iter++]);
            if (iter < values.Count)
                TimeLine_ms.ImportValues(new[] { values[iter++] }.ToList());
        }
        

        public static Cell GenerateFromCSVRow(string row)
        {
            string[] values = row.Split(',');
            if (values.Length != ColumnNames.Count) return null;
            string discriminator = values[0];
            Cell cell = CreateCell(discriminator);
            cell.ImportValues(row.Split(",").ToList());
            return cell;
        }
        public Cell()
        {
            Stimuli = new();
        }

        public override string ToString()
        {
            return ID + (Active ? "" : " (inactive)");
        }

        public string GetTooltip()
        {
            return $"{ToString()}\r\nTimeLine: {TimeLine_ms}";
        }

        public virtual void LinkObjects(RunningModel model, CellPool pool)
        {
            Model = model;
            CellPool = pool;
            Core = CellCoreUnit.CreateCore(Core.CoreType, Core.Parameters, Model.RunParam.DeltaT, Model.RunParam.DeltaTEuler);
            foreach (GapJunction jnc in GapJunctions.Where(j => j.Cell1 == null))//To prevent double linking
            {
                jnc.Cell1 = this;
                jnc.LinkObjects(model);
            }
            foreach (Stimulus stim in Stimuli.ListOfStimulus)
            {
                stim.TargetCell = this;
            }
        }

        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);

            int errorCount = errors.Count;
            Core.CheckValues(ref errors);
            foreach (GapJunction jnc in LeavingGapJunctions)
                jnc.CheckValues(ref errors);
            if (errors.Count > errorCount)
                errors.Insert(errorCount, $"{ID}:");
            return errors.Count == 0;
        }
        /// <summary>
        /// Removes all stimuli, and incoming and outgoing connections
        /// </summary>
        public void ClearLinks()
        {
            ClearStimuli();
            DeleteJunctions();
        }

        #region Sort Functions
        public virtual void SortJunctions()
        {
            GapJunctions.Sort();
        }
        public virtual void SortJunctionsBySource()
        {
            GapJunctions = GapJunctions.OrderBy(jnc => jnc.Cell1.ID).ToList();
        }
        public virtual void SortJunctionsByTarget()
        {
            GapJunctions = GapJunctions.OrderBy(jnc => jnc.Cell2.ID).ToList();
        }

        public virtual void SortStimuli()
        {
            Stimuli.Sort();
        }

        #endregion

        #region Stimulus Functions

        public override bool HasStimulus()
        {
            return Stimuli.HasStimulus;
        }
        public void ClearStimuli()
        {
            Stimuli.Clear();
            Stimuli = new();
        }
        public double GetStimulus(int timeIndex)
        {
            return Stimuli.GetStimulus(timeIndex);
        }

        public void AddStimulus(Stimulus stim)
        {
            Stimuli.Add(stim);
        }
        public void RemoveStimulus(Stimulus stim)
        {
            Stimuli.Remove(stim);
        }

        #endregion

        #region Connection Functions
        public GapJunction CreateGapJunction(Cell n2, double weight, DistanceMode distanceMode)
        {
            GapJunction jnc = new(weight, this, n2, distanceMode);
            if (jnc == null) return null;
            GapJunctions.Add(jnc);
            n2.GapJunctions.Add(jnc);
            return jnc;
        }
        public virtual void AddChemicalSynapse(ChemicalSynapse jnc)
        {
            throw (new NotImplementedException());
        }
        public virtual (double, double) GetConnectionRange()
        {
            double? maxWeight1 = GapJunctions.Select(j => j.Weight).DefaultIfEmpty(0).Max();
            double? minWeight1 = GapJunctions.Where(j => j.Weight > 0).Select(j => j.Weight).DefaultIfEmpty(999).Min();
            return (minWeight1 ?? 0, maxWeight1 ?? 999);
        }

        internal virtual bool RemoveJunction(JunctionBase junction)
        {
            if (junction is GapJunction jnc)
                return GapJunctions.Remove(jnc);
            return false;
        }
        public virtual void DeleteJunctions(bool gap = true, bool chemin = true, bool chemout = true)
        {
            if (!gap) return;
            while (GapJunctions.Any())
            {
                GapJunction jnc = GapJunctions.First();
                jnc.Cell2.RemoveJunction(jnc);
                jnc.Cell1.RemoveJunction(jnc);
            }
        }
        public virtual bool HasConnections(bool gap, bool chemin, bool chemout)
        {
            return gap && GapJunctions.Any();
        }
        #endregion

        #region RunTime
        public virtual void InitForSimulation(RunParam runParam)
        {
            if (ConductionVelocity < GlobalSettings.Epsilon)
                ConductionVelocity = Model.Settings.cv;
            V = new double[runParam.iMax];
            Core.Initialize(runParam.DeltaT, runParam.DeltaTEuler);
            Stimuli.InitForSimulation(Model.RunParam, Model.rand);
            foreach (GapJunction jnc in GapJunctions)
                jnc.InitForSimulation(runParam.iMax);
        }
        internal bool IsAlive(int timepoint)
        {
            double t_ms = Model.RunParam.GetTimeOfIndex(timepoint);
            return TimeLine_ms?.IsActive(t_ms) ?? true;
        }

        public virtual void NextStep(int t, double stim)
        {
            bool spike = false;
            double v = Core.GetNextVal(stim, ref spike);
            if (v < GlobalSettings.BiologicalMinPotential)
                v = GlobalSettings.BiologicalMinPotential;
            else if (v > GlobalSettings.BiologicalMaxPotential)
                v = GlobalSettings.BiologicalMaxPotential;
            V[t] = v;
        }
        
        public virtual void CalculateCellularOutputs(int t)
        {
            try
            {
                foreach (GapJunction jnc in GapJunctions.Where(j => j.Cell1 == this && j.Active)) //to prevent double call
                {
                    if (jnc.IsActive(t))
                        jnc.NextStep(t);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        public virtual void CalculateMembranePotential(int t)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;

        }
        #endregion

        #region Simulation Stats
        public double MinStimulusValue() { return Stimuli.MinValue; }
        public double MaxStimulusValue() { return Stimuli.MaxValue; }
        public double MinPotentialValue(int iStart = 0, int iEnd = -1)
        {
            return V?.MinValue(iStart, iEnd) ?? 0;
        }
        public double MaxPotentialValue(int iStart = 0, int iEnd = -1)
        {
            return V?.MaxValue(iStart, iEnd) ?? 0;
        }

        public virtual bool IsSpiking(int iStart = 0, int iEnd = -1)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual List<int> GetSpikeIndices(int iStart = 0, int iEnd = -1)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public int GetSpikeStart(int tIndex)
        {
            return V?.GetSpikeStart(Core.Vthreshold, tIndex) ?? -1;
        }

        public virtual double MinCurrentValue(bool gap, bool chemin, bool chemout, int iStart = 0, int iEnd = -1)
        {
            double gapcurrent = gap ? MinGapCurrentValue(iStart, iEnd) : 0;
            double syncurrent = chemin ? MinSynInCurrentValue(iStart, iEnd) : 0;
            double terminalcurrent = chemout ? MinSynOutCurrentValue(iStart, iEnd) : 0;
            return gapcurrent + syncurrent + terminalcurrent;
        }
        public virtual double MaxCurrentValue(bool gap, bool chemin, bool chemout, int iStart = 0, int iEnd = -1)
        {
            double gapcurrent = gap ? MaxGapCurrentValue(iStart, iEnd) : 0;
            double syncurrent = chemin ? MaxSynInCurrentValue(iStart, iEnd) : 0;
            double terminalcurrent = chemout ? MaxSynOutCurrentValue(iStart, iEnd) : 0;
            return gapcurrent + syncurrent + terminalcurrent;
        }

        public virtual double MinGapCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return GapJunctions != null && GapJunctions.Any() ? GapJunctions.Min(jnc => jnc.InputCurrent?.MinValue(iStart, iEnd) ?? 0) : 0;
        }
        public virtual double MaxGapCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return GapJunctions != null && GapJunctions.Any() ? GapJunctions.Max(jnc => jnc.InputCurrent?.MaxValue(iStart, iEnd) ?? 0) : 0;
        }

        public virtual double MinSynInCurrentValue(int iStart = 0, int iEnd = -1)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual double MaxSynInCurrentValue(int iStart = 0, int iEnd = -1)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public virtual double MinSynOutCurrentValue(int iStart = 0, int iEnd = -1)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual double MaxSynOutCurrentValue(int iStart = 0, int iEnd = -1)
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public virtual (Dictionary<string, Color>, Dictionary<string, List<double>>) GetGapCurrents()
        {
            Dictionary<string, Color> colors = new();
            Dictionary<string, List<double>> AffarentCurrents = new();
            foreach (GapJunction jnc in GapJunctions.Where(j => j.Cell2 == this))
            {
                colors.TryAdd(jnc.Cell1.ID, jnc.Cell1.CellPool.Color);
                AffarentCurrents.AddObject(jnc.Cell1.ID, jnc.InputCurrent.ToList());
            }
            foreach (GapJunction jnc in GapJunctions.Where(j => j.Cell1 == this))
            {
                colors.TryAdd(jnc.Cell2.ID, jnc.Cell2.CellPool.Color);
                AffarentCurrents.AddObject(jnc.Cell2.ID, jnc.InputCurrent.Select(d => -d).ToList());
            }
            return (colors, AffarentCurrents);
        }
        public virtual (Dictionary<string, Color>, Dictionary<string, List<double>>) GetIncomingSynapticCurrents()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;

        }
        public virtual (Dictionary<string, Color>, Dictionary<string, List<double>>) GetOutgoingSynapticCurrents()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        #endregion
    }
}