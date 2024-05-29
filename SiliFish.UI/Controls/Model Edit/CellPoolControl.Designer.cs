
namespace SiliFish.UI.Controls
{
    partial class CellPoolControl
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
            eGroupName = new TextBox();
            lNumOfCells = new Label();
            eNumOfCells = new NumericUpDown();
            l2DRenderColumn = new Label();
            e2DColumn = new NumericUpDown();
            ddCellType = new ComboBox();
            lCellType = new Label();
            pZAxis = new Panel();
            lZAxis = new Label();
            pXAxis = new Panel();
            lXAxis = new Label();
            pMain = new Panel();
            pMainInfo = new Panel();
            ddBodyPosition = new ComboBox();
            lBodyPosition = new Label();
            eSomiteRange = new TextBox();
            cbAllSomites = new CheckBox();
            ddCoreType = new ComboBox();
            lCoreType = new Label();
            ddSelection = new ComboBox();
            cbActive = new CheckBox();
            ddNeuronClass = new ComboBox();
            btnColor = new Button();
            lNeuronClass = new Label();
            eDescription = new RichTextBox();
            lDescription = new Label();
            pMainTop = new Panel();
            linkSavePool = new LinkLabel();
            linkLoadPool = new LinkLabel();
            timeLineControl = new TimeLineControl();
            lSagittalPosition = new Label();
            ddSagittalPosition = new ComboBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            distributionX = new DistributionControl();
            groupBox1 = new GroupBox();
            rbYZLinear = new RadioButton();
            rbYZAngular = new RadioButton();
            panel1 = new Panel();
            lYAxis = new Label();
            distributionY = new DistributionControl();
            distributionZ = new DistributionControl();
            toolTip1 = new ToolTip(components);
            tabCellPool = new TabControl();
            tSpatialDist = new TabPage();
            tProjections = new TabPage();
            flowLayoutPanel2 = new FlowLayoutPanel();
            pProjectionAscending = new Panel();
            cbAscendingAxon = new CheckBox();
            distributionAscending = new DistributionControl();
            pProjectionDescending = new Panel();
            cbDescendingAxon = new CheckBox();
            distributionDescending = new DistributionControl();
            tDynamics = new TabPage();
            dgDynamics = new DistributionDataGrid();
            panel2 = new Panel();
            lRheobaseDescription = new Label();
            eRheobase = new TextBox();
            lRheobase = new Label();
            grConductionVelocity = new GroupBox();
            distConductionVelocity = new DistributionControl();
            pDynamicsTops = new Panel();
            linkLoadCoreUnit = new LinkLabel();
            linkTestDynamics = new LinkLabel();
            tTimeline = new TabPage();
            splitMain = new SplitContainer();
            colorDialog = new ColorDialog();
            openFileJson = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)eNumOfCells).BeginInit();
            ((System.ComponentModel.ISupportInitialize)e2DColumn).BeginInit();
            pZAxis.SuspendLayout();
            pXAxis.SuspendLayout();
            pMain.SuspendLayout();
            pMainInfo.SuspendLayout();
            pMainTop.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            tabCellPool.SuspendLayout();
            tSpatialDist.SuspendLayout();
            tProjections.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            pProjectionAscending.SuspendLayout();
            pProjectionDescending.SuspendLayout();
            tDynamics.SuspendLayout();
            panel2.SuspendLayout();
            grConductionVelocity.SuspendLayout();
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
            lGroupName.Location = new Point(7, 10);
            lGroupName.Name = "lGroupName";
            lGroupName.Size = new Size(75, 15);
            lGroupName.TabIndex = 0;
            lGroupName.Text = "Group Name";
            // 
            // eGroupName
            // 
            eGroupName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eGroupName.Location = new Point(114, 7);
            eGroupName.Name = "eGroupName";
            eGroupName.Size = new Size(146, 23);
            eGroupName.TabIndex = 1;
            eGroupName.TextChanged += eGroupName_TextChanged;
            // 
            // lNumOfCells
            // 
            lNumOfCells.AutoSize = true;
            lNumOfCells.Location = new Point(7, 145);
            lNumOfCells.Name = "lNumOfCells";
            lNumOfCells.Size = new Size(56, 15);
            lNumOfCells.TabIndex = 8;
            lNumOfCells.Text = "# of Cells";
            // 
            // eNumOfCells
            // 
            eNumOfCells.Location = new Point(114, 142);
            eNumOfCells.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            eNumOfCells.Name = "eNumOfCells";
            eNumOfCells.Size = new Size(64, 23);
            eNumOfCells.TabIndex = 9;
            eNumOfCells.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // l2DRenderColumn
            // 
            l2DRenderColumn.AutoSize = true;
            l2DRenderColumn.Location = new Point(7, 172);
            l2DRenderColumn.Name = "l2DRenderColumn";
            l2DRenderColumn.Size = new Size(93, 15);
            l2DRenderColumn.TabIndex = 13;
            l2DRenderColumn.Text = "2D Rend. Spread";
            // 
            // e2DColumn
            // 
            e2DColumn.Location = new Point(114, 169);
            e2DColumn.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            e2DColumn.Name = "e2DColumn";
            e2DColumn.Size = new Size(64, 23);
            e2DColumn.TabIndex = 14;
            toolTip1.SetToolTip(e2DColumn, "Used for 2D renderings or predefined 3D models");
            e2DColumn.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // ddCellType
            // 
            ddCellType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddCellType.BackColor = Color.WhiteSmoke;
            ddCellType.DropDownStyle = ComboBoxStyle.DropDownList;
            ddCellType.FlatStyle = FlatStyle.Flat;
            ddCellType.FormattingEnabled = true;
            ddCellType.Location = new Point(114, 34);
            ddCellType.Name = "ddCellType";
            ddCellType.Size = new Size(146, 23);
            ddCellType.TabIndex = 3;
            ddCellType.SelectedIndexChanged += ddCellType_SelectedIndexChanged;
            // 
            // lCellType
            // 
            lCellType.AutoSize = true;
            lCellType.Location = new Point(7, 37);
            lCellType.Name = "lCellType";
            lCellType.Size = new Size(54, 15);
            lCellType.TabIndex = 2;
            lCellType.Text = "Cell Type";
            // 
            // pZAxis
            // 
            pZAxis.Controls.Add(lZAxis);
            pZAxis.Dock = DockStyle.Top;
            pZAxis.Location = new Point(0, 340);
            pZAxis.Margin = new Padding(0);
            pZAxis.Name = "pZAxis";
            pZAxis.Size = new Size(211, 30);
            pZAxis.TabIndex = 17;
            // 
            // lZAxis
            // 
            lZAxis.AutoSize = true;
            lZAxis.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lZAxis.Location = new Point(18, 8);
            lZAxis.Name = "lZAxis";
            lZAxis.Size = new Size(147, 15);
            lZAxis.TabIndex = 0;
            lZAxis.Text = "Z-Axis (Dorsal -> Ventral)";
            // 
            // pXAxis
            // 
            pXAxis.Controls.Add(lXAxis);
            pXAxis.Dock = DockStyle.Top;
            pXAxis.Location = new Point(0, 0);
            pXAxis.Margin = new Padding(0);
            pXAxis.Name = "pXAxis";
            pXAxis.Size = new Size(211, 30);
            pXAxis.TabIndex = 13;
            // 
            // lXAxis
            // 
            lXAxis.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lXAxis.Location = new Point(18, 8);
            lXAxis.Name = "lXAxis";
            lXAxis.Size = new Size(171, 15);
            lXAxis.TabIndex = 0;
            lXAxis.Text = "X-Axis (Rostral -> Caudal)";
            // 
            // pMain
            // 
            pMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pMain.Controls.Add(pMainInfo);
            pMain.Controls.Add(eDescription);
            pMain.Controls.Add(lDescription);
            pMain.Controls.Add(pMainTop);
            pMain.Dock = DockStyle.Fill;
            pMain.Location = new Point(0, 0);
            pMain.MaximumSize = new Size(400, 0);
            pMain.MinimumSize = new Size(244, 0);
            pMain.Name = "pMain";
            pMain.Size = new Size(264, 585);
            pMain.TabIndex = 0;
            // 
            // pMainInfo
            // 
            pMainInfo.Controls.Add(lGroupName);
            pMainInfo.Controls.Add(ddBodyPosition);
            pMainInfo.Controls.Add(eGroupName);
            pMainInfo.Controls.Add(lBodyPosition);
            pMainInfo.Controls.Add(l2DRenderColumn);
            pMainInfo.Controls.Add(eSomiteRange);
            pMainInfo.Controls.Add(e2DColumn);
            pMainInfo.Controls.Add(cbAllSomites);
            pMainInfo.Controls.Add(eNumOfCells);
            pMainInfo.Controls.Add(ddCoreType);
            pMainInfo.Controls.Add(lCellType);
            pMainInfo.Controls.Add(lCoreType);
            pMainInfo.Controls.Add(ddCellType);
            pMainInfo.Controls.Add(lNumOfCells);
            pMainInfo.Controls.Add(ddSelection);
            pMainInfo.Controls.Add(cbActive);
            pMainInfo.Controls.Add(ddNeuronClass);
            pMainInfo.Controls.Add(btnColor);
            pMainInfo.Controls.Add(lNeuronClass);
            pMainInfo.Dock = DockStyle.Top;
            pMainInfo.Location = new Point(0, 30);
            pMainInfo.Name = "pMainInfo";
            pMainInfo.Size = new Size(264, 257);
            pMainInfo.TabIndex = 29;
            // 
            // ddBodyPosition
            // 
            ddBodyPosition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddBodyPosition.BackColor = Color.WhiteSmoke;
            ddBodyPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            ddBodyPosition.FlatStyle = FlatStyle.Flat;
            ddBodyPosition.FormattingEnabled = true;
            ddBodyPosition.Location = new Point(114, 115);
            ddBodyPosition.Name = "ddBodyPosition";
            ddBodyPosition.Size = new Size(146, 23);
            ddBodyPosition.TabIndex = 28;
            // 
            // lBodyPosition
            // 
            lBodyPosition.AutoSize = true;
            lBodyPosition.Location = new Point(7, 118);
            lBodyPosition.Name = "lBodyPosition";
            lBodyPosition.Size = new Size(80, 15);
            lBodyPosition.TabIndex = 27;
            lBodyPosition.Text = "Body Position";
            // 
            // eSomiteRange
            // 
            eSomiteRange.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            eSomiteRange.Location = new Point(114, 196);
            eSomiteRange.Name = "eSomiteRange";
            eSomiteRange.ReadOnly = true;
            eSomiteRange.Size = new Size(146, 23);
            eSomiteRange.TabIndex = 12;
            eSomiteRange.Text = "Enter a range. Ex: 1, 3-5";
            toolTip1.SetToolTip(eSomiteRange, "Enter a range. Ex: 1, 3-5");
            // 
            // cbAllSomites
            // 
            cbAllSomites.AutoSize = true;
            cbAllSomites.Checked = true;
            cbAllSomites.CheckState = CheckState.Checked;
            cbAllSomites.Location = new Point(7, 199);
            cbAllSomites.Name = "cbAllSomites";
            cbAllSomites.Size = new Size(85, 19);
            cbAllSomites.TabIndex = 11;
            cbAllSomites.Text = "All Somites";
            cbAllSomites.UseVisualStyleBackColor = true;
            cbAllSomites.CheckedChanged += cbAllSomites_CheckedChanged;
            // 
            // ddCoreType
            // 
            ddCoreType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddCoreType.BackColor = Color.WhiteSmoke;
            ddCoreType.DropDownStyle = ComboBoxStyle.DropDownList;
            ddCoreType.FlatStyle = FlatStyle.Flat;
            ddCoreType.FormattingEnabled = true;
            ddCoreType.Location = new Point(114, 61);
            ddCoreType.Name = "ddCoreType";
            ddCoreType.Size = new Size(146, 23);
            ddCoreType.TabIndex = 5;
            ddCoreType.SelectedIndexChanged += ddCoreType_SelectedIndexChanged;
            // 
            // lCoreType
            // 
            lCoreType.AutoSize = true;
            lCoreType.Location = new Point(7, 64);
            lCoreType.Name = "lCoreType";
            lCoreType.Size = new Size(59, 15);
            lCoreType.TabIndex = 4;
            lCoreType.Text = "Core Type";
            // 
            // ddSelection
            // 
            ddSelection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            ddSelection.FormattingEnabled = true;
            ddSelection.Location = new Point(185, 141);
            ddSelection.Name = "ddSelection";
            ddSelection.Size = new Size(75, 23);
            ddSelection.TabIndex = 10;
            // 
            // cbActive
            // 
            cbActive.AutoSize = true;
            cbActive.Checked = true;
            cbActive.CheckState = CheckState.Checked;
            cbActive.Location = new Point(201, 225);
            cbActive.Name = "cbActive";
            cbActive.Size = new Size(59, 19);
            cbActive.TabIndex = 16;
            cbActive.Text = "Active";
            cbActive.UseVisualStyleBackColor = true;
            // 
            // ddNeuronClass
            // 
            ddNeuronClass.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ddNeuronClass.BackColor = Color.WhiteSmoke;
            ddNeuronClass.DropDownStyle = ComboBoxStyle.DropDownList;
            ddNeuronClass.FlatStyle = FlatStyle.Flat;
            ddNeuronClass.FormattingEnabled = true;
            ddNeuronClass.Location = new Point(114, 88);
            ddNeuronClass.Name = "ddNeuronClass";
            ddNeuronClass.Size = new Size(146, 23);
            ddNeuronClass.TabIndex = 7;
            // 
            // btnColor
            // 
            btnColor.Location = new Point(185, 169);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(75, 23);
            btnColor.TabIndex = 15;
            btnColor.Text = "Color";
            btnColor.UseVisualStyleBackColor = true;
            btnColor.Click += btnColor_Click;
            // 
            // lNeuronClass
            // 
            lNeuronClass.AutoSize = true;
            lNeuronClass.Location = new Point(7, 91);
            lNeuronClass.Name = "lNeuronClass";
            lNeuronClass.Size = new Size(77, 15);
            lNeuronClass.TabIndex = 6;
            lNeuronClass.Text = "Neuron Class";
            // 
            // eDescription
            // 
            eDescription.AcceptsTab = true;
            eDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            eDescription.Location = new Point(5, 308);
            eDescription.Name = "eDescription";
            eDescription.Size = new Size(253, 273);
            eDescription.TabIndex = 17;
            eDescription.Text = "";
            // 
            // lDescription
            // 
            lDescription.AutoSize = true;
            lDescription.Location = new Point(3, 290);
            lDescription.Name = "lDescription";
            lDescription.Size = new Size(67, 15);
            lDescription.TabIndex = 26;
            lDescription.Text = "Description";
            // 
            // pMainTop
            // 
            pMainTop.BackColor = Color.FromArgb(236, 239, 241);
            pMainTop.Controls.Add(linkSavePool);
            pMainTop.Controls.Add(linkLoadPool);
            pMainTop.Dock = DockStyle.Top;
            pMainTop.Location = new Point(0, 0);
            pMainTop.Name = "pMainTop";
            pMainTop.Size = new Size(264, 30);
            pMainTop.TabIndex = 10;
            // 
            // linkSavePool
            // 
            linkSavePool.AutoSize = true;
            linkSavePool.LinkColor = Color.FromArgb(64, 64, 64);
            linkSavePool.Location = new Point(72, 9);
            linkSavePool.Name = "linkSavePool";
            linkSavePool.Size = new Size(58, 15);
            linkSavePool.TabIndex = 9;
            linkSavePool.TabStop = true;
            linkSavePool.Text = "Save Pool";
            linkSavePool.LinkClicked += linkSavePool_LinkClicked;
            // 
            // linkLoadPool
            // 
            linkLoadPool.AutoSize = true;
            linkLoadPool.LinkColor = Color.FromArgb(64, 64, 64);
            linkLoadPool.Location = new Point(6, 9);
            linkLoadPool.Name = "linkLoadPool";
            linkLoadPool.Size = new Size(60, 15);
            linkLoadPool.TabIndex = 8;
            linkLoadPool.TabStop = true;
            linkLoadPool.Text = "Load Pool";
            linkLoadPool.LinkClicked += linkLoadPool_LinkClicked;
            // 
            // timeLineControl
            // 
            timeLineControl.AutoScroll = true;
            timeLineControl.BackColor = Color.FromArgb(236, 239, 241);
            timeLineControl.Dock = DockStyle.Fill;
            timeLineControl.Location = new Point(3, 3);
            timeLineControl.Name = "timeLineControl";
            timeLineControl.Size = new Size(314, 551);
            timeLineControl.TabIndex = 23;
            // 
            // lSagittalPosition
            // 
            lSagittalPosition.AutoSize = true;
            lSagittalPosition.Location = new Point(0, 51);
            lSagittalPosition.Name = "lSagittalPosition";
            lSagittalPosition.Size = new Size(71, 15);
            lSagittalPosition.TabIndex = 10;
            lSagittalPosition.Text = "Sagittal Pos.";
            // 
            // ddSagittalPosition
            // 
            ddSagittalPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            ddSagittalPosition.FormattingEnabled = true;
            ddSagittalPosition.Location = new Point(84, 47);
            ddSagittalPosition.Name = "ddSagittalPosition";
            ddSagittalPosition.Size = new Size(121, 23);
            ddSagittalPosition.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(pXAxis);
            flowLayoutPanel1.Controls.Add(distributionX);
            flowLayoutPanel1.Controls.Add(groupBox1);
            flowLayoutPanel1.Controls.Add(panel1);
            flowLayoutPanel1.Controls.Add(distributionY);
            flowLayoutPanel1.Controls.Add(pZAxis);
            flowLayoutPanel1.Controls.Add(distributionZ);
            flowLayoutPanel1.Dock = DockStyle.Left;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(217, 551);
            flowLayoutPanel1.TabIndex = 8;
            // 
            // distributionX
            // 
            distributionX.Absolute = false;
            distributionX.Angular = false;
            distributionX.AutoSize = true;
            distributionX.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            distributionX.BackColor = Color.White;
            distributionX.BorderStyle = BorderStyle.Fixed3D;
            distributionX.DefaultConstant = false;
            distributionX.Dock = DockStyle.Top;
            flowLayoutPanel1.SetFlowBreak(distributionX, true);
            distributionX.Location = new Point(3, 33);
            distributionX.MinimumSize = new Size(204, 0);
            distributionX.Name = "distributionX";
            distributionX.NoneIncluded = false;
            distributionX.Size = new Size(211, 91);
            distributionX.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbYZLinear);
            groupBox1.Controls.Add(rbYZAngular);
            groupBox1.Controls.Add(ddSagittalPosition);
            groupBox1.Controls.Add(lSagittalPosition);
            flowLayoutPanel1.SetFlowBreak(groupBox1, true);
            groupBox1.Location = new Point(3, 130);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(211, 80);
            groupBox1.TabIndex = 22;
            groupBox1.TabStop = false;
            groupBox1.Text = "Y-Z Distribution";
            // 
            // rbYZLinear
            // 
            rbYZLinear.AutoSize = true;
            rbYZLinear.Checked = true;
            rbYZLinear.Location = new Point(6, 22);
            rbYZLinear.Name = "rbYZLinear";
            rbYZLinear.Size = new Size(57, 19);
            rbYZLinear.TabIndex = 0;
            rbYZLinear.TabStop = true;
            rbYZLinear.Text = "Linear";
            rbYZLinear.UseVisualStyleBackColor = true;
            // 
            // rbYZAngular
            // 
            rbYZAngular.AutoSize = true;
            rbYZAngular.Location = new Point(95, 22);
            rbYZAngular.Name = "rbYZAngular";
            rbYZAngular.Size = new Size(67, 19);
            rbYZAngular.TabIndex = 1;
            rbYZAngular.Text = "Angular";
            rbYZAngular.UseVisualStyleBackColor = true;
            rbYZAngular.CheckedChanged += rbYZAngular_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(lYAxis);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 213);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(211, 30);
            panel1.TabIndex = 23;
            // 
            // lYAxis
            // 
            lYAxis.AutoSize = true;
            lYAxis.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lYAxis.Location = new Point(18, 8);
            lYAxis.Name = "lYAxis";
            lYAxis.Size = new Size(147, 15);
            lYAxis.TabIndex = 0;
            lYAxis.Text = "Y-Axis (Medial -> Lateral)";
            // 
            // distributionY
            // 
            distributionY.Absolute = false;
            distributionY.Angular = false;
            distributionY.AutoSize = true;
            distributionY.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            distributionY.BackColor = Color.White;
            distributionY.BorderStyle = BorderStyle.Fixed3D;
            distributionY.DefaultConstant = false;
            distributionY.Dock = DockStyle.Top;
            flowLayoutPanel1.SetFlowBreak(distributionY, true);
            distributionY.Location = new Point(3, 246);
            distributionY.MinimumSize = new Size(204, 0);
            distributionY.Name = "distributionY";
            distributionY.NoneIncluded = false;
            distributionY.Size = new Size(211, 91);
            distributionY.TabIndex = 1;
            // 
            // distributionZ
            // 
            distributionZ.Absolute = false;
            distributionZ.Angular = false;
            distributionZ.AutoSize = true;
            distributionZ.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            distributionZ.BackColor = Color.White;
            distributionZ.BorderStyle = BorderStyle.Fixed3D;
            distributionZ.DefaultConstant = false;
            distributionZ.Dock = DockStyle.Top;
            flowLayoutPanel1.SetFlowBreak(distributionZ, true);
            distributionZ.Location = new Point(3, 373);
            distributionZ.MinimumSize = new Size(204, 0);
            distributionZ.Name = "distributionZ";
            distributionZ.NoneIncluded = false;
            distributionZ.Size = new Size(211, 91);
            distributionZ.TabIndex = 2;
            // 
            // tabCellPool
            // 
            tabCellPool.Controls.Add(tSpatialDist);
            tabCellPool.Controls.Add(tProjections);
            tabCellPool.Controls.Add(tDynamics);
            tabCellPool.Controls.Add(tTimeline);
            tabCellPool.Dock = DockStyle.Fill;
            tabCellPool.Location = new Point(0, 0);
            tabCellPool.Name = "tabCellPool";
            tabCellPool.SelectedIndex = 0;
            tabCellPool.Size = new Size(328, 585);
            tabCellPool.TabIndex = 15;
            // 
            // tSpatialDist
            // 
            tSpatialDist.Controls.Add(flowLayoutPanel1);
            tSpatialDist.Location = new Point(4, 24);
            tSpatialDist.Name = "tSpatialDist";
            tSpatialDist.Padding = new Padding(3);
            tSpatialDist.Size = new Size(320, 557);
            tSpatialDist.TabIndex = 0;
            tSpatialDist.Text = "Spatial Dist.";
            tSpatialDist.UseVisualStyleBackColor = true;
            // 
            // tProjections
            // 
            tProjections.Controls.Add(flowLayoutPanel2);
            tProjections.Location = new Point(4, 24);
            tProjections.Name = "tProjections";
            tProjections.Padding = new Padding(3);
            tProjections.Size = new Size(320, 557);
            tProjections.TabIndex = 4;
            tProjections.Text = "Projections";
            tProjections.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(pProjectionAscending);
            flowLayoutPanel2.Controls.Add(distributionAscending);
            flowLayoutPanel2.Controls.Add(pProjectionDescending);
            flowLayoutPanel2.Controls.Add(distributionDescending);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(314, 551);
            flowLayoutPanel2.TabIndex = 15;
            // 
            // pProjectionAscending
            // 
            pProjectionAscending.Controls.Add(cbAscendingAxon);
            pProjectionAscending.Dock = DockStyle.Top;
            pProjectionAscending.Location = new Point(0, 0);
            pProjectionAscending.Margin = new Padding(0);
            pProjectionAscending.Name = "pProjectionAscending";
            pProjectionAscending.Size = new Size(211, 28);
            pProjectionAscending.TabIndex = 14;
            // 
            // cbAscendingAxon
            // 
            cbAscendingAxon.AutoSize = true;
            cbAscendingAxon.Checked = true;
            cbAscendingAxon.CheckState = CheckState.Checked;
            cbAscendingAxon.Location = new Point(3, 3);
            cbAscendingAxon.Name = "cbAscendingAxon";
            cbAscendingAxon.Size = new Size(113, 19);
            cbAscendingAxon.TabIndex = 0;
            cbAscendingAxon.Text = "Ascending Axon";
            cbAscendingAxon.UseVisualStyleBackColor = true;
            cbAscendingAxon.CheckedChanged += cbAscendingAxon_CheckedChanged;
            // 
            // distributionAscending
            // 
            distributionAscending.Absolute = true;
            distributionAscending.Angular = false;
            distributionAscending.AutoSize = true;
            distributionAscending.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            distributionAscending.BackColor = Color.White;
            distributionAscending.BorderStyle = BorderStyle.Fixed3D;
            distributionAscending.DefaultConstant = true;
            distributionAscending.Location = new Point(3, 31);
            distributionAscending.MinimumSize = new Size(204, 0);
            distributionAscending.Name = "distributionAscending";
            distributionAscending.NoneIncluded = false;
            distributionAscending.Size = new Size(211, 117);
            distributionAscending.TabIndex = 0;
            // 
            // pProjectionDescending
            // 
            pProjectionDescending.Controls.Add(cbDescendingAxon);
            pProjectionDescending.Dock = DockStyle.Top;
            pProjectionDescending.Location = new Point(0, 151);
            pProjectionDescending.Margin = new Padding(0);
            pProjectionDescending.Name = "pProjectionDescending";
            pProjectionDescending.Size = new Size(211, 28);
            pProjectionDescending.TabIndex = 15;
            // 
            // cbDescendingAxon
            // 
            cbDescendingAxon.AutoSize = true;
            cbDescendingAxon.Checked = true;
            cbDescendingAxon.CheckState = CheckState.Checked;
            cbDescendingAxon.Location = new Point(3, 3);
            cbDescendingAxon.Name = "cbDescendingAxon";
            cbDescendingAxon.Size = new Size(119, 19);
            cbDescendingAxon.TabIndex = 0;
            cbDescendingAxon.Text = "Descending Axon";
            cbDescendingAxon.UseVisualStyleBackColor = true;
            cbDescendingAxon.CheckedChanged += cbDescendingAxon_CheckedChanged;
            // 
            // distributionDescending
            // 
            distributionDescending.Absolute = true;
            distributionDescending.Angular = false;
            distributionDescending.AutoSize = true;
            distributionDescending.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            distributionDescending.BackColor = Color.White;
            distributionDescending.BorderStyle = BorderStyle.Fixed3D;
            distributionDescending.DefaultConstant = true;
            distributionDescending.Location = new Point(3, 182);
            distributionDescending.MinimumSize = new Size(204, 0);
            distributionDescending.Name = "distributionDescending";
            distributionDescending.NoneIncluded = false;
            distributionDescending.Size = new Size(211, 117);
            distributionDescending.TabIndex = 16;
            // 
            // tDynamics
            // 
            tDynamics.Controls.Add(dgDynamics);
            tDynamics.Controls.Add(panel2);
            tDynamics.Controls.Add(grConductionVelocity);
            tDynamics.Controls.Add(pDynamicsTops);
            tDynamics.Location = new Point(4, 24);
            tDynamics.Name = "tDynamics";
            tDynamics.Padding = new Padding(3);
            tDynamics.Size = new Size(320, 557);
            tDynamics.TabIndex = 1;
            tDynamics.Text = "Dynamics";
            tDynamics.UseVisualStyleBackColor = true;
            // 
            // dgDynamics
            // 
            dgDynamics.AutoSize = true;
            dgDynamics.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dgDynamics.BackColor = Color.WhiteSmoke;
            dgDynamics.Dock = DockStyle.Fill;
            dgDynamics.Location = new Point(3, 182);
            dgDynamics.MinimumSize = new Size(100, 100);
            dgDynamics.Name = "dgDynamics";
            dgDynamics.Size = new Size(314, 372);
            dgDynamics.TabIndex = 3;
            dgDynamics.Leave += dgDynamics_Leave;
            // 
            // panel2
            // 
            panel2.Controls.Add(lRheobaseDescription);
            panel2.Controls.Add(eRheobase);
            panel2.Controls.Add(lRheobase);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(3, 146);
            panel2.Name = "panel2";
            panel2.Size = new Size(314, 36);
            panel2.TabIndex = 4;
            // 
            // lRheobaseDescription
            // 
            lRheobaseDescription.Font = new Font("Segoe UI", 7F);
            lRheobaseDescription.Location = new Point(146, 4);
            lRheobaseDescription.Name = "lRheobaseDescription";
            lRheobaseDescription.Size = new Size(117, 31);
            lRheobaseDescription.TabIndex = 2;
            lRheobaseDescription.Text = "(for average values of the parameters below)";
            // 
            // eRheobase
            // 
            eRheobase.Location = new Point(84, 7);
            eRheobase.Name = "eRheobase";
            eRheobase.ReadOnly = true;
            eRheobase.Size = new Size(56, 23);
            eRheobase.TabIndex = 1;
            // 
            // lRheobase
            // 
            lRheobase.AutoSize = true;
            lRheobase.Location = new Point(13, 11);
            lRheobase.Name = "lRheobase";
            lRheobase.Size = new Size(58, 15);
            lRheobase.TabIndex = 0;
            lRheobase.Text = "Rheobase";
            // 
            // grConductionVelocity
            // 
            grConductionVelocity.AutoSize = true;
            grConductionVelocity.Controls.Add(distConductionVelocity);
            grConductionVelocity.Dock = DockStyle.Top;
            grConductionVelocity.Location = new Point(3, 33);
            grConductionVelocity.Name = "grConductionVelocity";
            grConductionVelocity.Size = new Size(314, 113);
            grConductionVelocity.TabIndex = 2;
            grConductionVelocity.TabStop = false;
            grConductionVelocity.Text = "ConductionVelocity";
            // 
            // distConductionVelocity
            // 
            distConductionVelocity.Absolute = false;
            distConductionVelocity.Angular = false;
            distConductionVelocity.AutoSize = true;
            distConductionVelocity.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            distConductionVelocity.BackColor = Color.White;
            distConductionVelocity.BorderStyle = BorderStyle.Fixed3D;
            distConductionVelocity.DefaultConstant = false;
            distConductionVelocity.Dock = DockStyle.Top;
            distConductionVelocity.Location = new Point(3, 19);
            distConductionVelocity.MinimumSize = new Size(204, 0);
            distConductionVelocity.Name = "distConductionVelocity";
            distConductionVelocity.NoneIncluded = true;
            distConductionVelocity.Size = new Size(308, 91);
            distConductionVelocity.TabIndex = 0;
            // 
            // pDynamicsTops
            // 
            pDynamicsTops.BackColor = Color.FromArgb(236, 239, 241);
            pDynamicsTops.Controls.Add(linkLoadCoreUnit);
            pDynamicsTops.Controls.Add(linkTestDynamics);
            pDynamicsTops.Dock = DockStyle.Top;
            pDynamicsTops.Location = new Point(3, 3);
            pDynamicsTops.Name = "pDynamicsTops";
            pDynamicsTops.Size = new Size(314, 30);
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
            tTimeline.Size = new Size(320, 557);
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
            splitMain.Size = new Size(600, 587);
            splitMain.SplitterDistance = 266;
            splitMain.TabIndex = 16;
            // 
            // openFileJson
            // 
            openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // CellPoolControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            Controls.Add(splitMain);
            MinimumSize = new Size(480, 200);
            Name = "CellPoolControl";
            Size = new Size(600, 587);
            ((System.ComponentModel.ISupportInitialize)eNumOfCells).EndInit();
            ((System.ComponentModel.ISupportInitialize)e2DColumn).EndInit();
            pZAxis.ResumeLayout(false);
            pZAxis.PerformLayout();
            pXAxis.ResumeLayout(false);
            pMain.ResumeLayout(false);
            pMain.PerformLayout();
            pMainInfo.ResumeLayout(false);
            pMainInfo.PerformLayout();
            pMainTop.ResumeLayout(false);
            pMainTop.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabCellPool.ResumeLayout(false);
            tSpatialDist.ResumeLayout(false);
            tProjections.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            pProjectionAscending.ResumeLayout(false);
            pProjectionAscending.PerformLayout();
            pProjectionDescending.ResumeLayout(false);
            pProjectionDescending.PerformLayout();
            tDynamics.ResumeLayout(false);
            tDynamics.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            grConductionVelocity.ResumeLayout(false);
            grConductionVelocity.PerformLayout();
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
        private TextBox eGroupName;
        private Label lNumOfCells;
        private NumericUpDown eNumOfCells;
        private Label l2DRenderColumn;
        private NumericUpDown e2DColumn;
        private ComboBox ddCellType;
        private Label lCellType;
        private Panel pZAxis;
        private Label lZAxis;
        private Panel pXAxis;
        private Label lXAxis;
        private Panel pMain;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel pMainTop;
        private LinkLabel linkSavePool;
        private LinkLabel linkLoadPool;
        private Controls.DistributionControl distributionX;
        private Controls.DistributionControl distributionY;
        private Controls.DistributionControl distributionZ;
        private Label lSagittalPosition;
        private ComboBox ddSagittalPosition;
        private ToolTip toolTip1;
        private TabControl tabCellPool;
        private TabPage tSpatialDist;
        private TabPage tDynamics;
        private SplitContainer splitMain;
        private GroupBox groupBox1;
        private RadioButton rbYZLinear;
        private RadioButton rbYZAngular;
        private Panel panel1;
        private Label lYAxis;
        private CheckBox cbActive;
        private TimeLineControl timeLineControl;
        private Button btnColor;
        private ColorDialog colorDialog;
        private Panel pDynamicsTops;
        private GroupBox grConductionVelocity;
        private DistributionControl distConductionVelocity;
        private LinkLabel linkTestDynamics;
        private TabPage tTimeline;
        private ComboBox ddNeuronClass;
        private Label lNeuronClass;
        private Label lDescription;
        private Controls.DistributionDataGrid dgDynamics;
        private ComboBox ddSelection;
        private OpenFileDialog openFileJson;
        private RichTextBox eDescription;
        private ComboBox ddCoreType;
        private Label lCoreType;
        private TextBox eSomiteRange;
        private CheckBox cbAllSomites;
        private ComboBox ddBodyPosition;
        private Label lBodyPosition;
        private LinkLabel linkLoadCoreUnit;
        private Panel pMainInfo;
        private TabPage tProjections;
        private Panel pProjectionAscending;
        private CheckBox cbAscendingAxon;
        private DistributionControl distributionAscending;
        private FlowLayoutPanel flowLayoutPanel2;
        private Panel pProjectionDescending;
        private CheckBox cbDescendingAxon;
        private DistributionControl distributionDescending;
        private Panel panel2;
        private TextBox eRheobase;
        private Label lRheobase;
        private Label lRheobaseDescription;
    }
}
