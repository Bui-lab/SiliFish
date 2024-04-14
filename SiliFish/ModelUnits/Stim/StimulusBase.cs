using SiliFish.DataTypes;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.ModelUnits.Stim
{
    public class StimulusBase: ModelUnitBase
    {
        public StimulusSettings Settings { get; set; } = new();

        public StimulusBase() {
            PlotType = "Stimulus";
        }

        public override List<Difference> DiffersFrom(ModelUnitBase other)
        {
            List<Difference> diffs = [];
            StimulusBase st = other as StimulusBase;
            if (Settings.ToString() != st.Settings.ToString())
                diffs.Add(new Difference(ID, "Settings", Settings, st.Settings));
            if (Active != st.Active)
                diffs.Add(new Difference(ID, "Active", Active, st.Active));
            if (TimeLine_ms.ToString() != st.TimeLine_ms.ToString())
                diffs.Add(new Difference(ID, "TimeLine", TimeLine_ms, st.TimeLine_ms));

            if (diffs.Count != 0)
                return diffs;
            return null;
        }

    }
}
