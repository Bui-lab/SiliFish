using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Services;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.Services;
using SiliFish.UI.Extensions;
using System.Data;
using System.Text.Json;
using System.Drawing.Imaging;
using SiliFish.ModelUnits;
using SiliFish.Services.Plotting;
using SiliFish.Repositories;
using SiliFish.Services.Plotting.PlotSelection;
using SiliFish.UI.Services;
using SiliFish.Services.Dynamics;
using SiliFish.ModelUnits.Junction;
using SiliFish.UI.EventArguments;

namespace SiliFish.UI.Controls
{
    public partial class AnimationControl : UserControl
    {
        int lastAnimationStartIndex;
        double[] lastAnimationTimeArray;
        Dictionary<string, Coordinate[]> lastAnimationSpineCoordinates;

        string htmlAnimation = "";
        decimal tAnimdt;
        int tAnimStart = 0;
        int tAnimEnd = 0;
        private string tempFile;
        Simulation simulation = null;
        RunningModel model = null;
        public AnimationControl()
        {
            InitializeComponent();
            if (!DesignMode) 
                WebViewInitializations();
        }

        public void SetRunningModel(Simulation simulation, RunningModel model)
        {
            if (model == null) return;
            this.simulation = simulation;
            this.model = model;
        }
        public void CompleteRun(RunParam runParam)
        {
            decimal dt = (decimal)runParam.DeltaT;
            eAnimationdt.Minimum = dt;
            eAnimationdt.Increment = dt;
            eAnimationdt.Value = dt; 
            timeRangeAnimation.EndTime = runParam.MaxTime;
            if (timeRangeAnimation.StartTime > timeRangeAnimation.EndTime)
                timeRangeAnimation.StartTime = 0;
            btnAnimate.Enabled = true; 
        }

        public void CancelRun()
        {
            btnAnimate.Enabled = true; //to make partial results available
        }
        #region webViewPlot
        private void WebViewInitializations()
        {
            InitAsync();
            Wait();
            webViewAnimation.ShowsAmCharts = true;
        }
        private async void InitAsync()
        {
            await webViewAnimation.EnsureCoreWebView2Async();
        }

        private void Wait()
        {
            while (!webViewAnimation.Initialized)
                Application.DoEvents();
        }



        private void cmiAnimationClearCache_Click(object sender, EventArgs e)
        {
            webViewAnimation.ClearBrowserCache();
        }
        #endregion


        private void Animate()
        {
            try
            {
                if (simulation == null || !simulation.SimulationRun) return;
                htmlAnimation = AnimationGenerator.GenerateAnimation(simulation, tAnimStart, tAnimEnd, (double)tAnimdt, out lastAnimationSpineCoordinates);
                Invoke(CompleteAnimation);
            }
            catch { Invoke(CancelAnimation); }
        }
        private void CompleteAnimation()
        {
            bool navigated = false;
            webViewAnimation.NavigateTo(htmlAnimation, "Animation", GlobalSettings.TempFolder, ref tempFile, ref navigated);
            if (!navigated)
                Warner.LargeFileWarning(tempFile);
            linkSaveAnimationHTML.Enabled = linkSaveAnimationCSV.Enabled = true;
            lAnimationTime.Text = $"Last animation: {DateTime.Now:t}";
            btnAnimate.Enabled = true;
        }
        private void CancelAnimation()
        {
            webViewAnimation.NavigateToString("about:blank");
            linkSaveAnimationHTML.Enabled = linkSaveAnimationCSV.Enabled = false;
            lAnimationTime.Text = $"Last animation aborted.";
            btnAnimate.Enabled = true;
        }
        private void btnAnimate_Click(object sender, EventArgs e)
        {
            if (simulation == null || !simulation.SimulationRun) return;

            btnAnimate.Enabled = false;

            tAnimStart = timeRangeAnimation.StartTime;
            tAnimEnd = timeRangeAnimation.EndTime;
            if (tAnimStart > simulation.RunParam.MaxTime || tAnimStart < 0)
                tAnimStart = 0;
            if (tAnimEnd > simulation.RunParam.MaxTime)
                tAnimEnd = simulation.RunParam.MaxTime;
            double dt = simulation.RunParam.DeltaT;
            lastAnimationStartIndex = (int)(tAnimStart / dt);
            int lastAnimationEndIndex = (int)(tAnimEnd / dt);
            lastAnimationTimeArray = model.TimeArray;
            model.SetAnimationParameters(model.KinemParam);

            tAnimdt = eAnimationdt.Value;
            Invoke(Animate);
        }
        private void linkSaveAnimationHTML_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(htmlAnimation))
                return;
            if (saveFileHTML.ShowDialog() != DialogResult.OK)
                return;
            File.WriteAllText(saveFileHTML.FileName, htmlAnimation.ToString());
        }
        private void linkSaveAnimationCSV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(htmlAnimation))
                return;
            if (saveFileCSV.ShowDialog() != DialogResult.OK)
                return;

            ModelFile.SaveAnimation(saveFileCSV.FileName, lastAnimationSpineCoordinates, lastAnimationTimeArray, lastAnimationStartIndex);
        }

    }

}
