using SiliFish.Definitions;
using SiliFish.Extensions;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiliFish.DataTypes
{

    //Base class for all distributions
    public class Distribution
    {
        public static Random Random = null;
        public bool Angular { get; set; } = false;
        private string distType;
        public string DistType { get { return distType; } set { distType = value; } }
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

        protected double LowerLimit { get { return Absolute ? RangeStart : Range * RangeStart / 100; } }

        protected double UpperLimit { get { return Absolute ? RangeEnd : Range * RangeEnd / 100; } }

        [JsonIgnore]
        public virtual double UniqueValue { get { throw new NotImplementedException(); } }
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
                return GetOfDerivedType(element.GetRawText());
            return null;
        }


        public static Distribution GetOfDerivedType(string json)
        {
            Distribution dist = JsonSerializer.Deserialize<Distribution>(json);
            if (dist != null)
            {
                if (dist.DistType == nameof(Constant_NoDistribution)|| dist.DistType == typeof(Constant_NoDistribution).FullName)
                    return JsonSerializer.Deserialize<Constant_NoDistribution>(json);
                if (dist.DistType == nameof(UniformDistribution)|| dist.DistType == typeof(UniformDistribution).FullName)
                    return JsonSerializer.Deserialize<UniformDistribution>(json);
                if (dist.DistType == nameof(SpacedDistribution)|| dist.DistType == typeof(SpacedDistribution).FullName)
                    return JsonSerializer.Deserialize<SpacedDistribution>(json);
                if (dist.DistType == nameof(GaussianDistribution)|| dist.DistType == typeof(GaussianDistribution).FullName)
                    return JsonSerializer.Deserialize<GaussianDistribution>(json);
                if (dist.DistType == nameof(BimodalDistribution)|| dist.DistType == typeof(BimodalDistribution).FullName)
                    return JsonSerializer.Deserialize<BimodalDistribution>(json);
            }
            return dist;
        }
        public static double[] GenerateNRandomNumbers(int n, double range)
        {
            if (Random == null)
                Random = new Random();
            return Random.Uniform(0, range, n);
        }
        public virtual double[] GenerateNNumbers(int n, double range)
        {
            Range = range;
            if (Random == null)
                Random = new Random();
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
        public void ReviewYDistribution(SagittalPlane leftright)
        {
            if (RangeStart >= 0 && leftright == SagittalPlane.Right ||
                RangeStart <= 0 && RangeEnd <= 0 && leftright == SagittalPlane.Left)
                return;
            //TODO SagittalPlane.Both - if gaussian, make it bimodal. trickier than flipping
            if (RangeStart >= 0 && leftright == SagittalPlane.Left)
                FlipOnYAxis();
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
            if (Random == null)
                Random = new Random();
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
            if (Random == null)
                Random = new Random();
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
            if (Random == null)
                Random = new Random();
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
            if (Random == null)
                Random = new Random();
            return Random.Bimodal(Mean * Range / 100, Stddev * Range / 100, Mean2 * Range / 100, Stddev2 * Range / 100, Mode1Weight, n, LowerLimit, UpperLimit);
        }
    }

    public class SpatialDistribution
    {
        public Distribution XDistribution { get; set; }
        public Distribution Y_AngleDistribution { get; set; }
        public Distribution Z_RadiusDistribution { get; set; }

        public SpatialDistribution()
        { }
        public SpatialDistribution(SpatialDistribution sd)
        {
            XDistribution = sd.XDistribution.CreateCopy();
            Y_AngleDistribution = sd.Y_AngleDistribution.CreateCopy();
            Z_RadiusDistribution = sd.Z_RadiusDistribution.CreateCopy();
        }
        public string GetTooltip()
        {
            if (Y_AngleDistribution!=null && Y_AngleDistribution.Angular)
                return $"X: {XDistribution}\r\nAngle: {Y_AngleDistribution}\r\nRadius:{Z_RadiusDistribution}";
            else
                return $"X: {XDistribution}\r\nY: {Y_AngleDistribution}\r\nZ: {Z_RadiusDistribution}";
        }
    }


}