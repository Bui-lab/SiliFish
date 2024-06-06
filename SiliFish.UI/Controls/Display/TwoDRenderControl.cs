﻿using Microsoft.Web.WebView2.Core;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using SiliFish.UI.Extensions;
using System.Data;
using System.Text.Json;
using SiliFish.UI.Services;
using SiliFish.ModelUnits.Parameters;

namespace SiliFish.UI.Controls
{
    public partial class TwoDRenderControl : UserControl
    {
        private string tempFile;
        Simulation simulation = null;
        RunningModel model = null;
        bool rendered2DFull = false; //whether the 2D rendering is done by hiding in inactive nodes - will need rerendering
        TwoDRenderer TwoDRenderer = new();
        public TwoDRenderControl()
        {
            InitializeComponent();
            if (!DesignMode)
                WebViewInitializations();
            gr2DCellPoolLegend.Top = p2DRenderOptions.Visible ? p2DRenderOptions.Bottom + 4 : p2DRender.Bottom + 4;
        }

        public void SetRunningModel(Simulation simulation, RunningModel model)
        {
            if (model == null) return;
            this.simulation = simulation;
            this.model = model;
        }

        private void WebViewInitializations()
        {
            InitAsync();
            Wait();
        }
        private async void InitAsync()
        {
            await webView2DRender.EnsureCoreWebView2Async();
        }

        private void Wait()
        {
            while (!webView2DRender.Initialized)
                Application.DoEvents();
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            (sender as Control).Tag = true;
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
        internal async Task ExecuteScriptAsync(string s)
        {
            await webView2DRender.ExecuteScriptAsync(s);
        }
        internal void RenderIn2D(bool refresh)
        {
            if (model == null) return;

            //If the full rendering has never been done, a brand new 2D rendering need to be created rather than refreshing the old one
            if (refresh && !cb2DHideNonspiking.Checked && !rendered2DFull)
                refresh = false;
            List<CellPool> cellPools = model.CellPools;
            if (cb2DHideNonspiking.Checked && simulation != null && simulation.SimulationRun)
                cellPools = model.CellPools.Where(cp => cp.Cells.Any(c => c.IsActivelySpiking(GlobalSettings.ActivityThresholdSpikeCount))).ToList();
            string html = TwoDRenderer.Create2DRendering(model, cellPools, refresh, webView2DRender.Width, webView2DRender.Height,
                showGap: cb2DGapJunc.Checked, showChem: cb2DChemJunc.Checked, offline: cb2DOffline.Checked);
            if (string.IsNullOrEmpty(html))
                return;
            gr2DCellPoolLegend.Controls.Clear();

            foreach (var cellPool in cellPools.GroupBy(cp => cp.CellGroup))
            {
                Color color = cellPool.First().Color;
                Label label = new()
                {
                    Text = cellPool.Key,
                    Height = 20,
                    Padding = new Padding(2, 2, 0, 0),
                    AutoSize = false,
                    Dock = DockStyle.Top,
                    BackColor = color
                };
                int grayValue = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                if (grayValue < 50)
                    label.ForeColor = Color.White;
                gr2DCellPoolLegend.Controls.Add(label);
            }
            bool navigated = false;
            webView2DRender.NavigateTo(html, "2DRendering", GlobalSettings.TempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
            if (!refresh)
            {
                rendered2DFull = !cb2DHideNonspiking.Checked;
            }
        }
        private void btn2DRender_Click(object sender, EventArgs e)
        {
            RenderIn2D(false);
        }
        private async void linkSaveHTML2D_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (saveFileHTML.ShowDialog() == DialogResult.OK)
                {
                    string htmlHead = JsonSerializer.Deserialize<string>(await webView2DRender.ExecuteScriptAsync("document.head.outerHTML"));
                    string htmlBody = JsonSerializer.Deserialize<string>(await webView2DRender.ExecuteScriptAsync("document.body.outerHTML"));
                    string html = $@"<!DOCTYPE html> <html lang=""en"">{htmlHead}{htmlBody}</html>";
                    File.WriteAllText(saveFileHTML.FileName, html);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is a problem in saving the file:" + exc.Message, "Error");
            }
        }

        private async void cb2DChemJunc_CheckedChanged(object sender, EventArgs e)
        {
            if (cb2DChemJunc.Checked)
                await webView2DRender.ExecuteScriptAsync("ShowChemJunc();");
            else
                await webView2DRender.ExecuteScriptAsync("HideChemJunc();");
        }

        private async void cb2DGapJunc_CheckedChanged(object sender, EventArgs e)
        {
            if (cb2DGapJunc.Checked)
                await webView2DRender.ExecuteScriptAsync("ShowGapJunc();");
            else
                await webView2DRender.ExecuteScriptAsync("HideGapJunc();");
        }
        private async void cb2DShowUnselectedNodes_CheckedChanged(object sender, EventArgs e)
        {
            if (cb2DShowUnselectedNodes.Checked)
                await webView2DRender.ExecuteScriptAsync("ShowInactiveNodes();");
            else
                await webView2DRender.ExecuteScriptAsync("HideInactiveNodes();");
        }
        private void cb2DLegend_CheckedChanged(object sender, EventArgs e)
        {
            gr2DLegend.Visible = gr2DCellPoolLegend.Visible = cb2DLegend.Checked;
        }
        private async void ud2DNodeSize_SelectedItemChanged(object sender, EventArgs e)
        {
            if (ud2DNodeSize.UpClick)
                await webView2DRender.ExecuteScriptAsync("SetNodeSizeMultiplier(1.1);");
            if (ud2DNodeSize.DownClick)
                await webView2DRender.ExecuteScriptAsync("SetNodeSizeMultiplier(0.9);");
        }

        private async void ud2DLinkSize_SelectedItemChanged(object sender, EventArgs e)
        {
            if (ud2DLinkSize.UpClick)
                await webView2DRender.ExecuteScriptAsync("SetLinkSizeMultiplier(1.1);");
            if (ud2DLinkSize.DownClick)
                await webView2DRender.ExecuteScriptAsync("SetLinkSizeMultiplier(0.9);");
        }
        private void cb2DRenderShowOptions_CheckedChanged(object sender, EventArgs e)
        {
            p2DRenderOptions.Visible = cb2DRenderShowOptions.Checked;
            gr2DCellPoolLegend.Top = p2DRenderOptions.Visible ? p2DRenderOptions.Bottom + 4 : p2DRender.Bottom + 4;
        }


        private void cb2DHideNonspiking_CheckedChanged(object sender, EventArgs e)
        {
            RenderIn2D(true);
        }

    }
}
