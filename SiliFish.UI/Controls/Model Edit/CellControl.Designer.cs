
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
            components = new System.ComponentModel.Container();
            lGroupName = new Label();
            lSomite = new Label();
            eSomite = new NumericUpDown();
            lSequence = new Label();
            eSequence = new NumericUpDown();
            lCellType = new Label();
            pMain = new Panel();
            eDescendingAxon = new TextBox();
            eAscendingAxon = new TextBox();
            lDescendingAxon = new Label();
            lAscendingAxon = new Label();
            ddCellPool = new ComboBox();
            ddCoreType = new ComboBox();
            ddCellType = new ComboBox();
            eZ = new TextBox();
            eY = new TextBox();
            eX = new TextBox();
            lX = new Label();
            lZ = new Label();
            lY = new Label();
            lCoordinates = new Label();
            pMainTop = new Panel();
            linkSaveCell = new LinkLabel();
            linkLoadCell = new LinkLabel();
            cbActive = new CheckBox();
            lCoreType = new Label();
            timeLineControl = new TimeLineControl();
            toolTip1 = new ToolTip(components);
            tabCellPool = new TabControl();
            tDynamics = new TabPage();
            propCore = new PropertyGrid();
            panel1 = new Panel();
            lConductionVelocity = new Label();
            eConductionVelocity = new TextBox();
            pDynamicsTops = new Panel();
            linkLoadCoreUnit = new LinkLabel();
            linkTestDynamics = new LinkLabel();
            tTimeline = new TabPage();
            splitMain = new SplitContainer();
            colorDialog = new ColorDialog();
            openFileJson = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)eSomite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eSequence).BeginInit();
            pMain.SuspendLayout();
            pMainTop.SuspendLayout();
            tabCellPool.SuspendLayout();
            tDynamics.SuspendLayout();
            panel1.SuspendLayout();
            pDynamicsTops.SuspendLayout();
            tTimeline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            SuspendLayout();
            // 
            // lGroupName
            // 
            lGroupName.AutoSize = true;
            lGroupName.Location = new Point(6, 36);
            lGroupName.Name = "lGroupName";
            lGroupName.Size = new Size(75, 15);
            lGroupName.TabIndex = 0;
            lGroupName.Text = "Group Name";
            // 
            // lSomite
            // 
            lSomite.AutoSize = true;
            lSomite.Location = new Point(6, 116);
            lSomite.Name = "lSomite";
            lSomite.Size = new Size(44, 15);
            lSomite.TabIndex = 8;
            lSomite.Text = "Somite";
            // 
            // eSomite
            // 
            eSomite.Location = new Point(87, 114);
            eSomite.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            eSomite.Name = "eSomite";
            eSomite.Size = new Size(64, 23);
            eSomite.TabIndex = 9;
            eSomite.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lSequence
            // 
            lSequence.AutoSize = true;
            lSequence.Location = new Point(6, 143);
            lSequence.Name = "lSequence";
            lSequence.Size = new Size(58, 15);
            lSequence.TabIndex = 13;
            lSequence.Text = "Sequence";
            // 
            // eSequence
            // 
            eSequence.Location = new Point(87, 141);
            eSequence.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            eSequence.Name = "eSequence";
            eSequence.Size = new Size(64, 23);
            eSequence.TabIndex = 14;
            toolTip1.SetToolTip(eSequence, "Used for 2D renderings or predefined 3D renderings");
            eSequence.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lCellType
            // 
            lCellType.AutoSize = true;
            lCellType.Location = new Point(6, 63);
            lCellType.Name = "lCellType";
            lCellType.Size = new Size(54, 15);
            lCellType.TabIndex = 2;
            lCellType.Text = "Cell Type";
            // 
            // pMain
            // 
            pMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pMain.Controls.Add(eDescendingAxon);
            pMain.Controls.Add(eAscendingAxon);
            pMain.Controls.Add(lDescendingAxon);
            pMain.Controls.Add(lAscendingAxon);
            pMain.Controls.Add(ddCellPool);
            pMain.Controls.Add(ddCoreType);
            pMain.Controls.Add(ddCellType);
            pMain.Controls.Add(eZ);
            pMain.Controls.Add(eY);
            pMain.Controls.Add(eX);
            pMain.Controls.Add(lX);
            pMain.Controls.Add(lZ);
            pMain.Controls.Add(lY);
            pMain.Controls.Add(lCoordinates);
            pMain.Controls.Add(lGroupName);
            pMain.Controls.Add(pMainTop);
            pMain.Controls.Add(lSequence);
            pMain.Controls.Add(eSequence);
            pMain.Controls.Add(cbActive);
            pMain.Controls.Add(eSomite);
            pMain.Controls.Add(lSomite);
            pMain.Controls.Add(lCellType);
            pMain.Controls.Add(lCoreType);
            pMain.Dock = DockStyle.Fill;
            pMain.Location = new Point(0, 0);
            pMain.MaximumSize = new Size(400, 0);
            pMain.MinimumSize = new Size(244, 0);
            pMain.Name = "pMain";
            pMain.Size = new Size(258, 721);
            pMain.TabIndex = 0;
            // 
            // eDescendingAxon
            // 
            eDescendingAxon.Location = new Point(133, 247);
            eDescendingAxon.Name = "eDescendingAxon";
            eDescendingAxon.Size = new Size(86, 23);
            eDescendingAxon.TabIndex = 46;
            // 
            // eAscendingAxon
            // 
            eAscendingAxon.Location = new Point(133, 218);
            eAscendingAxon.Name = "eAscendingAxon";
            eAscendingAxon.Size = new Size(86, 23);
            eAscendingAxon.TabIndex = 45;
            // 
            // lDescendingAxon
            // 
            lDescendingAxon.AutoSize = true;
            lDescendingAxon.Location = new Point(6, 251);
            lDescendingAxon.Name = "lDescendingAxon";
            lDescendingAxon.Size = new Size(106, 15);
            lDescendingAxon.TabIndex = 44;
            lDescendingAxon.Text = "Desc. Axon Length";
            // 
            // lAscendingAxon
            // 
            lAscendingAxon.AutoSize = true;
            lAscendingAxon.Location = new Point(6, 226);
            lAscendingAxon.Name = "lAscendingAxon";
            lAscendingAxon.Size = new Size(100, 15);
            lAscendingAxon.TabIndex = 43;
            lAscendingAxon.Text = "Asc. Axon Length";
            // 
            // ddCellPool
            // 
            ddCellPool.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddCellPool.BackColor = Color.WhiteSmoke;
            ddCellPool.DropDownStyle = ComboBoxStyle.DropDownList;
            ddCellPool.FlatStyle = FlatStyle.Flat;
            ddCellPool.FormattingEnabled = true;
            ddCellPool.Location = new Point(87, 33);
            ddCellPool.Name = "ddCellPool";
            ddCellPool.Size = new Size(162, 23);
            ddCellPool.TabIndex = 42;
            // 
            // ddCoreType
            // 
            ddCoreType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddCoreType.BackColor = Color.WhiteSmoke;
            ddCoreType.DropDownStyle = ComboBoxStyle.DropDownList;
            ddCoreType.FlatStyle = FlatStyle.Flat;
            ddCoreType.FormattingEnabled = true;
            ddCoreType.Location = new Point(87, 87);
            ddCoreType.Name = "ddCoreType";
            ddCoreType.Size = new Size(162, 23);
            ddCoreType.TabIndex = 40;
            ddCoreType.SelectedIndexChanged += ddCoreType_SelectedIndexChanged;
            // 
            // ddCellType
            // 
            ddCellType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddCellType.BackColor = Color.WhiteSmoke;
            ddCellType.DropDownStyle = ComboBoxStyle.DropDownList;
            ddCellType.Enabled = false;
            ddCellType.FlatStyle = FlatStyle.Flat;
            ddCellType.FormattingEnabled = true;
            ddCellType.Location = new Point(87, 60);
            ddCellType.Name = "ddCellType";
            ddCellType.Size = new Size(162, 23);
            ddCellType.TabIndex = 39;
            ddCellType.SelectedIndexChanged += ddCellType_SelectedIndexChanged;
            // 
            // eZ
            // 
            eZ.Location = new Point(179, 191);
            eZ.Name = "eZ";
            eZ.Size = new Size(40, 23);
            eZ.TabIndex = 38;
            // 
            // eY
            // 
            eY.Location = new Point(133, 191);
            eY.Name = "eY";
            eY.Size = new Size(40, 23);
            eY.TabIndex = 37;
            // 
            // eX
            // 
            eX.Location = new Point(87, 191);
            eX.Name = "eX";
            eX.Size = new Size(40, 23);
            eX.TabIndex = 36;
            // 
            // lX
            // 
            lX.AutoSize = true;
            lX.Location = new Point(101, 173);
            lX.Name = "lX";
            lX.Size = new Size(14, 15);
            lX.TabIndex = 35;
            lX.Text = "X";
            // 
            // lZ
            // 
            lZ.AutoSize = true;
            lZ.Location = new Point(191, 173);
            lZ.Name = "lZ";
            lZ.Size = new Size(14, 15);
            lZ.TabIndex = 34;
            lZ.Text = "Z";
            // 
            // lY
            // 
            lY.AutoSize = true;
            lY.Location = new Point(147, 173);
            lY.Name = "lY";
            lY.Size = new Size(14, 15);
            lY.TabIndex = 33;
            lY.Text = "Y";
            // 
            // lCoordinates
            // 
            lCoordinates.AutoSize = true;
            lCoordinates.Location = new Point(6, 194);
            lCoordinates.Name = "lCoordinates";
            lCoordinates.Size = new Size(71, 15);
            lCoordinates.TabIndex = 32;
            lCoordinates.Text = "Coordinates";
            toolTip1.SetToolTip(lCoordinates, "Positive Y values are considered as sagittal right, negative values as sagittal left.");
            // 
            // pMainTop
            // 
            pMainTop.BackColor = Color.FromArgb(236, 239, 241);
            pMainTop.Controls.Add(linkSaveCell);
            pMainTop.Controls.Add(linkLoadCell);
            pMainTop.Dock = DockStyle.Top;
            pMainTop.Location = new Point(0, 0);
            pMainTop.Name = "pMainTop";
            pMainTop.Size = new Size(258, 30);
            pMainTop.TabIndex = 10;
            // 
            // linkSaveCell
            // 
            linkSaveCell.AutoSize = true;
            linkSaveCell.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveCell.Location = new Point(72, 9);
            linkSaveCell.Name = "linkSaveCell";
            linkSaveCell.Size = new Size(54, 15);
            linkSaveCell.TabIndex = 9;
            linkSaveCell.TabStop = true;
            linkSaveCell.Text = "Save Cell";
            linkSaveCell.LinkClicked += linkSavePool_LinkClicked;
            // 
            // linkLoadCell
            // 
            linkLoadCell.AutoSize = true;
            linkLoadCell.LinkColor = Color.FromArgb(64, 64, 64);
            linkLoadCell.Location = new Point(6, 9);
            linkLoadCell.Name = "linkLoadCell";
            linkLoadCell.Size = new Size(56, 15);
            linkLoadCell.TabIndex = 8;
            linkLoadCell.TabStop = true;
            linkLoadCell.Text = "Load Cell";
            linkLoadCell.LinkClicked += linkLoadCell_LinkClicked;
            // 
            // cbActive
            // 
            cbActive.AutoSize = true;
            cbActive.Checked = true;
            cbActive.CheckState = CheckState.Checked;
            cbActive.Location = new Point(6, 297);
            cbActive.Name = "cbActive";
            cbActive.Size = new Size(59, 19);
            cbActive.TabIndex = 16;
            cbActive.Text = "Active";
            cbActive.UseVisualStyleBackColor = true;
            // 
            // lCoreType
            // 
            lCoreType.AutoSize = true;
            lCoreType.Location = new Point(6, 90);
            lCoreType.Name = "lCoreType";
            lCoreType.Size = new Size(59, 15);
            lCoreType.TabIndex = 4;
            lCoreType.Text = "Core Type";
            // 
            // timeLineControl
            // 
            timeLineControl.AutoScroll = true;
            timeLineControl.BackColor = Color.FromArgb(236, 239, 241);
            timeLineControl.Dock = DockStyle.Fill;
            timeLineControl.Location = new Point(3, 3);
            timeLineControl.Name = "timeLineControl";
            timeLineControl.Size = new Size(320, 687);
            timeLineControl.TabIndex = 23;
            // 
            // tabCellPool
            // 
            tabCellPool.Controls.Add(tDynamics);
            tabCellPool.Controls.Add(tTimeline);
            tabCellPool.Dock = DockStyle.Fill;
            tabCellPool.Location = new Point(0, 0);
            tabCellPool.Name = "tabCellPool";
            tabCellPool.SelectedIndex = 0;
            tabCellPool.Size = new Size(334, 721);
            tabCellPool.TabIndex = 15;
            // 
            // tDynamics
            // 
            tDynamics.Controls.Add(propCore);
            tDynamics.Controls.Add(panel1);
            tDynamics.Controls.Add(pDynamicsTops);
            tDynamics.Location = new Point(4, 24);
            tDynamics.Name = "tDynamics";
            tDynamics.Padding = new Padding(3);
            tDynamics.Size = new Size(326, 693);
            tDynamics.TabIndex = 1;
            tDynamics.Text = "Dynamics";
            tDynamics.UseVisualStyleBackColor = true;
            // 
            // propCore
            // 
            propCore.Dock = DockStyle.Fill;
            propCore.Location = new Point(3, 72);
            propCore.Name = "propCore";
            propCore.Size = new Size(320, 618);
            propCore.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(lConductionVelocity);
            panel1.Controls.Add(eConductionVelocity);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 36);
            panel1.Name = "panel1";
            panel1.Size = new Size(320, 36);
            panel1.TabIndex = 25;
            // 
            // lConductionVelocity
            // 
            lConductionVelocity.AutoSize = true;
            lConductionVelocity.Location = new Point(3, 9);
            lConductionVelocity.Name = "lConductionVelocity";
            lConductionVelocity.Size = new Size(114, 15);
            lConductionVelocity.TabIndex = 23;
            lConductionVelocity.Text = "Conduction Velocity";
            // 
            // eConductionVelocity
            // 
            eConductionVelocity.Location = new Point(123, 6);
            eConductionVelocity.Name = "eConductionVelocity";
            eConductionVelocity.Size = new Size(100, 23);
            eConductionVelocity.TabIndex = 24;
            // 
            // pDynamicsTops
            // 
            pDynamicsTops.BackColor = Color.FromArgb(236, 239, 241);
            pDynamicsTops.Controls.Add(linkLoadCoreUnit);
            pDynamicsTops.Controls.Add(linkTestDynamics);
            pDynamicsTops.Dock = DockStyle.Top;
            pDynamicsTops.Location = new Point(3, 3);
            pDynamicsTops.Name = "pDynamicsTops";
            pDynamicsTops.Size = new Size(320, 33);
            pDynamicsTops.TabIndex = 0;
            // 
            // linkLoadCoreUnit
            // 
            linkLoadCoreUnit.AutoSize = true;
            linkLoadCoreUnit.LinkColor = Color.FromArgb(64, 64, 64);
            linkLoadCoreUnit.Location = new Point(94, 9);
            linkLoadCoreUnit.Name = "linkLoadCoreUnit";
            linkLoadCoreUnit.Size = new Size(86, 15);
            linkLoadCoreUnit.TabIndex = 22;
            linkLoadCoreUnit.TabStop = true;
            linkLoadCoreUnit.Text = "Load Core Unit";
            linkLoadCoreUnit.LinkClicked += linkLoadCoreUnit_LinkClicked;
            // 
            // linkTestDynamics
            // 
            linkTestDynamics.AutoSize = true;
            linkTestDynamics.LinkColor = Color.FromArgb(64, 64, 64);
            linkTestDynamics.Location = new Point(6, 9);
            linkTestDynamics.Name = "linkTestDynamics";
            linkTestDynamics.Size = new Size(82, 15);
            linkTestDynamics.TabIndex = 9;
            linkTestDynamics.TabStop = true;
            linkTestDynamics.Text = "Test Dynamics";
            linkTestDynamics.LinkClicked += linkTestDynamics_LinkClicked;
            // 
            // tTimeline
            // 
            tTimeline.Controls.Add(timeLineControl);
            tTimeline.Location = new Point(4, 24);
            tTimeline.Name = "tTimeline";
            tTimeline.Padding = new Padding(3);
            tTimeline.Size = new Size(326, 693);
            tTimeline.TabIndex = 2;
            tTimeline.Text = "Timeline";
            tTimeline.UseVisualStyleBackColor = true;
            // 
            // splitMain
            // 
            splitMain.BorderStyle = BorderStyle.FixedSingle;
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 0);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(pMain);
            splitMain.Panel1MinSize = 260;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(tabCellPool);
            splitMain.Size = new Size(600, 723);
            splitMain.SplitterDistance = 260;
            splitMain.TabIndex = 16;
            splitMain.SplitterMoved += splitMain_SplitterMoved;
            // 
            // openFileJson
            // 
            openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // CellControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            Controls.Add(splitMain);
            MinimumSize = new Size(480, 200);
            Name = "CellControl";
            Size = new Size(600, 723);
            ((System.ComponentModel.ISupportInitialize)eSomite).EndInit();
            ((System.ComponentModel.ISupportInitialize)eSequence).EndInit();
            pMain.ResumeLayout(false);
            pMain.PerformLayout();
            pMainTop.ResumeLayout(false);
            pMainTop.PerformLayout();
            tabCellPool.ResumeLayout(false);
            tDynamics.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pDynamicsTops.ResumeLayout(false);
            pDynamicsTops.PerformLayout();
            tTimeline.ResumeLayout(false);
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            ResumeLayout(false);
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
        private Label lDescendingAxon;
        private Label lAscendingAxon;
        private TextBox eDescendingAxon;
        private TextBox eAscendingAxon;
    }
}
