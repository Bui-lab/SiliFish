namespace SiliFish.UI
{
    partial class WebViewMaximizedForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webViewPlot = new Controls.Display.SiliFishWebView();
            ((System.ComponentModel.ISupportInitialize)webViewPlot).BeginInit();
            SuspendLayout();
            // 
            // webViewPlot
            // 
            webViewPlot.AllowExternalDrop = true;
            webViewPlot.BackColor = Color.White;
            webViewPlot.CreationProperties = null;
            webViewPlot.DefaultBackgroundColor = Color.White;
            webViewPlot.Dock = DockStyle.Fill;
            webViewPlot.Location = new Point(0, 0);
            webViewPlot.Margin = new Padding(4, 5, 4, 5);
            webViewPlot.Name = "webViewPlot";
            webViewPlot.Size = new Size(800, 450);
            webViewPlot.TabIndex = 2;
            webViewPlot.ZoomFactor = 1D;
            webViewPlot.CoreWebView2InitializationCompleted += webView_CoreWebView2InitializationCompleted;
            // 
            // WebViewMaximizedForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(webViewPlot);
            Name = "WebViewMaximizedForm";
            Text = "WebViewForm";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)webViewPlot).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Controls.Display.SiliFishWebView webViewPlot;
    }
}