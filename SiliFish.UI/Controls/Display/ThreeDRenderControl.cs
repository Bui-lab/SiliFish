using Microsoft.Web.WebView2.Core;
using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using SiliFish.UI.Extensions;
using System.Data;
using System.Text.Json;
using SiliFish.ModelUnits;
using SiliFish.UI.Services;
using SiliFish.ModelUnits.Parameters;

namespace SiliFish.UI.Controls
{
    public partial class ThreeDRenderControl : UserControl
    {
        private string tempFile;
        Simulation simulation = null;
        RunningModel model = null;
        bool rendered3D = false;
        public ThreeDRenderControl()
        {
            InitializeComponent();
            if (!DesignMode)
                WebViewInitializations();
            dd3DViewpoint.SelectedIndex = 0;
        }

        public void SetRunningModel(Simulation simulation, RunningModel model)
        {
            if (model == null) return;
            this.simulation = simulation;
            this.model = model;
            int NumberOfSomites = model.ModelDimensions.NumberOfSomites;
            if (NumberOfSomites > 0)
            {
                cb3DAllSomites.Visible = true;
                e3DSomiteRange.Visible = !cb3DAllSomites.Checked;
            }
            else
            {
                cb3DAllSomites.Visible = e3DSomiteRange.Visible = false;
            }
        }

        private void WebViewInitializations()
        {
            InitAsync();
            Wait();
        }
        private async void InitAsync()
        {
            await webView3DRender.EnsureCoreWebView2Async();
        }

