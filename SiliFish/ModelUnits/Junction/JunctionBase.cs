using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiliFish.ModelUnits.Junction
{
    public class JunctionBase : ModelUnitBase
    {
        public DistanceMode DistanceMode { get; set; } = DistanceMode.Euclidean;
        public double? FixedDuration_ms { get; set; } = null;// in ms
        public double Delay_ms { get; set; } = 0;//in ms
        public double Weight { get; set; }

        /// <summary>
        /// used for JSON
        /// </summary>
        public string Target{ get; set; }

        public JunctionBase()
        { }
        public JunctionBase(JunctionBase jnc)
        {
            Target = jnc.Target;
            DistanceMode = jnc.DistanceMode;
            FixedDuration_ms = jnc.FixedDuration_ms;
            Delay_ms = jnc.Delay_ms;
            Weight = jnc.Weight;
        }

        public virtual void LinkObjects()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual void UnlinkObjects()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }


    }
}
