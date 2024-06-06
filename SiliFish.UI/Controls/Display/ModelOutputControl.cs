﻿using Microsoft.Web.WebView2.Core;
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
    public partial class ModelOutputControl : UserControl
    {
        Simulation simulation = null;
        RunningModel model = null;
        bool rendered2D = false;
        bool rendered3D = false;
        public ModelOutputControl()
        {
            InitializeComponent();
            WebViewInitializations();
        }

        private void ModelOutputControl_Load(object sender, EventArgs e)
        {
            tabOutputs_SelectedIndexChanged(sender, e);
        }


        public void SetRunningModel(Simulation simulation, RunningModel model)
        {
            this.simulation = simulation;
            this.model = model;
            animationControl.SetRunningModel(simulation, model);
            statOutputControl.SetRunningModel(simulation, model);
            plotControl.SetRunningModel(simulation, model);
            twoDRenderControl.SetRunningModel(simulation, model);
            threeDRenderControl.SetRunningModel(simulation, model);
        }

        public void CompleteRun(RunParam runParam)
        {
            animationControl.CompleteRun(runParam);
            statOutputControl.CompleteRun(runParam);
            plotControl.CompleteRun(runParam);
        }

        public void CancelRun()
        {
            animationControl.CancelRun();
            statOutputControl.CancelRun();
            plotControl.CancelRun();
        }
        #region webViewPlot
        private void WebViewInitializations()
        {
            //TODO InitAsync();
            //TODO Wait();
        }
        private async void InitAsync()
        {
            //TODO await twodRenderControl1.InitAsync();
            //TODO await webView2DRender.EnsureCoreWebView2Async();
            //TODO await webView3DRender.EnsureCoreWebView2Async();
            //TODO await plotControl.InitAsync();
            //TODO await animationControl.InitAsync();
            //TODO await statOutputControl.InitAsync();
        }

        private void Wait()
        {
            while (false)//TODO
                Application.DoEvents();
        }

        #endregion

        internal void Plot(List<ModelUnitBase> unitsToPlot)
        {
            plotControl.Plot(unitsToPlot);
        }
        private void tabOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabOutputs.SelectedTab == t2DRender && !rendered2D)
                twoDRenderControl.RenderIn2D(false);
            else if (tabOutputs.SelectedTab == t3DRender && !rendered3D)
            {
                int numOfJunctions = model.GetNumberOfJunctions();
                int numOfCells = model.GetNumberOfCells();
                if (numOfJunctions + numOfCells > GlobalSettings.MaxNumberOfUnitsToRender)
                    return; //rendering is done only explicitly by the user 
                threeDRenderControl.RenderIn3D();
            }
        }
        internal async void Highlight(ModelUnitBase unitToPlot, bool force)
        {
            if (model == null) return;
            if (unitToPlot == null) return;
            if (tabOutputs.SelectedTab != t2DRender && tabOutputs.SelectedTab != t3DRender)
            {
                if (!force) return;
                if (unitToPlot is Cell)
                    tabOutputs.SelectedTab = t2DRender;
                else
                    tabOutputs.SelectedTab = t3DRender;
            }
            if (unitToPlot is CellPool pool)
            {
                if (tabOutputs.SelectedTab == t2DRender)
                    await twoDRenderControl.ExecuteScriptAsync($"SelectCellPool('{pool.ID}');");
                else
                    await threeDRenderControl.ExecuteScriptAsync($"SelectCellPool('{pool.ID}');");
            }
            else if (unitToPlot is Cell cell)
            {
                if (tabOutputs.SelectedTab == t2DRender)
                    await twoDRenderControl.ExecuteScriptAsync($"SelectCellPool('{cell.CellPool.ID}');");
                else
                { }//TODOawait webView3DRender.ExecuteScriptAsync($"SelectCell('{cell.ID}');");
            }

        }
    }

}
