namespace SiliFish.UI.Controls
{
    partial class DynamicsTestControl
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
            this.picV = new System.Windows.Forms.PictureBox();
            this.eRheobaseLimit = new System.Windows.Forms.NumericUpDown();
            this.lRheobaseLimit = new System.Windows.Forms.Label();
            this.btnRheobase = new System.Windows.Forms.Button();
            this.eInstanceParams = new System.Windows.Forms.TextBox();
            this.ldt = new System.Windows.Forms.Label();
            this.edt = new System.Windows.Forms.NumericUpDown();
            this.ePlotEndTime = new System.Windows.Forms.NumericUpDown();
            this.btnDynamicsRun = new System.Windows.Forms.Button();
            this.eStepEndTime = new System.Windows.Forms.NumericUpDown();
            this.lStepStartTime = new System.Windows.Forms.Label();
            this.eStepStartTime = new System.Windows.Forms.NumericUpDown();
            this.lStepEndTime = new System.Windows.Forms.Label();
            this.lPlotEndTime = new System.Windows.Forms.Label();
            this.picu = new System.Windows.Forms.PictureBox();
            this.picI = new System.Windows.Forms.PictureBox();
            this.grRheobase = new System.Windows.Forms.GroupBox();
            this.eRheobase = new System.Windows.Forms.TextBox();
            this.lRheobase = new System.Windows.Forms.Label();
            this.grTest = new System.Windows.Forms.GroupBox();
            this.stimulusControl1 = new SiliFish.UI.Controls.StimulusControl();
            this.grInstanceParameters = new System.Windows.Forms.GroupBox();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.dgDynamics = new SiliFish.UI.Controls.DistributionDataGrid();
            this.pBottom = new System.Windows.Forms.Panel();
            this.pUseUpdatedParams = new System.Windows.Forms.Panel();
            this.linkUseUpdatedParams = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eRheobaseLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picI)).BeginInit();
            this.grRheobase.SuspendLayout();
            this.grTest.SuspendLayout();
            this.grInstanceParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pBottom.SuspendLayout();
            this.pUseUpdatedParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // picV
            // 
            this.picV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picV.Location = new System.Drawing.Point(0, 0);
            this.picV.Name = "picV";
            this.picV.Size = new System.Drawing.Size(518, 482);
            this.picV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picV.TabIndex = 11;
            this.picV.TabStop = false;
            // 
            // eRheobaseLimit
            // 
            this.eRheobaseLimit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.eRheobaseLimit.Location = new System.Drawing.Point(38, 23);
            this.eRheobaseLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.eRheobaseLimit.Name = "eRheobaseLimit";
            this.eRheobaseLimit.Size = new System.Drawing.Size(54, 23);
            this.eRheobaseLimit.TabIndex = 15;
            this.eRheobaseLimit.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lRheobaseLimit
            // 
            this.lRheobaseLimit.AutoSize = true;
            this.lRheobaseLimit.Location = new System.Drawing.Point(6, 26);
            this.lRheobaseLimit.Name = "lRheobaseLimit";
            this.lRheobaseLimit.Size = new System.Drawing.Size(34, 15);
            this.lRheobaseLimit.TabIndex = 14;
            this.lRheobaseLimit.Text = "Limit";
            // 
            // btnRheobase
            // 
            this.btnRheobase.Location = new System.Drawing.Point(98, 22);
            this.btnRheobase.Name = "btnRheobase";
            this.btnRheobase.Size = new System.Drawing.Size(75, 23);
            this.btnRheobase.TabIndex = 13;
            this.btnRheobase.Text = "Calculate";
            this.btnRheobase.UseVisualStyleBackColor = true;
            this.btnRheobase.Click += new System.EventHandler(this.btnRheobase_Click);
            // 
            // eInstanceParams
            // 
            this.eInstanceParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eInstanceParams.Location = new System.Drawing.Point(3, 19);
            this.eInstanceParams.Multiline = true;
            this.eInstanceParams.Name = "eInstanceParams";
            this.eInstanceParams.ReadOnly = true;
            this.eInstanceParams.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.eInstanceParams.Size = new System.Drawing.Size(234, 324);
            this.eInstanceParams.TabIndex = 12;
            // 
            // ldt
            // 
            this.ldt.AutoSize = true;
            this.ldt.Location = new System.Drawing.Point(6, 183);
            this.ldt.Name = "ldt";
            this.ldt.Size = new System.Drawing.Size(18, 15);
            this.ldt.TabIndex = 10;
            this.ldt.Text = "dt";
            // 
            // edt
            // 
            this.edt.DecimalPlaces = 2;
            this.edt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edt.Location = new System.Drawing.Point(103, 181);
            this.edt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edt.Name = "edt";
            this.edt.Size = new System.Drawing.Size(66, 23);
            this.edt.TabIndex = 11;
            this.edt.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // ePlotEndTime
            // 
            this.ePlotEndTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ePlotEndTime.Location = new System.Drawing.Point(103, 156);
            this.ePlotEndTime.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.ePlotEndTime.Name = "ePlotEndTime";
            this.ePlotEndTime.Size = new System.Drawing.Size(66, 23);
            this.ePlotEndTime.TabIndex = 9;
            this.ePlotEndTime.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // btnDynamicsRun
            // 
            this.btnDynamicsRun.Location = new System.Drawing.Point(102, 210);
            this.btnDynamicsRun.Name = "btnDynamicsRun";
            this.btnDynamicsRun.Size = new System.Drawing.Size(67, 23);
            this.btnDynamicsRun.TabIndex = 3;
            this.btnDynamicsRun.Text = "Run";
            this.btnDynamicsRun.UseVisualStyleBackColor = true;
            this.btnDynamicsRun.Click += new System.EventHandler(this.btnDynamicsRun_Click);
            // 
            // eStepEndTime
            // 
            this.eStepEndTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eStepEndTime.Location = new System.Drawing.Point(103, 132);
            this.eStepEndTime.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.eStepEndTime.Name = "eStepEndTime";
            this.eStepEndTime.Size = new System.Drawing.Size(66, 23);
            this.eStepEndTime.TabIndex = 8;
            this.eStepEndTime.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // lStepStartTime
            // 
            this.lStepStartTime.AutoSize = true;
            this.lStepStartTime.Location = new System.Drawing.Point(6, 110);
            this.lStepStartTime.Name = "lStepStartTime";
            this.lStepStartTime.Size = new System.Drawing.Size(60, 15);
            this.lStepStartTime.TabIndex = 4;
            this.lStepStartTime.Text = "Start Time";
            // 
            // eStepStartTime
            // 
            this.eStepStartTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.eStepStartTime.Location = new System.Drawing.Point(103, 106);
            this.eStepStartTime.Name = "eStepStartTime";
            this.eStepStartTime.Size = new System.Drawing.Size(66, 23);
            this.eStepStartTime.TabIndex = 7;
            this.eStepStartTime.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lStepEndTime
            // 
            this.lStepEndTime.AutoSize = true;
            this.lStepEndTime.Location = new System.Drawing.Point(6, 134);
            this.lStepEndTime.Name = "lStepEndTime";
            this.lStepEndTime.Size = new System.Drawing.Size(56, 15);
            this.lStepEndTime.TabIndex = 5;
            this.lStepEndTime.Text = "End Time";
            // 
            // lPlotEndTime
            // 
            this.lPlotEndTime.AutoSize = true;
            this.lPlotEndTime.Location = new System.Drawing.Point(6, 160);
            this.lPlotEndTime.Name = "lPlotEndTime";
            this.lPlotEndTime.Size = new System.Drawing.Size(80, 15);
            this.lPlotEndTime.TabIndex = 6;
            this.lPlotEndTime.Text = "Plot End Time";
            // 
            // picu
            // 
            this.picu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picu.Location = new System.Drawing.Point(0, 609);
            this.picu.Name = "picu";
            this.picu.Size = new System.Drawing.Size(518, 74);
            this.picu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picu.TabIndex = 13;
            this.picu.TabStop = false;
            // 
            // picI
            // 
            this.picI.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picI.Location = new System.Drawing.Point(0, 482);
            this.picI.Name = "picI";
            this.picI.Size = new System.Drawing.Size(518, 127);
            this.picI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picI.TabIndex = 14;
            this.picI.TabStop = false;
            // 
            // grRheobase
            // 
            this.grRheobase.Controls.Add(this.eRheobase);
            this.grRheobase.Controls.Add(this.lRheobase);
            this.grRheobase.Controls.Add(this.btnRheobase);
            this.grRheobase.Controls.Add(this.eRheobaseLimit);
            this.grRheobase.Controls.Add(this.lRheobaseLimit);
            this.grRheobase.Location = new System.Drawing.Point(6, 19);
            this.grRheobase.Name = "grRheobase";
            this.grRheobase.Size = new System.Drawing.Size(200, 100);
            this.grRheobase.TabIndex = 16;
            this.grRheobase.TabStop = false;
            this.grRheobase.Text = "Calculate Rheobase";
            // 
            // eRheobase
            // 
            this.eRheobase.Location = new System.Drawing.Point(73, 59);
            this.eRheobase.Name = "eRheobase";
            this.eRheobase.ReadOnly = true;
            this.eRheobase.Size = new System.Drawing.Size(100, 23);
            this.eRheobase.TabIndex = 17;
            // 
            // lRheobase
            // 
            this.lRheobase.AutoSize = true;
            this.lRheobase.Location = new System.Drawing.Point(9, 62);
            this.lRheobase.Name = "lRheobase";
            this.lRheobase.Size = new System.Drawing.Size(58, 15);
            this.lRheobase.TabIndex = 16;
            this.lRheobase.Text = "Rheobase";
            // 
            // grTest
            // 
            this.grTest.Controls.Add(this.edt);
            this.grTest.Controls.Add(this.lPlotEndTime);
            this.grTest.Controls.Add(this.lStepEndTime);
            this.grTest.Controls.Add(this.eStepStartTime);
            this.grTest.Controls.Add(this.ePlotEndTime);
            this.grTest.Controls.Add(this.lStepStartTime);
            this.grTest.Controls.Add(this.btnDynamicsRun);
            this.grTest.Controls.Add(this.eStepEndTime);
            this.grTest.Controls.Add(this.stimulusControl1);
            this.grTest.Controls.Add(this.ldt);
            this.grTest.Location = new System.Drawing.Point(6, 125);
            this.grTest.Name = "grTest";
            this.grTest.Size = new System.Drawing.Size(200, 236);
            this.grTest.TabIndex = 17;
            this.grTest.TabStop = false;
            this.grTest.Text = "Test";
            // 
            // stimulusControl1
            // 
            this.stimulusControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.stimulusControl1.Location = new System.Drawing.Point(3, 19);
            this.stimulusControl1.Name = "stimulusControl1";
            this.stimulusControl1.Size = new System.Drawing.Size(194, 93);
            this.stimulusControl1.TabIndex = 19;
            // 
            // grInstanceParameters
            // 
            this.grInstanceParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grInstanceParameters.Controls.Add(this.eInstanceParams);
            this.grInstanceParameters.Location = new System.Drawing.Point(209, 19);
            this.grInstanceParameters.Name = "grInstanceParameters";
            this.grInstanceParameters.Size = new System.Drawing.Size(240, 346);
            this.grInstanceParameters.TabIndex = 18;
            this.grInstanceParameters.TabStop = false;
            this.grInstanceParameters.Text = "Instance Params";
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.dgDynamics);
            this.splitMain.Panel1.Controls.Add(this.pBottom);
            this.splitMain.Panel1.Controls.Add(this.pUseUpdatedParams);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.picV);
            this.splitMain.Panel2.Controls.Add(this.picI);
            this.splitMain.Panel2.Controls.Add(this.picu);
            this.splitMain.Panel2.Resize += new System.EventHandler(this.splitMain_Panel2_Resize);
            this.splitMain.Size = new System.Drawing.Size(974, 683);
            this.splitMain.SplitterDistance = 452;
            this.splitMain.TabIndex = 17;
            // 
            // dgDynamics
            // 
            this.dgDynamics.AutoSize = true;
            this.dgDynamics.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dgDynamics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDynamics.Location = new System.Drawing.Point(0, 30);
            this.dgDynamics.MinimumSize = new System.Drawing.Size(100, 100);
            this.dgDynamics.Name = "dgDynamics";
            this.dgDynamics.Size = new System.Drawing.Size(452, 285);
            this.dgDynamics.TabIndex = 21;
            // 
            // pBottom
            // 
            this.pBottom.Controls.Add(this.grRheobase);
            this.pBottom.Controls.Add(this.grInstanceParameters);
            this.pBottom.Controls.Add(this.grTest);
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(0, 315);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(452, 368);
            this.pBottom.TabIndex = 21;
            // 
            // pUseUpdatedParams
            // 
            this.pUseUpdatedParams.Controls.Add(this.linkUseUpdatedParams);
            this.pUseUpdatedParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.pUseUpdatedParams.Location = new System.Drawing.Point(0, 0);
            this.pUseUpdatedParams.Name = "pUseUpdatedParams";
            this.pUseUpdatedParams.Size = new System.Drawing.Size(452, 30);
            this.pUseUpdatedParams.TabIndex = 20;
            // 
            // linkUseUpdatedParams
            // 
            this.linkUseUpdatedParams.AutoSize = true;
            this.linkUseUpdatedParams.Location = new System.Drawing.Point(3, 9);
            this.linkUseUpdatedParams.Name = "linkUseUpdatedParams";
            this.linkUseUpdatedParams.Size = new System.Drawing.Size(136, 15);
            this.linkUseUpdatedParams.TabIndex = 19;
            this.linkUseUpdatedParams.TabStop = true;
            this.linkUseUpdatedParams.Text = "Use Updated Parameters";
            this.linkUseUpdatedParams.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkUseUpdatedParams_LinkClicked);
            // 
            // DynamicsTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Name = "DynamicsTestControl";
            this.Size = new System.Drawing.Size(974, 683);
            ((System.ComponentModel.ISupportInitialize)(this.picV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eRheobaseLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ePlotEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eStepStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picI)).EndInit();
            this.grRheobase.ResumeLayout(false);
            this.grRheobase.PerformLayout();
            this.grTest.ResumeLayout(false);
            this.grTest.PerformLayout();
            this.grInstanceParameters.ResumeLayout(false);
            this.grInstanceParameters.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel1.PerformLayout();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pBottom.ResumeLayout(false);
            this.pUseUpdatedParams.ResumeLayout(false);
            this.pUseUpdatedParams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox picV;
        private NumericUpDown eRheobaseLimit;
        private Label lRheobaseLimit;
        private Button btnRheobase;
        private TextBox eInstanceParams;
        private Label ldt;
        private NumericUpDown edt;
        private NumericUpDown ePlotEndTime;
        private Button btnDynamicsRun;
        private NumericUpDown eStepEndTime;
        private Label lStepStartTime;
        private NumericUpDown eStepStartTime;
        private Label lStepEndTime;
        private Label lPlotEndTime;
        private PictureBox picu;
        private PictureBox picI;
        private GroupBox grRheobase;
        private TextBox eRheobase;
        private Label lRheobase;
        private GroupBox grTest;
        private GroupBox grInstanceParameters;
        private SplitContainer splitMain;
        private Panel pUseUpdatedParams;
        private LinkLabel linkUseUpdatedParams;
        private DistributionDataGrid dgDynamics;
        private Panel pBottom;
        private StimulusControl stimulusControl1;
    }
}
