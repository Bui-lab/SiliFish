
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
            this.components = new System.ComponentModel.Container();
            this.lGroupName = new System.Windows.Forms.Label();
            this.eGroupName = new System.Windows.Forms.TextBox();
            this.lNumOfCells = new System.Windows.Forms.Label();
            this.eNumOfCells = new System.Windows.Forms.NumericUpDown();
            this.l2DModelColumn = new System.Windows.Forms.Label();
            this.e2DColumn = new System.Windows.Forms.NumericUpDown();
            this.ddCellType = new System.Windows.Forms.ComboBox();
            this.lCellType = new System.Windows.Forms.Label();
            this.pZAxis = new System.Windows.Forms.Panel();
            this.lZAxis = new System.Windows.Forms.Label();
            this.pXAxis = new System.Windows.Forms.Panel();
            this.lXAxis = new System.Windows.Forms.Label();
            this.pMain = new System.Windows.Forms.Panel();
            this.ddBodyPosition = new System.Windows.Forms.ComboBox();
            this.lBodyPosition = new System.Windows.Forms.Label();
            this.eSomiteRange = new System.Windows.Forms.TextBox();
            this.cbAllSomites = new System.Windows.Forms.CheckBox();
            this.ddCoreType = new System.Windows.Forms.ComboBox();
            this.lCoreType = new System.Windows.Forms.Label();
            this.eDescription = new System.Windows.Forms.RichTextBox();
            this.ddSelection = new System.Windows.Forms.ComboBox();
            this.ddNeuronClass = new System.Windows.Forms.ComboBox();
            this.lNeuronClass = new System.Windows.Forms.Label();
            this.lDescription = new System.Windows.Forms.Label();
            this.btnColor = new System.Windows.Forms.Button();
            this.pMainTop = new System.Windows.Forms.Panel();
            this.linkSavePool = new System.Windows.Forms.LinkLabel();
            this.linkLoadPool = new System.Windows.Forms.LinkLabel();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.timeLineControl = new SiliFish.UI.Controls.TimeLineControl();
            this.lSagittalPosition = new System.Windows.Forms.Label();
            this.ddSagittalPosition = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.distributionX = new SiliFish.UI.Controls.DistributionControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbYZLinear = new System.Windows.Forms.RadioButton();
            this.rbYZAngular = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lYAxis = new System.Windows.Forms.Label();
            this.distributionY = new SiliFish.UI.Controls.DistributionControl();
            this.distributionZ = new SiliFish.UI.Controls.DistributionControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabCellPool = new System.Windows.Forms.TabControl();
            this.tSpatialDist = new System.Windows.Forms.TabPage();
            this.tDynamics = new System.Windows.Forms.TabPage();
            this.dgDynamics = new SiliFish.UI.Controls.DistributionDataGrid();
            this.grConductionVelocity = new System.Windows.Forms.GroupBox();
            this.distConductionVelocity = new SiliFish.UI.Controls.DistributionControl();
            this.pDynamicsTops = new System.Windows.Forms.Panel();
            this.linkLoadCoreUnit = new System.Windows.Forms.LinkLabel();
            this.linkTestDynamics = new System.Windows.Forms.LinkLabel();
            this.tTimeline = new System.Windows.Forms.TabPage();
            this.tAttachments = new System.Windows.Forms.TabPage();
            this.attachmentList = new SiliFish.UI.Controls.AttachmentListControl();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileJson = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.eNumOfCells)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e2DColumn)).BeginInit();
            this.pZAxis.SuspendLayout();
            this.pXAxis.SuspendLayout();
            this.pMain.SuspendLayout();
            this.pMainTop.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabCellPool.SuspendLayout();
            this.tSpatialDist.SuspendLayout();
            this.tDynamics.SuspendLayout();
            this.grConductionVelocity.SuspendLayout();
            this.pDynamicsTops.SuspendLayout();
            this.tTimeline.SuspendLayout();
            this.tAttachments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lGroupName
            // 
            this.lGroupName.AutoSize = true;
            this.lGroupName.Location = new System.Drawing.Point(5, 39);
            this.lGroupName.Name = "lGroupName";
            this.lGroupName.Size = new System.Drawing.Size(75, 15);
            this.lGroupName.TabIndex = 0;
            this.lGroupName.Text = "Group Name";
            // 
            // eGroupName
            // 
            this.eGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eGroupName.Location = new System.Drawing.Point(112, 36);
            this.eGroupName.Name = "eGroupName";
            this.eGroupName.Size = new System.Drawing.Size(146, 23);
            this.eGroupName.TabIndex = 1;
            this.eGroupName.TextChanged += new System.EventHandler(this.eGroupName_TextChanged);
            // 
            // lNumOfCells
            // 
            this.lNumOfCells.AutoSize = true;
            this.lNumOfCells.Location = new System.Drawing.Point(5, 174);
            this.lNumOfCells.Name = "lNumOfCells";
            this.lNumOfCells.Size = new System.Drawing.Size(56, 15);
            this.lNumOfCells.TabIndex = 8;
            this.lNumOfCells.Text = "# of Cells";
            // 
            // eNumOfCells
            // 
            this.eNumOfCells.Location = new System.Drawing.Point(112, 171);
            this.eNumOfCells.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.eNumOfCells.Name = "eNumOfCells";
            this.eNumOfCells.Size = new System.Drawing.Size(64, 23);
            this.eNumOfCells.TabIndex = 9;
            this.eNumOfCells.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // l2DModelColumn
            // 
            this.l2DModelColumn.AutoSize = true;
            this.l2DModelColumn.Location = new System.Drawing.Point(5, 201);
            this.l2DModelColumn.Name = "l2DModelColumn";
            this.l2DModelColumn.Size = new System.Drawing.Size(104, 15);
            this.l2DModelColumn.TabIndex = 13;
            this.l2DModelColumn.Text = "2D Model Column";
            // 
            // e2DColumn
            // 
            this.e2DColumn.Location = new System.Drawing.Point(112, 198);
            this.e2DColumn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.e2DColumn.Name = "e2DColumn";
            this.e2DColumn.Size = new System.Drawing.Size(64, 23);
            this.e2DColumn.TabIndex = 14;
            this.toolTip1.SetToolTip(this.e2DColumn, "Used for 2D models or predefined 3D models");
            this.e2DColumn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ddCellType
            // 
            this.ddCellType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddCellType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCellType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddCellType.FormattingEnabled = true;
            this.ddCellType.Location = new System.Drawing.Point(112, 63);
            this.ddCellType.Name = "ddCellType";
            this.ddCellType.Size = new System.Drawing.Size(146, 23);
            this.ddCellType.TabIndex = 3;
            this.ddCellType.SelectedIndexChanged += new System.EventHandler(this.ddCellType_SelectedIndexChanged);
            // 
            // lCellType
            // 
            this.lCellType.AutoSize = true;
            this.lCellType.Location = new System.Drawing.Point(5, 66);
            this.lCellType.Name = "lCellType";
            this.lCellType.Size = new System.Drawing.Size(54, 15);
            this.lCellType.TabIndex = 2;
            this.lCellType.Text = "Cell Type";
            // 
            // pZAxis
            // 
            this.pZAxis.Controls.Add(this.lZAxis);
            this.pZAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this.pZAxis.Location = new System.Drawing.Point(0, 340);
            this.pZAxis.Margin = new System.Windows.Forms.Padding(0);
            this.pZAxis.Name = "pZAxis";
            this.pZAxis.Size = new System.Drawing.Size(211, 30);
            this.pZAxis.TabIndex = 17;
            // 
            // lZAxis
            // 
            this.lZAxis.AutoSize = true;
            this.lZAxis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lZAxis.Location = new System.Drawing.Point(18, 8);
            this.lZAxis.Name = "lZAxis";
            this.lZAxis.Size = new System.Drawing.Size(147, 15);
            this.lZAxis.TabIndex = 0;
            this.lZAxis.Text = "Z-Axis (Dorsal -> Ventral)";
            // 
            // pXAxis
            // 
            this.pXAxis.Controls.Add(this.lXAxis);
            this.pXAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this.pXAxis.Location = new System.Drawing.Point(0, 0);
            this.pXAxis.Margin = new System.Windows.Forms.Padding(0);
            this.pXAxis.Name = "pXAxis";
            this.pXAxis.Size = new System.Drawing.Size(211, 30);
            this.pXAxis.TabIndex = 13;
            // 
            // lXAxis
            // 
            this.lXAxis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lXAxis.Location = new System.Drawing.Point(18, 8);
            this.lXAxis.Name = "lXAxis";
            this.lXAxis.Size = new System.Drawing.Size(171, 15);
            this.lXAxis.TabIndex = 0;
            this.lXAxis.Text = "X-Axis (Rostral -> Caudal)";
            // 
            // pMain
            // 
            this.pMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pMain.Controls.Add(this.ddBodyPosition);
            this.pMain.Controls.Add(this.lBodyPosition);
            this.pMain.Controls.Add(this.eSomiteRange);
            this.pMain.Controls.Add(this.cbAllSomites);
            this.pMain.Controls.Add(this.ddCoreType);
            this.pMain.Controls.Add(this.lCoreType);
            this.pMain.Controls.Add(this.eDescription);
            this.pMain.Controls.Add(this.ddSelection);
            this.pMain.Controls.Add(this.ddNeuronClass);
            this.pMain.Controls.Add(this.lNeuronClass);
            this.pMain.Controls.Add(this.lDescription);
            this.pMain.Controls.Add(this.btnColor);
            this.pMain.Controls.Add(this.pMainTop);
            this.pMain.Controls.Add(this.cbActive);
            this.pMain.Controls.Add(this.lGroupName);
            this.pMain.Controls.Add(this.lNumOfCells);
            this.pMain.Controls.Add(this.ddCellType);
            this.pMain.Controls.Add(this.lCellType);
            this.pMain.Controls.Add(this.eNumOfCells);
            this.pMain.Controls.Add(this.e2DColumn);
            this.pMain.Controls.Add(this.l2DModelColumn);
            this.pMain.Controls.Add(this.eGroupName);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.MaximumSize = new System.Drawing.Size(400, 0);
            this.pMain.MinimumSize = new System.Drawing.Size(244, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(264, 721);
            this.pMain.TabIndex = 0;
            // 
            // ddBodyPosition
            // 
            this.ddBodyPosition.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddBodyPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddBodyPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddBodyPosition.FormattingEnabled = true;
            this.ddBodyPosition.Location = new System.Drawing.Point(112, 144);
            this.ddBodyPosition.Name = "ddBodyPosition";
            this.ddBodyPosition.Size = new System.Drawing.Size(146, 23);
            this.ddBodyPosition.TabIndex = 28;
            // 
            // lBodyPosition
            // 
            this.lBodyPosition.AutoSize = true;
            this.lBodyPosition.Location = new System.Drawing.Point(5, 147);
            this.lBodyPosition.Name = "lBodyPosition";
            this.lBodyPosition.Size = new System.Drawing.Size(80, 15);
            this.lBodyPosition.TabIndex = 27;
            this.lBodyPosition.Text = "Body Position";
            // 
            // eSomiteRange
            // 
            this.eSomiteRange.Location = new System.Drawing.Point(112, 225);
            this.eSomiteRange.Name = "eSomiteRange";
            this.eSomiteRange.ReadOnly = true;
            this.eSomiteRange.Size = new System.Drawing.Size(146, 23);
            this.eSomiteRange.TabIndex = 12;
            this.eSomiteRange.Text = "Enter a range. Ex: 1, 3-5";
            this.toolTip1.SetToolTip(this.eSomiteRange, "Enter a range. Ex: 1, 3-5");
            // 
            // cbAllSomites
            // 
            this.cbAllSomites.AutoSize = true;
            this.cbAllSomites.Checked = true;
            this.cbAllSomites.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllSomites.Location = new System.Drawing.Point(5, 228);
            this.cbAllSomites.Name = "cbAllSomites";
            this.cbAllSomites.Size = new System.Drawing.Size(85, 19);
            this.cbAllSomites.TabIndex = 11;
            this.cbAllSomites.Text = "All Somites";
            this.cbAllSomites.UseVisualStyleBackColor = true;
            this.cbAllSomites.CheckedChanged += new System.EventHandler(this.cbAllSomites_CheckedChanged);
            // 
            // ddCoreType
            // 
            this.ddCoreType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddCoreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCoreType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddCoreType.FormattingEnabled = true;
            this.ddCoreType.Location = new System.Drawing.Point(112, 90);
            this.ddCoreType.Name = "ddCoreType";
            this.ddCoreType.Size = new System.Drawing.Size(146, 23);
            this.ddCoreType.TabIndex = 5;
            this.ddCoreType.SelectedIndexChanged += new System.EventHandler(this.ddCoreType_SelectedIndexChanged);
            // 
            // lCoreType
            // 
            this.lCoreType.AutoSize = true;
            this.lCoreType.Location = new System.Drawing.Point(5, 93);
            this.lCoreType.Name = "lCoreType";
            this.lCoreType.Size = new System.Drawing.Size(59, 15);
            this.lCoreType.TabIndex = 4;
            this.lCoreType.Text = "Core Type";
            // 
            // eDescription
            // 
            this.eDescription.AcceptsTab = true;
            this.eDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eDescription.Location = new System.Drawing.Point(5, 291);
            this.eDescription.Name = "eDescription";
            this.eDescription.Size = new System.Drawing.Size(253, 426);
            this.eDescription.TabIndex = 17;
            this.eDescription.Text = "";
            // 
            // ddSelection
            // 
            this.ddSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSelection.FormattingEnabled = true;
            this.ddSelection.Location = new System.Drawing.Point(183, 170);
            this.ddSelection.Name = "ddSelection";
            this.ddSelection.Size = new System.Drawing.Size(75, 23);
            this.ddSelection.TabIndex = 10;
            // 
            // ddNeuronClass
            // 
            this.ddNeuronClass.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ddNeuronClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddNeuronClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddNeuronClass.FormattingEnabled = true;
            this.ddNeuronClass.Location = new System.Drawing.Point(112, 117);
            this.ddNeuronClass.Name = "ddNeuronClass";
            this.ddNeuronClass.Size = new System.Drawing.Size(146, 23);
            this.ddNeuronClass.TabIndex = 7;
            // 
            // lNeuronClass
            // 
            this.lNeuronClass.AutoSize = true;
            this.lNeuronClass.Location = new System.Drawing.Point(5, 120);
            this.lNeuronClass.Name = "lNeuronClass";
            this.lNeuronClass.Size = new System.Drawing.Size(77, 15);
            this.lNeuronClass.TabIndex = 6;
            this.lNeuronClass.Text = "Neuron Class";
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.Location = new System.Drawing.Point(5, 273);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(67, 15);
            this.lDescription.TabIndex = 26;
            this.lDescription.Text = "Description";
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(183, 198);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(75, 23);
            this.btnColor.TabIndex = 15;
            this.btnColor.Text = "Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // pMainTop
            // 
            this.pMainTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pMainTop.Controls.Add(this.linkSavePool);
            this.pMainTop.Controls.Add(this.linkLoadPool);
            this.pMainTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMainTop.Location = new System.Drawing.Point(0, 0);
            this.pMainTop.Name = "pMainTop";
            this.pMainTop.Size = new System.Drawing.Size(264, 30);
            this.pMainTop.TabIndex = 10;
            // 
            // linkSavePool
            // 
            this.linkSavePool.AutoSize = true;
            this.linkSavePool.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkSavePool.Location = new System.Drawing.Point(72, 9);
            this.linkSavePool.Name = "linkSavePool";
            this.linkSavePool.Size = new System.Drawing.Size(58, 15);
            this.linkSavePool.TabIndex = 9;
            this.linkSavePool.TabStop = true;
            this.linkSavePool.Text = "Save Pool";
            this.linkSavePool.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSavePool_LinkClicked);
            // 
            // linkLoadPool
            // 
            this.linkLoadPool.AutoSize = true;
            this.linkLoadPool.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLoadPool.Location = new System.Drawing.Point(6, 9);
            this.linkLoadPool.Name = "linkLoadPool";
            this.linkLoadPool.Size = new System.Drawing.Size(60, 15);
            this.linkLoadPool.TabIndex = 8;
            this.linkLoadPool.TabStop = true;
            this.linkLoadPool.Text = "Load Pool";
            this.linkLoadPool.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLoadPool_LinkClicked);
            // 
            // cbActive
            // 
            this.cbActive.AutoSize = true;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(199, 254);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(59, 19);
            this.cbActive.TabIndex = 16;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // timeLineControl
            // 
            this.timeLineControl.AutoScroll = true;
            this.timeLineControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.timeLineControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLineControl.Location = new System.Drawing.Point(3, 3);
            this.timeLineControl.Name = "timeLineControl";
            this.timeLineControl.Size = new System.Drawing.Size(314, 687);
            this.timeLineControl.TabIndex = 23;
            // 
            // lSagittalPosition
            // 
            this.lSagittalPosition.AutoSize = true;
            this.lSagittalPosition.Location = new System.Drawing.Point(0, 51);
            this.lSagittalPosition.Name = "lSagittalPosition";
            this.lSagittalPosition.Size = new System.Drawing.Size(71, 15);
            this.lSagittalPosition.TabIndex = 10;
            this.lSagittalPosition.Text = "Sagittal Pos.";
            // 
            // ddSagittalPosition
            // 
            this.ddSagittalPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddSagittalPosition.FormattingEnabled = true;
            this.ddSagittalPosition.Items.AddRange(new object[] {
            "Left/Right",
            "Left",
            "Right"});
            this.ddSagittalPosition.Location = new System.Drawing.Point(84, 47);
            this.ddSagittalPosition.Name = "ddSagittalPosition";
            this.ddSagittalPosition.Size = new System.Drawing.Size(121, 23);
            this.ddSagittalPosition.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pXAxis);
            this.flowLayoutPanel1.Controls.Add(this.distributionX);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.distributionY);
            this.flowLayoutPanel1.Controls.Add(this.pZAxis);
            this.flowLayoutPanel1.Controls.Add(this.distributionZ);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(217, 687);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // distributionX
            // 
            this.distributionX.Angular = false;
            this.distributionX.AutoSize = true;
            this.distributionX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.distributionX.BackColor = System.Drawing.Color.White;
            this.distributionX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.distributionX.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.SetFlowBreak(this.distributionX, true);
            this.distributionX.Location = new System.Drawing.Point(3, 33);
            this.distributionX.MinimumSize = new System.Drawing.Size(204, 0);
            this.distributionX.Name = "distributionX";
            this.distributionX.Size = new System.Drawing.Size(211, 91);
            this.distributionX.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbYZLinear);
            this.groupBox1.Controls.Add(this.rbYZAngular);
            this.groupBox1.Controls.Add(this.ddSagittalPosition);
            this.groupBox1.Controls.Add(this.lSagittalPosition);
            this.flowLayoutPanel1.SetFlowBreak(this.groupBox1, true);
            this.groupBox1.Location = new System.Drawing.Point(3, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 80);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Y-Z Distribution";
            // 
            // rbYZLinear
            // 
            this.rbYZLinear.AutoSize = true;
            this.rbYZLinear.Checked = true;
            this.rbYZLinear.Location = new System.Drawing.Point(6, 22);
            this.rbYZLinear.Name = "rbYZLinear";
            this.rbYZLinear.Size = new System.Drawing.Size(57, 19);
            this.rbYZLinear.TabIndex = 0;
            this.rbYZLinear.TabStop = true;
            this.rbYZLinear.Text = "Linear";
            this.rbYZLinear.UseVisualStyleBackColor = true;
            // 
            // rbYZAngular
            // 
            this.rbYZAngular.AutoSize = true;
            this.rbYZAngular.Location = new System.Drawing.Point(95, 22);
            this.rbYZAngular.Name = "rbYZAngular";
            this.rbYZAngular.Size = new System.Drawing.Size(67, 19);
            this.rbYZAngular.TabIndex = 1;
            this.rbYZAngular.Text = "Angular";
            this.rbYZAngular.UseVisualStyleBackColor = true;
            this.rbYZAngular.CheckedChanged += new System.EventHandler(this.rbYZAngular_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lYAxis);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 213);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(211, 30);
            this.panel1.TabIndex = 23;
            // 
            // lYAxis
            // 
            this.lYAxis.AutoSize = true;
            this.lYAxis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lYAxis.Location = new System.Drawing.Point(18, 8);
            this.lYAxis.Name = "lYAxis";
            this.lYAxis.Size = new System.Drawing.Size(147, 15);
            this.lYAxis.TabIndex = 0;
            this.lYAxis.Text = "Y-Axis (Medial -> Lateral)";
            // 
            // distributionY
            // 
            this.distributionY.Angular = false;
            this.distributionY.AutoSize = true;
            this.distributionY.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.distributionY.BackColor = System.Drawing.Color.White;
            this.distributionY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.distributionY.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.SetFlowBreak(this.distributionY, true);
            this.distributionY.Location = new System.Drawing.Point(3, 246);
            this.distributionY.MinimumSize = new System.Drawing.Size(204, 0);
            this.distributionY.Name = "distributionY";
            this.distributionY.Size = new System.Drawing.Size(211, 91);
            this.distributionY.TabIndex = 1;
            // 
            // distributionZ
            // 
            this.distributionZ.Angular = false;
            this.distributionZ.AutoSize = true;
            this.distributionZ.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.distributionZ.BackColor = System.Drawing.Color.White;
            this.distributionZ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.distributionZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.SetFlowBreak(this.distributionZ, true);
            this.distributionZ.Location = new System.Drawing.Point(3, 373);
            this.distributionZ.MinimumSize = new System.Drawing.Size(204, 0);
            this.distributionZ.Name = "distributionZ";
            this.distributionZ.Size = new System.Drawing.Size(211, 91);
            this.distributionZ.TabIndex = 2;
            // 
            // tabCellPool
            // 
            this.tabCellPool.Controls.Add(this.tSpatialDist);
            this.tabCellPool.Controls.Add(this.tDynamics);
            this.tabCellPool.Controls.Add(this.tTimeline);
            this.tabCellPool.Controls.Add(this.tAttachments);
            this.tabCellPool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCellPool.Location = new System.Drawing.Point(0, 0);
            this.tabCellPool.Name = "tabCellPool";
            this.tabCellPool.SelectedIndex = 0;
            this.tabCellPool.Size = new System.Drawing.Size(328, 721);
            this.tabCellPool.TabIndex = 15;
            // 
            // tSpatialDist
            // 
            this.tSpatialDist.Controls.Add(this.flowLayoutPanel1);
            this.tSpatialDist.Location = new System.Drawing.Point(4, 24);
            this.tSpatialDist.Name = "tSpatialDist";
            this.tSpatialDist.Padding = new System.Windows.Forms.Padding(3);
            this.tSpatialDist.Size = new System.Drawing.Size(320, 693);
            this.tSpatialDist.TabIndex = 0;
            this.tSpatialDist.Text = "Spatial Distribution";
            this.tSpatialDist.UseVisualStyleBackColor = true;
            // 
            // tDynamics
            // 
            this.tDynamics.Controls.Add(this.dgDynamics);
            this.tDynamics.Controls.Add(this.grConductionVelocity);
            this.tDynamics.Controls.Add(this.pDynamicsTops);
            this.tDynamics.Location = new System.Drawing.Point(4, 24);
            this.tDynamics.Name = "tDynamics";
            this.tDynamics.Padding = new System.Windows.Forms.Padding(3);
            this.tDynamics.Size = new System.Drawing.Size(320, 693);
            this.tDynamics.TabIndex = 1;
            this.tDynamics.Text = "Dynamics";
            this.tDynamics.UseVisualStyleBackColor = true;
            // 
            // dgDynamics
            // 
            this.dgDynamics.AutoSize = true;
            this.dgDynamics.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dgDynamics.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgDynamics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDynamics.Location = new System.Drawing.Point(3, 146);
            this.dgDynamics.MinimumSize = new System.Drawing.Size(100, 100);
            this.dgDynamics.Name = "dgDynamics";
            this.dgDynamics.Size = new System.Drawing.Size(314, 544);
            this.dgDynamics.TabIndex = 3;
            // 
            // grConductionVelocity
            // 
            this.grConductionVelocity.AutoSize = true;
            this.grConductionVelocity.Controls.Add(this.distConductionVelocity);
            this.grConductionVelocity.Dock = System.Windows.Forms.DockStyle.Top;
            this.grConductionVelocity.Location = new System.Drawing.Point(3, 33);
            this.grConductionVelocity.Name = "grConductionVelocity";
            this.grConductionVelocity.Size = new System.Drawing.Size(314, 113);
            this.grConductionVelocity.TabIndex = 2;
            this.grConductionVelocity.TabStop = false;
            this.grConductionVelocity.Text = "ConductionVelocity";
            // 
            // distConductionVelocity
            // 
            this.distConductionVelocity.Angular = false;
            this.distConductionVelocity.AutoSize = true;
            this.distConductionVelocity.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.distConductionVelocity.BackColor = System.Drawing.Color.White;
            this.distConductionVelocity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.distConductionVelocity.Dock = System.Windows.Forms.DockStyle.Top;
            this.distConductionVelocity.Location = new System.Drawing.Point(3, 19);
            this.distConductionVelocity.MinimumSize = new System.Drawing.Size(204, 0);
            this.distConductionVelocity.Name = "distConductionVelocity";
            this.distConductionVelocity.Size = new System.Drawing.Size(308, 91);
            this.distConductionVelocity.TabIndex = 0;
            // 
            // pDynamicsTops
            // 
            this.pDynamicsTops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pDynamicsTops.Controls.Add(this.linkLoadCoreUnit);
            this.pDynamicsTops.Controls.Add(this.linkTestDynamics);
            this.pDynamicsTops.Dock = System.Windows.Forms.DockStyle.Top;
            this.pDynamicsTops.Location = new System.Drawing.Point(3, 3);
            this.pDynamicsTops.Name = "pDynamicsTops";
            this.pDynamicsTops.Size = new System.Drawing.Size(314, 30);
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
            this.tTimeline.Size = new System.Drawing.Size(320, 693);
            this.tTimeline.TabIndex = 2;
            this.tTimeline.Text = "Timeline";
            this.tTimeline.UseVisualStyleBackColor = true;
            // 
            // tAttachments
            // 
            this.tAttachments.Controls.Add(this.attachmentList);
            this.tAttachments.Location = new System.Drawing.Point(4, 24);
            this.tAttachments.Name = "tAttachments";
            this.tAttachments.Size = new System.Drawing.Size(320, 693);
            this.tAttachments.TabIndex = 3;
            this.tAttachments.Text = "Attachments";
            this.tAttachments.UseVisualStyleBackColor = true;
            // 
            // attachmentList
            // 
            this.attachmentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attachmentList.Location = new System.Drawing.Point(0, 0);
            this.attachmentList.Name = "attachmentList";
            this.attachmentList.Size = new System.Drawing.Size(320, 693);
            this.attachmentList.TabIndex = 0;
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
            this.splitMain.SplitterDistance = 266;
            this.splitMain.TabIndex = 16;
            // 
            // openFileJson
            // 
            this.openFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // CellPoolControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitMain);
            this.MinimumSize = new System.Drawing.Size(480, 200);
            this.Name = "CellPoolControl";
            this.Size = new System.Drawing.Size(600, 723);
            ((System.ComponentModel.ISupportInitialize)(this.eNumOfCells)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e2DColumn)).EndInit();
            this.pZAxis.ResumeLayout(false);
            this.pZAxis.PerformLayout();
            this.pXAxis.ResumeLayout(false);
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            this.pMainTop.ResumeLayout(false);
            this.pMainTop.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabCellPool.ResumeLayout(false);
            this.tSpatialDist.ResumeLayout(false);
            this.tDynamics.ResumeLayout(false);
            this.tDynamics.PerformLayout();
            this.grConductionVelocity.ResumeLayout(false);
            this.grConductionVelocity.PerformLayout();
            this.pDynamicsTops.ResumeLayout(false);
            this.pDynamicsTops.PerformLayout();
            this.tTimeline.ResumeLayout(false);
            this.tAttachments.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lGroupName;
        private TextBox eGroupName;
        private Label lNumOfCells;
        private NumericUpDown eNumOfCells;
        private Label l2DModelColumn;
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
        private TabPage tAttachments;
        private AttachmentListControl attachmentList;
        private TextBox eSomiteRange;
        private CheckBox cbAllSomites;
        private ComboBox ddBodyPosition;
        private Label lBodyPosition;
        private LinkLabel linkLoadCoreUnit;
    }
}
