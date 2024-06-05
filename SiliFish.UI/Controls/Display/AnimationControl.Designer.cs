using SiliFish.UI.Controls.Display;

namespace SiliFish.UI.Controls
{
    partial class AnimationControl
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
            webViewAnimation = new SiliFishWebView();
            pAnimation = new Panel();
            timeRangeAnimation = new General.StartEndTimeControl();
            pLineAnimation = new Panel();
            eAnimationdt = new NumericUpDown();
            lAnimationdt = new Label();
            linkSaveAnimationCSV = new LinkLabel();
            lAnimationTime = new Label();
            linkSaveAnimationHTML = new LinkLabel();
            btnAnimate = new Button();
            cmAnimation = new ContextMenuStrip(components);
            cmiAnimationClearCache = new ToolStripMenuItem();
            saveFileHTML = new SaveFileDialog();
            saveFileCSV = new SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)webViewAnimation).BeginInit();
            pAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)eAnimationdt).BeginInit();
            cmAnimation.SuspendLayout();
            SuspendLayout();
            // 
            // webViewAnimation
            // 
            webViewAnimation.AllowExternalDrop = true;
            webViewAnimation.BackColor = Color.White;
            webViewAnimation.CreationProperties = null;
            webViewAnimation.DefaultBackgroundColor = Color.White;
            webViewAnimation.Dock = DockStyle.Fill;
            webViewAnimation.Location = new Point(0, 64);
            webViewAnimation.Name = "webViewAnimation";
            webViewAnimation.Size = new Size(702, 731);
            webViewAnimation.TabIndex = 2;
            webViewAnimation.ZoomFactor = 1D;
            // 
            // pAnimation
            // 
            pAnimation.BackColor = Color.FromArgb(236, 239, 241);
            pAnimation.Controls.Add(timeRangeAnimation);
            pAnimation.Controls.Add(pLineAnimation);
            pAnimation.Controls.Add(eAnimationdt);
            pAnimation.Controls.Add(lAnimationdt);
            pAnimation.Controls.Add(linkSaveAnimationCSV);
            pAnimation.Controls.Add(lAnimationTime);
            pAnimation.Controls.Add(linkSaveAnimationHTML);
            pAnimation.Controls.Add(btnAnimate);
            pAnimation.Dock = DockStyle.Top;
            pAnimation.Location = new Point(0, 0);
            pAnimation.Name = "pAnimation";
            pAnimation.Size = new Size(702, 64);
            pAnimation.TabIndex = 5;
            // 
            // timeRangeAnimation
            // 
            timeRangeAnimation.EndTime = 1000;
            timeRangeAnimation.Location = new Point(3, 4);
            timeRangeAnimation.Name = "timeRangeAnimation";
            timeRangeAnimation.Size = new Size(207, 56);
            timeRangeAnimation.StartTime = 0;
            timeRangeAnimation.TabIndex = 68;
            // 
            // pLineAnimation
            // 
            pLineAnimation.BackColor = Color.LightGray;
            pLineAnimation.Dock = DockStyle.Bottom;
            pLineAnimation.Location = new Point(0, 63);
            pLineAnimation.Name = "pLineAnimation";
            pLineAnimation.Size = new Size(702, 1);
            pLineAnimation.TabIndex = 38;
            // 
            // eAnimationdt
            // 
            eAnimationdt.DecimalPlaces = 2;
            eAnimationdt.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            eAnimationdt.Location = new Point(286, 6);
            eAnimationdt.Name = "eAnimationdt";
            eAnimationdt.Size = new Size(53, 23);
            eAnimationdt.TabIndex = 37;
            eAnimationdt.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lAnimationdt
            // 
            lAnimationdt.AutoSize = true;
            lAnimationdt.Location = new Point(261, 9);
            lAnimationdt.Name = "lAnimationdt";
            lAnimationdt.Size = new Size(19, 15);
            lAnimationdt.TabIndex = 36;
            lAnimationdt.Text = "Δt";
            // 
            // linkSaveAnimationCSV
            // 
            linkSaveAnimationCSV.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkSaveAnimationCSV.AutoSize = true;
            linkSaveAnimationCSV.Enabled = false;
            linkSaveAnimationCSV.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveAnimationCSV.Location = new Point(641, 37);
            linkSaveAnimationCSV.Name = "linkSaveAnimationCSV";
            linkSaveAnimationCSV.Size = new Size(58, 15);
            linkSaveAnimationCSV.TabIndex = 34;
            linkSaveAnimationCSV.TabStop = true;
            linkSaveAnimationCSV.Text = "Save CSV ";
            linkSaveAnimationCSV.TextAlign = ContentAlignment.MiddleRight;
            linkSaveAnimationCSV.LinkClicked += linkSaveAnimationCSV_LinkClicked;
            // 
            // lAnimationTime
            // 
            lAnimationTime.Location = new Point(345, 6);
            lAnimationTime.Name = "lAnimationTime";
            lAnimationTime.Size = new Size(271, 54);
            lAnimationTime.TabIndex = 33;
            lAnimationTime.Text = "Last animation:";
            // 
            // linkSaveAnimationHTML
            // 
            linkSaveAnimationHTML.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkSaveAnimationHTML.AutoSize = true;
            linkSaveAnimationHTML.Enabled = false;
            linkSaveAnimationHTML.LinkColor = Color.FromArgb(64, 64, 64);
            linkSaveAnimationHTML.Location = new Point(630, 14);
            linkSaveAnimationHTML.Name = "linkSaveAnimationHTML";
            linkSaveAnimationHTML.Size = new Size(69, 15);
            linkSaveAnimationHTML.TabIndex = 31;
            linkSaveAnimationHTML.TabStop = true;
            linkSaveAnimationHTML.Text = "Save HTML ";
            linkSaveAnimationHTML.TextAlign = ContentAlignment.MiddleRight;
            linkSaveAnimationHTML.LinkClicked += linkSaveAnimationHTML_LinkClicked;
            // 
            // btnAnimate
            // 
            btnAnimate.BackColor = Color.FromArgb(96, 125, 139);
            btnAnimate.ContextMenuStrip = cmAnimation;
            btnAnimate.Enabled = false;
            btnAnimate.FlatAppearance.BorderColor = Color.LightGray;
            btnAnimate.FlatStyle = FlatStyle.Flat;
            btnAnimate.ForeColor = Color.White;
            btnAnimate.Location = new Point(264, 32);
            btnAnimate.Name = "btnAnimate";
            btnAnimate.Size = new Size(75, 24);
            btnAnimate.TabIndex = 29;
            btnAnimate.Text = "Animate";
            btnAnimate.UseVisualStyleBackColor = false;
            btnAnimate.Click += btnAnimate_Click;
            // 
            // cmAnimation
            // 
            cmAnimation.Items.AddRange(new ToolStripItem[] { cmiAnimationClearCache });
            cmAnimation.Name = "cmWebView";
            cmAnimation.Size = new Size(138, 26);
            // 
            // cmiAnimationClearCache
            // 
            cmiAnimationClearCache.Name = "cmiAnimationClearCache";
            cmiAnimationClearCache.Size = new Size(137, 22);
            cmiAnimationClearCache.Text = "Clear Cache";
            cmiAnimationClearCache.Click += cmiAnimationClearCache_Click;
            // 
            // saveFileHTML
            // 
            saveFileHTML.Filter = "HTML files(*.html)|*.html";
            // 
            // saveFileCSV
            // 
            saveFileCSV.Filter = "CSV files(*.csv)|*.csv";
            // 
            // AnimationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(webViewAnimation);
            Controls.Add(pAnimation);
            Name = "AnimationControl";
            Size = new Size(702, 795);
            ((System.ComponentModel.ISupportInitialize)webViewAnimation).EndInit();
            pAnimation.ResumeLayout(false);
            pAnimation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)eAnimationdt).EndInit();
            cmAnimation.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private SiliFishWebView webViewAnimation;
        private Panel pAnimation;
        private Panel pLineAnimation;
        private NumericUpDown eAnimationdt;
        private Label lAnimationdt;
        private LinkLabel linkSaveAnimationCSV;
        private Label lAnimationTime;
        private LinkLabel linkSaveAnimationHTML;
        private Button btnAnimate;
        private SaveFileDialog saveFileHTML;
        private ContextMenuStrip cmAnimation;
        private ToolStripMenuItem cmiAnimationClearCache;
        private General.StartEndTimeControl timeRangeAnimation;
        private SaveFileDialog saveFileCSV;
    }
}