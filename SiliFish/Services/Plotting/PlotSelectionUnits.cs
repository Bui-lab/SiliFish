using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.Services.Plotting
{

    public class PlotSelectionUnits : PlotSelectionInterface
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
        public override string ToString()
        {
            if (Units == null || !Units.Any()) return "";
            return string.Join("; ", Units.Select(unit => unit.ID));
        }

        public void AddUnit(ModelUnitBase unit)
        {
            Units ??= new();
            Units.Add(unit);
        }
    }
}
