﻿using Microsoft.VisualBasic;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Stim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Cells
{
    public class CellPoolTemplate: ModelUnitBase 
    {
        private static int csvExportParamCount = 10;
        public string CellGroup { get; set; }
        public CellType CellType { get; set; }
        public string Description { get; set; }
        public string CoreType { get; set; }

        public BodyLocation BodyLocation { get; set; }
        public Color Color { get; set; } = Color.Red;

        public SagittalPlane PositionLeftRight { get; set; } = SagittalPlane.Both;

        public NeuronClass NTMode { get; set; }//relevant only if CellType==Neuron

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
        public override string ID { get { return Position + "_" + CellGroup; }  }

        public List<string> Attachments { get; set; } = new();
        [JsonIgnore, Browsable(false)]
        public static string CSVExportColumnNames => $"CellGroup, CellType, CoreType, " +
            $"BodyLocation, PositionLeftRight, NumOfCells, PerSomiteOrTotal, SomiteRange, " +
            $"Conduction Velocity, " +
            $"{SpatialDistribution.CSVExportColumnNames}," +
            $"{TimeLine.CSVExportColumnNames}, " +
            $"{string.Join(',', Enumerable.Range(1,csvExportParamCount).Select(i=>$"Param{i},Value{i}"))} ";

        [JsonIgnore, Browsable(false)]
        private static int CSVExportColumCount => CSVExportColumnNames.Split(',').Length;
        [JsonIgnore, Browsable(false)]
        public virtual string CSVExportValues
        {
            get => $"{CellGroup}, {CellType}, {CoreType}, " +
                $"{BodyLocation}, {PositionLeftRight}, {NumOfCells}, {PerSomiteOrTotal}, {SomiteRange}, " +
                $"{ConductionVelocity?.UniqueValue}, " +
                $"{SpatialDistribution.CSVExportValues}," +
                $"{TimeLine_ms.CSVExportValues}," +
                $"{string.Join(",", Parameters.Select(kv => $"{kv.Key},{kv.Value.UniqueValue}"))}";
            set
            {
                string[] values = value.Split(',');
                if (values.Length < CSVExportColumCount - csvExportParamCount) return;
                CellGroup = values[0];
                CellType = (CellType)Enum.Parse(typeof(CellType), values[1]); 
                CoreType = values[2];
                BodyLocation = (BodyLocation)Enum.Parse(typeof(BodyLocation), values[3]);
                PositionLeftRight = (SagittalPlane)Enum.Parse(typeof(SagittalPlane), values[4]);
                NumOfCells = int.Parse(values[5]);
                PerSomiteOrTotal = (CountingMode)Enum.Parse(typeof(CountingMode), values[6]);
                SomiteRange = values[7];
                //TODO
        }
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
            ConductionVelocity = cpl.ConductionVelocity?.Clone();
            TimeLine_ms = new TimeLine(cpl.TimeLine_ms);
        }
        public void GenerateFromCSVRow(ModelTemplate Model, string row)
        {
            string[] values = row.Split(',');
            if (values.Length != CSVExportColumCount) return;
            /*TODO TargetCell = Model.GetCell(values[1]);
            Settings.CSVExportValues = string.Join(",", values[2..(StimulusSettings.CSVExportColumCount + 1)]);
            TimeLine_ms.CSVExportValues = string.Join(",", values[(StimulusSettings.CSVExportColumCount + 2)..]);
            TargetCell.AddStimulus(this);*/
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
            CellCoreUnit.CheckValues(ref errors, CoreType, Parameters.GenerateSingleInstanceValues());
            if (errors.Count > errorCount)
                errors.Insert(errorCount, $"{ID}:");
            return errors.Count == 0;
        }
        public void BackwardCompatibility()
        {
            var currentParams = CellCoreUnit.GetParameters(CoreType);
            while (parameters.Keys.FirstOrDefault(k => k.StartsWith($"{CoreType}."))!=null)
            {
                string key = parameters.Keys.FirstOrDefault(k => k.StartsWith($"{CoreType}."));
                object value = parameters[key];
                parameters.Remove(key);
                parameters.Add(key.Replace($"{CoreType}.",""), value);
            }
            foreach(var key in currentParams.Keys ) 
            {
                if (!parameters.ContainsKey(key))
                    parameters.Add(key, currentParams[key]);
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
