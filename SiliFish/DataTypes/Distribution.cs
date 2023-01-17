using SiliFish.Definitions;
using SiliFish.Extensions;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiliFish.DataTypes
{

    //Base class for all distributions
    [JsonDerivedType(typeof(Distribution), typeDiscriminator: "distribution")]
    [JsonDerivedType(typeof(UniformDistribution), typeDiscriminator: "uniform")]
    [JsonDerivedType(typeof(Constant_NoDistribution), typeDiscriminator: "constant")]
    [JsonDerivedType(typeof(SpacedDistribution), typeDiscriminator: "spaced")]
    [JsonDerivedType(typeof(GaussianDistribution), typeDiscriminator: "gaussian")]
    [JsonDerivedType(typeof(BimodalDistribution), typeDiscriminator: "bimodal")]
    public class Distribution
    {
        public static Random Random = null;
        public bool Angular { get; set; } = false;
        private string distType;
        
        [JsonIgnore]
        public string DistType => distType;
        public bool Absolute { get; set; } = true;
        /// <summary>
        /// Can be percentage or absolute value
        /// </summary>
        public double RangeStart { get; set; } = 0;
        /// <summary>
        /// Can be percentage or absolute value
        /// </summary>
        public double RangeEnd { get; set; } = 100;
        /// <summary>
        /// Obsolote is Absolute is set to true
        /// </summary>
        public double Range { get; set; } = 0;

        [JsonIgnore]
        public bool IsConstant
        {
            get
            {
                return DistType == nameof(Constant_NoDistribution) && (this as Constant_NoDistribution).NoiseStdDev < CurrentSettings.Settings.Epsilon;
            }
        }
        [JsonIgnore]
        public string RangeStr 
        {
            get
            {
                return Absolute ? $"{RangeStart}-{RangeEnd}" :
                    $"{LowerLimit}%-{UpperLimit}%";
            }
        }

        protected double LowerLimit { get { return Absolute ? RangeStart : Range * RangeStart / 100; } }

        protected double UpperLimit { get { return Absolute ? RangeEnd : Range * RangeEnd / 100; } }

        [JsonIgnore]
        public virtual double UniqueValue { get { return 666; } }// throw new NotImplementedException(); } }
        public override string ToString()
        {
            int dot = DistType.LastIndexOf('.');
            string displayedType = distType[(dot + 1)..];
            return Absolute ? String.Format("{0}-{1} [{2}]", RangeStart, RangeEnd, displayedType) :
                String.Format("%{0}-{1}; [{2}-{3}] [{4}]", RangeStart, RangeEnd, LowerLimit, UpperLimit, displayedType);
        }
        public Distribution()
        {
            distType = GetType().Name;
            //default values of 0 and 999 used
        }
        public virtual Distribution CreateCopy()
        {
            throw new NotImplementedException();
        }
        public Distribution(double rangeStart, double rangeEnd, bool absolute, bool angular)
        {
            Absolute = absolute;
            Angular = angular;
            distType = GetType().Name;
            if (rangeStart < rangeEnd)
            {
                RangeStart = rangeStart;
                RangeEnd = rangeEnd;
            }
            else
            {
                RangeStart = rangeEnd;
                RangeEnd = rangeStart;
            }
        }

        public static Distribution CreateDistributionObject(object obj)
        {
            if (obj == null)
                return null;
            if (obj is Distribution distribution)
                return distribution;
            if (double.TryParse(obj.ToString(), out double d))
                return new Constant_NoDistribution(d, true, false, 0);
            if (obj is JsonElement element)
            {
                return JsonSerializer.Deserialize<Distribution>(element.GetRawText());//if this works, get rid of DistType field
            }
            return null;
        }

        public static double[] GenerateNRandomNumbers(int n, double range)
        {
            Random ??= new Random();
            return Random.Uniform(0, range, n);
        }
        public virtual double[] GenerateNNumbers(int n, double range)
        {
            Range = range;
            Random ??= new Random();
            return Random.Uniform(LowerLimit, UpperLimit, n);
        }

        protected virtual void FlipOnYAxis()
        {
            RangeStart = -RangeStart;
            RangeEnd = -RangeEnd;
            if (RangeStart > RangeEnd)
                (RangeEnd, RangeStart) = (RangeStart, RangeEnd);
        }

        //Y Distribution is different than the other distributions
        //medial --> lateral limits are defined for one side only
        //depending on the side (left/right), the distribution needs to be reviewed
        public Distribution ReviewYDistribution(SagittalPlane leftright)
        {
            if (RangeStart >= 0 && leftright == SagittalPlane.Right ||
                RangeStart <= 0 && RangeEnd <= 0 && leftright == SagittalPlane.Left)
                return this;
            if (RangeStart >= 0 && leftright == SagittalPlane.Left)
            {
                FlipOnYAxis();
                return this;
            }
            //Convert gaussian to bimodal
            if (leftright == SagittalPlane.Both && this is GaussianDistribution gd)
            {
                BimodalDistribution bd = new(gd.Mean, gd.Stddev, -gd.Mean, gd.Stddev, 0.5);
                return bd;
            }
            return this;
        }
    }

    public class UniformDistribution : Distribution
    {
        public override double UniqueValue
        {
            get { return (RangeStart + RangeEnd) / 2; }
        }

        public UniformDistribution()
        { }
        public UniformDistribution(double rangeStart, double rangeEnd, bool absolute, bool angular)
            : base(rangeStart, rangeEnd, absolute, angular)
        {
        }

        public override Distribution CreateCopy()
        {
            return new UniformDistribution(RangeStart, RangeEnd, Absolute, Angular);
        }
    }

    public class Constant_NoDistribution : Distribution
    {
        public override double UniqueValue
        {
            get { return (RangeStart + RangeEnd) / 2; }
        }
        public double NoiseStdDev { get; set; } = 0;

        public override string ToString()
        {
            string noise = NoiseStdDev > 0 ? $"\r\nNoise: µ:1; SD:{1:0.#####}" : "";
            return $"{UniqueValue}{noise}";
        }
        public Constant_NoDistribution()
        { }
        public Constant_NoDistribution(double val)
            : base(val, val, absolute: true, angular: false)
        {
            NoiseStdDev = 0;
        }
        public Constant_NoDistribution(double val, bool absolute, bool angular, double noiseStdDev)
            : base(val - 10 * noiseStdDev, val + 10 * noiseStdDev, absolute, angular)
        {
            NoiseStdDev = noiseStdDev;
        }

        public override Distribution CreateCopy()
        {
            return new Constant_NoDistribution(RangeStart, Absolute, Angular, NoiseStdDev);
        }

        public override double[] GenerateNNumbers(int n, double range)
        {
            Random ??= new Random();
            Range = range;
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                double noise = NoiseStdDev > 0 ? Random.Gauss(0, NoiseStdDev) : 0;
                result[i] = LowerLimit + noise;
            }
            return result;
        }
    }
    public class SpacedDistribution : Distribution
    {
        public double NoiseStdDev { get; set; } = 0;
        public override double UniqueValue
        {
            get { return (RangeStart + RangeEnd) / 2; }
        }
        public override string ToString()
        {
            return string.Format("{0}\r\nNoise: µ:1; SD:{1:0.#####}", base.ToString(), NoiseStdDev);
        }
        public SpacedDistribution()
        { }
        public SpacedDistribution(double start, double end, double noiseStdDev, bool absolute, bool angular)
            : base(start, end, absolute, angular)
        {
            NoiseStdDev = noiseStdDev;
        }

        public override Distribution CreateCopy()
        {
            return new SpacedDistribution(RangeStart, RangeEnd, NoiseStdDev, Absolute, Angular);
        }

        public override double[] GenerateNNumbers(int n, double range)
        {
            Range = range;
            Random ??= new Random();
            return Random.Spaced(LowerLimit, UpperLimit, NoiseStdDev, n);
        }
    }
    public class GaussianDistribution : Distribution
    {
        public double Mean { get; set; } = 0;
        public double Stddev { get; set; } = 0;
        public override double UniqueValue
        {
            get { return Mean; }
        }
        public override string ToString()
        {
            return String.Format("µ:{0:0.#####}; SD:{1:0.#####}; {2}\r\n", Mean, Stddev, base.ToString());
        }

        protected override void FlipOnYAxis()
        {
            base.FlipOnYAxis();
            Mean = -Mean;
        }
        public GaussianDistribution()
        { }

        public GaussianDistribution(double rangeStart, double rangeEnd, double mean, double stddev, bool absolute, bool angular)
            : base(rangeStart, rangeEnd, absolute, angular)
        {
            Mean = mean;
            Stddev = stddev;
        }
        public GaussianDistribution(double mean, double stddev)
            : base()
        {
            Mean = mean;
            Stddev = stddev;
        }

        public override Distribution CreateCopy()
        {
            return new GaussianDistribution(RangeStart, RangeEnd, Mean, Stddev, Absolute, Angular);
        }
        public override double[] GenerateNNumbers(int n, double range)
        {
            Range = Absolute ? 100 : double.Parse(range.ToString());
            Random ??= new Random();
            return Random.Gauss(Mean * Range / 100, Stddev * Range / 100, n, LowerLimit, UpperLimit);
        }
    }
    public class BimodalDistribution : GaussianDistribution
    {
        public override double UniqueValue
        {
            get { return (Mean + Mean2) / 2; }
        }
        public double Mean2 { get; set; } = 1;
        public double Stddev2 { get; set; } = 0;
        public double Mode1Weight { get; set; } = 1;
        public override string ToString()
        {
            return String.Format("{0}\r\nµ2:{1:0.#####}; SD2:{2:0.#####}\r\nMode 1 Weight{3:0.##}", base.ToString(), Mean2, Stddev2, Mode1Weight);
        }

        protected override void FlipOnYAxis()
        {
            base.FlipOnYAxis();
            Mean2 = -Mean2;
        }
        public BimodalDistribution()
        { }
        public BimodalDistribution(double start, double end, double mean, double stddev, double mean2, double stddev2, double mode1Weight, bool absolute, bool angular)
            : base(start, end, mean, stddev, absolute, angular)
        {
            Mean2 = mean2;
            Stddev2 = stddev2;
            Mode1Weight = mode1Weight;
        }
        public BimodalDistribution(double mean, double stddev, double mean2, double stddev2, double mode1Weight)
            : base(mean, stddev)
        {
            Mean2 = mean2;
            Stddev2 = stddev2;
            Mode1Weight = mode1Weight;
        }

        public override Distribution CreateCopy()
        {
            return new BimodalDistribution(RangeStart, RangeEnd, Mean, Stddev, Mean2, Stddev2, Mode1Weight, Absolute, Angular);
        }
        public override double[] GenerateNNumbers(int n, double range)
        {
            Range = Absolute ? 100 : range;
            Random ??= new Random();
            return Random.Bimodal(Mean * Range / 100, Stddev * Range / 100, Mean2 * Range / 100, Stddev2 * Range / 100, Mode1Weight, n, LowerLimit, UpperLimit);
        }
    }
}