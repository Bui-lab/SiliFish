using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiliFish.ModelUnits.Junction
{
    public class JunctionBase : ModelUnitBase
    {
        protected string target;//used to temporarily hold the target cell's id while reading the JSON files
        public DistanceMode DistanceMode { get; set; } = DistanceMode.Euclidean;
        public double? FixedDuration_ms { get; set; } = null;// in ms
        public double Delay_ms { get; set; } = 0;//in ms
        public double Weight { get; set; }
        public virtual string Target
        {
            get => target;
            set => target = value;
        }
        public JunctionBase()
        { }
        public JunctionBase(JunctionBase jnc)
        {
            target = jnc.Target;
            DistanceMode = jnc.DistanceMode;
            FixedDuration_ms = jnc.FixedDuration_ms;
            Delay_ms = jnc.Delay_ms;
            Weight = jnc.Weight;
        }
    }
}
