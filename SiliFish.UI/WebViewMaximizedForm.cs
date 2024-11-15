using Microsoft.Web.WebView2.Core;
using SiliFish.Definitions;
using SiliFish.UI.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiliFish.UI
{
    public partial class WebViewMaximizedForm : Form
    {
        public WebViewMaximizedForm()
        {
            InitializeComponent();
            if (!DesignMode)
                WebViewInitializations();
        }

        #region webViewPlot
        private void WebViewInitializations()
        {
            InitAsync();
            Wait();
        }
        private async void InitAsync()
        {
            await webViewPlot.EnsureCoreWebView2Async();
        }

        private void Wait()
        {
            while (!webViewPlot.Initialized)
                Application.DoEvents();
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            (sender as Control).Tag = true;
        }

        #endregion

        public void NavigateTo(string htmlPlot, string plotName, string tempFolder,
            ref string tempFile, ref bool navigated)
        {
            webViewPlot.NavigateTo(htmlPlot, plotName, tempFolder, ref tempFile, ref navigated);
        }
    }
}
