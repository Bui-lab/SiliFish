using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using SiliFish.Helpers;
using System.ComponentModel;

namespace SiliFish.UI.Controls.Display
{
    public partial class SiliFishWebView : WebView2
    {
        public bool Initialized { get; private set; } = false;
        public SiliFishWebView()
        {
            InitializeComponent();
            CoreWebView2InitializationCompleted += new EventHandler<CoreWebView2InitializationCompletedEventArgs>(webView_CoreWebView2InitializationCompleted);

            if (CoreWebView2 != null) 
                CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
        }
        /// <summary>
        /// if AmCharts is displayed, the context menu needs to be reviewed
        /// </summary>
        public bool ShowsAmCharts
        {
            set
            {
                if (value)
                    CoreWebView2.ContextMenuRequested += AmChartsCoreWebView2_ContextMenuRequested;
            }
        }
        private void RegenerateWebview(WebView2 webView)
        {
            if (webView == null) return;
            string name = webView.Name;
            Control parent = webView.Parent;
            parent.Controls.Remove(webView);
            try { webView.Dispose(); }
            catch { }

            webView = new WebView2();

            (webView as ISupportInitialize).BeginInit();
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 30);
            webView.Name = name;
            webView.Size = new Size(622, 481);
            webView.TabIndex = 1;
            webView.ZoomFactor = 1D;
            webView.CoreWebView2InitializationCompleted += new EventHandler<CoreWebView2InitializationCompletedEventArgs>(webView_CoreWebView2InitializationCompleted);

            (webView as ISupportInitialize).EndInit();
            parent.Controls.Add(webView);
            webView.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;

        }

        private static void WarningMessage(string s)
        {
            MessageBox.Show(s, "Warning");
        }
        private void CoreWebView2_ProcessFailed(object sender, CoreWebView2ProcessFailedEventArgs e)
        {
            try
            {
                string tempFile = FileUtil.GetUniqueFileName() + ".html";
                string target = (sender as CoreWebView2).DocumentTitle;
                target = FileUtil.CopyFileToOutputFolder(tempFile, target);
                RegenerateWebview(sender as WebView2);
                Invoke(() => WarningMessage("There was a problem with displaying the html file. It is saved as " + target + "."));
            }
            catch { }
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            Initialized = true;
        }

        
        private void AmChartsCoreWebView2_ContextMenuRequested(object sender, CoreWebView2ContextMenuRequestedEventArgs args)
        {
            IList<CoreWebView2ContextMenuItem> menuList = args.MenuItems;
            CoreWebView2ContextMenuTargetKind context = args.ContextMenuTarget.Kind;
            if (context == CoreWebView2ContextMenuTargetKind.Page)
            {
                for (int index = menuList.Count - 1; index >= 0; index--)
                {
                    if (menuList[index].Name == "saveImageAs" ||
                        menuList[index].Name == "copyImage" ||
                        menuList[index].Name == "inspectElement")
                    {
                        menuList.RemoveAt(index);
                    }
                }
            }
        }
        public async void ClearBrowserCache()
        {
            await CoreWebView2.Profile.ClearBrowsingDataAsync();
        }
    }
}
