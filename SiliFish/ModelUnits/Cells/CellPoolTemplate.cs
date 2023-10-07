using Microsoft.VisualBasic;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.DynamicUnits.JncCore;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Stim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Cells
{
    public class CellPoolTemplate : ModelUnitBase, IDataExporterImporter
    {
        private string coreType;
        public string CellGroup { get; set; }
        public CellType CellType { get; set; }
        public string Description { get; set; }
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
        public double Rheobase { get => GetSampleRheobase(); set { } }


        public BodyLocation BodyLocation { get; set; }
        public Color Color { get; set; } = Color.Red;

        public SagittalPlane PositionLeftRight { get; set; } = SagittalPlane.Both;

        public NeuronClass NTMode { get; set; }//relevant only if CellType==Neuron

        [JsonIgnore]
        public CellOutputMode CellOutputMode
        {
            get
            {
                switch (NTMode)
                {
                    case NeuronClass.NotSet:
                        return CellOutputMode.NotSet;
                    case NeuronClass.Glycinergic:
                    case NeuronClass.GABAergic:
                        return CellOutputMode.Inhibitory;
                    case NeuronClass.Glutamatergic:
                        return CellOutputMode.Excitatory;
                    case NeuronClass.Cholinergic:
                        return CellOutputMode.Cholinergic;
                    case NeuronClass.Modulatory:
                        return CellOutputMode.Modulatory;
                    case NeuronClass.Mixed:
                        return CellOutputMode.NotSet;
                };
                return CellOutputMode.NotSet;
            }
        }

        private Dictionary<string, object> parameters;
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
        public int NumOfCells { get; set; } = 1;
        public CountingMode PerSomiteOrTotal { get; set; } = CountingMode.Total;

        public string SomiteRange { get; set; }

        public Distribution ConductionVelocity { get; set; }
        public Distribution AscendingAxonLength { get; set; }
        public Distribution DescendingAxonLength { get; set; }
        public override string ToString()
        {
            return CellGroup + (Active ? "" : " (inactive)");
        }
        [JsonIgnore]
        public override string Tooltip
        {
            get
            {
                string ntmode = CellType == CellType.Neuron && NTMode != NeuronClass.NotSet ?
                    $"Neurotransmitter: {NTMode}\r\n" : "";
                string persomite = PerSomiteOrTotal == CountingMode.PerSomite ? "//somite" : "";
                return $"{CellGroup}\r\n" +
                    $"{Description}\r\n" +
                    $"{ntmode}" +
                    $"Position: {Position}\r\n" +
                    $"# of cells: {NumOfCells}{persomite}\r\n" +
                    $"Spatial Distribution:\r\n{SpatialDistribution.GetTooltip()}\r\n" +
                    $"Rheobase: {Rheobase}\r\n" +
                    $"TimeLine: {TimeLine_ms}\r\n" +
                    $"Active: {Active}";
            }
        }
        [JsonIgnore]
        public string Position
        {
            get
            {
                string FTS =
                    //FUTURE_IMPROVEMENT
                    //(PositionDorsalVentral == FrontalPlane.Ventral ? "V" : PositionDorsalVentral == FrontalPlane.Dorsal ? "D" : "") +
                    //(PositionAnteriorPosterior == TransversePlane.Posterior ? "P" : PositionAnteriorPosterior == TransversePlane.Anterior ? "A" : PositionAnteriorPosterior == TransversePlane.Central ? "C" : "") +
                    (PositionLeftRight == SagittalPlane.Left ? "L" : PositionLeftRight == SagittalPlane.Right ? "R" : "LR");
                return FTS;
            }

        }
        public SpatialDistribution SpatialDistribution = new();

        public Distribution XDistribution
        {
            get { return SpatialDistribution.XDistribution; }
            set { SpatialDistribution.XDistribution = value; }
        }
        public Distribution Y_AngleDistribution
        {
            get { return SpatialDistribution.Y_AngleDistribution; }
            set { SpatialDistribution.Y_AngleDistribution = value; }
        }
        public Distribution Z_RadiusDistribution
        {
            get { return SpatialDistribution.Z_RadiusDistribution; }
            set { SpatialDistribution.Z_RadiusDistribution = value; }
        }

        public int ColumnIndex2D { get; set; } = 1; //the multiplier to differentiate the positions of different cellpools while plotting 2D rendering

        [JsonIgnore]
        public override string ID { get { return Position + "_" + CellGroup; } }

        public List<string> Attachments { get; set; } = new();
        [JsonIgnore, Browsable(false)]
        public static List<string> ColumnNames { get; } =
            ListBuilder.Build<string>("CellGroup", "CellType", "NTMode",
                "BodyLocation", "PositionLeftRight", "NumOfCells", "PerSomiteOrTotal", "SomiteRange","2DRendSpread",
                SpatialDistribution.ColumnNames, 
                "Descending Axon", "Ascending Axon",
                "Conduction Velocity", "CoreType", "Rheobase (output only/sample)",
                Enumerable.Range(1, CellCore.CoreParamMaxCount).SelectMany(i => new[] { $"Param{i}", $"Value{i}" }),
                "Active", "Color", TimeLine.ColumnNames);

        [JsonIgnore, Browsable(false)]
        private List<string> csvExportParamValues
        {
            get
            {
                List<string> paramValues = Parameters.Take(CellCore.CoreParamMaxCount).OrderBy(kv => kv.Key).SelectMany(kv => new[] { kv.Key, kv.Value.CSVCellExportValues }).ToList();
                for (int i = parameters.Count; i < CellCore.CoreParamMaxCount; i++)
                {
                    paramValues.Add(string.Empty);
                    paramValues.Add(string.Empty);
                }
                return paramValues;
            }
        }

        public List<string> ExportValues()
        {
            return ListBuilder.Build<string>(CellGroup, CellType, NTMode,
                BodyLocation, PositionLeftRight, NumOfCells, PerSomiteOrTotal, SomiteRange, ColumnIndex2D,
                SpatialDistribution.ExportValues(),
                DescendingAxonLength?.CSVCellExportValues ?? string.Empty,
                AscendingAxonLength?.CSVCellExportValues ?? string.Empty,
                ConductionVelocity?.CSVCellExportValues ?? string.Empty,
                CoreType, Rheobase, csvExportParamValues,
                Active, Color.ToHex(), TimeLine_ms.ExportValues());
        }
        public void ImportValues(List<string> values)
        {
            int iter = 0;
            if (values.Count < ColumnNames.Count - CellCore.CoreParamMaxCount) return;
            CellGroup = values[iter++];
            CellType = (CellType)Enum.Parse(typeof(CellType), values[iter++]);
            NTMode = (NeuronClass)Enum.Parse(typeof(NeuronClass), values[iter++]);
            BodyLocation = (BodyLocation)Enum.Parse(typeof(BodyLocation), values[iter++]);
            PositionLeftRight = (SagittalPlane)Enum.Parse(typeof(SagittalPlane), values[iter++]);
            NumOfCells = int.Parse(values[iter++]);
            PerSomiteOrTotal = (CountingMode)Enum.Parse(typeof(CountingMode), values[iter++]);
            SomiteRange = values[iter++];
            ColumnIndex2D = int.Parse(values[iter++]);
            List<string> spatDistValues = values.Take(new Range(iter, iter + SpatialDistribution.ColumnNames.Count)).ToList();
            SpatialDistribution.ImportValues(spatDistValues);
            iter += SpatialDistribution.ColumnNames.Count;
            DescendingAxonLength = Distribution.CreateDistributionObjectFromCSVCell(values[iter++]);
            AscendingAxonLength = Distribution.CreateDistributionObjectFromCSVCell(values[iter++]);
            ConductionVelocity = Distribution.CreateDistributionObjectFromCSVCell(values[iter++]);
            CoreType = values[iter++].Trim();
            iter++;//skip rheobase
            parameters = new();
            for (int i = 1; i <= CellCore.CoreParamMaxCount; i++)
            {
                if (iter > values.Count - 2) break;
                string paramkey = values[iter++].Trim();
                string paramvalue = values[iter++];
                if (!string.IsNullOrEmpty(paramkey))
                {
                    parameters.Add(paramkey, Distribution.CreateDistributionObjectFromCSVCell(paramvalue));
                }
            }
            Active = bool.Parse(values[iter++]);
            ColorConverter cc = new();
            Color = (Color)cc.ConvertFromString(values[iter++]);
            if (iter < values.Count)
                TimeLine_ms.ImportValues(new[] { values[iter++] }.ToList());
        }
    

        public CellPoolTemplate() { }
        public CellPoolTemplate(CellPoolTemplate cpl)
        {
            CellGroup = cpl.CellGroup;
            Description = cpl.Description;
            CellType = cpl.CellType;
            CoreType = cpl.CoreType;
            NTMode = cpl.NTMode;
            Color = cpl.Color;
            Parameters = new Dictionary<string, Distribution>(cpl.Parameters);
            BodyLocation = cpl.BodyLocation;
            PositionLeftRight = cpl.PositionLeftRight;
            ColumnIndex2D = cpl.ColumnIndex2D;
            NumOfCells = cpl.NumOfCells;
            PerSomiteOrTotal = cpl.PerSomiteOrTotal;
            SomiteRange = cpl.SomiteRange;
            SpatialDistribution = cpl.SpatialDistribution.Clone();
            DescendingAxonLength = cpl.DescendingAxonLength?.Clone();
            AscendingAxonLength = cpl.AscendingAxonLength?.Clone();
            ConductionVelocity = cpl.ConductionVelocity?.Clone();
            TimeLine_ms = new TimeLine(cpl.TimeLine_ms);
        }

        private double GetSampleRheobase()
        {
            if (Parameters==null) return 0.0; 
            Dictionary<string, double> dparams = Parameters.ToDictionary(kvp => kvp.Key, kvp => kvp.Value is Distribution dist ? dist.UniqueValue : double.Parse(kvp.Value.ToString()));
            CellCore core = CellCore.CreateCore(CoreType, dparams);
            return core?.CalculateRheoBase(maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity_ms: GlobalSettings.RheobaseInfinity, dt: 0.1) ?? 0;
           
        }

        public override int CompareTo(ModelUnitBase otherbase)
        {
            CellPoolTemplate other = otherbase as CellPoolTemplate;
            return CellGroup.CompareTo(other.CellGroup);
        }

        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            int errorCount = errors.Count;
            CellCore.CheckValues(ref errors, CoreType, Parameters.GenerateSingleInstanceValues());
            if (errors.Count > errorCount)
                errors.Insert(errorCount, $"{ID}:");
            return errors.Count == 0;
        }
        public void BackwardCompatibility()
        {
            var currentParams = CellCore.GetParameters(CoreType);
            while (parameters.Keys.FirstOrDefault(k => k.StartsWith($"{CoreType}.")) != null)
            {
                string key = parameters.Keys.FirstOrDefault(k => k.StartsWith($"{CoreType}."));
                object value = parameters[key];
                parameters.Remove(key);
                parameters.Add(key.Replace($"{CoreType}.", ""), value);
            }
            foreach (var key in currentParams.Keys)
            {
                if (!parameters.ContainsKey(key))
                    parameters.Add(key, currentParams[key]);
            }
        }
        public void BackwardCompatibilityAxonLength(ModelTemplate model)
        {
            if (AscendingAxonLength == null)
            {
                List<InterPoolTemplate> ascIPT = model.InterPoolTemplates
                    .Where(ipt => ipt.SourcePool == CellGroup && ipt.CellReach.Ascending).ToList();
                double ascending = ascIPT!=null &&  ascIPT.Any() ? ascIPT.Max(ipt => ipt.CellReach.MaxAscReach) : double.NaN;
                if (!double.IsNaN(ascending))
                    AscendingAxonLength = new Constant_NoDistribution(ascending);
            }
            if (DescendingAxonLength == null)
            {
                List<InterPoolTemplate> descIPT = model.InterPoolTemplates
                    .Where(ipt => ipt.SourcePool == CellGroup && ipt.CellReach.Descending).ToList();
                double descending = descIPT != null && descIPT.Any() ? descIPT.Max(ipt => ipt.CellReach.MaxDescReach) : double.NaN;
                if (!double.IsNaN(descending))
                    DescendingAxonLength = new Constant_NoDistribution(descending);
            }

        }
        public virtual CellPoolTemplate CreateTemplateCopy()
        {
            return new CellPoolTemplate(this);
        }

        public Coordinate[] GenerateCoordinates(Random random, ModelDimensions modelDimensions, int n, int somite = -1)
        {
            return Coordinate.GenerateCoordinates(random, modelDimensions, BodyLocation, XDistribution, Y_AngleDistribution, Z_RadiusDistribution, n, somite);
        }


    }
}