        private void Wait()
        {
            while (!webView3DRender.Initialized)
                Application.DoEvents();
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            (sender as Control).Tag = true;
        }
        internal async Task ExecuteScriptAsync(string s)
        {
            await webView3DRender.ExecuteScriptAsync(s);
        }
        public void RenderIn3D()
        {
            try
            {
                ThreeDRenderer threeDRenderer = new();
                string html = threeDRenderer.RenderIn3D(model, model.CellPools,
                    somiteRange: cb3DAllSomites.Checked ? "All" : e3DSomiteRange.Text,
                    webView3DRender.Width, webView3DRender.Height,
                    showGap: cb3DGapJunc.Checked, showChem: cb3DChemJunc.Checked, offline: cb3DOffline.Checked);
                bool navigated = false;
                webView3DRender.NavigateTo(html, "3DRendering", GlobalSettings.TempFolder, ref tempFile, ref navigated);
                if (!navigated)
                    Warner.LargeFileWarning(tempFile);
                rendered3D = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void btn3DRender_Click(object sender, EventArgs e)
        {
            int numOfJunctions = model.GetNumberOfJunctions();
            int numOfCells = model.GetNumberOfCells();
            if (numOfJunctions + numOfCells > GlobalSettings.MaxNumberOfUnitsToRender)
            {
                string msg = $"To render {numOfCells + numOfJunctions} units will be resource intensive. Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            RenderIn3D();
        }
        private async void cb3DAllSomites_CheckedChanged(object sender, EventArgs e)
        {
            if (model == null) return;
            e3DSomiteRange.Visible = !cb3DAllSomites.Checked;
            string func = $"SetSomites([]);";
            if (!cb3DAllSomites.Checked)
            {
                List<int> somites = Util.ParseRange(e3DSomiteRange.Text, 1, model.ModelDimensions.NumberOfSomites);
                func = $"SetSomites([{string.Join(',', somites)}]);";
            }
            await webView3DRender.ExecuteScriptAsync(func);
        }

        private string lastSomiteSelection;
        private void e3DSomiteRange_Enter(object sender, EventArgs e)
        {
            lastSomiteSelection = e3DSomiteRange.Text;
        }

        private async void e3DSomiteRange_Leave(object sender, EventArgs e)
        {
            if (model == null) return;
            if (lastSomiteSelection != e3DSomiteRange.Text)
            {
                List<int> somites = Util.ParseRange(e3DSomiteRange.Text, 1, model.ModelDimensions.NumberOfSomites);
                string func = $"SetSomites([{string.Join(',', somites)}]);";
                await webView3DRender.ExecuteScriptAsync(func);
            }
        }
        private async void linkSaveHTML3D_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (saveFileHTML.ShowDialog() == DialogResult.OK)
                {
                    string htmlHead = JsonSerializer.Deserialize<string>(await webView3DRender.ExecuteScriptAsync("document.head.outerHTML"));
                    string htmlBody = JsonSerializer.Deserialize<string>(await webView3DRender.ExecuteScriptAsync("document.body.outerHTML"));
                    string html = $@"<!DOCTYPE html> <html lang=""en"">{htmlHead}{htmlBody}</html>";
                    File.WriteAllText(saveFileHTML.FileName, html);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is a problem in saving the file:" + exc.Message, "Error");
            }
        }

        private async void cb3DChemJunc_CheckedChanged(object sender, EventArgs e)
        {
            if (cb3DChemJunc.Checked)
                await webView3DRender.ExecuteScriptAsync("ShowChemJunc();");
            else
                await webView3DRender.ExecuteScriptAsync("HideChemJunc();");
        }

        private async void cb3DGapJunc_CheckedChanged(object sender, EventArgs e)
        {
            if (cb3DGapJunc.Checked)
                await webView3DRender.ExecuteScriptAsync("ShowGapJunc();");
            else
                await webView3DRender.ExecuteScriptAsync("HideGapJunc();");
        }
        private async void cb3DShowUnselectedNodes_CheckedChanged(object sender, EventArgs e)
        {
            if (cb3DShowUnselectedNodes.Checked)
                await webView3DRender.ExecuteScriptAsync("ShowInactiveNodes();");
            else
                await webView3DRender.ExecuteScriptAsync("HideInactiveNodes();");
        }
        private void cb3DLegend_CheckedChanged(object sender, EventArgs e)
        {
            gr3DLegend.Visible = cb3DLegend.Checked;
        }

        private async void dd3DViewpoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dd3DViewpoint.Text == "Dorsal view")
                await webView3DRender.ExecuteScriptAsync("DorsalView();");
            else if (dd3DViewpoint.Text == "Ventral view")
                await webView3DRender.ExecuteScriptAsync("VentralView();");
            else if (dd3DViewpoint.Text == "Rostral view")
                await webView3DRender.ExecuteScriptAsync("RostralView();");
            else if (dd3DViewpoint.Text == "Caudal view")
                await webView3DRender.ExecuteScriptAsync("CaudalView();");
            else if (dd3DViewpoint.Text == "Lateral view (left)")
                await webView3DRender.ExecuteScriptAsync("LateralLeftView();");
            else if (dd3DViewpoint.Text == "Lateral view (right)")
                await webView3DRender.ExecuteScriptAsync("LateralRightView();");
            else
                await webView3DRender.ExecuteScriptAsync("FreeView();");
        }

        private async void btnZoomOut_Click(object sender, EventArgs e)
        {
            await webView3DRender.ExecuteScriptAsync("ZoomOut();");
        }

        private async void btnZoomIn_Click(object sender, EventArgs e)
        {
            await webView3DRender.ExecuteScriptAsync("ZoomIn();");
        }
        internal async Task RunScript(string s)
        {
            await webView3DRender.ExecuteScriptAsync(s);
        }
        private void cb3DRenderShowOptions_CheckedChanged(object sender, EventArgs e)
        {
            p3DRenderOptions.Visible = cb3DRenderShowOptions.Checked;
        }

        private async void ud3DNodeSize_DownClicked(object sender, EventArgs e)
        {
            await webView3DRender.ExecuteScriptAsync("SetNodeSizeMultiplier(0.9);");
        }

        private async void ud3DNodeSize_UpClicked(object sender, EventArgs e)
        {
            await webView3DRender.ExecuteScriptAsync("SetNodeSizeMultiplier(1.1);");
        }
    }

}
