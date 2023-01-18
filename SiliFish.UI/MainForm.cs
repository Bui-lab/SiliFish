using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Services;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Services;
using SiliFish.UI.Controls;
using SiliFish.UI.Extensions;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.Json;
using SiliFish.Repositories;
using SiliFish.UI.Definitions;

namespace SiliFish.UI
{
    public partial class MainForm : Form
    {
        static string modelFileDefaultFolder;

        private ModelTemplate ModelTemplate = null;
        private RunningModel RunningModel = null;

        DateTime runStart;

        public MainForm()
        {
            try
            {
                InitializeComponent();
                ModelTemplate = new();
                ModelTemplate.Settings.TempFolder = Path.GetTempPath() + "SiliFish";
                ModelTemplate.Settings.OutputFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\SiliFish\\Output";
                modelControl.SetModel(ModelTemplate);
                SetCurrentMode(RunMode.Template);
                CurrentSettings.Settings = ModelTemplate.Settings;
                if (!Directory.Exists(ModelTemplate.Settings.TempFolder))
                    Directory.CreateDirectory(ModelTemplate.Settings.TempFolder);
                if (!Directory.Exists(ModelTemplate.Settings.OutputFolder))
                    Directory.CreateDirectory(ModelTemplate.Settings.OutputFolder);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        #region Model selection

        public void SetRunningModel(RunningModel model)
        {
            ModelTemplate = null;
            RunningModel = model;
            modelControl.SetModel(model);
            modelOutputControl.SetRunningModel(model);
            SetCurrentMode(RunMode.RunningModel);
        }
        private void RefreshModel()
        {
            RunningModel = modelControl.GetModel() as RunningModel;
            modelOutputControl.SetRunningModel(RunningModel);
        }


        #endregion


        
        #region Settings
        private void linkOpenOutputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", ModelTemplate.Settings.OutputFolder);
            }
            catch { }
        }

