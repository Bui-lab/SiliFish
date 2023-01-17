using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.ModelUnits.Stim
{
    public class StimulusBase: ModelUnitBase
    {
        public StimulusSettings StimulusSettings { get; set; }
        public double Value1
        {
            get { return StimulusSettings.Value1; }
            set { StimulusSettings.Value1 = value; }
        }
        public double Value2
        {
            get { return StimulusSettings.Value2; }
            set { StimulusSettings.Value2 = value; }
        }
    }
}
