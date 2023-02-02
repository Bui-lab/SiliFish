using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiliFish.DataTypes
{
    //Base class for all distributions
    [JsonDerivedType(typeof(Distribution), typeDiscriminator: "None")]
    [JsonDerivedType(typeof(UniformDistribution), typeDiscriminator: "Uniform")]
    [JsonDerivedType(typeof(Constant_NoDistribution), typeDiscriminator: "Constant")]
    [JsonDerivedType(typeof(SpacedDistribution), typeDiscriminator: "Equally Spaced")]
    [JsonDerivedType(typeof(GaussianDistribution), typeDiscriminator: "Gaussian")]
    [JsonDerivedType(typeof(BimodalDistribution), typeDiscriminator: "Bimodal")]
    public class Distribution
    {
        private static readonly Dictionary<string, Type> typeMap = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => typeof(Distribution).IsAssignableFrom(type))
            .ToDictionary(type => type.Name, type => type);

        public static Random Random = null;
        public bool Angular { get; set; } = false;
        
        [JsonIgnore]
        public virtual string Discriminator => "None";
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
        /// Obsolote if Absolute is set to true
        /// </summary>
        public double Range { get; set; } = 0;

        [JsonIgnore]
        public bool IsConstant
        {
            get
            {
                return (this is Constant_NoDistribution constant_NoDistribution) && constant_NoDistribution.NoiseStdDev < GlobalSettings.Epsilon;
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
            return Absolute ? String.Format("{0}-{1} [{2}]", RangeStart, RangeEnd, Discriminator) :
                String.Format("%{0}-{1}; [{2}-{3}] [{4}]", RangeStart, RangeEnd, LowerLimit, UpperLimit, Discriminator);
        }
        public Distribution()
        {
            //default values of 0 and 999 used
        }
        public Distribution Clone()
        {
            return (Distribution)this.MemberwiseClone();
        }
        public Distribution(double rangeStart, double rangeEnd, bool absolute, bool angular)
        {
            Absolute = absolute;
            Angular = angular;
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
        public static List<string> GetDistributionTypes()
        {
            return typeMap.Keys
                .Where(k => k != nameof(Distribution))
                .Select(k => ((Distribution)Activator.CreateInstance(typeMap[k])).Discriminator).ToList();
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

        public static double[] GenerateNRandomNumbers(int n, double range, bool ordered)
        {
            Random ??= new Random();
            return Random.Uniform(0, range, n, ordered);
        }
        public virtual double[] GenerateNNumbers(int n, double range, bool ordered)
        {
            Range = range;
            Random ??= new Random();
            return Random.Uniform(LowerLimit, UpperLimit, n, ordered);
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
        public override string Discriminator => "Uniform";

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

    }

    public class Constant_NoDistribution : Distribution
    {
        public override string Discriminator => "Constant";

        public override double UniqueValue
        {
            get { return (RangeStart + RangeEnd) / 2; }
        }
        public double NoiseStdDev { get; set; } = 0;

        public override string ToString()
        {
            string noise = NoiseStdDev > 0 ? $"\r\nNoise: µ:0; SD:{1:0.#####}" : "";
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


        public override double[] GenerateNNumbers(int n, double range, bool ordered)
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
        public override string Discriminator => "Equally Spaced";

           public double NoiseStdDev { get; set; } = 0;
        public override double UniqueValue
        {
            get { return (RangeStart + RangeEnd) / 2; }
        }
        public override string ToString()
        {
            return $"{base.ToString()}\r\nNoise: µ:0; SD:{NoiseStdDev:0.#####}";
        }
        public SpacedDistribution()
        { }
        public SpacedDistribution(double start, double end, double noiseStdDev, bool absolute, bool angular)
            : base(start, end, absolute, angular)
        {
            NoiseStdDev = noiseStdDev;
        }

        public override double[] GenerateNNumbers(int n, double range, bool ordered)
        {
            Range = range;
            Random ??= new Random();
            return Random.Spaced(LowerLimit, UpperLimit, noisemean: 0, NoiseStdDev, n, ordered);
        }
    }
    public class GaussianDistribution : Distribution
    {
        public override string Discriminator => "Gaussian";

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

        public override double[] GenerateNNumbers(int n, double range, bool ordered)
        {
            Range = Absolute ? 100 : double.Parse(range.ToString());
            Random ??= new Random();
            double[] arr = Random.Gauss(Mean * Range / 100, Stddev * Range / 100, n, LowerLimit, UpperLimit);
            if (ordered)
                return arr.Order().ToArray();
            return arr;
        }
    }
    public class BimodalDistribution : GaussianDistribution
    {
        public override string Discriminator => "Bimodal";

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

        public override double[] GenerateNNumbers(int n, double range, bool ordered)
        {
            Range = Absolute ? 100 : range;
            Random ??= new Random();
            double mean1 = Mean * Range / 100;
            double stdDev1 = Stddev * Range / 100;
            double mean2 = Mean2 * Range / 100;
            double stdDev2= Stddev2 * Range / 100;
            double[] arr = Random.Bimodal(mean1, stdDev1, mean2, stdDev2, Mode1Weight, n, LowerLimit, UpperLimit);
            if (ordered)
                return arr.Order().ToArray();
            return arr;
        }
    }
}