        private void linkOpenTempFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", ModelTemplate.Settings.TempFolder);
            }
            catch { }
        }




        #endregion

        #region General Form Functions
        private void btnAbout_Click(object sender, EventArgs e)
        {
            About about = new();
            about.ShowDialog();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            About about = new();
            about.SetTimer(2000);
            about.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ModelTemplate != null)
                {
                    //check whether the custom model has changed
                    string jsonstring = JsonUtil.ToJson(ModelTemplate);
                    //TODO if (jsonstring != lastSavedCustomModelJSON)
                    {
                        DialogResult res = MessageBox.Show("The template has changed. Do you want to save the modifications?", "Model Template Save", MessageBoxButtons.YesNoCancel);
                        if (res == DialogResult.Yes)
                        {
                            //TODO if (!SaveModelTemplate())
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                        else if (res == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                foreach (string f in CurrentSettings.Settings.TempFiles)
                {
                    File.Delete(f);
                }
                    
            }
            catch { }
        }
        #endregion

        #region Simulation
        private void RunModel()
        {
            try
            {
                RunningModel.RunModel(0, (int)eRunNumber.Value);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (RunningModel.ModelRun)
                    Invoke(CompleteRun);
                else
                    Invoke(CancelRun);
            }
        }
        private void CancelRun()
        {
            UseWaitCursor = false;
            timerRun.Enabled = false;
            progressBarRun.Value = progressBarRun.Maximum;
            progressBarRun.Visible = false;
            btnRun.Text = "Run";

            linkExportOutput.Visible = false;

            lRunTime.Text = $"Last run cancelled\r\n" +
                $"Model: {RunningModel.ModelName}";
            modelOutputControl.CancelRun();
        }
        private void CompleteRun()
        {
            UseWaitCursor = false;
            timerRun.Enabled = false;
            progressBarRun.Value = progressBarRun.Maximum;
            progressBarRun.Visible = false;
            btnRun.Text = "Run";

            linkExportOutput.Visible = true;
            
            TimeSpan ts = DateTime.Now - runStart;

            lRunTime.Text = $"Last run: {runStart:t}\r\n" +
                $"Duration: {Util.TimeSpanToString(ts)}\r\n" +
                $"Model: {RunningModel.ModelName}";

            modelOutputControl.CompleteRun();
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (btnRun.Text == "Stop Run")
            {
                if (RunningModel != null)
                    RunningModel.CancelRun = true;
                return;
            }
            runStart = DateTime.Now;
            lRunTime.Text = "";
            UseWaitCursor = true;
            progressBarRun.Value = 0;
            progressBarRun.Visible = true;
            timerRun.Enabled = true;

            RefreshModel();
            if (RunningModel == null) return;
            RunningModel.RunParam = new()
            {
                tSkip_ms = (int)eSkip.Value,
                tMax = (int)eTimeEnd.Value,
                dt = (double)edt.Value,
                dtEuler = (double)edtEuler.Value
            };
            btnRun.Text = "Stop Run";
            Task.Run(RunModel);
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            if (RunningModel == null) return;
            progressBarRun.Value = (int)((RunningModel?.GetProgress() ?? 0) * progressBarRun.Maximum);
            if (eRunNumber.Value > 1)
            {
                int? i = RunningModel?.GetRunCounter();
                if (i != null)
                    lRunTime.Text = $"Run number: {i}";
            }
        }
        private void linkExportOutput_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string msg = "Save Run may take a while depending on the duration and number of cells.";
            if (MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                RunningModel.SaveToFile(saveFileCSV.FileName);
            }
        }

        #endregion

        private void btnCellularDynamics_Click(object sender, EventArgs e)
        {
            DynamicsTestControl dynControl = new("Izhikevich_9P", null, testMode: true);
            ControlContainer frmControl = new();
            frmControl.AddControl(dynControl);
            frmControl.Text = "Cellular Dynamics Test";
            frmControl.SaveVisible = false;
            frmControl.ShowDialog();
        }

        private void SetCurrentMode(RunMode mode)
        {
            splitMain.Panel2Collapsed = mode == RunMode.Template;
            pSimulation.Visible = mode == RunMode.RunningModel;
            pGenerateModel.Visible = mode == RunMode.Template;            
        }
        private void linkLoadModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                openFileJson.InitialDirectory = modelFileDefaultFolder;
                if (openFileJson.ShowDialog() == DialogResult.OK)
                {
                    ModelBase mb;
                    try
                    {
                        mb = ModelFile.Load(openFileJson.FileName, out List<string> issues);
                    }
                    catch
                    {
                        MessageBox.Show("Selected file is not a valid Model or Template file.");
                        return;
                    }
                    mb.BackwardCompatibility();
                    mb.LinkObjects();
                    if (mb is ModelTemplate)
                    {
                        ModelTemplate = mb as ModelTemplate;
                        modelControl.SetModel(ModelTemplate);
                        RunningModel = null;
                        SetCurrentMode(RunMode.Template);
                    }
                    else
                    {
                        RunningModel = mb as RunningModel;
                        modelControl.SetModel(RunningModel);
                        ModelTemplate = null;
                        SetCurrentMode(RunMode.RunningModel);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show($"There is a problem in generating the model template from the JSON file.\r\n{exc.Message}");
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
            }
        }
        private bool SaveModel()
        {
            try
            {
                ModelBase mb = modelControl.GetModel();

                if (!mb.CheckValues(out List<string> errors))
                {
                    MessageBox.Show(string.Join("\r\n", errors));
                    return false;
                }
                if (string.IsNullOrEmpty(saveFileJson.FileName))
                    saveFileJson.FileName = mb.ModelName;
                else
                    saveFileJson.InitialDirectory = modelFileDefaultFolder;
                if (saveFileJson.ShowDialog() == DialogResult.OK)
                {
                    modelFileDefaultFolder = Path.GetDirectoryName(saveFileJson.FileName);
                    ModelFile.Save(saveFileJson.FileName, mb);

                    this.Text = $"SiliFish {mb.ModelName}";
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        private void linkSaveModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveModel();
        }

        private void linkClearModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Do you want to start from a brand new template? All changes you have made will be cleared.", "Warning",
                MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (ModelTemplate != null)
                ModelTemplate = new();
            else
                RunningModel = new();
            modelControl.SetModel(ModelTemplate);
        }

        private void btnGenerateModel_Click(object sender, EventArgs e)
        {
            if (ModelTemplate == null)
            {
                MessageBox.Show("There is no model template loaded.", "Error");
                return;
            }
            MainForm mf = new();
            mf.SetRunningModel(new RunningModel(ModelTemplate));
            mf.Show();
        }
    }
}