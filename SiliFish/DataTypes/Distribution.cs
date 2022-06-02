using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using SiliFish.Extensions;

namespace SiliFish.DataTypes
{

    //Base class for uniform distribution
    public abstract class Distribution
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

        protected double UpperLimit { get { return Absolute ? RangeEnd : Range * RangeEnd/ 100; } }
        public override string ToString()
        {
            int dot = DistType.LastIndexOf('.');
            string displayedType = distType.Substring(dot + 1);
            return Absolute ? String.Format("{0}: [{1}-{2}]", displayedType, RangeStart, RangeEnd) :
                String.Format("{0}: %[{1}-{2}]; [{3}-{4}]", displayedType, RangeStart, RangeEnd, LowerLimit, UpperLimit);
        }
        public Distribution()
        {
            distType = GetType().ToString();
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
            distType = GetType().ToString();
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

        public static Distribution GetOfDerivedType(string json)
        {
            Distribution dist = JsonSerializer.Deserialize<UniformDistribution>(json);
            if (dist.DistType == typeof(Constant_NoDistribution).ToString())
                return JsonSerializer.Deserialize<Constant_NoDistribution>(json);
            if (dist.DistType == typeof(UniformDistribution).ToString())
                return JsonSerializer.Deserialize<UniformDistribution>(json);
            if (dist.DistType == typeof(SpacedDistribution).ToString())
                return JsonSerializer.Deserialize<SpacedDistribution>(json); 
            if (dist.DistType == typeof(GaussianDistribution).ToString())
                return JsonSerializer.Deserialize<GaussianDistribution>(json);
            if (dist.DistType == typeof(BimodalDistribution).ToString())
                return JsonSerializer.Deserialize<BimodalDistribution>(json);
            return dist;
        }
        public static double[] GenerateNRandomNumbers(int n, double range)
        {
            if (Random == null)
                Random = new Random();
            return Random.Uniform(0, range, n);
        }
        public virtual double[] GenerateNNumbers(int n, double? range)
        {
            Range = (double)range;
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

    public class UniformDistribution: Distribution
    {
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
        public Constant_NoDistribution()
        { }
        public Constant_NoDistribution(double val, bool angular)
            : base(val, val, true, angular)
        {
        }

        public override Distribution CreateCopy()
        {
            return new Constant_NoDistribution(RangeStart, Angular);
        }

        public override double[] GenerateNNumbers(int n, double? range)
        {
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
                result[i] = LowerLimit;
            return result;
        }
    }
    public class SpacedDistribution : Distribution
    {
        public double NoiseStdDev { get; set; } = 0;
        public override string ToString()
        {
            return String.Format("{0}\r\nNoise: µ:1; SD:{1:0.#####}", base.ToString(), NoiseStdDev);
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

        public override double[] GenerateNNumbers(int n, double? range)
        {
            if (range != null)
                Range = (double)range;
            if (Random == null)
                Random = new Random();
            return Random.Spaced(LowerLimit, UpperLimit, NoiseStdDev, n);
        }
    }
    public class GaussianDistribution : Distribution
    {
        public double Mean { get; set; } = 0;
        public double Stddev { get; set; } = 0;
        public override string ToString()
        {
            return String.Format("{0}\r\nµ:{1:0.#####}; SD:{2:0.#####}", base.ToString(), Mean, Stddev);
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
        public override double[] GenerateNNumbers(int n, double? range)
        {
            Range = Absolute ? 100 : double.Parse(range?.ToString() ?? "0");
            if (Random == null)
                Random = new Random();
            return Random.Gauss(Mean * Range / 100, Stddev * Range / 100, n, LowerLimit, UpperLimit);
        }
    }
    public class BimodalDistribution : GaussianDistribution
    {
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
        public override double[] GenerateNNumbers(int n, double? range)
        {
            Range = Absolute ? 100 : double.Parse(range?.ToString() ?? "0");
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
            if (Y_AngleDistribution.Angular)
                return $"X: {XDistribution}\r\nAngle: {Y_AngleDistribution}\r\nRadius:{Z_RadiusDistribution}";
            else
                return $"X: {XDistribution}\r\nY: {Y_AngleDistribution}\r\nZ: {Z_RadiusDistribution}";
        }
    }


}