using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Junction
{
    public class InterPoolTemplate : InterPoolBase
    {
        #region Private fields
        private string sourcePool, targetPool;
        private string coreType;
        private Dictionary<string, object> parameters;
        
        #endregion

        #region Properties
        [JsonIgnore]
        public override string ID => $"{Name} [{ConnectionType}]/{AxonReachMode} {CellReach.Projection}";
        public string Description { get; set; }
        public double Probability { get; set; } = 1;
        public bool JncActive //to be used in creating the model
        {
            get
            {
                if (linkedSource != null && !linkedSource.Active ||
                    linkedTarget != null && !linkedTarget.Active)
                    return false;
                return base.Active; }
            set { base.Active = value; }
        }

        public override string SourcePool
        {
            get { return sourcePool; }
            set
            {
                bool rename = GeneratedName() == Name;
                sourcePool = value;
                if (rename) Name = GeneratedName();
            }
        }
        public override string TargetPool
        {
            get { return targetPool; }
            set
            {
                bool rename = GeneratedName() == Name;
                targetPool = value;
                if (rename) Name = GeneratedName();
            }
        }

        [JsonIgnore]
        public override double Duration_ms => double.NaN;
        public string CoreType
        {
            get => coreType;
            set
            {
                if (value.Trim() != value)
                { }
                coreType = value;
            }
        }
        [JsonPropertyOrder(2)]
        public CellReach CellReach { get; set; } = new();


        [JsonIgnore]
        public CellPoolTemplate linkedSource, linkedTarget;
        public AxonReachMode AxonReachMode { get; set; } = AxonReachMode.NotSet;
        public ConnectionType ConnectionType { get; set; } = ConnectionType.NotSet;

        public override bool Active
        {
            get
            {
                return base.Active;
            }
            set { base.Active = value; }
        }

        [JsonPropertyOrder(3)]
        public List<string> Attachments { get; set; } = [];

        [JsonIgnore]
        public override string Tooltip
        {
            get
            {
                return $"{Name}\r\n" +
                    $"{Description}\r\n" +
                    $"From {SourcePool} to {TargetPool}\r\n" +
                    $"Core:{CoreType}\r\n" +
                    $"\t{string.Join(',', Parameters.Select((k, v) => k.Key + ": " + k.Value))}\r\n" +
                    $"Reach: {CellReach?.GetTooltip()}\r\n" +
                    $"Fixed Duration:{FixedDuration_ms: 0.###}\r\n" +
                    $"Delay:{Delay_ms: 0.###}\r\n" +
                    $"Probability: {Probability}\r\n" +
                    $"Mode: {AxonReachMode}\r\n" +
                    $"Type: {ConnectionType}\r\n" +
                    $"TimeLine: {TimeLine_ms}\r\n" +
                    $"Active: {Active}";
            }
        }

        [JsonIgnore, Browsable(false)]
        public static new List<string> ColumnNames { get; } =
            ListBuilder.Build<string>("Name", "Source", "Target",
                "Axon Reach Mode", "Conn. Type", "Core Type" ,
                Enumerable.Range(1, JunctionCore.CoreParamMaxCount).SelectMany(i => new[] { $"Param{i}", $"Value{i}" }),
                "Dist. Mode",
                CellReach.ColumnNames,
                "Prob.", "Fixed Dur. (ms)", "Delay (ms)",
                "Active",
                TimeLine.ColumnNames);

        public override List<string> ExportValues()
        {
            return ListBuilder.Build<string>(
            CSVUtil.CSVEncode(Name), SourcePool, TargetPool,
                AxonReachMode, ConnectionType, CoreType,
                csvExportCoreValues,
                DistanceMode,
                CellReach.ExportValues(),
                Probability, FixedDuration_ms, Delay_ms,
                Active,
                TimeLine_ms?.ExportValues());
        }
        public override void ImportValues(List<string> values)
        {
            try
            {
                int iter = 0;
                if (values.Count < ColumnNames.Count - TimeLine.ColumnNames.Count) return;
                Name = values[iter++].Trim();
                SourcePool = values[iter++].Trim();
                TargetPool = values[iter++].Trim();

                AxonReachMode = (AxonReachMode)Enum.Parse(typeof(AxonReachMode), values[iter++]);
                ConnectionType = (ConnectionType)Enum.Parse(typeof(ConnectionType), values[iter++]);
                CoreType = values[iter++].Trim();
                parameters = [];
                for (int i = 1; i <= JunctionCore.CoreParamMaxCount; i++)
                {
                    if (iter > values.Count - 2) break;
                    string paramkey = values[iter++]?.Trim() ?? "";
                    string paramvalue = values[iter++];
                    if (!string.IsNullOrEmpty(paramkey))
                    {
                        parameters.Add(paramkey, Distribution.CreateDistributionObjectFromCSVCell(paramvalue));
                    }
                }
                DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), values[iter++]);
                CellReach.Importvalues(values.Take(new Range(iter, iter + CellReach.ColumnNames.Count)).ToList());
                iter += CellReach.ColumnNames.Count;
                if (double.TryParse(values[iter++], out double p))
                    Probability = p;
                if (double.TryParse(values[iter++], out double d))
                    FixedDuration_ms = d;
                if (double.TryParse(values[iter++], out double f))
                    Delay_ms = f;
                if (bool.TryParse(values[iter++], out bool b))
                    Active = b;
                if (iter < values.Count)
                    TimeLine_ms.ImportValues([values[iter++]]);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        #endregion

        [JsonPropertyOrder(1)]
        public Dictionary<string, Distribution> Parameters
        {
            get
            {
                return parameters?.ToDictionary(kvp => kvp.Key,
                    kvp => Distribution.CreateDistributionObject(kvp.Value));
            }
            set
            {
                parameters = value?.ToDictionary(kvp => kvp.Key,
                    kvp => Distribution.CreateDistributionObject(kvp.Value) as object);
            }
        }
        public void SetParameter(string key, Distribution value)
        {
            if (parameters != null)
                parameters[key] = value;
        }

        [JsonIgnore, Browsable(false)]
        private List<string> csvExportCoreValues
        {
            get
            {
                List<string> paramValues = Parameters.Take(JunctionCore.CoreParamMaxCount).OrderBy(kv => kv.Key).SelectMany(kv => new[] { kv.Key, kv.Value.CSVCellExportValues }).ToList();
                for (int i = parameters.Count; i < JunctionCore.CoreParamMaxCount; i++)
                {
                    paramValues.Add(string.Empty);
                    paramValues.Add(string.Empty);
                }
                return paramValues;
            }
        }
        public InterPoolTemplate() : base()
        { }
        public InterPoolTemplate(InterPoolTemplate ipt) : base(ipt)
        {
            Name = ipt.Name;
            Description = ipt.Description;
            SourcePool = ipt.SourcePool;
            TargetPool = ipt.TargetPool;
            CellReach = new CellReach(ipt.CellReach);
            Probability = ipt.Probability;
            AxonReachMode = ipt.AxonReachMode;
            ConnectionType = ipt.ConnectionType;
            CoreType = ipt.CoreType;
            Parameters = new Dictionary<string, Distribution>(ipt.Parameters);
            Active = ipt.Active;
            TimeLine_ms = new TimeLine(ipt.TimeLine_ms);
        }
        public override string ToString()
        {
            string activeStatus = Active && TimeLine_ms.IsBlank() ? "" :
                Active ? " (timeline)" : " (inactive)";
            string arrows = "";
            if (AxonReachMode == AxonReachMode.Ipsilateral || AxonReachMode == AxonReachMode.Bilateral)
            {
                if (CellReach.Ascending)
                    arrows += "⬆";
                if (CellReach.Descending)
                    arrows += "⬇";
            }
            if (AxonReachMode == AxonReachMode.Contralateral || AxonReachMode == AxonReachMode.Bilateral)
            {
                if (CellReach.Ascending)
                    arrows += "⬈";
                if (CellReach.Descending)
                    arrows += "⬊";
            }
            bool electrical = ConnectionType == ConnectionType.Gap;
            string ntmode = electrical ? "🗲" :
                (linkedSource?.CellOutputMode == CellOutputMode.Cholinergic ? "C" :
                    linkedSource?.CellOutputMode == CellOutputMode.Excitatory ? "+" :
                    linkedSource?.CellOutputMode == CellOutputMode.Inhibitory ? "-" :
                    linkedSource?.CellOutputMode == CellOutputMode.Modulatory ? "M" : "?");
            return $"({ntmode}) {Name}{activeStatus}{arrows}";
        }

        public override List<Difference> DiffersFrom(ModelUnitBase other)
        {
            List<Difference> differences = [];
            InterPoolTemplate ipt = other as InterPoolTemplate;
            if (Name != ipt.Name)
                differences.Add(new Difference(Name, "Name", Name, ipt.Name));
            if ((Description ?? "") != (ipt.Description ?? ""))
                differences.Add(new Difference(Name, "Description", Description, ipt.Description));
            if (SourcePool != ipt.SourcePool)
                differences.Add(new Difference(Name, "SourcePool", SourcePool, ipt.SourcePool));
            if (TargetPool != ipt.TargetPool)
                differences.Add(new Difference(Name, "TargetPool", TargetPool, ipt.TargetPool));
            if (CellReach.GetTooltip().ToString() != ipt.CellReach.GetTooltip().ToString())
                differences.Add(new Difference(Name, "Cell reach", CellReach.GetTooltip(), ipt.CellReach.GetTooltip()));//TODO implement ToString and use that rather than GetToolTip
            if (Probability != ipt.Probability)
                differences.Add(new Difference(Name, "Probability", Probability, ipt.Probability));
            if (AxonReachMode != ipt.AxonReachMode)
                differences.Add(new Difference(Name, "AxonReachMode", AxonReachMode, ipt.AxonReachMode));
            if (ConnectionType != ipt.ConnectionType)
                differences.Add(new Difference(Name, "ConnectionType", ConnectionType, ipt.ConnectionType));
            if (CoreType != ipt.CoreType)
                differences.Add(new Difference(Name, "CoreType", CoreType, ipt.CoreType));
            if (!Parameters.SameAs(ipt.Parameters, out List<Difference> diff))
                differences.AddRange(diff.Select(d => new Difference(Name + " Parameters", d.Item + d.Parameter, d.Value1, d.Value2)));
            if (Active != ipt.Active)
                differences.Add(new Difference(Name, "Active", Active, ipt.Active));
            if (TimeLine_ms.ToString() !=  ipt.TimeLine_ms.ToString())
                differences.Add(new Difference(Name, "TimeLine", TimeLine_ms, ipt.TimeLine_ms));
            if (differences.Count != 0)
                return differences;
            return null;
        }
        public override int CompareTo(ModelUnitBase otherbase)
        {
            InterPoolTemplate other = otherbase as InterPoolTemplate;
            int c = this.SourcePool.CompareTo(other.SourcePool);
            if (c != 0) return c;
            c = this.TargetPool.CompareTo(other.TargetPool);
            if (c != 0) return c;
            return this.ConnectionType.CompareTo(other.ConnectionType);
        }

        public override bool CheckValues(ref List<string> errors)
        {
            int preCount = errors.Count;
            base.CheckValues(ref errors);
            if ((ConnectionType == ConnectionType.Synapse || ConnectionType == ConnectionType.NMJ)
                && !ChemSynapseCore.CheckValues(ref errors, CoreType, Parameters.GenerateSingleInstanceValues()))
                errors.Insert(preCount, $"{ID}:");
            return errors.Count != preCount;
        }
        public override string GeneratedName()
        {
            return $"{(!string.IsNullOrEmpty(SourcePool) ? SourcePool : "__")}-->{(!string.IsNullOrEmpty(TargetPool) ? TargetPool : "__")}";
        }

        internal void BackwardCompatibility()
        {
            if (string.IsNullOrEmpty(coreType))
            {
                if (ConnectionType == ConnectionType.Gap)
                    coreType = nameof(SimpleGap); 
                else
                    coreType = nameof(SimpleSyn);
            }
        }
    }

}
