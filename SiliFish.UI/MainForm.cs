using Controls;
using System.Diagnostics;

using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.Repositories;
using SiliFish.Services;
using SiliFish.UI.Controls;
using SiliFish.UI.Definitions;
using SiliFish.UI.EventArguments;
using System.Diagnostics.Eventing.Reader;
using SiliFish.UI.Services;
using SiliFish.UI.Dialogs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System;
using SiliFish.ModelUnits.Parameters;

namespace SiliFish.UI
{
    public partial class MainForm : Form
    {
        private bool multiRunMode = false;
        private bool parallelRun = false;
        private int numSimulations = 1;
        private static bool showAboutForm = true;
        public static bool currentPlotWarning = false;
        private DateTime runStart;
        private string modelFileDefaultFolder, lastFileName;
        private ModelTemplate modelTemplate = null;
        private RunningModel runningModel = null;
        private RunParam runParam = new();
        private ModelSimulator modelSimulator;
        public ModelBase Model
        {
            get
            {
                return (ModelBase)modelTemplate ?? runningModel;
            }
        }

        public MainForm()
        {
            try
            {
                InitializeComponent();
                modelTemplate = new();
                modelControl.SetModel(modelTemplate);
                SetCurrentMode(RunMode.Template, null);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public MainForm(RunningModel model)
        {
            try
            {
                InitializeComponent();
                modelTemplate = null;
                runningModel = model;

                modelControl.SetModel(model);
                modelOutputControl.SetRunningModel(null, model);
                FillRunParams();
                SetCurrentMode(RunMode.RunningModel, null);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private void FillRunParams()
        {
            edt.Value = (decimal)runParam.DeltaT;
            eSkip.Value = runParam.SkipDuration;
            eTimeEnd.Value = runParam.MaxTime;
        }
        private void RefreshModel()
        {
            runningModel = modelControl.GetModel() as RunningModel;
            modelOutputControl.SetRunningModel(null, runningModel);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (showAboutForm)
            {
                About about = new();
                about.SetTimer(2000);
                about.ShowDialog();
                showAboutForm = false;
            }
            else
                GetLastRunSettings();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (modelControl.ModelUpdated)
                {
                    DialogResult res = MessageBox.Show("The model/template has changed. Do you want to save the modifications?", "Model Save", MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.Yes)
                    {
                        if (!SaveModelAsJSON())
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
                else if (modelSimulator?.ModelRun ?? false)
                {
                    if (MessageBox.Show("Do you want to close the form? The simulation values will be lost.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                modelTemplate = null;
                runningModel = null;
                if (Program.MainForm == this)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want to exit SiliFish?", "SiliFish", MessageBoxButtons.OKCancel);
                    if (dialogResult != DialogResult.OK)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
            catch { }
        }

        private void SaveLastRunSettings()
        {
            GlobalSettings.LastRunSettings[lTimeEnd.Name] = eTimeEnd.Text;
            GlobalSettings.LastRunSettings[lSkip.Name] = eSkip.Text;
            GlobalSettings.LastRunSettings[ldt.Name] = edt.Text;
        }
        private void GetLastRunSettings()
        {
            if (GlobalSettings.LastRunSettings.Any())
            {
                try
                {
                    eTimeEnd.Text = GlobalSettings.LastRunSettings[lTimeEnd.Name];
                    eSkip.Text = GlobalSettings.LastRunSettings[lSkip.Name];
                    edt.Text = GlobalSettings.LastRunSettings[ldt.Name];
                }
                catch { }
            }
        }

        #region General Form Functions
        private void btnAbout_Click(object sender, EventArgs e)
        {
            About about = new();
            about.ShowDialog();
        }

        #endregion

        #region Simulation

        private void CompleteRuns(List<Simulation> simulations, bool cancelled)
        {
            multiRunMode = false; 
            UseWaitCursor = false;
            timerRun.Enabled = false;
            progressBarRun.Value = progressBarRun.Maximum;
            progressBarRun.Visible = false;
            btnRun.Text = "Run";
            linkExportOutput.Visible = !cancelled;
            if (cancelled)
            {
                lRunTime.Text = $"Last run cancelled\r\n" +
                    $"Model: {runningModel.ModelName}";
                modelOutputControl.CancelRun();
            }
            else
            {
                List<string> list = [];
                int i = 1;
                string simText = "";
                if (simulations.Count > 1)
                {
                    foreach (Simulation simulation in simulations)
                    {
                        list.Add($"Simulation {i++}: {simulation.RunTime}");
                    }
                    TextDisplayer.Display($"Run time stats", list, saveFileText);
                    simText = $"Number of simulations: {simulations.Count}\r\n";
                }
                TimeSpan ts = DateTime.Now - runStart;
                lRunTime.Text = $"Last run: {runStart:t}\r\n" +
                    $"Model: {runningModel.ModelName}\r\n" +
                    simText +
                    $"# of cells: {runningModel.GetNumberOfCells():n0}; # of conn.: {runningModel.GetNumberOfConnections():n0}\r\n" +
                    $"Simulation time: {Util.TimeSpanToString(ts)}";
                modelOutputControl.SetRunningModel(modelSimulator.LastSimulation, runningModel);
                modelOutputControl.CompleteRun(modelSimulator.LastSimulation.RunParam);
            }
        }
        private bool PreRun()
        {
            SaveLastRunSettings();
            runStart = DateTime.Now;
            lRunTime.Text = "";
            UseWaitCursor = true;
            progressBarRun.Value = 0;
            progressBarRun.Visible = true;
            timerRun.Enabled = true;

            RefreshModel();
            if (runningModel == null) return false;
            List<string> errors = new();
            if (!runningModel.CheckValues(ref errors))
            {
                MessageBox.Show($"There are errors in the model. Please correct them before running a simulation: \r\n" +
                    $"{string.Join("\r\n", errors)}", "Error");
                return false;
            }
            runningModel.JunctionCurrentTrackingOn = GlobalSettings.JunctionLevelTracking;
            if (GlobalSettings.JunctionLevelTracking)
            {
                int iMax = (int)(eTimeEnd.Value / edt.Value);
                long numConn = runningModel.GetNumberOfConnections();
                long memoryRequired = numConn * iMax * 8;//number of bytes
                memoryRequired /= (1024 * 1024 * 1024);
                if (memoryRequired > GlobalSettings.MemoryWarningLimit)
                {
                    string msg = $"There are {numConn:n0} connections, which would require more than {memoryRequired} GB extra memory for junction based current tracking.\r\n" +
                        $"Do you want to turn off current tracking to minimize memory problems? You will not be able to plot the currents at a specific junction.";

                    DialogResult dlg = MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNoCancel);
                    if (dlg == DialogResult.Yes)
                        runningModel.JunctionCurrentTrackingOn = false;
                    else if (dlg == DialogResult.No)
                        runningModel.JunctionCurrentTrackingOn = true;
                    else
                        return false;
                }
            }
            runParam = new()
            {
                SkipDuration = (int)eSkip.Value,
                MaxTime = (int)eTimeEnd.Value,
                DeltaT = (double)edt.Value,
                TrackJunctionCurrent = runningModel.JunctionCurrentTrackingOn
            };
            btnRun.Text = "Stop Run";
            return true;
        }

        private void RunSimulation()
        {
            modelSimulator = new(runningModel, numSimulations, parallelRun, CompleteRuns);
            modelSimulator.Run();
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (btnRun.Text == "Stop Run")
            {
                if (MessageBox.Show("Do you want to stop the stimulation? You can access the values till the point it was executed.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                if (modelSimulator != null)
                    modelSimulator.CancelRun();
                return;
            }
            if (modelSimulator?.ModelRun ?? false)
            {
                if (MessageBox.Show("Do you want to run the stimulation? Old simulation values will be lost.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            if (!PreRun())
                return;
            numSimulations = 1;
            parallelRun = false;
            RunSimulation();
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            if (modelSimulator == null) return;
            progressBarRun.Value = (int)(modelSimulator.GetProgress() * progressBarRun.Maximum);
        }
        private void linkExportOutput_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string msg = "Exporting the output may take a while depending on the duration of the simulation and the number of cells." +
                " The resulting files can also be large. You may want to consider exporting partial data exporting indivusal plots instead." +
                " Do you still want to continue with the full export?";
            if (MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                ModelFile.SaveDataToFile(runningModel, saveFileCSV.FileName);
            }
        }

        #endregion

        ControlContainer frmDynamics;

        private void SetCurrentMode(RunMode mode, string name)
        {
            bool prevCollapsed = splitMain.Panel2Collapsed;
            miToolsGenerateStatsData.Visible =
                miToolsRunForStats.Visible = 
                miToolsSepStats.Visible =
                    mode == RunMode.RunningModel;
            splitMain.Panel2Collapsed = mode == RunMode.Template;
            pSimulation.Visible = mode == RunMode.RunningModel;
            pGenerateModel.Visible = mode == RunMode.Template;
            if (prevCollapsed != splitMain.Panel2Collapsed)
                Width = splitMain.Panel2Collapsed ? Width / 2 : Width * 2;
            pDistinguisher.BackColor =
                 pDistinguisherTop.BackColor = pDistinguisherRight.BackColor = pDistinguisherBottom.BackColor
                 = mode == RunMode.Template ? Color.FromArgb(192, 192, 255) :
                 Color.Blue;
            Text = $"SiliFish [{mode} mode] - {name ?? Model.ModelName}";
        }


        private void SetModel(ModelBase mb, string name, bool readFromControl = false)
        {
            if (mb is ModelTemplate)
            {
                modelTemplate = mb as ModelTemplate;
                if (!readFromControl) //to prevent recursive setting
                    modelControl.SetModel(modelTemplate);
                runningModel = null;
                SetCurrentMode(RunMode.Template, name);
            }
            else
            {
                runningModel = mb as RunningModel;
                if (!readFromControl)
                    modelControl.SetModel(runningModel);
                modelOutputControl.SetRunningModel(null, runningModel);
                modelTemplate = null;
                SetCurrentMode(RunMode.RunningModel, name);
            }
        }

        private bool CheckModelBeforeSave()
        {
            ModelBase mb = modelControl.GetModel();
            List<string> errors = new();
            if (!mb.CheckValues(ref errors))
            {
                if (MessageBox.Show($"There are some errors in the model. Do you want to save as-is?\r\n" +
                    $"{string.Join("\r\n", errors)}", "Data Errors", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                    return false;
            }
            return true;
        }
        private bool SaveModelAsJSON()
        {
            try
            {
                if (!CheckModelBeforeSave())
                    return false;
                ModelBase mb = modelControl.GetModel();
                if (!string.IsNullOrEmpty(lastFileName))
                    saveFileJson.FileName = lastFileName;
                else
                    saveFileJson.FileName = mb.ModelName;
                saveFileJson.InitialDirectory = modelFileDefaultFolder;
                if (saveFileJson.ShowDialog() == DialogResult.OK)
                {
                    modelFileDefaultFolder = Path.GetDirectoryName(saveFileJson.FileName);
                    ModelFile.SaveToJson(saveFileJson.FileName, mb);
                    lastFileName = saveFileJson.FileName;
                    this.Text = $"SiliFish {Path.GetFileNameWithoutExtension(saveFileJson.FileName)}";
                    modelControl.ModelUpdated = false;
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
        private bool SaveModelAsExcel()
        {
            try
            {
                if (!CheckModelBeforeSave())
                    return false;
                ModelBase mb = modelControl.GetModel();
                if (!string.IsNullOrEmpty(lastFileName))
                    saveFileExcel.FileName = Path.ChangeExtension(lastFileName, "xlsx");
                else
                    saveFileExcel.FileName = $"{mb.ModelName}.xlsx";
                saveFileExcel.InitialDirectory = modelFileDefaultFolder;
                if (saveFileExcel.ShowDialog() == DialogResult.OK)
                {
                    modelFileDefaultFolder = Path.GetDirectoryName(saveFileExcel.FileName);
                    ModelFile.SaveToExcel(saveFileExcel.FileName, mb);
                    lastFileName = saveFileExcel.FileName;
                    this.Text = $"SiliFish {Path.GetFileNameWithoutExtension(saveFileExcel.FileName)}";
                    modelControl.ModelUpdated = false;
                    FileUtil.ShowFile(saveFileExcel.FileName);

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

        private void btnGenerateModel_Click(object sender, EventArgs e)
        {
            if (modelTemplate == null)
            {
                MessageBox.Show("There is no model template loaded.", "Error");
                return;
            }
            UseWaitCursor = true;
            List<string> errors = new();
            if (!modelTemplate.CheckValues(ref errors))
            {
                UseWaitCursor = false;
                MessageBox.Show($"There are errors in the template file. Please correct them before generating a model: \r\n" +
                    $"{string.Join("\r\n", errors)}", "Error");
                return;
            }
            MainForm mf = new(new RunningModel(modelTemplate));
            UseWaitCursor = false;
            mf.Show();
        }

        private void modelControl_PlotRequested(object sender, EventArgs e)
        {
            modelOutputControl.Plot((e as SelectedUnitArgs).unitsSelected);
        }


        private void modelControl_ModelChanged(object sender, EventArgs e)
        {
            SetModel(modelControl.GetModel(), null, true);
        }

        private void modelControl_HighlightRequested(object sender, EventArgs e)
        {
            if (e is SelectedUnitArgs selectedUnitArgs)
                modelOutputControl.Highlight(selectedUnitArgs.unitsSelected.First(), selectedUnitArgs.enforce);
        }



        private void miFileLoad_Click(object sender, EventArgs e)
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
                        lastFileName = openFileJson.FileName;
                        SetModel(mb, Path.GetFileNameWithoutExtension(openFileJson.FileName));
                    }
                    catch
                    {
                        MessageBox.Show("Selected file is not a valid Model or Template file.", "Error");
                        return;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show($"There is a problem in generating the model template from the JSON file.\r\n{exc.Message}", "Error");
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
            }
        }

        private void miFileSave_Click(object sender, EventArgs e)
        {
            SaveModelAsJSON();
        }

        private void miFileExport_Click(object sender, EventArgs e)
        {
            SaveModelAsExcel();
        }

        private void miFileImport_Click(object sender, EventArgs e)
        {
            try
            {
                openFileExcel.InitialDirectory = modelFileDefaultFolder;
                if (openFileExcel.ShowDialog() == DialogResult.OK)
                {
                    ModelBase mb;
                    try
                    {
                        mb = ModelFile.ReadFromExcel(openFileExcel.FileName);
                        lastFileName = openFileExcel.FileName;
                        SetModel(mb, Path.GetFileNameWithoutExtension(openFileExcel.FileName));
                    }
                    catch
                    {
                        MessageBox.Show("Selected file is not a valid Model or Template file.", "Error");
                        return;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show($"There is a problem in generating the model template from the JSON file.\r\n{exc.Message}", "Error");
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
            }
        }

 
        private void miFileNewModel_Click(object sender, EventArgs e)
        {
            MainForm mf = new();
            mf.Show();
        }

        private void miFileClearModel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to start from a brand new template? All changes you have made will be cleared.", "Warning",
                MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (modelTemplate != null)
                modelTemplate = new();
            else
                runningModel = new();
            modelControl.SetModel(modelTemplate);
        }

       private void miToolsCompareModel_Click(object sender, EventArgs e)
        {
            try
            {
                openFileJson.InitialDirectory = modelFileDefaultFolder;
                if (openFileJson.ShowDialog() == DialogResult.OK)
                {
                    ModelBase currentModel = (ModelBase)modelTemplate ?? runningModel;
                    ModelBase mb;
                    try
                    {
                        mb = ModelFile.Load(openFileJson.FileName, out List<string> issues);
                    }
                    catch
                    {
                        MessageBox.Show("Selected file is not a valid Model or Template file.", "Error");
                        return;
                    }
                    try
                    {
                        List<string> diffs = currentModel.DiffersFrom(mb);
                        if (diffs == null || diffs.Count == 0)
                            MessageBox.Show("Models are identical", "");
                        else
                            TextDisplayer.Display($"Comparing {currentModel.ModelName} to {mb.ModelName}", diffs, saveFileText);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("There is a problem in model comparison.", "Error");
                        ExceptionHandler.ExceptionHandling("Model comparison", exc);
                        return;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show($"There is a problem in generating the model template from the JSON file.\r\n{exc.Message}", "Error");
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
            }
        }
        private void miToolsCellularDynamics_Click(object sender, EventArgs e)
        {
            DynamicsTestControl dynControl = new("Izhikevich_9P", null, testMode: true);
            frmDynamics = new();
            frmDynamics.AddControl(dynControl, null);
            dynControl.ContentChanged += frmDynamics.ChangeCaption;
            frmDynamics.Text = "Cellular Dynamics Test";
            frmDynamics.SaveVisible = false;
            frmDynamics.Show();
        }

        private bool StatsWarning()
        {
            if (modelSimulator != null && modelSimulator.LastSimulation != null && modelSimulator.LastSimulation.SimulationRun)
            {
                string msg = "Exporting the stats data may take a while depending on the duration of the simulation and the number of cells." +
                    " Do you want to continue?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return false;
                return true;
            }
            MessageBox.Show("Please run a simulation to generate statistics for.");
            return false;

        }
        private void miToolsStatsSpikeStats_Click(object sender, EventArgs e)
        {
            if (!StatsWarning()) return;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                ModelFile.SaveSpikeFreqStats(modelSimulator.LastSimulation, saveFileCSV.FileName);
            }
        }

        private void miToolsStatsSpikes_Click(object sender, EventArgs e)
        {
            if (!StatsWarning()) return;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                ModelFile.SaveSpikes(modelSimulator.LastSimulation, saveFileCSV.FileName);
            }
        }

        private void miToolsStatsTBFs_Click(object sender, EventArgs e)
        {
            if (!StatsWarning()) return;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                ModelFile.SaveTBFs(modelSimulator.LastSimulation, saveFileCSV.FileName);
            }
        }

        private void miToolsStatsFull_Click(object sender, EventArgs e)
        {
            if (!StatsWarning()) return;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                ModelFile.SaveFullStats(modelSimulator.LastSimulation, saveFileCSV.FileName);
            }
        }
        private void miToolsRunForStats_Click(object sender, EventArgs e)
        {
            try
            {
                MultipleRunDialog runTimeStatsDialog = new(runTimeStats: true);
                runTimeStatsDialog.ShowDialog();
                numSimulations = runTimeStatsDialog.NumOfSimulations;
                if (numSimulations <= 0) return;
                if (!PreRun()) return;
                parallelRun = false;
                Application.DoEvents();
                RunSimulation();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                multiRunMode = false;
            }
        }
        private void miToolsSettings_Click(object sender, EventArgs e)
        {
            ControlContainer controlContainer = new()
            {
                Text = "Settings"
            };
            GlobalSettingsControl gsc = new(new GlobalSettingsProperties());
            controlContainer.AddControl(gsc, null);
            if (controlContainer.ShowDialog() == DialogResult.OK)
                gsc.GSProperties.Save();//save to global.settings 
            else
                _ = GlobalSettingsProperties.Load();//reload from global.settings
        }

        private void miViewOutputFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", GlobalSettings.OutputFolder);
            }
            catch { }
        }

        private void miViewTempFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", GlobalSettings.TempFolder);
            }
            catch { }
        }

        private void miViewAbout_Click(object sender, EventArgs e)
        {
            About about = new();
            about.ShowDialog();
        }


    }
}