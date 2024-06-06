using SiliFish.UI.Controls.Display;

namespace SiliFish.UI.Controls
{
    partial class StatOutputControl
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            tabStats = new TabControl();
            tSpikes = new TabPage();
            dgSpikeStats = new DataGridView();
            cmGrid = new ContextMenuStrip(components);
            cmGridExport = new ToolStripMenuItem();
            cmGridCopyAll = new ToolStripMenuItem();
            tRCTrains = new TabPage();
            splitRCTrains = new SplitContainer();
            dgRCTrains = new DataGridView();
            colRCTrainNumber = new DataGridViewTextBoxColumn();
            colRCTrainCellGroup = new DataGridViewTextBoxColumn();
            colRCTrainSomite = new DataGridViewTextBoxColumn();
            colRCTrainStart = new DataGridViewTextBoxColumn();
            colRCTrainEnd = new DataGridViewTextBoxColumn();
            colRCTrainMedianPoint = new DataGridViewTextBoxColumn();
            colRCTrainCenter = new DataGridViewTextBoxColumn();
            colRCTrainStartDelay = new DataGridViewTextBoxColumn();
            colRCTrainMedianDelay = new DataGridViewTextBoxColumn();
            colRCTrainCenterDelay = new DataGridViewTextBoxColumn();
            tSpikeSummary = new TabPage();
            splitContainer1 = new SplitContainer();
            dgSpikeSummary = new DataGridView();
            colSumPeriod = new DataGridViewTextBoxColumn();
            colSumCellPool = new DataGridViewTextBoxColumn();
            colSumSpikeCount = new DataGridViewTextBoxColumn();
            dgSpikeSummaryDetails = new DataGridView();
            colSumDetailsPeriod = new DataGridViewTextBoxColumn();
            colSumDetailsCellID = new DataGridViewTextBoxColumn();
            colSumDetailsSagittal = new DataGridViewTextBoxColumn();
            colSumDetailsCellPool = new DataGridViewTextBoxColumn();
            colSumDetailsSomite = new DataGridViewTextBoxColumn();
            colSumDetailsCellSeq = new DataGridViewTextBoxColumn();
            colSumDetailsSpikeCount = new DataGridViewTextBoxColumn();
            tEpisodes = new TabPage();
            dgEpisodes = new DataGridView();
            pStatsTop = new Panel();
            timeRangeStat = new General.StartEndTimeControl();
            cbStatsAppend = new CheckBox();
            btnListStats = new Button();
            cellSelectionStats = new CellSelectionControl();
            panel3 = new Panel();
            toolTip = new ToolTip(components);
            saveFileText = new SaveFileDialog();
            saveFileCSV = new SaveFileDialog();
            saveFileImage = new SaveFileDialog();
            webViewRCTrains = new SiliFishWebView();
            tabStats.SuspendLayout();
            tSpikes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgSpikeStats).BeginInit();
            cmGrid.SuspendLayout();
            tRCTrains.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitRCTrains).BeginInit();
            splitRCTrains.Panel1.SuspendLayout();
            splitRCTrains.Panel2.SuspendLayout();
            splitRCTrains.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgRCTrains).BeginInit();
            tSpikeSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgSpikeSummary).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgSpikeSummaryDetails).BeginInit();
            tEpisodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgEpisodes).BeginInit();
            pStatsTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webViewRCTrains).BeginInit();
            SuspendLayout();
            // 
            // tabStats
            // 
            tabStats.Controls.Add(tSpikes);
            tabStats.Controls.Add(tRCTrains);
            tabStats.Controls.Add(tSpikeSummary);
            tabStats.Controls.Add(tEpisodes);
            tabStats.Dock = DockStyle.Fill;
            tabStats.Location = new Point(0, 120);
            tabStats.Name = "tabStats";
            tabStats.SelectedIndex = 0;
            tabStats.Size = new Size(702, 675);
            tabStats.TabIndex = 8;
            tabStats.SelectedIndexChanged += tabStats_SelectedIndexChanged;
            // 
            // tSpikes
            // 
            tSpikes.Controls.Add(dgSpikeStats);
            tSpikes.Location = new Point(4, 24);
            tSpikes.Name = "tSpikes";
            tSpikes.Padding = new Padding(3);
            tSpikes.Size = new Size(694, 647);
            tSpikes.TabIndex = 0;
            tSpikes.Text = "Spikes";
            tSpikes.UseVisualStyleBackColor = true;
            // 
            // dgSpikeStats
            // 
            dgSpikeStats.AllowUserToAddRows = false;
            dgSpikeStats.AllowUserToDeleteRows = false;
            dgSpikeStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgSpikeStats.ContextMenuStrip = cmGrid;
            dgSpikeStats.Dock = DockStyle.Fill;
            dgSpikeStats.Location = new Point(3, 3);
            dgSpikeStats.Name = "dgSpikeStats";
            dgSpikeStats.ReadOnly = true;
            dgSpikeStats.Size = new Size(688, 641);
            dgSpikeStats.TabIndex = 7;
            // 
            // cmGrid
            // 
            cmGrid.Items.AddRange(new ToolStripItem[] { cmGridExport, cmGridCopyAll });
            cmGrid.Name = "cmGrid";
            cmGrid.Size = new Size(147, 48);
            // 
            // cmGridExport
            // 
            cmGridExport.Name = "cmGridExport";
            cmGridExport.Size = new Size(146, 22);
            cmGridExport.Text = "Export to CSV";
            cmGridExport.Click += cmGridExport_Click;
            // 
            // cmGridCopyAll
            // 
            cmGridCopyAll.Name = "cmGridCopyAll";
            cmGridCopyAll.Size = new Size(146, 22);
            cmGridCopyAll.Text = "Copy All";
            cmGridCopyAll.Click += cmGridCopyAll_Click;
            // 
            // tRCTrains
            // 
            tRCTrains.Controls.Add(splitRCTrains);
            tRCTrains.Location = new Point(4, 24);
            tRCTrains.Name = "tRCTrains";
            tRCTrains.Padding = new Padding(3);
            tRCTrains.Size = new Size(694, 647);
            tRCTrains.TabIndex = 1;
            tRCTrains.Text = "Rostro-Caudal Burst Trains";
            tRCTrains.UseVisualStyleBackColor = true;
            // 
            // splitRCTrains
            // 
            splitRCTrains.Dock = DockStyle.Fill;
            splitRCTrains.Location = new Point(3, 3);
            splitRCTrains.Name = "splitRCTrains";
            splitRCTrains.Orientation = Orientation.Horizontal;
            // 
            // splitRCTrains.Panel1
            // 
            splitRCTrains.Panel1.Controls.Add(dgRCTrains);
            // 
            // splitRCTrains.Panel2
            // 
            splitRCTrains.Panel2.Controls.Add(webViewRCTrains);
            splitRCTrains.Size = new Size(688, 641);
            splitRCTrains.SplitterDistance = 319;
            splitRCTrains.TabIndex = 9;
            // 
            // dgRCTrains
            // 
            dgRCTrains.AllowUserToAddRows = false;
            dgRCTrains.AllowUserToDeleteRows = false;
            dgRCTrains.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgRCTrains.Columns.AddRange(new DataGridViewColumn[] { colRCTrainNumber, colRCTrainCellGroup, colRCTrainSomite, colRCTrainStart, colRCTrainEnd, colRCTrainMedianPoint, colRCTrainCenter, colRCTrainStartDelay, colRCTrainMedianDelay, colRCTrainCenterDelay });
            dgRCTrains.ContextMenuStrip = cmGrid;
            dgRCTrains.Dock = DockStyle.Fill;
            dgRCTrains.Location = new Point(0, 0);
            dgRCTrains.Name = "dgRCTrains";
            dgRCTrains.ReadOnly = true;
            dgRCTrains.Size = new Size(688, 319);
            dgRCTrains.TabIndex = 8;
            dgRCTrains.ColumnHeaderMouseDoubleClick += dgRCTrains_ColumnHeaderMouseDoubleClick;
            // 
            // colRCTrainNumber
            // 
            colRCTrainNumber.HeaderText = "Train #";
            colRCTrainNumber.Name = "colRCTrainNumber";
            colRCTrainNumber.ReadOnly = true;
            colRCTrainNumber.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainCellGroup
            // 
            colRCTrainCellGroup.HeaderText = "Cell Group";
            colRCTrainCellGroup.Name = "colRCTrainCellGroup";
            colRCTrainCellGroup.ReadOnly = true;
            colRCTrainCellGroup.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainSomite
            // 
            colRCTrainSomite.HeaderText = "Somite";
            colRCTrainSomite.Name = "colRCTrainSomite";
            colRCTrainSomite.ReadOnly = true;
            colRCTrainSomite.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainStart
            // 
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            colRCTrainStart.DefaultCellStyle = dataGridViewCellStyle1;
            colRCTrainStart.HeaderText = "Start Time";
            colRCTrainStart.Name = "colRCTrainStart";
            colRCTrainStart.ReadOnly = true;
            colRCTrainStart.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainEnd
            // 
            dataGridViewCellStyle2.Format = "N2";
            colRCTrainEnd.DefaultCellStyle = dataGridViewCellStyle2;
            colRCTrainEnd.HeaderText = "End Time";
            colRCTrainEnd.Name = "colRCTrainEnd";
            colRCTrainEnd.ReadOnly = true;
            colRCTrainEnd.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainMedianPoint
            // 
            dataGridViewCellStyle3.Format = "N2";
            colRCTrainMedianPoint.DefaultCellStyle = dataGridViewCellStyle3;
            colRCTrainMedianPoint.HeaderText = "Median";
            colRCTrainMedianPoint.Name = "colRCTrainMedianPoint";
            colRCTrainMedianPoint.ReadOnly = true;
            colRCTrainMedianPoint.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainCenter
            // 
            dataGridViewCellStyle4.Format = "N2";
            colRCTrainCenter.DefaultCellStyle = dataGridViewCellStyle4;
            colRCTrainCenter.HeaderText = "Center";
            colRCTrainCenter.Name = "colRCTrainCenter";
            colRCTrainCenter.ReadOnly = true;
            colRCTrainCenter.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainStartDelay
            // 
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            colRCTrainStartDelay.DefaultCellStyle = dataGridViewCellStyle5;
            colRCTrainStartDelay.HeaderText = "RC Delay [Start]";
            colRCTrainStartDelay.Name = "colRCTrainStartDelay";
            colRCTrainStartDelay.ReadOnly = true;
            colRCTrainStartDelay.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainMedianDelay
            // 
            dataGridViewCellStyle6.Format = "N2";
            colRCTrainMedianDelay.DefaultCellStyle = dataGridViewCellStyle6;
            colRCTrainMedianDelay.HeaderText = "RC Delay [Median]";
            colRCTrainMedianDelay.Name = "colRCTrainMedianDelay";
            colRCTrainMedianDelay.ReadOnly = true;
            colRCTrainMedianDelay.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // colRCTrainCenterDelay
            // 
            dataGridViewCellStyle7.Format = "N2";
            colRCTrainCenterDelay.DefaultCellStyle = dataGridViewCellStyle7;
            colRCTrainCenterDelay.HeaderText = "RC Delay [Center]";
            colRCTrainCenterDelay.Name = "colRCTrainCenterDelay";
            colRCTrainCenterDelay.ReadOnly = true;
            colRCTrainCenterDelay.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // tSpikeSummary
            // 
            tSpikeSummary.Controls.Add(splitContainer1);
            tSpikeSummary.Location = new Point(4, 24);
            tSpikeSummary.Name = "tSpikeSummary";
            tSpikeSummary.Padding = new Padding(3);
            tSpikeSummary.Size = new Size(694, 647);
            tSpikeSummary.TabIndex = 2;
            tSpikeSummary.Text = "Spike Summary";
            tSpikeSummary.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dgSpikeSummary);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgSpikeSummaryDetails);
            splitContainer1.Size = new Size(688, 641);
            splitContainer1.SplitterDistance = 255;
            splitContainer1.TabIndex = 9;
            // 
            // dgSpikeSummary
            // 
            dgSpikeSummary.AllowUserToAddRows = false;
            dgSpikeSummary.AllowUserToDeleteRows = false;
            dgSpikeSummary.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgSpikeSummary.Columns.AddRange(new DataGridViewColumn[] { colSumPeriod, colSumCellPool, colSumSpikeCount });
            dgSpikeSummary.ContextMenuStrip = cmGrid;
            dgSpikeSummary.Dock = DockStyle.Fill;
            dgSpikeSummary.Location = new Point(0, 0);
            dgSpikeSummary.Name = "dgSpikeSummary";
            dgSpikeSummary.ReadOnly = true;
            dgSpikeSummary.Size = new Size(688, 255);
            dgSpikeSummary.TabIndex = 9;
            // 
            // colSumPeriod
            // 
            colSumPeriod.HeaderText = "Period";
            colSumPeriod.Name = "colSumPeriod";
            colSumPeriod.ReadOnly = true;
            // 
            // colSumCellPool
            // 
            colSumCellPool.HeaderText = "Cell Pool";
            colSumCellPool.Name = "colSumCellPool";
            colSumCellPool.ReadOnly = true;
            // 
            // colSumSpikeCount
            // 
            colSumSpikeCount.HeaderText = "Spike Count";
            colSumSpikeCount.Name = "colSumSpikeCount";
            colSumSpikeCount.ReadOnly = true;
            // 
            // dgSpikeSummaryDetails
            // 
            dgSpikeSummaryDetails.AllowUserToAddRows = false;
            dgSpikeSummaryDetails.AllowUserToDeleteRows = false;
            dgSpikeSummaryDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgSpikeSummaryDetails.Columns.AddRange(new DataGridViewColumn[] { colSumDetailsPeriod, colSumDetailsCellID, colSumDetailsSagittal, colSumDetailsCellPool, colSumDetailsSomite, colSumDetailsCellSeq, colSumDetailsSpikeCount });
            dgSpikeSummaryDetails.ContextMenuStrip = cmGrid;
            dgSpikeSummaryDetails.Dock = DockStyle.Fill;
            dgSpikeSummaryDetails.Location = new Point(0, 0);
            dgSpikeSummaryDetails.Name = "dgSpikeSummaryDetails";
            dgSpikeSummaryDetails.ReadOnly = true;
            dgSpikeSummaryDetails.Size = new Size(688, 382);
            dgSpikeSummaryDetails.TabIndex = 8;
            // 
            // colSumDetailsPeriod
            // 
            colSumDetailsPeriod.HeaderText = "Period";
            colSumDetailsPeriod.Name = "colSumDetailsPeriod";
            colSumDetailsPeriod.ReadOnly = true;
            // 
            // colSumDetailsCellID
            // 
            colSumDetailsCellID.HeaderText = "Cell ID";
            colSumDetailsCellID.Name = "colSumDetailsCellID";
            colSumDetailsCellID.ReadOnly = true;
            // 
            // colSumDetailsSagittal
            // 
            colSumDetailsSagittal.HeaderText = "Sagittal";
            colSumDetailsSagittal.Name = "colSumDetailsSagittal";
            colSumDetailsSagittal.ReadOnly = true;
            colSumDetailsSagittal.Width = 75;
            // 
            // colSumDetailsCellPool
            // 
            colSumDetailsCellPool.HeaderText = "Cell Pool";
            colSumDetailsCellPool.Name = "colSumDetailsCellPool";
            colSumDetailsCellPool.ReadOnly = true;
            // 
            // colSumDetailsSomite
            // 
            colSumDetailsSomite.HeaderText = "Somite";
            colSumDetailsSomite.Name = "colSumDetailsSomite";
            colSumDetailsSomite.ReadOnly = true;
            colSumDetailsSomite.Width = 75;
            // 
            // colSumDetailsCellSeq
            // 
            colSumDetailsCellSeq.HeaderText = "Cell Seq";
            colSumDetailsCellSeq.Name = "colSumDetailsCellSeq";
            colSumDetailsCellSeq.ReadOnly = true;
            colSumDetailsCellSeq.Width = 75;
            // 
            // colSumDetailsSpikeCount
            // 
            colSumDetailsSpikeCount.HeaderText = "Spike Count";
            colSumDetailsSpikeCount.Name = "colSumDetailsSpikeCount";
            colSumDetailsSpikeCount.ReadOnly = true;
            // 
            // tEpisodes
            // 
            tEpisodes.Controls.Add(dgEpisodes);
            tEpisodes.Location = new Point(4, 24);
            tEpisodes.Name = "tEpisodes";
            tEpisodes.Padding = new Padding(3);
            tEpisodes.Size = new Size(694, 647);
            tEpisodes.TabIndex = 3;
            tEpisodes.Text = "Episodes";
            tEpisodes.UseVisualStyleBackColor = true;
            // 
            // dgEpisodes
            // 
            dgEpisodes.AllowUserToAddRows = false;
            dgEpisodes.AllowUserToDeleteRows = false;
            dgEpisodes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgEpisodes.ContextMenuStrip = cmGrid;
            dgEpisodes.Dock = DockStyle.Fill;
            dgEpisodes.Location = new Point(3, 3);
            dgEpisodes.Name = "dgEpisodes";
            dgEpisodes.ReadOnly = true;
            dgEpisodes.Size = new Size(688, 641);
            dgEpisodes.TabIndex = 8;
            // 
            // pStatsTop
            // 
            pStatsTop.BackColor = Color.FromArgb(236, 239, 241);
            pStatsTop.Controls.Add(timeRangeStat);
            pStatsTop.Controls.Add(cbStatsAppend);
            pStatsTop.Controls.Add(btnListStats);
            pStatsTop.Controls.Add(cellSelectionStats);
            pStatsTop.Controls.Add(panel3);
            pStatsTop.Dock = DockStyle.Top;
            pStatsTop.Location = new Point(0, 0);
            pStatsTop.Name = "pStatsTop";
            pStatsTop.Size = new Size(702, 120);
            pStatsTop.TabIndex = 6;
            // 
            // timeRangeStat
            // 
            timeRangeStat.EndTime = 1000;
            timeRangeStat.Location = new Point(3, 4);
            timeRangeStat.Name = "timeRangeStat";
            timeRangeStat.Size = new Size(207, 56);
            timeRangeStat.StartTime = 0;
            timeRangeStat.TabIndex = 73;
            // 
            // cbStatsAppend
            // 
            cbStatsAppend.AutoSize = true;
            cbStatsAppend.Location = new Point(445, 38);
            cbStatsAppend.Name = "cbStatsAppend";
            cbStatsAppend.Size = new Size(68, 19);
            cbStatsAppend.TabIndex = 72;
            cbStatsAppend.Text = "Append";
            cbStatsAppend.UseVisualStyleBackColor = true;
            // 
            // btnListStats
            // 
            btnListStats.AutoSize = true;
            btnListStats.BackColor = Color.FromArgb(96, 125, 139);
            btnListStats.Enabled = false;
            btnListStats.FlatAppearance.BorderColor = Color.LightGray;
            btnListStats.FlatStyle = FlatStyle.Flat;
            btnListStats.ForeColor = Color.White;
            btnListStats.Location = new Point(445, 11);
            btnListStats.Name = "btnListStats";
            btnListStats.Size = new Size(100, 27);
            btnListStats.TabIndex = 61;
            btnListStats.Text = "List Stats";
            btnListStats.UseVisualStyleBackColor = false;
            btnListStats.Click += btnListStats_Click;
            // 
            // cellSelectionStats
            // 
            cellSelectionStats.CombineOptionsVisible = false;
            cellSelectionStats.Location = new Point(217, 4);
            cellSelectionStats.Name = "cellSelectionStats";
            cellSelectionStats.SelectedUnits = null;
            cellSelectionStats.Size = new Size(232, 113);
            cellSelectionStats.TabIndex = 60;
            // 
            // panel3
            // 
            panel3.BackColor = Color.LightGray;
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 119);
            panel3.Name = "panel3";
            panel3.Size = new Size(702, 1);
            panel3.TabIndex = 58;
            // 
            // saveFileText
            // 
            saveFileText.Filter = "Text files(*.txt)|*.txt";
            // 
            // saveFileCSV
            // 
            saveFileCSV.Filter = "CSV files(*.csv)|*.csv";
            // 
            // saveFileImage
            // 
            saveFileImage.Filter = "Image files(*.png)|*.png";
            // 
            // webViewRCTrains
            // 
            webViewRCTrains.AllowExternalDrop = true;
            webViewRCTrains.CreationProperties = null;
            webViewRCTrains.DefaultBackgroundColor = Color.White;
            webViewRCTrains.Dock = DockStyle.Fill;
            webViewRCTrains.Location = new Point(0, 0);
            webViewRCTrains.Name = "webViewRCTrains";
            webViewRCTrains.Size = new Size(688, 318);
            webViewRCTrains.TabIndex = 0;
            webViewRCTrains.ZoomFactor = 1D;
            // 
            // StatOutputControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabStats);
            Controls.Add(pStatsTop);
            Name = "StatOutputControl";
            Size = new Size(702, 795);
            tabStats.ResumeLayout(false);
            tSpikes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgSpikeStats).EndInit();
            cmGrid.ResumeLayout(false);
            tRCTrains.ResumeLayout(false);
            splitRCTrains.Panel1.ResumeLayout(false);
            splitRCTrains.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitRCTrains).EndInit();
            splitRCTrains.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgRCTrains).EndInit();
            tSpikeSummary.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgSpikeSummary).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgSpikeSummaryDetails).EndInit();
            tEpisodes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgEpisodes).EndInit();
            pStatsTop.ResumeLayout(false);
            pStatsTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webViewRCTrains).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ToolTip toolTip;
        private SaveFileDialog saveFileText;
        private SaveFileDialog saveFileCSV;
        private SaveFileDialog saveFileImage;
        private DataGridView dgSpikeStats;
        private Panel pStatsTop;
        private Panel panel3;
        private Display.CellSelectionControl cellSelectionStats;
        private Button btnListStats;

        public StatOutputControl(Button btnListStats)
        {
            this.btnListStats = btnListStats;
        }

        private TabControl tabStats;
        private TabPage tSpikes;
        internal TabPage tRCTrains;
        internal DataGridView dgRCTrains;
        private SplitContainer splitRCTrains;
        private SiliFishWebView webViewRCTrains;
        private TabPage tSpikeSummary;
        private DataGridView dgSpikeSummaryDetails;
        private SplitContainer splitContainer1;
        private DataGridView dgSpikeSummary;
        private DataGridViewTextBoxColumn colSumPeriod;
        private DataGridViewTextBoxColumn colSumCellPool;
        private DataGridViewTextBoxColumn colSumSpikeCount;
        private DataGridViewTextBoxColumn colSumDetailsPeriod;
        private DataGridViewTextBoxColumn colSumDetailsCellID;
        private DataGridViewTextBoxColumn colSumDetailsSagittal;
        private DataGridViewTextBoxColumn colSumDetailsCellPool;
        private DataGridViewTextBoxColumn colSumDetailsSomite;
        private DataGridViewTextBoxColumn colSumDetailsCellSeq;
        private DataGridViewTextBoxColumn colSumDetailsSpikeCount;
        private CheckBox cbStatsAppend;
        private ContextMenuStrip cmGrid;
        private ToolStripMenuItem cmGridExport;
        private ToolStripMenuItem cmGridCopyAll;
        private TabPage tEpisodes;
        private DataGridView dgEpisodes;
        private General.StartEndTimeControl timeRangeStat;
        private DataGridViewTextBoxColumn colRCTrainNumber;
        private DataGridViewTextBoxColumn colRCTrainCellGroup;
        private DataGridViewTextBoxColumn colRCTrainSomite;
        private DataGridViewTextBoxColumn colRCTrainStart;
        private DataGridViewTextBoxColumn colRCTrainEnd;
        private DataGridViewTextBoxColumn colRCTrainMedianPoint;
        private DataGridViewTextBoxColumn colRCTrainCenter;
        private DataGridViewTextBoxColumn colRCTrainStartDelay;
        private DataGridViewTextBoxColumn colRCTrainMedianDelay;
        private DataGridViewTextBoxColumn colRCTrainCenterDelay;
    }
}