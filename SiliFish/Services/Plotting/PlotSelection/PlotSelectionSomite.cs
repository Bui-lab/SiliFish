using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Services.Plotting.PlotSelection
{
    public class PlotSelectionSomite : PlotSelectionInterface
    {
        public int Somite { get; set; } = -1;
        public PlotSelectionSomite()
        {
        }
        public override string ToString()
        {
            return $"{Somite}";
        }
        public override bool Equals(object obj)
        {
            if (obj is not PlotSelectionSomite pss)
            {
                return false;
            }
            return Somite == pss.Somite;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
