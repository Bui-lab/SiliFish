using SiliFish.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting
{
    public class Plot
    {
        public string PlotSubset { get; set; }
        public PlotType PlotType { get; set; }
        public PlotSelectionInterface Selection { get; set; }
        public Plot() 
        {
        }

        public override string ToString()
        {
            return PlotType == PlotType.Selection ? $"{PlotType}:{Selection}" :
                $"{PlotType}-{PlotSubset}:{Selection}";
        }
    }
}
