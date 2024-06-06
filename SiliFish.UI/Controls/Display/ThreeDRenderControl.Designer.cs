using SiliFish.UI.Controls.Display;

namespace SiliFish.UI.Controls
{
    partial class ThreeDRenderControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThreeDRenderControl));
            p3DRenderOptions = new Panel();
            cb3DOffline = new CheckBox();
            l3DNodeSize = new Label();
            ud3DNodeSize = new General.UpDownControl();
            cb3DShowUnselectedNodes = new CheckBox();
            cb3DGapJunc = new CheckBox();
            cb3DLegend = new CheckBox();
            cb3DChemJunc = new CheckBox();
            e3DSomiteRange = new TextBox();
            dd3DViewpoint = new ComboBox();
            cb3DAllSomites = new CheckBox();
            btnZoomIn = new Button();
            btnZoomOut = new Button();
            gr3DLegend = new GroupBox();
            legendUnselected = new Label();
            legendOutgoing = new Label();
            legendGap = new Label();
            legendIncoming = new Label();
            p3DRender = new Panel();
            cb3DRenderShowOptions = new CheckBox();
            pLine3D = new Panel();
            linkSaveHTML3D = new LinkLabel();
            btn3DRender = new Button();
            saveFileHTML = new SaveFileDialog();
            saveFileImage = new SaveFileDialog();
            webView3DRender = new SiliFishWebView();
            p3DRenderOptions.SuspendLayout();
            gr3DLegend.SuspendLayout();
            p3DRender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView3DRender).BeginInit();
            SuspendLayout();
            // 
            // p3DRenderOptions
            // 
            p3DRenderOptions.BackColor = Color.FromArgb(236, 239, 241);
            p3DRenderOptions.Controls.Add(cb3DOffline);
            p3DRenderOptions.Controls.Add(l3DNodeSize);
            p3DRenderOptions.Controls.Add(ud3DNodeSize);
            p3DRenderOptions.Controls.Add(cb3DShowUnselectedNodes);
            p3DRenderOptions.Controls.Add(cb3DGapJunc);
            p3DRenderOptions.Controls.Add(cb3DLegend);
            p3DRenderOptions.Controls.Add(cb3DChemJunc);
            p3DRenderOptions.Controls.Add(e3DSomiteRange);
            p3DRenderOptions.Controls.Add(dd3DViewpoint);
            p3DRenderOptions.Controls.Add(cb3DAllSomites);
            p3DRenderOptions.Controls.Add(btnZoomIn);
            p3DRenderOptions.Controls.Add(btnZoomOut);
            p3DRenderOptions.Dock = DockStyle.Top;
            p3DRenderOptions.Location = new Point(0, 40);
            p3DRenderOptions.Name = "p3DRenderOptions";
            p3DRenderOptions.Size = new Size(702, 85);
            p3DRenderOptions.TabIndex = 58;
            p3DRenderOptions.Visible = false;
            // 
            // cb3DOffline
            // 
            cb3DOffline.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cb3DOffline.AutoSize = true;
            cb3DOffline.Checked = true;
            cb3DOffline.CheckState = CheckState.Checked;
            cb3DOffline.Location = new Point(633, 29);
            cb3DOffline.Name = "cb3DOffline";
            cb3DOffline.Size = new Size(62, 19);
            cb3DOffline.TabIndex = 76;
            cb3DOffline.Text = "Offline";
            cb3DOffline.UseVisualStyleBackColor = true;
            // 
            // l3DNodeSize
            // 
            l3DNodeSize.AutoSize = true;
            l3DNodeSize.Location = new Point(249, 6);
            l3DNodeSize.Name = "l3DNodeSize";
            l3DNodeSize.Size = new Size(59, 15);
            l3DNodeSize.TabIndex = 67;
            l3DNodeSize.Text = "Node Size";
            // 
            // ud3DNodeSize
            // 
            ud3DNodeSize.Location = new Point(314, 3);
            ud3DNodeSize.Name = "ud3DNodeSize";
            ud3DNodeSize.Size = new Size(19, 23);
            ud3DNodeSize.TabIndex = 77;
            ud3DNodeSize.Text = "ud3DNodeSize";
            // 
            // cb3DShowUnselectedNodes
            // 
            cb3DShowUnselectedNodes.Checked = true;
            cb3DShowUnselectedNodes.CheckState = CheckState.Checked;
            cb3DShowUnselectedNodes.Location = new Point(145, 58);
            cb3DShowUnselectedNodes.Name = "cb3DShowUnselectedNodes";
            cb3DShowUnselectedNodes.Size = new Size(180, 20);
            cb3DShowUnselectedNodes.TabIndex = 58;
            cb3DShowUnselectedNodes.Text = "Show Unselected Nodes";
            cb3DShowUnselectedNodes.UseVisualStyleBackColor = true;
            cb3DShowUnselectedNodes.CheckedChanged += cb3DShowUnselectedNodes_CheckedChanged;
            // 
            // cb3DGapJunc
            // 
            cb3DGapJunc.Checked = true;
            cb3DGapJunc.CheckState = CheckState.Checked;
            cb3DGapJunc.Location = new Point(145, 32);
            cb3DGapJunc.Name = "cb3DGapJunc";
            cb3DGapJunc.Size = new Size(80, 20);
            cb3DGapJunc.TabIndex = 39;
            cb3DGapJunc.Text = "Gap Junc";
            cb3DGapJunc.UseVisualStyleBackColor = true;
            cb3DGapJunc.CheckedChanged += cb3DGapJunc_CheckedChanged;
            // 
            // cb3DLegend
            // 
            cb3DLegend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cb3DLegend.AutoSize = true;
            cb3DLegend.Checked = true;
            cb3DLegend.CheckState = CheckState.Checked;
            cb3DLegend.Location = new Point(633, 8);
            cb3DLegend.Name = "cb3DLegend";
            cb3DLegend.Size = new Size(65, 19);
            cb3DLegend.TabIndex = 63;
            cb3DLegend.Text = "Legend";
            cb3DLegend.UseVisualStyleBackColor = true;
            cb3DLegend.CheckedChanged += cb3DLegend_CheckedChanged;
            // 
            // cb3DChemJunc
            // 
            cb3DChemJunc.Checked = true;
            cb3DChemJunc.CheckState = CheckState.Checked;
            cb3DChemJunc.Location = new Point(145, 6);
            cb3DChemJunc.Name = "cb3DChemJunc";
            cb3DChemJunc.Size = new Size(85, 20);
            cb3DChemJunc.TabIndex = 38;
            cb3DChemJunc.Text = "Chem Junc";
            cb3DChemJunc.UseVisualStyleBackColor = true;
            cb3DChemJunc.CheckedChanged += cb3DChemJunc_CheckedChanged;
            // 
            // e3DSomiteRange
            // 
            e3DSomiteRange.Location = new Point(8, 29);
            e3DSomiteRange.Name = "e3DSomiteRange";
            e3DSomiteRange.Size = new Size(89, 23);
            e3DSomiteRange.TabIndex = 36;
            e3DSomiteRange.Text = "Ex: 1, 3-5";
            e3DSomiteRange.Visible = false;
            e3DSomiteRange.Enter += e3DSomiteRange_Enter;
            e3DSomiteRange.Leave += e3DSomiteRange_Leave;
            // 
            // dd3DViewpoint
            // 
            dd3DViewpoint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dd3DViewpoint.DropDownStyle = ComboBoxStyle.DropDownList;
            dd3DViewpoint.FormattingEnabled = true;
            dd3DViewpoint.Items.AddRange(new object[] { "Free view", "Dorsal view", "Ventral view", "Rostral view", "Caudal view", "Lateral view (left)", "Lateral view (right)" });
            dd3DViewpoint.Location = new Point(454, 51);
            dd3DViewpoint.Name = "dd3DViewpoint";
            dd3DViewpoint.Size = new Size(175, 23);
            dd3DViewpoint.TabIndex = 54;
            dd3DViewpoint.SelectedIndexChanged += dd3DViewpoint_SelectedIndexChanged;
            // 
            // cb3DAllSomites
            // 
            cb3DAllSomites.Checked = true;
            cb3DAllSomites.CheckState = CheckState.Checked;
            cb3DAllSomites.Location = new Point(8, 6);
            cb3DAllSomites.Name = "cb3DAllSomites";
            cb3DAllSomites.Size = new Size(85, 20);
            cb3DAllSomites.TabIndex = 35;
            cb3DAllSomites.Text = "All Somites";
            cb3DAllSomites.UseVisualStyleBackColor = true;
            cb3DAllSomites.CheckedChanged += cb3DAllSomites_CheckedChanged;
            // 
            // btnZoomIn
            // 
            btnZoomIn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnZoomIn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnZoomIn.Image = (Image)resources.GetObject("btnZoomIn.Image");
            btnZoomIn.ImageAlign = ContentAlignment.TopLeft;
            btnZoomIn.Location = new Point(666, 46);
            btnZoomIn.Name = "btnZoomIn";
            btnZoomIn.Size = new Size(32, 32);
            btnZoomIn.TabIndex = 48;
            btnZoomIn.UseVisualStyleBackColor = true;
            btnZoomIn.Click += btnZoomIn_Click;
            // 
            // btnZoomOut
            // 
            btnZoomOut.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnZoomOut.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnZoomOut.Image = (Image)resources.GetObject("btnZoomOut.Image");
            btnZoomOut.ImageAlign = ContentAlignment.TopLeft;
            btnZoomOut.Location = new Point(632, 46);
            btnZoomOut.Name = "btnZoomOut";
            btnZoomOut.Size = new Size(32, 32);
            btnZoomOut.TabIndex = 47;
            btnZoomOut.UseVisualStyleBackColor = true;
            btnZoomOut.Click += btnZoomOut_Click;
            // 
            // gr3DLegend
            // 
            gr3DLegend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            gr3DLegend.Controls.Add(legendUnselected);
            gr3DLegend.Controls.Add(legendOutgoing);
            gr3DLegend.Controls.Add(legendGap);
            gr3DLegend.Controls.Add(legendIncoming);
            gr3DLegend.Location = new Point(560, 680);
            gr3DLegend.Name = "gr3DLegend";
            gr3DLegend.Size = new Size(122, 94);
            gr3DLegend.TabIndex = 53;
            gr3DLegend.TabStop = false;
            gr3DLegend.Text = "Projection Legend";
            // 
            // legendUnselected
            // 
            legendUnselected.BackColor = Color.Gray;
            legendUnselected.ForeColor = SystemColors.ControlLightLight;
            legendUnselected.Location = new Point(6, 17);
            legendUnselected.Name = "legendUnselected";
            legendUnselected.Size = new Size(110, 18);
            legendUnselected.TabIndex = 49;
            legendUnselected.Text = "Unselected";
            // 
            // legendOutgoing
            // 
            legendOutgoing.BackColor = Color.FromArgb(0, 204, 0);
            legendOutgoing.ForeColor = SystemColors.ControlLightLight;
            legendOutgoing.Location = new Point(6, 71);
            legendOutgoing.Name = "legendOutgoing";
            legendOutgoing.Size = new Size(110, 18);
            legendOutgoing.TabIndex = 52;
            legendOutgoing.Text = "Outgoing";
            // 
            // legendGap
            // 
            legendGap.BackColor = Color.FromArgb(51, 51, 51);
            legendGap.ForeColor = SystemColors.ControlLightLight;
            legendGap.Location = new Point(6, 35);
            legendGap.Name = "legendGap";
            legendGap.Size = new Size(110, 18);
            legendGap.TabIndex = 50;
            legendGap.Text = "Gap junc";
            // 
            // legendIncoming
            // 
            legendIncoming.BackColor = Color.FromArgb(204, 0, 0);
            legendIncoming.ForeColor = SystemColors.ControlLightLight;
            legendIncoming.Location = new Point(6, 53);
            legendIncoming.Name = "legendIncoming";
            legendIncoming.Size = new Size(110, 18);
            legendIncoming.TabIndex = 51;
            legendIncoming.Text = "Incoming";
            // 
            // p3DRender
            // 
            p3DRender.BackColor = Color.FromArgb(236, 239, 241);
            p3DRender.Controls.Add(cb3DRenderShowOptions);
            p3DRender.Controls.Add(pLine3D);
            p3DRender.Controls.Add(linkSaveHTML3D);
            p3DRender.Controls.Add(btn3DRender);
            p3DRender.Dock = DockStyle.Top;
            p3DRender.Location = new Point(0, 0);
            p3DRender.Name = "p3DRender";
            p3DRender.Size = new Size(702, 40);
            p3DRender.TabIndex = 3;
            // 
            // cb3DRenderShowOptions
            // 
            cb3DRenderShowOptions.AutoSize = true;
            cb3DRenderShowOptions.Location = new Point(145, 12);
            cb3DRenderShowOptions.Name = "cb3DRenderShowOptions";
            cb3DRenderShowOptions.Size = new Size(100, 19);
            cb3DRenderShowOptions.TabIndex = 59;
            cb3DRenderShowOptions.Text = "Show Options";
            cb3DRenderShowOptions.UseVisualStyleBackColor = true;
            cb3DRenderShowOptions.CheckedChanged += cb3DRenderShowOptions_CheckedChanged;
            // 
            // pLine3D
            // 
            pLine3D.BackColor = Color.LightGray;
            pLine3D.Dock = DockStyle.Bottom;
            pLine3D.Location = new Point(0, 39);
            pLine3D.Name = "pLine3D";
            pLine3D.Size = new Size(702, 1);
            pLine3D.TabIndex = 30;
            // 
            // linkSaveHTML3D
            // 
            linkSaveHTML3D.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkSaveHTML3D.AutoSize = true;
            linkSaveHTML3D.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveHTML3D.Location = new Point(629, 13);
            linkSaveHTML3D.Name = "linkSaveHTML3D";
            linkSaveHTML3D.Size = new Size(69, 15);
            linkSaveHTML3D.TabIndex = 27;
            linkSaveHTML3D.TabStop = true;
            linkSaveHTML3D.Text = "Save HTML ";
            linkSaveHTML3D.TextAlign = ContentAlignment.MiddleRight;
            linkSaveHTML3D.LinkClicked += linkSaveHTML3D_LinkClicked;
            // 
            // btn3DRender
            // 
            btn3DRender.BackColor = Color.FromArgb(96, 125, 139);
            btn3DRender.FlatAppearance.BorderColor = Color.LightGray;
            btn3DRender.FlatStyle = FlatStyle.Flat;
            btn3DRender.ForeColor = Color.White;
            btn3DRender.Location = new Point(8, 8);
            btn3DRender.Name = "btn3DRender";
            btn3DRender.Size = new Size(120, 24);
            btn3DRender.TabIndex = 26;
            btn3DRender.Text = "Render";
            btn3DRender.UseVisualStyleBackColor = false;
            btn3DRender.Click += btn3DRender_Click;
            // 
            // saveFileHTML
            // 
            saveFileHTML.Filter = "HTML files(*.html)|*.html";
            // 
            // saveFileImage
            // 
            saveFileImage.Filter = "Image files(*.png)|*.png";
            // 
            // webView3DRender
            // 
            webView3DRender.AllowExternalDrop = true;
            webView3DRender.CreationProperties = null;
            webView3DRender.DefaultBackgroundColor = Color.White;
            webView3DRender.Dock = DockStyle.Fill;
            webView3DRender.Location = new Point(0, 0);
            webView3DRender.Name = "webView3DRender";
            webView3DRender.Size = new Size(702, 795);
            webView3DRender.TabIndex = 56;
            webView3DRender.ZoomFactor = 1D;
            // 
            // ThreeDRenderControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gr3DLegend);
            Controls.Add(p3DRenderOptions);
            Controls.Add(p3DRender);
            Controls.Add(webView3DRender);
            Name = "ThreeDRenderControl";
            Size = new Size(702, 795);
            p3DRenderOptions.ResumeLayout(false);
            p3DRenderOptions.PerformLayout();
            gr3DLegend.ResumeLayout(false);
            p3DRender.ResumeLayout(false);
            p3DRender.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView3DRender).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ComboBox dd3DViewpoint;
        private Button btnZoomIn;
        private Button btnZoomOut;
        private GroupBox gr3DLegend;
        private Label legendUnselected;
        private Label legendOutgoing;
        private Label legendGap;
        private Label legendIncoming;
        private SiliFishWebView webView3DRender;
        private Panel p3DRender;
        private CheckBox cb3DGapJunc;
        private CheckBox cb3DChemJunc;
        private TextBox e3DSomiteRange;
        private CheckBox cb3DAllSomites;
        private Panel pLine3D;
        private LinkLabel linkSaveHTML3D;
        private Button btn3DRender;
        private SaveFileDialog saveFileHTML;
        private SaveFileDialog saveFileImage;
        private CheckBox cb3DShowUnselectedNodes;
        private Panel p3DRenderOptions;
        private CheckBox cb3DLegend;
        private Label l3DNodeSize;
        private General.UpDownControl ud3DNodeSize;
        private CheckBox cb3DRenderShowOptions;
        private CheckBox cb3DOffline;
    }
}