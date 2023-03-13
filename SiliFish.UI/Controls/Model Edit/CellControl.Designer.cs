
namespace SiliFish.UI.Controls
{
    partial class CellControl
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
            this.lGroupName = new System.Windows.Forms.Label();
            this.lSomite = new System.Windows.Forms.Label();
            this.eSomite = new System.Windows.Forms.NumericUpDown();
            this.lSequence = new System.Windows.Forms.Label();
            this.eSequence = new System.Windows.Forms.NumericUpDown();
            this.lCellType = new System.Windows.Forms.Label();
            this.pMain = new System.Windows.Forms.Panel();
            this.ddCellPool = new System.Windows.Forms.ComboBox();
            this.ddCoreType = new System.Windows.Forms.ComboBox();
            this.ddCellType = new System.Windows.Forms.ComboBox();
            this.eZ = new System.Windows.Forms.TextBox();
            this.eY = new System.Windows.Forms.TextBox();
            this.eX = new System.Windows.Forms.TextBox();
            this.lX = new System.Windows.Forms.Label();
            this.lZ = new System.Windows.Forms.Label();
            this.lY = new System.Windows.Forms.Label();
            this.lCoordinates = new System.Windows.Forms.Label();
            this.pMainTop = new System.Windows.Forms.Panel();
            this.linkSaveCell = new System.Windows.Forms.LinkLabel();
            this.linkLoadCell = new System.Windows.Forms.LinkLabel();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.lCoreType = new System.Windows.Forms.Label();
            this.timeLineControl = new SiliFish.UI.Controls.TimeLineControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabCellPool = new System.Windows.Forms.TabControl();
            this.tDynamics = new System.Windows.Forms.TabPage();
            this.propCore = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lConductionVelocity = new System.Windows.Forms.Label();
            this.eConductionVelocity = new System.Windows.Forms.TextBox();
            this.pDynamicsTops = new System.Windows.Forms.Panel();
            this.linkLoadCoreUnit = new System.Windows.Forms.LinkLabel();
            this.linkTestDynamics = new System.Windows.Forms.LinkLabel();
            this.tTimeline = new System.Windows.Forms.TabPage();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileJson = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.eSomite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSequence)).BeginInit();
            this.pMain.SuspendLayout();
            this.pMainTop.SuspendLayout();
            this.tabCellPool.SuspendLayout();
            this.tDynamics.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pDynamicsTops.SuspendLayout();
            this.tTimeline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lGroupName
            // 
            this.lGroupName.AutoSize = true;
            this.lGroupName.Location = new System.Drawing.Point(6, 36);
            this.lGroupName.Name = "lGroupName";
            this.lGroupName.Size = new System.Drawing.Size(75, 15);
            this.lGroupName.TabIndex = 0;
            this.lGroupName.Text = "Group Name";
            // 
            // lSomite
            // 
            this.lSomite.AutoSize = true;
            this.lSomite.Location = new System.Drawing.Point(6, 116);
            this.lSomite.Name = "lSomite";
            this.lSomite.Size = new System.Drawing.Size(44, 15);
            this.lSomite.TabIndex = 8;
            this.lSomite.Text = "Somite";
            // 
            // eSomite
            // 
            this.eSomite.Location = new System.Drawing.Point(87, 114);
            this.eSomite.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.eSomite.Name = "eSomite";
            this.eSomite.Size = new System.Drawing.Size(64, 23);
            this.eSomite.TabIndex = 9;
            this.eSomite.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lSequence
            // 
            this.lSequence.AutoSize = true;
            this.lSequence.Location = new System.Drawing.Point(6, 143);
            this.lSequence.Name = "lSequence";
            this.lSequence.Size = new System.Drawing.Size(58, 15);
            this.lSequence.TabIndex = 13;
            this.lSequence.Text = "Sequence";
            // 
            // eSequence
            // 
            this.eSequence.Location = new System.Drawing.Point(87, 141);
            this.eSequence.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eSequence.Name = "eSequence";
            this.eSequence.Size = new System.Drawing.Size(64, 23);
            this.eSequence.TabIndex = 14;
            this.toolTip1.SetToolTip(this.eSequence, "Used for 2D renderings or predefined 3D renderings");
            this.eSequence.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lCellType
            // 
            this.lCellType.AutoSize = true;
            this.lCellType.Location = new System.Drawing.Point(6, 63);
            this.lCellType.Name = "lCellType";
            this.lCellType.Size = new System.Drawing.Size(54, 15);
            this.lCellType.TabIndex = 2;
            this.lCellType.Text = "Cell Type";
            // 
            // pMain
            // 
            this.pMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pMain.Controls.Add(this.ddCellPool);
            this.pMain.Controls.Add(this.ddCoreType);
            this.pMain.Controls.Add(this.ddCellType);
            this.pMain.Controls.Add(this.eZ);
            this.pMain.Controls.Add(this.eY);
            this.pMain.Controls.Add(this.eX);
            this.pMain.Controls.Add(this.lX);
            this.pMain.Controls.Add(this.lZ);
            this.pMain.Controls.Add(this.lY);
            this.pMain.Controls.Add(this.lCoordinates);
            this.pMain.Controls.Add(this.lGroupName);
            this.pMain.Controls.Add(this.pMainTop);
            this.pMain.Controls.Add(this.lSequence);
            this.pMain.Controls.Add(this.eSequence);
            this.pMain.Controls.Add(this.cbActive);
            this.pMain.Controls.Add(this.eSomite);
            this.pMain.Controls.Add(this.lSomite);
            this.pMain.Controls.Add(this.lCellType);
            this.pMain.Controls.Add(this.lCoreType);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.MaximumSize = new System.Drawing.Size(400, 0);
            this.pMain.MinimumSize = new System.Drawing.Size(244, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(258, 721);
            this.pMain.TabIndex = 0;
            // 
            // ddCellPool
            // 
            this.ddCellPool.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddCellPool.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddCellPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCellPool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddCellPool.FormattingEnabled = true;
            this.ddCellPool.Location = new System.Drawing.Point(87, 33);
            this.ddCellPool.Name = "ddCellPool";
            this.ddCellPool.Size = new System.Drawing.Size(162, 23);
            this.ddCellPool.TabIndex = 42;
            // 
            // ddCoreType
            // 
            this.ddCoreType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddCoreType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddCoreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCoreType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddCoreType.FormattingEnabled = true;
            this.ddCoreType.Location = new System.Drawing.Point(87, 87);
            this.ddCoreType.Name = "ddCoreType";
            this.ddCoreType.Size = new System.Drawing.Size(162, 23);
            this.ddCoreType.TabIndex = 40;
            this.ddCoreType.SelectedIndexChanged += new System.EventHandler(this.ddCoreType_SelectedIndexChanged);
            // 
            // ddCellType
            // 
            this.ddCellType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddCellType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddCellType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCellType.Enabled = false;
            this.ddCellType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddCellType.FormattingEnabled = true;
            this.ddCellType.Location = new System.Drawing.Point(87, 60);
            this.ddCellType.Name = "ddCellType";
            this.ddCellType.Size = new System.Drawing.Size(162, 23);
            this.ddCellType.TabIndex = 39;
            this.ddCellType.SelectedIndexChanged += new System.EventHandler(this.ddCellType_SelectedIndexChanged);
            // 
            // eZ
            // 
            this.eZ.Location = new System.Drawing.Point(179, 191);
            this.eZ.Name = "eZ";
            this.eZ.Size = new System.Drawing.Size(40, 23);
            this.eZ.TabIndex = 38;
            // 
            // eY
            // 
            this.eY.Location = new System.Drawing.Point(133, 191);
            this.eY.Name = "eY";
            this.eY.Size = new System.Drawing.Size(40, 23);
            this.eY.TabIndex = 37;
            // 
            // eX
            // 
            this.eX.Location = new System.Drawing.Point(87, 191);
            this.eX.Name = "eX";
            this.eX.Size = new System.Drawing.Size(40, 23);
            this.eX.TabIndex = 36;
            // 
            // lX
            // 
            this.lX.AutoSize = true;
            this.lX.Location = new System.Drawing.Point(101, 173);
            this.lX.Name = "lX";
            this.lX.Size = new System.Drawing.Size(14, 15);
            this.lX.TabIndex = 35;
            this.lX.Text = "X";
            // 
            // lZ
            // 
            this.lZ.AutoSize = true;
            this.lZ.Location = new System.Drawing.Point(191, 173);
            this.lZ.Name = "lZ";
            this.lZ.Size = new System.Drawing.Size(14, 15);
            this.lZ.TabIndex = 34;
            this.lZ.Text = "Z";
            // 
            // lY
            // 
            this.lY.AutoSize = true;
            this.lY.Location = new System.Drawing.Point(147, 173);
            this.lY.Name = "lY";
            this.lY.Size = new System.Drawing.Size(14, 15);
            this.lY.TabIndex = 33;
            this.lY.Text = "Y";
            // 
            // lCoordinates
            // 
            this.lCoordinates.AutoSize = true;
            this.lCoordinates.Location = new System.Drawing.Point(6, 194);
            this.lCoordinates.Name = "lCoordinates";
            this.lCoordinates.Size = new System.Drawing.Size(71, 15);
            this.lCoordinates.TabIndex = 32;
            this.lCoordinates.Text = "Coordinates";
            this.toolTip1.SetToolTip(this.lCoordinates, "Positive Y values are considered as sagittal right, negative values as sagittal l" +
        "eft.");
            // 
            // pMainTop
            // 
            this.pMainTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pMainTop.Controls.Add(this.linkSaveCell);
            this.pMainTop.Controls.Add(this.linkLoadCell);
            this.pMainTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMainTop.Location = new System.Drawing.Point(0, 0);
            this.pMainTop.Name = "pMainTop";
            this.pMainTop.Size = new System.Drawing.Size(258, 30);
            this.pMainTop.TabIndex = 10;
            // 
            // linkSaveCell
            // 
            this.linkSaveCell.AutoSize = true;
            this.linkSaveCell.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSaveCell.Location = new System.Drawing.Point(72, 9);
            this.linkSaveCell.Name = "linkSaveCell";
            this.linkSaveCell.Size = new System.Drawing.Size(54, 15);
            this.linkSaveCell.TabIndex = 9;
            this.linkSaveCell.TabStop = true;
            this.linkSaveCell.Text = "Save Cell";
            this.linkSaveCell.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSavePool_LinkClicked);
            // 
            // linkLoadCell
            // 
            this.linkLoadCell.AutoSize = true;
            this.linkLoadCell.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLoadCell.Location = new System.Drawing.Point(6, 9);
            this.linkLoadCell.Name = "linkLoadCell";
            this.linkLoadCell.Size = new System.Drawing.Size(56, 15);
            this.linkLoadCell.TabIndex = 8;
            this.linkLoadCell.TabStop = true;
            this.linkLoadCell.Text = "Load Cell";
            this.linkLoadCell.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLoadCell_LinkClicked);
            // 
            // cbActive
            // 
            this.cbActive.AutoSize = true;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(7, 222);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(59, 19);
            this.cbActive.TabIndex = 16;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // lCoreType
            // 
            this.lCoreType.AutoSize = true;
            this.lCoreType.Location = new System.Drawing.Point(6, 90);
            this.lCoreType.Name = "lCoreType";
            this.lCoreType.Size = new System.Drawing.Size(59, 15);
            this.lCoreType.TabIndex = 4;
            this.lCoreType.Text = "Core Type";
            // 
            // timeLineControl
            // 
            this.timeLineControl.AutoScroll = true;
            this.timeLineControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.timeLineControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLineControl.Location = new System.Drawing.Point(3, 3);
            this.timeLineControl.Name = "timeLineControl";
            this.timeLineControl.Size = new System.Drawing.Size(320, 687);
            this.timeLineControl.TabIndex = 23;
            // 
            // tabCellPool
            // 
            this.tabCellPool.Controls.Add(this.tDynamics);
            this.tabCellPool.Controls.Add(this.tTimeline);
            this.tabCellPool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCellPool.Location = new System.Drawing.Point(0, 0);
            this.tabCellPool.Name = "tabCellPool";
            this.tabCellPool.SelectedIndex = 0;
            this.tabCellPool.Size = new System.Drawing.Size(334, 721);
            this.tabCellPool.TabIndex = 15;
            // 
            // tDynamics
            // 
            this.tDynamics.Controls.Add(this.propCore);
            this.tDynamics.Controls.Add(this.panel1);
            this.tDynamics.Controls.Add(this.pDynamicsTops);
            this.tDynamics.Location = new System.Drawing.Point(4, 24);
            this.tDynamics.Name = "tDynamics";
            this.tDynamics.Padding = new System.Windows.Forms.Padding(3);
            this.tDynamics.Size = new System.Drawing.Size(326, 693);
            this.tDynamics.TabIndex = 1;
            this.tDynamics.Text = "Dynamics";
            this.tDynamics.UseVisualStyleBackColor = true;
            // 
            // propCore
            // 
            this.propCore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propCore.Location = new System.Drawing.Point(3, 72);
            this.propCore.Name = "propCore";
            this.propCore.Size = new System.Drawing.Size(320, 618);
            this.propCore.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lConductionVelocity);
            this.panel1.Controls.Add(this.eConductionVelocity);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 36);
            this.panel1.TabIndex = 25;
            // 
            // lConductionVelocity
            // 
            this.lConductionVelocity.AutoSize = true;
            this.lConductionVelocity.Location = new System.Drawing.Point(3, 9);
            this.lConductionVelocity.Name = "lConductionVelocity";
            this.lConductionVelocity.Size = new System.Drawing.Size(114, 15);
            this.lConductionVelocity.TabIndex = 23;
            this.lConductionVelocity.Text = "Conduction Velocity";
            // 
            // eConductionVelocity
            // 
            this.eConductionVelocity.Location = new System.Drawing.Point(123, 6);
            this.eConductionVelocity.Name = "eConductionVelocity";
            this.eConductionVelocity.Size = new System.Drawing.Size(100, 23);
            this.eConductionVelocity.TabIndex = 24;
            // 
            // pDynamicsTops
            // 
            this.pDynamicsTops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pDynamicsTops.Controls.Add(this.linkLoadCoreUnit);
            this.pDynamicsTops.Controls.Add(this.linkTestDynamics);
            this.pDynamicsTops.Dock = System.Windows.Forms.DockStyle.Top;
            this.pDynamicsTops.Location = new System.Drawing.Point(3, 3);
            this.pDynamicsTops.Name = "pDynamicsTops";
            this.pDynamicsTops.Size = new System.Drawing.Size(320, 33);
            this.pDynamicsTops.TabIndex = 0;
            // 
            // linkLoadCoreUnit
            // 
            this.linkLoadCoreUnit.AutoSize = true;
            this.linkLoadCoreUnit.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLoadCoreUnit.Location = new System.Drawing.Point(94, 9);
            this.linkLoadCoreUnit.Name = "linkLoadCoreUnit";
            this.linkLoadCoreUnit.Size = new System.Drawing.Size(86, 15);
            this.linkLoadCoreUnit.TabIndex = 22;
            this.linkLoadCoreUnit.TabStop = true;
            this.linkLoadCoreUnit.Text = "Load Core Unit";
            this.linkLoadCoreUnit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLoadCoreUnit_LinkClicked);
            // 
            // linkTestDynamics
            // 
            this.linkTestDynamics.AutoSize = true;
            this.linkTestDynamics.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkTestDynamics.Location = new System.Drawing.Point(6, 9);
            this.linkTestDynamics.Name = "linkTestDynamics";
            this.linkTestDynamics.Size = new System.Drawing.Size(82, 15);
            this.linkTestDynamics.TabIndex = 9;
            this.linkTestDynamics.TabStop = true;
            this.linkTestDynamics.Text = "Test Dynamics";
            this.linkTestDynamics.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTestDynamics_LinkClicked);
            // 
            // tTimeline
            // 
            this.tTimeline.Controls.Add(this.timeLineControl);
            this.tTimeline.Location = new System.Drawing.Point(4, 24);
            this.tTimeline.Name = "tTimeline";
            this.tTimeline.Padding = new System.Windows.Forms.Padding(3);
            this.tTimeline.Size = new System.Drawing.Size(326, 693);
            this.tTimeline.TabIndex = 2;
            this.tTimeline.Text = "Timeline";
            this.tTimeline.UseVisualStyleBackColor = true;
            // 
            // splitMain
            // 
            this.splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pMain);
            this.splitMain.Panel1MinSize = 260;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.tabCellPool);
            this.splitMain.Size = new System.Drawing.Size(600, 723);
            this.splitMain.SplitterDistance = 260;
            this.splitMain.TabIndex = 16;
            // 
            // openFileJson
            // 
            this.openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // CellControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitMain);
            this.MinimumSize = new System.Drawing.Size(480, 200);
            this.Name = "CellControl";
            this.Size = new System.Drawing.Size(600, 723);
            ((System.ComponentModel.ISupportInitialize)(this.eSomite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eSequence)).EndInit();
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            this.pMainTop.ResumeLayout(false);
            this.pMainTop.PerformLayout();
            this.tabCellPool.ResumeLayout(false);
            this.tDynamics.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pDynamicsTops.ResumeLayout(false);
            this.pDynamicsTops.PerformLayout();
            this.tTimeline.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lGroupName;
        private Label lSomite;
        private NumericUpDown eSomite;
        private Label lSequence;
        private NumericUpDown eSequence;
        private Label lCellType;
        private Panel pMain;
        private Panel pMainTop;
        private LinkLabel linkSaveCell;
        private LinkLabel linkLoadCell;
        private ToolTip toolTip1;
        private TabControl tabCellPool;
        private TabPage tDynamics;
        private SplitContainer splitMain;
        private CheckBox cbActive;
        private TimeLineControl timeLineControl;
        private ColorDialog colorDialog;
        private Panel pDynamicsTops;
        private LinkLabel linkTestDynamics;
        private TabPage tTimeline;
        private OpenFileDialog openFileJson;
        private Label lCoreType;
        private LinkLabel linkLoadCoreUnit;
        private TextBox eConductionVelocity;
        private Label lConductionVelocity;
        private TextBox eZ;
        private TextBox eY;
        private TextBox eX;
        private Label lX;
        private Label lZ;
        private Label lY;
        private Label lCoordinates;
        private PropertyGrid propCore;
        private Panel panel1;
        private ComboBox ddCoreType;
        private ComboBox ddCellType;
        private ComboBox ddCellPool;
    }
}
