using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Junction
{
    public class InterPoolTemplate : InterPoolBase
    {
        #region Private fields
        private string _Name;
        private string poolSource, poolTarget;
        private string coreType;
        #endregion

        #region Properties
        [JsonIgnore]
        public override string ID => $"{GeneratedName()} [{ConnectionType}]/{AxonReachMode} {CellReach.Projection}";
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_Name))
                    _Name = GeneratedName();
                return _Name;
            }
            set { _Name = value; }
        }

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

        public string PoolSource
        {
            get { return poolSource; }
            set
            {
                bool rename = GeneratedName() == Name;
                poolSource = value;
                if (rename) Name = GeneratedName();
            }
        }
        public string PoolTarget
        {
            get { return poolTarget; }
            set
            {
                bool rename = GeneratedName() == Name;
                poolTarget = value;
                if (rename) Name = GeneratedName();
            }
        }
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

        [JsonPropertyOrder(1)]
        public SynapseParameters SynapseParameters { get; set; }

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
        public List<string> Attachments { get; set; } = new();

        [JsonIgnore]
        public override string Tooltip
        {
            get
            {
                return $"{Name}\r\n" +
                    $"{Description}\r\n" +
                    $"From {PoolSource} to {PoolTarget}\r\n" +
                    $"Weight:{Weight: 0.#####}\r\n" +
                    $"Reach: {CellReach?.GetTooltip()}\r\n" +
                    $"Fixed Duration:{FixedDuration_ms: 0.###}\r\n" +
                    $"Delay:{Delay_ms: 0.###}\r\n" +
                    $"Probability: {Probability}\r\n" +
                    $"Mode: {AxonReachMode}\r\n" +
                    $"Type: {ConnectionType}\r\n" +
                    $"Parameters: {SynapseParameters?.GetTooltip()}\r\n" +
                    $"TimeLine: {TimeLine_ms}\r\n" +
                    $"Active: {Active}";
            }
        }

        [JsonIgnore, Browsable(false)]
        public static new List<string> ColumnNames { get; } =
            ListBuilder.Build<string>("Name", "Source", "Target",
                "Axon Reach Mode", "Conn. Type", "Core Type" , "Dist. Mode",
                CellReach.ColumnNames,
                SynapseParameters.ColumnNames,
                "Prob.", "Weight", "Fixed Dur. (ms)", "Delay (ms)",
                "Active",
                TimeLine.ColumnNames);

        public override List<string> ExportValues()
        {
            return ListBuilder.Build<string>(
            Util.CSVEncode(Name), PoolSource, PoolTarget,
                AxonReachMode, ConnectionType, CoreType, DistanceMode,
                CellReach.ExportValues(),
                SynapseParameters?.ExportValues() ?? SynapseParameters.ExportBlankValues(),
                Probability, Weight, FixedDuration_ms, Delay_ms,
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
                PoolSource = values[iter++].Trim();
                PoolTarget = values[iter++].Trim();

                AxonReachMode = (AxonReachMode)Enum.Parse(typeof(AxonReachMode), values[iter++]);
                ConnectionType = (ConnectionType)Enum.Parse(typeof(ConnectionType), values[iter++]);
                CoreType = values[iter++].Trim();
                DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), values[iter++]);
                CellReach.Importvalues(values.Take(new Range(iter, iter + CellReach.ColumnNames.Count)).ToList());
                iter += CellReach.ColumnNames.Count;
                List<string> synapseString = values.Take(new Range(iter, iter + SynapseParameters.ColumnNames.Count)).ToList();
                if (synapseString.Any(s => !string.IsNullOrEmpty(s)))
                {
                    SynapseParameters = new();
                    SynapseParameters.ImportValues(synapseString);
                }
                iter += SynapseParameters.ColumnNames.Count;
                if (double.TryParse(values[iter++], out double p))
                    Probability = p;
                Weight = double.Parse(values[iter++]);
                if (double.TryParse(values[iter++], out double d))
                    FixedDuration_ms = d;
                if (double.TryParse(values[iter++], out double f))
                    Delay_ms = f;
                if (bool.TryParse(values[iter++], out bool b))
                    Active = b;
                if (iter < values.Count)
                    TimeLine_ms.ImportValues(new[] { values[iter++] }.ToList());
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        #endregion

        public InterPoolTemplate() : base()
        { }
        public InterPoolTemplate(InterPoolTemplate ipt) : base(ipt)
        {
            Name = ipt.Name;
            Description = ipt.Description;
            PoolSource = ipt.PoolSource;
            PoolTarget = ipt.PoolTarget;
            CellReach = new CellReach(ipt.CellReach);
            Probability = ipt.Probability;
            AxonReachMode = ipt.AxonReachMode;
            ConnectionType = ipt.ConnectionType;
            SynapseParameters = new SynapseParameters(ipt.SynapseParameters);
            Active = ipt.Active;
            TimeLine_ms = new TimeLine(ipt.TimeLine_ms);
        }
        public override string ToString()
        {
            string activeStatus = Active && TimeLine_ms.IsBlank() ? "" :
                Active ? " (timeline)" : " (inactive)";
            return $"{ID}{activeStatus}";
        }
        public override int CompareTo(ModelUnitBase otherbase)
        {
            InterPoolTemplate other = otherbase as InterPoolTemplate;
            int c = this.PoolSource.CompareTo(other.PoolSource);
            if (c != 0) return c;
            c = this.PoolTarget.CompareTo(other.PoolTarget);
            if (c != 0) return c;
            return this.ConnectionType.CompareTo(other.ConnectionType);
        }

        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            int errorCount = errors.Count;
            if (ConnectionType == ConnectionType.Synapse || ConnectionType == ConnectionType.NMJ)
                SynapseParameters.CheckValues(ref errors);
            if (errors.Count > errorCount)
                errors.Insert(errorCount, $"{ID}:");
            return errors.Count == 0;
        }
        public string GeneratedName()
        {
            return $"{(!string.IsNullOrEmpty(PoolSource) ? PoolSource : "__")}-->{(!string.IsNullOrEmpty(PoolTarget) ? PoolTarget : "__")}";
        }

        internal void BackwardCompatibility()
        {
            if ((ConnectionType == ConnectionType.Synapse || ConnectionType == ConnectionType.NMJ) && string.IsNullOrEmpty(coreType))
                coreType = typeof(SimpleSyn).Name;
        }
    }

}
