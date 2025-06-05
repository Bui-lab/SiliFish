using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
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
                    await threeDRenderControl.ExecuteScriptAsync($"SelectCell('{cell.ID}');");
            }

        }
    }

}
