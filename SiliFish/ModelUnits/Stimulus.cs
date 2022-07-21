using System;
using System.Linq;
using System.Text.Json.Serialization;
using SiliFish.DataTypes;
using SiliFish.Extensions;

namespace SiliFish.ModelUnits
{
    public class Stimulus
    {
        public static int nMax; //in time increments

        public TimeLine TimeSpan_ms { get; set; } //in  milliseconds
        public StimulusMode Mode { get; set; }
        //mode = Gaussian: value1 and value2 are mean and SD 
        //mode = Step: Value1 is the stimulus value, Value2 is obsolete
        //mode = Ramp: Value1: stimulus at time start, Value2: stimulus at time end
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        private double tangent; //valid only if mode==Ramp
        private int iStart, iEnd;
        private double[] values = null;
        private bool initialized = false;
        public double MinValue { get { return values?.Min() ?? 0; } }
        public double MaxValue { get { return values?.Max() ?? 0; } }

        public Stimulus() { }
        public Stimulus(StimulusMode mode, TimeLine tl, double value1, double value2 = 0)
        {
            this.Mode = mode;
            TimeSpan_ms = new(tl);
            this.Value1 = value1;
            this.Value2 = value2;
        }
        public double this[int index]
        {
            get 
            { 
                if (values?.Length > index) 
                    return values[index];
                return 0;
            }
        }
        public override string ToString()
        {
            if (Mode == StimulusMode.Step)
                return String.Format("{0} {1}", Mode.ToString(), Value1);
            return String.Format("{0} {1} - {2}", Mode.ToString(), Value1, Value2);
        }

        public string GetTooltip()
        {
            return $"{ToString()}\r\nTimeLine: {TimeSpan_ms}";
        }
        private void Initialize()
        {
            if (initialized)
                return;

            iEnd = (int)(TimeSpan_ms.End / RunParam.static_dt );
            if (iEnd < 0)
                iEnd = nMax;
            iStart = (int)(TimeSpan_ms.Start / RunParam.static_dt );
            if (Mode == StimulusMode.Ramp)
                if (iEnd > iStart)
                    tangent = (Value2 - Value1) / (iEnd - iStart);
                else tangent = 0;

            if (values != null)
            {
                double[] copyArr = new double[values.Length];
                values.CopyTo(copyArr, 0);
                values = new double[iEnd];
                copyArr.CopyTo(values, 0);
            }
            else
                values = new double[iEnd];
            initialized = true;
        }
        public double getStimulus(int t, Random rand)
        {
            int t_ms = (int)(t * RunParam.static_dt );
            if (!TimeSpan_ms.IsActive(t_ms))
                return 0;
            if (!initialized)
                Initialize();
            if (values.Length <= t)
            {
                double[] copyArr = new double[nMax];  
                values.CopyTo(copyArr, 0);
                values = copyArr;
            }
            double value = 0;
            switch (Mode)
            {
                case StimulusMode.Step:
                    value = Value1;
                    break;
                case StimulusMode.Ramp:
                    value = Value1 + (t - iStart) * tangent;
                    break;
                case StimulusMode.Gaussian:
                    value = rand.Gauss(Value1, Value2, Value1 - 3 * Value2, Value1 + 3 * Value2); // µ ± 3SD range
                    break;
            }
            values[t] = value;
            return value;
        }

        public double[] getValues(int nMax)
        {
            if (values?.Length < nMax)
            {
                double[] values2 = new double[nMax];
                Array.Copy(values, values2, values.Length);
                values = values2;
            }
            return values;
        }
    }

}
