using SiliFish.UI.Controls.Display;

namespace SiliFish.UI.Controls
{
    partial class PlotControl
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
            tabPlotSub = new TabControl();
            tPlotHTML = new TabPage();
            webViewPlot = new SiliFishWebView();
            tPlotWindows = new TabPage();
            pPictureBox = new Panel();
            pictureBox = new PictureBox();
            cmPlotImageSave = new ContextMenuStrip(components);
            cmiPlotImageSave = new ToolStripMenuItem();
            pPlot = new Panel();
            timeRangePlot = new SiliFish.UI.Controls.General.StartEndTimeControl();
            btnPlotHTML = new Button();
            cmPlot = new ContextMenuStrip(components);
            cmiNonInteractivePlot = new ToolStripMenuItem();
            cmiMaximizePlotWindow = new ToolStripMenuItem();
            ddPlot = new ComboBox();
            listPlotHistory = new HistoryListControl();
            cellSelectionPlot = new CellSelectionControl();
            cbPlotHistory = new CheckBox();
            linkExportPlotData = new LinkLabel();
            pLinePlots = new Panel();
            linkSaveHTMLPlots = new LinkLabel();
            lPlotPlot = new Label();
            saveFileHTML = new SaveFileDialog();
            saveFileJson = new SaveFileDialog();
            openFileJson = new OpenFileDialog();
            toolTip = new ToolTip(components);
            saveFileText = new SaveFileDialog();
            saveFileCSV = new SaveFileDialog();
            saveFileImage = new SaveFileDialog();
            tabPlotSub.SuspendLayout();
            tPlotHTML.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webViewPlot).BeginInit();
            tPlotWindows.SuspendLayout();
            pPictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            cmPlotImageSave.SuspendLayout();
            pPlot.SuspendLayout();
            cmPlot.SuspendLayout();
            SuspendLayout();
            // 
            // tabPlotSub
            // 
            tabPlotSub.Controls.Add(tPlotHTML);
            tabPlotSub.Controls.Add(tPlotWindows);
            tabPlotSub.Dock = DockStyle.Fill;
            tabPlotSub.Location = new Point(0, 140);
            tabPlotSub.Name = "tabPlotSub";
            tabPlotSub.SelectedIndex = 0;
            tabPlotSub.Size = new Size(702, 655);
            tabPlotSub.TabIndex = 6;
            // 
            // tPlotHTML
            // 
            tPlotHTML.Controls.Add(webViewPlot);
            tPlotHTML.Location = new Point(4, 24);
            tPlotHTML.Name = "tPlotHTML";
            tPlotHTML.Padding = new Padding(3, 3, 3, 3);
            tPlotHTML.Size = new Size(694, 627);
            tPlotHTML.TabIndex = 1;
            tPlotHTML.Text = "HTML Plots";
            tPlotHTML.UseVisualStyleBackColor = true;
            // 
            // webViewPlot
            // 
            webViewPlot.AllowExternalDrop = true;
            webViewPlot.BackColor = Color.White;
            webViewPlot.CreationProperties = null;
            webViewPlot.DefaultBackgroundColor = Color.White;
            webViewPlot.Dock = DockStyle.Fill;
            webViewPlot.Location = new Point(3, 3);
            webViewPlot.Name = "webViewPlot";
            webViewPlot.Size = new Size(688, 621);
            webViewPlot.TabIndex = 1;
            webViewPlot.ZoomFactor = 1D;
            webViewPlot.CoreWebView2InitializationCompleted += webView_CoreWebView2InitializationCompleted;
            // 
            // tPlotWindows
            // 
            tPlotWindows.AutoScroll = true;
            tPlotWindows.Controls.Add(pPictureBox);
            tPlotWindows.Location = new Point(4, 24);
            tPlotWindows.Name = "tPlotWindows";
            tPlotWindows.Padding = new Padding(3, 3, 3, 3);
            tPlotWindows.Size = new Size(694, 627);
            tPlotWindows.TabIndex = 0;
            tPlotWindows.Text = "Image Plots";
            // 
            // pPictureBox
            // 
            pPictureBox.AutoScroll = true;
            pPictureBox.AutoSize = true;
            pPictureBox.Controls.Add(pictureBox);
            pPictureBox.Dock = DockStyle.Fill;
            pPictureBox.Location = new Point(3, 3);
            pPictureBox.Name = "pPictureBox";
            pPictureBox.Size = new Size(688, 621);
            pPictureBox.TabIndex = 1;
            // 
            // pictureBox
            // 
            pictureBox.ContextMenuStrip = cmPlotImageSave;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(104, 45);
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // cmPlotImageSave
            // 
            cmPlotImageSave.ImageScalingSize = new Size(24, 24);
            cmPlotImageSave.Items.AddRange(new ToolStripItem[] { cmiPlotImageSave });
            cmPlotImageSave.Name = "cmImageSave";
            cmPlotImageSave.Size = new Size(135, 26);
            // 
            // cmiPlotImageSave
            // 
            cmiPlotImageSave.Name = "cmiPlotImageSave";
            cmiPlotImageSave.Size = new Size(134, 22);
            cmiPlotImageSave.Text = "Save Image";
            cmiPlotImageSave.Click += cmiPlotImageSave_Click;
            // 
            // pPlot
            // 
            pPlot.BackColor = Color.FromArgb(236, 239, 241);
            pPlot.Controls.Add(timeRangePlot);
            pPlot.Controls.Add(btnPlotHTML);
            pPlot.Controls.Add(ddPlot);
            pPlot.Controls.Add(listPlotHistory);
            pPlot.Controls.Add(cellSelectionPlot);
            pPlot.Controls.Add(cbPlotHistory);
            pPlot.Controls.Add(linkExportPlotData);
            pPlot.Controls.Add(pLinePlots);
            pPlot.Controls.Add(linkSaveHTMLPlots);
            pPlot.Controls.Add(lPlotPlot);
            pPlot.Dock = DockStyle.Top;
            pPlot.Location = new Point(0, 0);
            pPlot.Name = "pPlot";
            pPlot.Size = new Size(702, 140);
            pPlot.TabIndex = 5;
            // 
            // timeRangePlot
            // 
            timeRangePlot.EndTime = 1000;
            timeRangePlot.Location = new Point(4, 3);
            timeRangePlot.Margin = new Padding(4, 5, 4, 5);
            timeRangePlot.Name = "timeRangePlot";
            timeRangePlot.Size = new Size(207, 56);
            timeRangePlot.StartTime = 0;
            timeRangePlot.TabIndex = 67;
            // 
            // btnPlotHTML
            // 
            btnPlotHTML.BackColor = Color.FromArgb(96, 125, 139);
            btnPlotHTML.ContextMenuStrip = cmPlot;
            btnPlotHTML.Enabled = false;
            btnPlotHTML.FlatAppearance.BorderColor = Color.LightGray;
            btnPlotHTML.FlatStyle = FlatStyle.Flat;
            btnPlotHTML.ForeColor = Color.White;
            btnPlotHTML.Location = new Point(446, 5);
            btnPlotHTML.Name = "btnPlotHTML";
            btnPlotHTML.Size = new Size(75, 24);
            btnPlotHTML.TabIndex = 52;
            btnPlotHTML.Text = "Plot";
            btnPlotHTML.UseVisualStyleBackColor = false;
            btnPlotHTML.Click += btnPlotHTML_Click;
            // 
            // cmPlot
            // 
            cmPlot.ImageScalingSize = new Size(24, 24);
            cmPlot.Items.AddRange(new ToolStripItem[] { cmiNonInteractivePlot, cmiMaximizePlotWindow });
            cmPlot.Name = "cmPlot";
            cmPlot.Size = new Size(196, 48);
            // 
            // cmiNonInteractivePlot
            // 
            cmiNonInteractivePlot.Name = "cmiNonInteractivePlot";
            cmiNonInteractivePlot.Size = new Size(195, 22);
            cmiNonInteractivePlot.Text = "Non-interactive plot";
            cmiNonInteractivePlot.Click += cmiNonInteractivePlot_Click;
            // 
            // cmiMaximizePlotWindow
            // 
            cmiMaximizePlotWindow.Name = "cmiMaximizePlotWindow";
            cmiMaximizePlotWindow.Size = new Size(195, 22);
            cmiMaximizePlotWindow.Text = "Maximize Plot Window";
            cmiMaximizePlotWindow.Click += cmiMaximizePlotWindow_Click;
            // 
            // ddPlot
            // 
            ddPlot.BackColor = Color.White;
            ddPlot.DropDownStyle = ComboBoxStyle.DropDownList;
            ddPlot.FlatStyle = FlatStyle.Flat;
            ddPlot.FormattingEnabled = true;
            ddPlot.Location = new Point(278, 6);
            ddPlot.Name = "ddPlot";
            ddPlot.Size = new Size(165, 23);
            ddPlot.TabIndex = 34;
            ddPlot.SelectedIndexChanged += ddPlot_SelectedIndexChanged;
            // 
            // listPlotHistory
            // 
            listPlotHistory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listPlotHistory.FirstItem = "(Plot History - Double click to select.)";
            listPlotHistory.Location = new Point(539, 28);
            listPlotHistory.Margin = new Padding(4, 5, 4, 5);
            listPlotHistory.Name = "listPlotHistory";
            listPlotHistory.SelectedIndex = -1;
            listPlotHistory.Size = new Size(160, 108);
            listPlotHistory.TabIndex = 64;
            listPlotHistory.ItemSelect += listPlotHistory_ItemSelect;
            listPlotHistory.ItemEdit += listPlotHistory_ItemEdit;
            listPlotHistory.ItemsExport += listPlotHistory_ItemsExport;
            listPlotHistory.ItemsImport += listPlotHistory_ItemsImport;
            // 
            // cellSelectionPlot
            // 
            cellSelectionPlot.CombineOptionsVisible = false;
            cellSelectionPlot.Location = new Point(212, 25);
            cellSelectionPlot.Margin = new Padding(4, 5, 4, 5);
            cellSelectionPlot.Name = "cellSelectionPlot";
            cellSelectionPlot.SelectedUnits = null;
            cellSelectionPlot.Size = new Size(316, 111);
            cellSelectionPlot.StickToNeurons = false;
            cellSelectionPlot.TabIndex = 66;
            // 
            // cbPlotHistory
            // 
            cbPlotHistory.AutoSize = true;
            cbPlotHistory.Checked = true;
            cbPlotHistory.CheckState = CheckState.Checked;
            cbPlotHistory.Location = new Point(539, 6);
            cbPlotHistory.Name = "cbPlotHistory";
            cbPlotHistory.Size = new Size(93, 19);
            cbPlotHistory.TabIndex = 65;
            cbPlotHistory.Text = "Keep History";
            cbPlotHistory.UseVisualStyleBackColor = true;
            // 
            // linkExportPlotData
            // 
            linkExportPlotData.AutoSize = true;
            linkExportPlotData.Enabled = false;
            linkExportPlotData.LinkColor = Color.FromArgb(64, 64, 64);
            linkExportPlotData.Location = new Point(69, 75);
            linkExportPlotData.Name = "linkExportPlotData";
            linkExportPlotData.Size = new Size(43, 15);
            linkExportPlotData.TabIndex = 59;
            linkExportPlotData.TabStop = true;
            linkExportPlotData.Text = "Export ";
            toolTip.SetToolTip(linkExportPlotData, "Export plot data as a csv file");
            linkExportPlotData.LinkClicked += linkExportPlotData_LinkClicked;
            // 
            // pLinePlots
            // 
            pLinePlots.BackColor = Color.LightGray;
            pLinePlots.Dock = DockStyle.Bottom;
            pLinePlots.Location = new Point(0, 139);
            pLinePlots.Name = "pLinePlots";
            pLinePlots.Size = new Size(702, 1);
            pLinePlots.TabIndex = 58;
            // 
            // linkSaveHTMLPlots
            // 
            linkSaveHTMLPlots.AutoSize = true;
            linkSaveHTMLPlots.Enabled = false;
            linkSaveHTMLPlots.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveHTMLPlots.Location = new Point(11, 75);
            linkSaveHTMLPlots.Name = "linkSaveHTMLPlots";
            linkSaveHTMLPlots.Size = new Size(34, 15);
            linkSaveHTMLPlots.TabIndex = 53;
            linkSaveHTMLPlots.TabStop = true;
            linkSaveHTMLPlots.Text = "Save ";
            toolTip.SetToolTip(linkSaveHTMLPlots, "Save plot as an HTML file");
            linkSaveHTMLPlots.LinkClicked += linkSaveHTMLPlots_LinkClicked;
            // 
            // lPlotPlot
            // 
            lPlotPlot.AutoSize = true;
            lPlotPlot.Location = new Point(217, 10);
            lPlotPlot.Name = "lPlotPlot";
            lPlotPlot.Size = new Size(28, 15);
            lPlotPlot.TabIndex = 19;
            lPlotPlot.Text = "Plot";
            lPlotPlot.DoubleClick += lPlotPlot_DoubleClick;
            // 
            // saveFileHTML
            // 
            saveFileHTML.Filter = "HTML files(*.html)|*.html";
            // 
            // saveFileJson
            // 
            saveFileJson.Filter = "JSON files(*.json)|*.json";
            // 
            // openFileJson
            // 
            openFileJson.Filter = "JSON files(*.json)|*.json";
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
            // PlotControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabPlotSub);
            Controls.Add(pPlot);
            Name = "PlotControl";
            Size = new Size(702, 795);
            tabPlotSub.ResumeLayout(false);
            tPlotHTML.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webViewPlot).EndInit();
            tPlotWindows.ResumeLayout(false);
            tPlotWindows.PerformLayout();
            pPictureBox.ResumeLayout(false);
            pPictureBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            cmPlotImageSave.ResumeLayout(false);
            pPlot.ResumeLayout(false);
            pPlot.PerformLayout();
            cmPlot.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabPlotSub;
        private TabPage tPlotWindows;
        private TabPage tPlotHTML;
        private SiliFishWebView webViewPlot;
        private Panel pPlot;
        private LinkLabel linkExportPlotData;
        private Panel pLinePlots;
        private Button btnPlotHTML;
        private LinkLabel linkSaveHTMLPlots;
        private Label lPlotPlot;
        private ComboBox ddPlot;
        private SaveFileDialog saveFileHTML;
        private SaveFileDialog saveFileJson;
        private OpenFileDialog openFileJson;
        private ToolTip toolTip;
        private SaveFileDialog saveFileText;
        private SaveFileDialog saveFileCSV;
        private SaveFileDialog saveFileImage;
        private ContextMenuStrip cmPlot;
        private ToolStripMenuItem cmiNonInteractivePlot;
        private ToolStripMenuItem cmiMaximizePlotWindow;
        private ContextMenuStrip cmPlotImageSave;
        private ToolStripMenuItem cmiPlotImageSave;
        private HistoryListControl listPlotHistory;
        private CheckBox cbPlotHistory;
        private Display.CellSelectionControl cellSelectionPlot;
        private PictureBox pictureBox;
        private Panel pPictureBox;
        private General.StartEndTimeControl timeRangePlot;
    }
}