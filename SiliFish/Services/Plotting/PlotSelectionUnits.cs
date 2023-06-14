using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Services.Plotting
{

    public class PlotSelectionUnits : PlotSelectionInterface
    {
        public List<ModelUnitBase> Units { get; set; }
        public PlotSelectionUnits()
        {
        }
        public override string ToString()
        {
            return string.Join("; ", Units.Select(jnc => jnc.ToString()));
        }
    }
}
