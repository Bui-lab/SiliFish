using SiliFish.UI.Controls.Display;

namespace SiliFish.UI.Controls
{
    partial class ModelOutputControl
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
            tabOutputs = new TabControl();
            t2DRender = new TabPage();
            twoDRenderControl = new TwoDRenderControl();
            t3DRender = new TabPage();
            threeDRenderControl = new ThreeDRenderControl();
            tPlot = new TabPage();
            plotControl = new PlotControl();
            tStats = new TabPage();
            statOutputControl = new StatOutputControl();
            tAnimation = new TabPage();
            animationControl = new AnimationControl();
            tabOutputs.SuspendLayout();
            t2DRender.SuspendLayout();
            t3DRender.SuspendLayout();
            tPlot.SuspendLayout();
            tStats.SuspendLayout();
            tAnimation.SuspendLayout();
            SuspendLayout();
            // 
            // tabOutputs
            // 
            tabOutputs.Controls.Add(t2DRender);
            tabOutputs.Controls.Add(t3DRender);
            tabOutputs.Controls.Add(tPlot);
            tabOutputs.Controls.Add(tStats);
            tabOutputs.Controls.Add(tAnimation);
            tabOutputs.Dock = DockStyle.Fill;
            tabOutputs.Location = new Point(0, 0);
            tabOutputs.Name = "tabOutputs";
            tabOutputs.SelectedIndex = 0;
            tabOutputs.Size = new Size(702, 795);
            tabOutputs.TabIndex = 2;
            tabOutputs.SelectedIndexChanged += tabOutputs_SelectedIndexChanged;
            // 
            // t2DRender
            // 
            t2DRender.Controls.Add(twoDRenderControl);
            t2DRender.Location = new Point(4, 24);
            t2DRender.Name = "t2DRender";
            t2DRender.Size = new Size(694, 767);
            t2DRender.TabIndex = 2;
            t2DRender.Text = "2D Rendering";
            t2DRender.UseVisualStyleBackColor = true;
            // 
            // twoDRenderControl
            // 
            twoDRenderControl.Dock = DockStyle.Fill;
            twoDRenderControl.Location = new Point(0, 0);
            twoDRenderControl.Name = "twoDRenderControl";
            twoDRenderControl.Size = new Size(694, 767);
            twoDRenderControl.TabIndex = 0;
            // 
            // t3DRender
            // 
            t3DRender.Controls.Add(threeDRenderControl);
            t3DRender.Location = new Point(4, 24);
            t3DRender.Name = "t3DRender";
            t3DRender.Size = new Size(694, 767);
            t3DRender.TabIndex = 0;
            t3DRender.Text = "3D Rendering";
            t3DRender.UseVisualStyleBackColor = true;
            // 
            // threeDRenderControl
            // 
            threeDRenderControl.Dock = DockStyle.Fill;
            threeDRenderControl.Location = new Point(0, 0);
            threeDRenderControl.Name = "threeDRenderControl";
            threeDRenderControl.Size = new Size(694, 767);
            threeDRenderControl.TabIndex = 0;
            // 
            // tPlot
            // 
            tPlot.Controls.Add(plotControl);
            tPlot.Location = new Point(4, 24);
            tPlot.Name = "tPlot";
            tPlot.Padding = new Padding(3);
            tPlot.Size = new Size(694, 767);
            tPlot.TabIndex = 4;
            tPlot.Text = "Plots";
            tPlot.UseVisualStyleBackColor = true;
            // 
            // plotControl
            // 
            plotControl.Dock = DockStyle.Fill;
            plotControl.Location = new Point(3, 3);
            plotControl.Name = "plotControl";
            plotControl.Size = new Size(688, 761);
            plotControl.TabIndex = 0;
            // 
            // tStats
            // 
            tStats.Controls.Add(statOutputControl);
            tStats.Location = new Point(4, 24);
            tStats.Name = "tStats";
            tStats.Size = new Size(694, 767);
            tStats.TabIndex = 8;
            tStats.Text = "Stats";
            tStats.UseVisualStyleBackColor = true;
            // 
            // statOutputControl
            // 
            statOutputControl.Dock = DockStyle.Fill;
            statOutputControl.Location = new Point(0, 0);
            statOutputControl.Name = "statOutputControl";
            statOutputControl.Size = new Size(694, 767);
            statOutputControl.TabIndex = 0;
            // 
            // tAnimation
            // 
            tAnimation.Controls.Add(animationControl);
            tAnimation.Location = new Point(4, 24);
            tAnimation.Name = "tAnimation";
            tAnimation.Size = new Size(694, 767);
            tAnimation.TabIndex = 3;
            tAnimation.Text = "Animation";
            tAnimation.UseVisualStyleBackColor = true;
            // 
            // animationControl
            // 
            animationControl.Dock = DockStyle.Fill;
            animationControl.Location = new Point(0, 0);
            animationControl.Name = "animationControl";
            animationControl.Size = new Size(694, 767);
            animationControl.TabIndex = 0;
            // 
            // ModelOutputControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabOutputs);
            Name = "ModelOutputControl";
            Size = new Size(702, 795);
            Load += ModelOutputControl_Load;
            tabOutputs.ResumeLayout(false);
            t2DRender.ResumeLayout(false);
            t3DRender.ResumeLayout(false);
            tPlot.ResumeLayout(false);
            tStats.ResumeLayout(false);
            tAnimation.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabOutputs;
        private TabPage tPlot;
        private TabPage t3DRender;
        private TabPage tAnimation;
        private TabPage tStats;
        private TabPage t2DRender;
        private TwoDRenderControl twoDRenderControl;
        private AnimationControl animationControl;
        private StatOutputControl statOutputControl;
        private PlotControl plotControl;
        private ThreeDRenderControl threeDRenderControl;
    }
}