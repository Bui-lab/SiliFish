namespace SiliFish.UI.Controls
{
    partial class DistributionControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ddDistribution = new System.Windows.Forms.ComboBox();
            this.lDistributionType = new System.Windows.Forms.Label();
            this.lRange = new System.Windows.Forms.Label();
            this.lRangeSeparator = new System.Windows.Forms.Label();
            this.lNoise = new System.Windows.Forms.Label();
            this.lMean = new System.Windows.Forms.Label();
            this.lStdDev = new System.Windows.Forms.Label();
            this.pBimodal = new System.Windows.Forms.Panel();
            this.eMode1Weight = new System.Windows.Forms.TextBox();
            this.lMode1Weight = new System.Windows.Forms.Label();
            this.eStdDev2 = new System.Windows.Forms.TextBox();
            this.eMean2 = new System.Windows.Forms.TextBox();
            this.lMean2 = new System.Windows.Forms.Label();
            this.lStdDev2 = new System.Windows.Forms.Label();
            this.pGaussian = new System.Windows.Forms.Panel();
            this.eStdDev1 = new System.Windows.Forms.TextBox();
            this.eMean1 = new System.Windows.Forms.TextBox();
            this.pEquallySpaced = new System.Windows.Forms.Panel();
            this.eNoise = new System.Windows.Forms.TextBox();
            this.pTop = new System.Windows.Forms.Panel();
            this.pTopIn = new System.Windows.Forms.Panel();
            this.rbPerc = new System.Windows.Forms.RadioButton();
            this.rbAbsolute = new System.Windows.Forms.RadioButton();
            this.eRangeEnd = new System.Windows.Forms.TextBox();
            this.eRangeStart = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.pBimodal.SuspendLayout();
            this.pGaussian.SuspendLayout();
            this.pEquallySpaced.SuspendLayout();
            this.pTop.SuspendLayout();
            this.pTopIn.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // ddDistribution
            // 
            this.ddDistribution.FormattingEnabled = true;
            this.ddDistribution.Items.AddRange(new object[] {
            "None",
            "Constant",
            "Uniform",
            "Equally Spaced",
            "Gaussian",
            "Bimodal"});
            this.ddDistribution.Location = new System.Drawing.Point(78, 4);
            this.ddDistribution.Name = "ddDistribution";
            this.ddDistribution.Size = new System.Drawing.Size(121, 23);
            this.ddDistribution.TabIndex = 0;
            this.ddDistribution.SelectedIndexChanged += new System.EventHandler(this.ddDistribution_SelectedIndexChanged);
            // 
            // lDistributionType
            // 
            this.lDistributionType.AutoSize = true;
            this.lDistributionType.Location = new System.Drawing.Point(5, 9);
            this.lDistributionType.Name = "lDistributionType";
            this.lDistributionType.Size = new System.Drawing.Size(69, 15);
            this.lDistributionType.TabIndex = 8;
            this.lDistributionType.Text = "Distribution";
            // 
            // lRange
            // 
            this.lRange.AutoSize = true;
            this.lRange.Location = new System.Drawing.Point(5, 62);
            this.lRange.Name = "lRange";
            this.lRange.Size = new System.Drawing.Size(61, 15);
            this.lRange.TabIndex = 10;
            this.lRange.Text = "Range (%)";
            // 
            // lRangeSeparator
            // 
            this.lRangeSeparator.AutoSize = true;
            this.lRangeSeparator.Location = new System.Drawing.Point(132, 66);
            this.lRangeSeparator.Name = "lRangeSeparator";
            this.lRangeSeparator.Size = new System.Drawing.Size(12, 15);
            this.lRangeSeparator.TabIndex = 11;
            this.lRangeSeparator.Text = "-";
            // 
            // lNoise
            // 
            this.lNoise.AutoSize = true;
            this.lNoise.Location = new System.Drawing.Point(5, 8);
            this.lNoise.Name = "lNoise";
            this.lNoise.Size = new System.Drawing.Size(37, 15);
            this.lNoise.TabIndex = 0;
            this.lNoise.Text = "Noise";
            this.toolTip.SetToolTip(this.lNoise, "Std Dev for noise multiplier (µ = 1)");
            // 
            // lMean
            // 
            this.lMean.AutoSize = true;
            this.lMean.Location = new System.Drawing.Point(5, 10);
            this.lMean.Name = "lMean";
            this.lMean.Size = new System.Drawing.Size(37, 15);
            this.lMean.TabIndex = 0;
            this.lMean.Text = "Mean";
            // 
            // lStdDev
            // 
            this.lStdDev.AutoSize = true;
            this.lStdDev.Location = new System.Drawing.Point(5, 33);
            this.lStdDev.Name = "lStdDev";
            this.lStdDev.Size = new System.Drawing.Size(47, 15);
            this.lStdDev.TabIndex = 2;
            this.lStdDev.Text = "Std Dev";
            // 
            // pBimodal
            // 
            this.pBimodal.Controls.Add(this.eMode1Weight);
            this.pBimodal.Controls.Add(this.lMode1Weight);
            this.pBimodal.Controls.Add(this.eStdDev2);
            this.pBimodal.Controls.Add(this.eMean2);
            this.pBimodal.Controls.Add(this.lMean2);
            this.pBimodal.Controls.Add(this.lStdDev2);
            this.pBimodal.Location = new System.Drawing.Point(0, 78);
            this.pBimodal.Margin = new System.Windows.Forms.Padding(0);
            this.pBimodal.Name = "pBimodal";
            this.pBimodal.Size = new System.Drawing.Size(204, 80);
            this.pBimodal.TabIndex = 22;
            // 
            // eMode1Weight
            // 
            this.eMode1Weight.Location = new System.Drawing.Point(78, 54);
            this.eMode1Weight.Name = "eMode1Weight";
            this.eMode1Weight.Size = new System.Drawing.Size(52, 23);
            this.eMode1Weight.TabIndex = 5;
            // 
            // lMode1Weight
            // 
            this.lMode1Weight.AutoSize = true;
            this.lMode1Weight.Location = new System.Drawing.Point(5, 58);
            this.lMode1Weight.Name = "lMode1Weight";
            this.lMode1Weight.Size = new System.Drawing.Size(60, 15);
            this.lMode1Weight.TabIndex = 4;
            this.lMode1Weight.Text = "Mode 1 %";
            // 
            // eStdDev2
            // 
            this.eStdDev2.Location = new System.Drawing.Point(78, 28);
            this.eStdDev2.Name = "eStdDev2";
            this.eStdDev2.Size = new System.Drawing.Size(52, 23);
            this.eStdDev2.TabIndex = 3;
            // 
            // eMean2
            // 
            this.eMean2.Location = new System.Drawing.Point(78, 2);
            this.eMean2.Name = "eMean2";
            this.eMean2.Size = new System.Drawing.Size(52, 23);
            this.eMean2.TabIndex = 1;
            // 
            // lMean2
            // 
            this.lMean2.AutoSize = true;
            this.lMean2.Location = new System.Drawing.Point(5, 10);
            this.lMean2.Name = "lMean2";
            this.lMean2.Size = new System.Drawing.Size(46, 15);
            this.lMean2.TabIndex = 0;
            this.lMean2.Text = "Mean 2";
            // 
            // lStdDev2
            // 
            this.lStdDev2.AutoSize = true;
            this.lStdDev2.Location = new System.Drawing.Point(5, 34);
            this.lStdDev2.Name = "lStdDev2";
            this.lStdDev2.Size = new System.Drawing.Size(56, 15);
            this.lStdDev2.TabIndex = 2;
            this.lStdDev2.Text = "Std Dev 2";
            // 
            // pGaussian
            // 
            this.pGaussian.Controls.Add(this.eStdDev1);
            this.pGaussian.Controls.Add(this.eMean1);
            this.pGaussian.Controls.Add(this.lMean);
            this.pGaussian.Controls.Add(this.lStdDev);
            this.pGaussian.Location = new System.Drawing.Point(0, 26);
            this.pGaussian.Margin = new System.Windows.Forms.Padding(0);
            this.pGaussian.Name = "pGaussian";
            this.pGaussian.Size = new System.Drawing.Size(204, 52);
            this.pGaussian.TabIndex = 21;
            // 
            // eStdDev1
            // 
            this.eStdDev1.Location = new System.Drawing.Point(78, 28);
            this.eStdDev1.Name = "eStdDev1";
            this.eStdDev1.Size = new System.Drawing.Size(52, 23);
            this.eStdDev1.TabIndex = 3;
            // 
            // eMean1
            // 
            this.eMean1.Location = new System.Drawing.Point(78, 2);
            this.eMean1.Name = "eMean1";
            this.eMean1.Size = new System.Drawing.Size(52, 23);
            this.eMean1.TabIndex = 1;
            // 
            // pEquallySpaced
            // 
            this.pEquallySpaced.Controls.Add(this.eNoise);
            this.pEquallySpaced.Controls.Add(this.lNoise);
            this.pEquallySpaced.Location = new System.Drawing.Point(0, 0);
            this.pEquallySpaced.Margin = new System.Windows.Forms.Padding(0);
            this.pEquallySpaced.Name = "pEquallySpaced";
            this.pEquallySpaced.Size = new System.Drawing.Size(204, 26);
            this.pEquallySpaced.TabIndex = 20;
            // 
            // eNoise
            // 
            this.eNoise.Location = new System.Drawing.Point(78, 2);
            this.eNoise.Name = "eNoise";
            this.eNoise.Size = new System.Drawing.Size(52, 23);
            this.eNoise.TabIndex = 5;
            this.toolTip.SetToolTip(this.eNoise, "Std Dev for noise multiplier (µ = 1)");
            // 
            // pTop
            // 
            this.pTop.Controls.Add(this.pTopIn);
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Margin = new System.Windows.Forms.Padding(0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(204, 84);
            this.pTop.TabIndex = 19;
            // 
            // pTopIn
            // 
            this.pTopIn.Controls.Add(this.rbPerc);
            this.pTopIn.Controls.Add(this.rbAbsolute);
            this.pTopIn.Controls.Add(this.lDistributionType);
            this.pTopIn.Controls.Add(this.eRangeEnd);
            this.pTopIn.Controls.Add(this.lRange);
            this.pTopIn.Controls.Add(this.eRangeStart);
            this.pTopIn.Controls.Add(this.lRangeSeparator);
            this.pTopIn.Controls.Add(this.ddDistribution);
            this.pTopIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTopIn.Location = new System.Drawing.Point(0, 0);
            this.pTopIn.Margin = new System.Windows.Forms.Padding(0);
            this.pTopIn.Name = "pTopIn";
            this.pTopIn.Size = new System.Drawing.Size(204, 84);
            this.pTopIn.TabIndex = 14;
            // 
            // rbPerc
            // 
            this.rbPerc.AutoSize = true;
            this.rbPerc.Location = new System.Drawing.Point(78, 33);
            this.rbPerc.Name = "rbPerc";
            this.rbPerc.Size = new System.Drawing.Size(84, 19);
            this.rbPerc.TabIndex = 2;
            this.rbPerc.Text = "Percentage";
            this.rbPerc.CheckedChanged += new System.EventHandler(this.rbPerc_CheckedChanged);
            // 
            // rbAbsolute
            // 
            this.rbAbsolute.AutoSize = true;
            this.rbAbsolute.Location = new System.Drawing.Point(5, 33);
            this.rbAbsolute.Name = "rbAbsolute";
            this.rbAbsolute.Size = new System.Drawing.Size(72, 19);
            this.rbAbsolute.TabIndex = 1;
            this.rbAbsolute.Text = "Absolute";
            this.rbAbsolute.CheckedChanged += new System.EventHandler(this.rbAbsolute_CheckedChanged);
            // 
            // eRangeEnd
            // 
            this.eRangeEnd.Location = new System.Drawing.Point(145, 58);
            this.eRangeEnd.Name = "eRangeEnd";
            this.eRangeEnd.Size = new System.Drawing.Size(54, 23);
            this.eRangeEnd.TabIndex = 4;
            // 
            // eRangeStart
            // 
            this.eRangeStart.Location = new System.Drawing.Point(78, 58);
            this.eRangeStart.Name = "eRangeStart";
            this.eRangeStart.Size = new System.Drawing.Size(52, 23);
            this.eRangeStart.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.pTop);
            this.flowLayoutPanel1.Controls.Add(this.pOptions);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(211, 0);
            this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(204, 70);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(204, 242);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // pOptions
            // 
            this.pOptions.AutoSize = true;
            this.pOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pOptions.Controls.Add(this.pEquallySpaced);
            this.pOptions.Controls.Add(this.pGaussian);
            this.pOptions.Controls.Add(this.pBimodal);
            this.pOptions.Location = new System.Drawing.Point(0, 84);
            this.pOptions.Margin = new System.Windows.Forms.Padding(0);
            this.pOptions.Name = "pOptions";
            this.pOptions.Size = new System.Drawing.Size(204, 158);
            this.pOptions.TabIndex = 20;
            // 
            // DistributionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.flowLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(204, 0);
            this.Name = "DistributionControl";
            this.Size = new System.Drawing.Size(207, 245);
            this.pBimodal.ResumeLayout(false);
            this.pBimodal.PerformLayout();
            this.pGaussian.ResumeLayout(false);
            this.pGaussian.PerformLayout();
            this.pEquallySpaced.ResumeLayout(false);
            this.pEquallySpaced.PerformLayout();
            this.pTop.ResumeLayout(false);
            this.pTopIn.ResumeLayout(false);
            this.pTopIn.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.pOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox ddDistribution;
        private Label lDistributionType;
        private Label lRange;
        private Label lRangeSeparator;
        private Label lNoise;
        private Label lMean;
        private Label lStdDev;
        private Panel pEquallySpaced;
        private TextBox eNoise;
        private Panel pTop;
        private ToolTip toolTip;
        private Panel pGaussian;
        private Panel pBimodal;
        private TextBox eStdDev2;
        private TextBox eMean2;
        private Label lMean2;
        private Label lStdDev2;
        private TextBox eStdDev1;
        private TextBox eMean1;
        private TextBox eRangeEnd;
        private TextBox eRangeStart;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel pOptions;
        private Panel pTopIn;
        private TextBox eMode1Weight;
        private Label lMode1Weight;
        private RadioButton rbPerc;
        private RadioButton rbAbsolute;
    }
}
