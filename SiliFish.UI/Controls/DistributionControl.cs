using SiliFish.DataTypes;
using SiliFish.DynamicUnits;
using System.DirectoryServices.ActiveDirectory;

namespace SiliFish.UI.Controls
{
    public partial class DistributionControl : UserControl
    {
        public bool NoneIncluded { get; set; } = false;
        public bool Angular { get; set; } = false;
        public bool AbsoluteEnforced
        {
            set
            {
                rbAbsolute.Checked = value;
                rbAbsolute.Visible = rbPerc.Visible =
                rbAbsolute.Enabled = rbPerc.Enabled = !value;

                if (value)
                {
                    lRange.Top = eRangeStart.Top = eRangeEnd.Top = lRangeSeparator.Top =
                        lValue.Top = eUniqueValue.Top = 
                        rbAbsolute.Top;
                    pTop.Height = lRange.Bottom + lRange.Margin.Bottom + 12;
                }
            }
        }
        public DistributionControl()
        {
            InitializeComponent();
            if (NoneIncluded)
                ddDistribution.Items.Add("None");
            ddDistribution.Items.AddRange(Distribution.GetDistributionTypes().ToArray());
            ddDistribution.SelectedIndex = 0;
            rbAbsolute.Checked = true;
        }
        private void ddDistribution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddDistribution.Text == "Uniform" || ddDistribution.Text == "None" || ddDistribution.SelectedIndex <= -1)
            {
                pOptions.Visible = false;
            }
            else
            {
                pOptions.Visible = true;
                string mode = ddDistribution.Text;
                pNoise.Visible = mode is "Constant";
                pGaussian.Visible = mode is "Gaussian" or "Bimodal" or "Equally Spaced";
                pBimodal.Visible = mode == "Bimodal";
            }
            if (ddDistribution.Text == "Constant")
            {
                lRangeSeparator.Visible = lRange.Visible =
                    eRangeEnd.Visible = eRangeStart.Visible = false;
                lValue.Visible = eUniqueValue.Visible = true;
            }
            else if (ddDistribution.Text == "None")
            {
                lRangeSeparator.Visible = false;
                eRangeStart.Visible = eRangeEnd.Visible = false;
                lRange.Visible = false;
                lValue.Visible = eUniqueValue.Visible = false;
            }
            else
            {
                lRangeSeparator.Visible = true;
                eRangeStart.Visible = eRangeEnd.Visible = true;
                lRange.Text = "Range";
                lRange.Visible = true;
                lValue.Visible = eUniqueValue.Visible = false;
            }
        }

        public void SetDistribution(Distribution dist)
        {
            if (dist == null)
                return;
            eUniqueValue.Text = dist.UniqueValue.ToString();
            eRangeStart.Text = dist.RangeStart.ToString();
            eRangeEnd.Text = dist.RangeEnd.ToString();
            rbAbsolute.Checked = dist.Absolute;
            rbPerc.Checked = !rbAbsolute.Checked;
            Angular = dist.Angular;
            if (Angular)
                AbsoluteEnforced = true;
            if (dist is SpacedDistribution)
            {
                ddDistribution.Text = "Equally Spaced";
                eMean1.Text = (dist as SpacedDistribution).NoiseMean.ToString("0.###");
                eStdDev1.Text = (dist as SpacedDistribution).NoiseStdDev.ToString("0.###");
            }
            else if (dist is BimodalDistribution)
            {
                ddDistribution.Text = "Bimodal";
                eMean1.Text = (dist as BimodalDistribution).Mean.ToString("0.###");
                eStdDev1.Text = (dist as BimodalDistribution).Stddev.ToString("0.###");
                eMean2.Text = (dist as BimodalDistribution).Mean2.ToString("0.###");
                eStdDev2.Text = (dist as BimodalDistribution).Stddev2.ToString("0.###");
                eMode1Weight.Text = (dist as BimodalDistribution).Mode1Weight.ToString("0.###");
            }
            else if (dist is GaussianDistribution)//Bimodal is also Gaussian - order of 'if's is important
            {
                ddDistribution.Text = "Gaussian";
                eMean1.Text = (dist as GaussianDistribution).Mean.ToString("0.###");
                eStdDev1.Text = (dist as GaussianDistribution).Stddev.ToString("0.###");
            }
            else if (dist is UniformDistribution)
                ddDistribution.Text = "Uniform";
            else //constant_NoDistribution
            {
                ddDistribution.Text = "Constant";
                if (dist is Constant_NoDistribution d)
                    eNoise.Text = d.NoiseStdDev.ToString("0.###");
            }
        }

        public Distribution GetDistribution()
        {
            string mode = ddDistribution.Text;
            if (mode == "None")
                return null;
            bool absolute = rbAbsolute.Checked;
            if (!double.TryParse(eRangeStart.Text, out double start))
                start = 0;
            if (!double.TryParse(eRangeEnd.Text, out double end))
                end = 100;
            if (!double.TryParse(eUniqueValue.Text, out double value))
                value = 0;
            if (Angular && end > 180) end = 180;
            else if (!Angular && !absolute && end > 100) end = 100;

            if (!double.TryParse(eNoise.Text, out double noise))
                noise = 0;
            switch (mode)
            {
                case "Constant":
                    return new Constant_NoDistribution(value, absolute, Angular, noise);
                case "Uniform":
                    return new UniformDistribution(start, end, absolute, Angular);
                case "Equally Spaced":
                    if (!double.TryParse(eMean1.Text, out double meanNoise))
                        meanNoise = 1;
                    if (!double.TryParse(eStdDev1.Text, out double stddevNoise))
                        stddevNoise = 0;
                    return new SpacedDistribution(start, end, meanNoise, stddevNoise, absolute, Angular);
                case "Gaussian":
                    if (!double.TryParse(eMean1.Text, out double mean))
                        mean = 1;
                    if (!double.TryParse(eStdDev1.Text, out double stddev))
                        stddev = 0;
                    return new GaussianDistribution(start, end, mean, stddev, absolute, Angular);
                case "Bimodal":
                    if (!double.TryParse(eMean1.Text, out double mean1))
                        mean1 = 1;
                    if (!double.TryParse(eStdDev1.Text, out double stddev1))
                        stddev1 = 0;
                    if (!double.TryParse(eMean2.Text, out double mean2))
                        mean2 = 1;
                    if (!double.TryParse(eStdDev2.Text, out double stddev2))
                        stddev2 = 0;
                    if (!double.TryParse(eMode1Weight.Text, out double mode1weight))
                        mode1weight = 0.5;
                    else if (mode1weight < 0 || mode1weight > 1)
                        mode1weight = 0.5;
                    return new BimodalDistribution(start, end, mean1, stddev1, mean2, stddev2, mode1weight, absolute, Angular);
            }
            return null;
        }

        private void rbAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbsolute.Checked)
            {
                lRange.Text = "Range";
                lMean.Text = "Mean";
                lStdDev.Text = "Std Dev";
                lMean2.Text = "Mean 2";
                lStdDev2.Text = "Std Dev 2";
            }
        }

        private void rbPerc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPerc.Checked)
            {
                lRange.Text = "Range (%)";
                lMean.Text = "Mean (%)";
                lStdDev.Text = "Std Dev (%)";
                lMean2.Text = "Mean 2 (%)";
                lStdDev2.Text = "Std Dev 2 (%)";
            }
        }
    }
}
