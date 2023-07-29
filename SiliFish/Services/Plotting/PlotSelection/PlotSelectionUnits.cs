using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.Services.Plotting.PlotSelection
{

    public class PlotSelectionUnits : PlotSelectionMultiCells
    {
        private List<Tuple<string, string>> unitTags;

        [JsonIgnore]
        public List<ModelUnitBase> Units { get; set; }

        public List<Tuple<string, string>> UnitTags
        {
            get
            {
                if (unitTags != null) return unitTags;
                unitTags = new List<Tuple<string, string>>();
                if (Units == null || !Units.Any()) return unitTags;
                foreach (ModelUnitBase unit in Units)
                {
                    unitTags.Add(Tuple.Create(unit.GetType().Name, unit.ID));
                }
                return unitTags;
            }
            set
            {
                unitTags = value;
            }
        }
        public PlotSelectionUnits()
        {
        }

        public PlotSelectionUnits(PlotSelectionInterface copyFrom) :
            base(copyFrom)
        {
        }

        public override int GetHashCode() => base.GetHashCode();
        public override string ToString()
        {
            if (Units == null || !Units.Any()) return "";
            return string.Join("; ", Units.Select(unit => unit.ID));
        }
        public override bool Equals(object obj)
        {
            if (obj is not PlotSelectionUnits psu)
                return false;
            List<Tuple<string, string>> tags1 = UnitTags;
            List<Tuple<string, string>> tags2 = psu.UnitTags;
            if (tags1.Count != tags2.Count) return false;
            tags1.Sort();
            tags2.Sort();
            for (int i = 0; i < tags1.Count; i++)
            {
                if (!tags1[i].Equals(tags2[i]))
                    return false;
            }
            return true;           
        }
        public void AddUnit(ModelUnitBase unit)
        {
            Units ??= new();
            Units.Add(unit);
        }
    }
}
