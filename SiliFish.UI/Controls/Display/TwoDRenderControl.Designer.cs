using SiliFish.UI.Controls.Display;

namespace SiliFish.UI.Controls
{
    partial class TwoDRenderControl
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
            gr2DCellPoolLegend = new GroupBox();
            p2DRenderOptions = new Panel();
            cb2DMuscleCells = new CheckBox();
            cb2DInterneuron = new CheckBox();
            cb2DMotoneuron = new CheckBox();
            ud2DNodeSize = new General.UpDownControl();
            ud2DLinkSize = new General.UpDownControl();
            cb2DOffline = new CheckBox();
            cb2DShowUnselectedNodes = new CheckBox();
            cb2DHideNonspiking = new CheckBox();
            cb2DLegend = new CheckBox();
            l2DLinkSize = new Label();
            cb2DChemJunc = new CheckBox();
            cb2DGapJunc = new CheckBox();
            l2DNodeSize = new Label();
            gr2DLegend = new GroupBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            p2DRender = new Panel();
            cb2DRenderShowOptions = new CheckBox();
            pLine2D = new Panel();
            linkSaveHTML2D = new LinkLabel();
            btn2DRender = new Button();
            saveFileHTML = new SaveFileDialog();
            webView2DRender = new SiliFishWebView();
            p2DRenderOptions.SuspendLayout();
            gr2DLegend.SuspendLayout();
            p2DRender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView2DRender).BeginInit();
            SuspendLayout();
            // 
            // gr2DCellPoolLegend
            // 
            gr2DCellPoolLegend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gr2DCellPoolLegend.AutoSize = true;
            gr2DCellPoolLegend.Location = new Point(560, 131);
            gr2DCellPoolLegend.Name = "gr2DCellPoolLegend";
            gr2DCellPoolLegend.Size = new Size(122, 210);
            gr2DCellPoolLegend.TabIndex = 55;
            gr2DCellPoolLegend.TabStop = false;
            gr2DCellPoolLegend.Text = "Cell Pool Legend";
            // 
            // p2DRenderOptions
            // 
            p2DRenderOptions.BackColor = Color.FromArgb(236, 239, 241);
            p2DRenderOptions.Controls.Add(cb2DMuscleCells);
            p2DRenderOptions.Controls.Add(cb2DInterneuron);
            p2DRenderOptions.Controls.Add(cb2DMotoneuron);
            p2DRenderOptions.Controls.Add(ud2DNodeSize);
            p2DRenderOptions.Controls.Add(ud2DLinkSize);
            p2DRenderOptions.Controls.Add(cb2DOffline);
            p2DRenderOptions.Controls.Add(cb2DShowUnselectedNodes);
            p2DRenderOptions.Controls.Add(cb2DHideNonspiking);
            p2DRenderOptions.Controls.Add(cb2DLegend);
            p2DRenderOptions.Controls.Add(l2DLinkSize);
            p2DRenderOptions.Controls.Add(cb2DChemJunc);
            p2DRenderOptions.Controls.Add(cb2DGapJunc);
            p2DRenderOptions.Controls.Add(l2DNodeSize);
            p2DRenderOptions.Dock = DockStyle.Top;
            p2DRenderOptions.Location = new Point(0, 40);
            p2DRenderOptions.Name = "p2DRenderOptions";
            p2DRenderOptions.Size = new Size(702, 85);
            p2DRenderOptions.TabIndex = 55;
            p2DRenderOptions.Visible = false;
            // 
            // cb2DMuscleCells
            // 
            cb2DMuscleCells.Checked = true;
            cb2DMuscleCells.CheckState = CheckState.Checked;
            cb2DMuscleCells.Location = new Point(134, 56);
            cb2DMuscleCells.Name = "cb2DMuscleCells";
            cb2DMuscleCells.Size = new Size(115, 20);
            cb2DMuscleCells.TabIndex = 80;
            cb2DMuscleCells.Text = "Muscle Cells";
            cb2DMuscleCells.UseVisualStyleBackColor = true;
            cb2DMuscleCells.CheckedChanged += cb2DCellSelection_CheckedChanged;
            // 
            // cb2DInterneuron
            // 
            cb2DInterneuron.Checked = true;
            cb2DInterneuron.CheckState = CheckState.Checked;
            cb2DInterneuron.Location = new Point(134, 4);
            cb2DInterneuron.Name = "cb2DInterneuron";
            cb2DInterneuron.Size = new Size(103, 20);
            cb2DInterneuron.TabIndex = 78;
            cb2DInterneuron.Text = "Interneurons";
            cb2DInterneuron.UseVisualStyleBackColor = true;
            cb2DInterneuron.CheckedChanged += cb2DCellSelection_CheckedChanged;
            // 
            // cb2DMotoneuron
            // 
            cb2DMotoneuron.Checked = true;
            cb2DMotoneuron.CheckState = CheckState.Checked;
            cb2DMotoneuron.Location = new Point(134, 30);
            cb2DMotoneuron.Name = "cb2DMotoneuron";
            cb2DMotoneuron.Size = new Size(115, 20);
            cb2DMotoneuron.TabIndex = 79;
            cb2DMotoneuron.Text = "Motoneurons";
            cb2DMotoneuron.UseVisualStyleBackColor = true;
            cb2DMotoneuron.CheckedChanged += cb2DCellSelection_CheckedChanged;
            // 
            // ud2DNodeSize
            // 
            ud2DNodeSize.Location = new Point(423, 2);
            ud2DNodeSize.Name = "ud2DNodeSize";
            ud2DNodeSize.Size = new Size(18, 23);
            ud2DNodeSize.TabIndex = 77;
            ud2DNodeSize.Text = "ud2DNodeSize";
            ud2DNodeSize.DownClicked += ud2DNodeSize_DownClicked;
            ud2DNodeSize.UpClicked += ud2DNodeSize_UpClicked;
            // 
            // ud2DLinkSize
            // 
            ud2DLinkSize.Location = new Point(423, 28);
            ud2DLinkSize.Name = "ud2DLinkSize";
            ud2DLinkSize.Size = new Size(18, 23);
            ud2DLinkSize.TabIndex = 76;
            ud2DLinkSize.Text = "ud2DLinkSize";
            ud2DLinkSize.DownClicked += ud2DLinkSize_DownClicked;
            ud2DLinkSize.UpClicked += ud2DLinkSize_UpClicked;
            // 
            // cb2DOffline
            // 
            cb2DOffline.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cb2DOffline.AutoSize = true;
            cb2DOffline.Checked = true;
            cb2DOffline.CheckState = CheckState.Checked;
            cb2DOffline.Location = new Point(633, 30);
            cb2DOffline.Name = "cb2DOffline";
            cb2DOffline.Size = new Size(62, 19);
            cb2DOffline.TabIndex = 75;
            cb2DOffline.Text = "Offline";
            cb2DOffline.UseVisualStyleBackColor = true;
            // 
            // cb2DShowUnselectedNodes
            // 
            cb2DShowUnselectedNodes.Checked = true;
            cb2DShowUnselectedNodes.CheckState = CheckState.Checked;
            cb2DShowUnselectedNodes.Location = new Point(262, 56);
            cb2DShowUnselectedNodes.Name = "cb2DShowUnselectedNodes";
            cb2DShowUnselectedNodes.Size = new Size(180, 20);
            cb2DShowUnselectedNodes.TabIndex = 74;
            cb2DShowUnselectedNodes.Text = "Show Unselected Nodes";
            cb2DShowUnselectedNodes.UseVisualStyleBackColor = true;
            cb2DShowUnselectedNodes.CheckedChanged += cb2DShowUnselectedNodes_CheckedChanged;
            // 
            // cb2DHideNonspiking
            // 
            cb2DHideNonspiking.AutoSize = true;
            cb2DHideNonspiking.Location = new Point(8, 4);
            cb2DHideNonspiking.Name = "cb2DHideNonspiking";
            cb2DHideNonspiking.Size = new Size(120, 19);
            cb2DHideNonspiking.TabIndex = 73;
            cb2DHideNonspiking.Text = "Hide Non-spiking";
            cb2DHideNonspiking.ThreeState = true;
            cb2DHideNonspiking.UseVisualStyleBackColor = true;
            cb2DHideNonspiking.CheckStateChanged += cb2DHideNonspiking_CheckStateChanged;
            // 
            // cb2DLegend
            // 
            cb2DLegend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cb2DLegend.AutoSize = true;
            cb2DLegend.Checked = true;
            cb2DLegend.CheckState = CheckState.Checked;
            cb2DLegend.Location = new Point(633, 8);
            cb2DLegend.Name = "cb2DLegend";
            cb2DLegend.Size = new Size(65, 19);
            cb2DLegend.TabIndex = 60;
            cb2DLegend.Text = "Legend";
            cb2DLegend.UseVisualStyleBackColor = true;
            cb2DLegend.CheckedChanged += cb2DLegend_CheckedChanged;
            // 
            // l2DLinkSize
            // 
            l2DLinkSize.AutoSize = true;
            l2DLinkSize.Location = new Point(353, 32);
            l2DLinkSize.Name = "l2DLinkSize";
            l2DLinkSize.Size = new Size(64, 15);
            l2DLinkSize.TabIndex = 71;
            l2DLinkSize.Text = "Link Width";
            l2DLinkSize.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cb2DChemJunc
            // 
            cb2DChemJunc.Checked = true;
            cb2DChemJunc.CheckState = CheckState.Checked;
            cb2DChemJunc.Location = new Point(262, 4);
            cb2DChemJunc.Name = "cb2DChemJunc";
            cb2DChemJunc.Size = new Size(85, 20);
            cb2DChemJunc.TabIndex = 58;
            cb2DChemJunc.Text = "Chem Junc";
            cb2DChemJunc.UseVisualStyleBackColor = true;
            cb2DChemJunc.CheckedChanged += cb2DChemJunc_CheckedChanged;
            // 
            // cb2DGapJunc
            // 
            cb2DGapJunc.Checked = true;
            cb2DGapJunc.CheckState = CheckState.Checked;
            cb2DGapJunc.Location = new Point(262, 30);
            cb2DGapJunc.Name = "cb2DGapJunc";
            cb2DGapJunc.Size = new Size(80, 20);
            cb2DGapJunc.TabIndex = 59;
            cb2DGapJunc.Text = "Gap Junc";
            cb2DGapJunc.UseVisualStyleBackColor = true;
            cb2DGapJunc.CheckedChanged += cb2DGapJunc_CheckedChanged;
            // 
            // l2DNodeSize
            // 
            l2DNodeSize.AutoSize = true;
            l2DNodeSize.Location = new Point(353, 6);
            l2DNodeSize.Name = "l2DNodeSize";
            l2DNodeSize.Size = new Size(59, 15);
            l2DNodeSize.TabIndex = 69;
            l2DNodeSize.Text = "Node Size";
            l2DNodeSize.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gr2DLegend
            // 
            gr2DLegend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            gr2DLegend.Controls.Add(label1);
            gr2DLegend.Controls.Add(label2);
            gr2DLegend.Controls.Add(label3);
            gr2DLegend.Controls.Add(label4);
            gr2DLegend.Location = new Point(560, 680);
            gr2DLegend.Name = "gr2DLegend";
            gr2DLegend.Size = new Size(122, 94);
            gr2DLegend.TabIndex = 54;
            gr2DLegend.TabStop = false;
            gr2DLegend.Text = "Projection Legend";
            // 
            // label1
            // 
            label1.BackColor = Color.Gray;
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(6, 17);
            label1.Name = "label1";
            label1.Size = new Size(110, 18);
            label1.TabIndex = 49;
            label1.Text = "Cholinergic";
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(0, 204, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(6, 71);
            label2.Name = "label2";
            label2.Size = new Size(110, 18);
            label2.TabIndex = 52;
            label2.Text = "Excitatory Projections";
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(51, 51, 51);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(6, 35);
            label3.Name = "label3";
            label3.Size = new Size(110, 18);
            label3.TabIndex = 50;
            label3.Text = "Gap junc";
            // 
            // label4
            // 
            label4.BackColor = Color.Magenta;
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(6, 53);
            label4.Name = "label4";
            label4.Size = new Size(110, 18);
            label4.TabIndex = 51;
            label4.Text = "Inhibitory";
            // 
            // p2DRender
            // 
            p2DRender.BackColor = Color.FromArgb(236, 239, 241);
            p2DRender.Controls.Add(cb2DRenderShowOptions);
            p2DRender.Controls.Add(pLine2D);
            p2DRender.Controls.Add(linkSaveHTML2D);
            p2DRender.Controls.Add(btn2DRender);
            p2DRender.Dock = DockStyle.Top;
            p2DRender.Location = new Point(0, 0);
            p2DRender.Name = "p2DRender";
            p2DRender.Size = new Size(702, 40);
            p2DRender.TabIndex = 2;
            // 
            // cb2DRenderShowOptions
            // 
            cb2DRenderShowOptions.AutoSize = true;
            cb2DRenderShowOptions.Location = new Point(145, 12);
            cb2DRenderShowOptions.Name = "cb2DRenderShowOptions";
            cb2DRenderShowOptions.Size = new Size(100, 19);
            cb2DRenderShowOptions.TabIndex = 25;
            cb2DRenderShowOptions.Text = "Show Options";
            cb2DRenderShowOptions.UseVisualStyleBackColor = true;
            cb2DRenderShowOptions.CheckedChanged += cb2DRenderShowOptions_CheckedChanged;
            // 
            // pLine2D
            // 
            pLine2D.BackColor = Color.LightGray;
            pLine2D.Dock = DockStyle.Bottom;
            pLine2D.Location = new Point(0, 39);
            pLine2D.Name = "pLine2D";
            pLine2D.Size = new Size(702, 1);
            pLine2D.TabIndex = 24;
            // 
            // linkSaveHTML2D
            // 
            linkSaveHTML2D.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkSaveHTML2D.AutoSize = true;
            linkSaveHTML2D.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveHTML2D.Location = new Point(629, 13);
            linkSaveHTML2D.Name = "linkSaveHTML2D";
            linkSaveHTML2D.Size = new Size(69, 15);
            linkSaveHTML2D.TabIndex = 23;
            linkSaveHTML2D.TabStop = true;
            linkSaveHTML2D.Text = "Save HTML ";
            linkSaveHTML2D.TextAlign = ContentAlignment.MiddleRight;
            linkSaveHTML2D.LinkClicked += linkSaveHTML2D_LinkClicked;
            // 
            // btn2DRender
            // 
            btn2DRender.BackColor = Color.FromArgb(96, 125, 139);
            btn2DRender.FlatAppearance.BorderColor = Color.LightGray;
            btn2DRender.FlatStyle = FlatStyle.Flat;
            btn2DRender.ForeColor = Color.White;
            btn2DRender.Location = new Point(8, 8);
            btn2DRender.Name = "btn2DRender";
            btn2DRender.Size = new Size(120, 24);
            btn2DRender.TabIndex = 22;
            btn2DRender.Text = "Render";
            btn2DRender.UseVisualStyleBackColor = false;
            btn2DRender.Click += btn2DRender_Click;
            // 
            // saveFileHTML
            // 
            saveFileHTML.Filter = "HTML files(*.html)|*.html";
            // 
            // webView2DRender
            // 
            webView2DRender.AllowExternalDrop = true;
            webView2DRender.CreationProperties = null;
            webView2DRender.DefaultBackgroundColor = Color.White;
            webView2DRender.Dock = DockStyle.Fill;
            webView2DRender.Location = new Point(0, 0);
            webView2DRender.Name = "webView2DRender";
            webView2DRender.Size = new Size(702, 795);
            webView2DRender.TabIndex = 56;
            webView2DRender.ZoomFactor = 1D;
            // 
            // TwoDRenderControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gr2DLegend);
            Controls.Add(gr2DCellPoolLegend);
            Controls.Add(p2DRenderOptions);
            Controls.Add(p2DRender);
            Controls.Add(webView2DRender);
            Name = "TwoDRenderControl";
            Size = new Size(702, 795);
            p2DRenderOptions.ResumeLayout(false);
            p2DRenderOptions.PerformLayout();
            gr2DLegend.ResumeLayout(false);
            p2DRender.ResumeLayout(false);
            p2DRender.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView2DRender).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel p2DRender;
        private Panel pLine2D;
        private LinkLabel linkSaveHTML2D;
        private Button btn2DRender;
        private SaveFileDialog saveFileHTML;
        private CheckBox cb2DLegend;
        private CheckBox cb2DGapJunc;
        private CheckBox cb2DChemJunc;
        private GroupBox gr2DLegend;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label l2DNodeSize;
        private Label l2DLinkSize;
        private CheckBox cb2DHideNonspiking;
        private CheckBox cb2DShowUnselectedNodes;
        private Panel p2DRenderOptions;
        private CheckBox cb2DRenderShowOptions;
        private CheckBox cb2DOffline;
        private GroupBox gr2DCellPoolLegend;
        private SiliFishWebView webView2DRender;
        private General.UpDownControl ud2DNodeSize;
        private General.UpDownControl ud2DLinkSize;
        private CheckBox cb2DMuscleCells;
        private CheckBox cb2DInterneuron;
        private CheckBox cb2DMotoneuron;
    }
}