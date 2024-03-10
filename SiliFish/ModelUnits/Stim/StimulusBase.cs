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

        public override List<string> DiffersFrom(ModelUnitBase other)
        {
            List<string> diffs = [];
            StimulusBase st = other as StimulusBase;
            if (Settings.ToString() != st.Settings.ToString())
                diffs.Add($"{ID} Settings: {Settings} vs {st.Settings}");
            if (Active != st.Active)
                diffs.Add($"{ID} Active: {Active} vs {st.Active}");
            if (TimeLine_ms.ToString() != st.TimeLine_ms.ToString())
                diffs.Add($"{ID} TimeLine: {TimeLine_ms} vs {st.TimeLine_ms}");

            if (diffs.Count != 0)
                return diffs;
            return null;
        }

    }
}
