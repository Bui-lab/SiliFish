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

        public StimulusBase() { }
        public virtual StimulusBase CreateCopy() { throw new NotImplementedException(); }

    }
}
