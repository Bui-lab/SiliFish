using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Junction
{
    public class InterPoolTemplate : JunctionBase
    {
        #region Private fields
        private string _Name;
        private string poolSource, poolTarget;
        #endregion

        #region Properties
        [JsonIgnore]
        public override string ID => $"{GeneratedName()} [{ConnectionType}]/{AxonReachMode}";
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
        public bool JncActive //does not check the active flags of the cell pools
        {
            get { return base.Active; }
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
                if (linkedSource != null && !linkedSource.Active ||
                    linkedTarget != null && !linkedTarget.Active)
                    return false;
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
                    $"Weight:{Weight: 0.###}\r\n" +
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

        #endregion

        public InterPoolTemplate()
        { }
        public InterPoolTemplate(InterPoolTemplate ipt)
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
            string activeStatus = JncActive && TimeLine_ms.IsBlank() ? "" :
                JncActive ? " (timeline)" : " (inactive)";
            return $"{Name} [{ConnectionType}]/{AxonReachMode}{activeStatus}";
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
        public string GeneratedName()
        {
            return $"{(!string.IsNullOrEmpty(PoolSource) ? PoolSource : "__")}-->{(!string.IsNullOrEmpty(PoolTarget) ? PoolTarget : "__")}";
        }
    }

}
