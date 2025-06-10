using Controls;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SiliFish.Database;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using SiliFish.Repositories;
using SiliFish.Services;
using SiliFish.UI.Controls;
using SiliFish.UI.Definitions;
using SiliFish.UI.Dialogs;
using SiliFish.UI.EventArguments;
using SiliFish.UI.Services;
using System.Diagnostics;

namespace SiliFish.UI
{
    public partial class MainForm : Form
    {
        private bool parallelRun = false;
        private int numSimulations = 1;
        private static bool showAboutForm = true;
        public static bool currentPlotWarning = false;
        private DateTime runStart;
        private string modelFileDefaultFolder, lastFileName;
        private ModelTemplate modelTemplate = null;
        private RunningModel runningModel = null;
        private RunParam runParam = new();
        private Simulator modelSimulator;
        private SimulationDBWriter simulationDBWriter;
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
                modelControl.SetSelectionToLast();
                runParam = model.Settings.GetRunParam();
                FillRunParams();
                SetCurrentMode(RunMode.RunningModel, null);
                miToolsMemoryFlush.ToolTipText =
                    "If the current information is plotted, flushing clears the memory allocated to generate those lists.";

            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private void FillRunParams()
        {
            simulationSettings.SetValues(runParam);
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
            simulationSettings.SaveSettings(GlobalSettings.LastRunSettings);
        }
        private void GetLastRunSettings()
        {
            if (GlobalSettings.LastRunSettings.Count != 0)
            {
                simulationSettings.ReadSettings(GlobalSettings.LastRunSettings);
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
        private void SetProgressVisibility(bool visible)
        {
            progressBarRun.Visible = visible;
            lProgress.Visible = visible;
        }
        private void completeRunsAction(List<Simulation> simulations, int completion)
        {
            if (IsDisposed)//the form might have been closed without stopping the simulation
                return;
            Invoke(CompleteRuns, simulations, completion);
        }
        private void CompleteRuns(List<Simulation> simulations, int completion)
        {
            UseWaitCursor = false;
            timerRun.Enabled = false;
            progressBarRun.Value = progressBarRun.Maximum;
            SetProgressVisibility(false);
            btnStart.Visible = true;
            btnPause.Visible = btnResume.Visible = btnStop.Visible = false;
            if (completion < 0)
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
                    simText = $" - Number of simulations: {simulations.Count}";
                }
                TimeSpan ts = DateTime.Now - runStart;
                lRunTime.Text = $"Last run: {runStart:t} {simText}\r\n" +
                    (completion == 0 ? "Simulation interrupted - partial results displayed.\r\n" : "") +
                    $"Model: {runningModel.ModelName}\r\n" +
                    $"# of cells: {runningModel.GetNumberOfCells():n0}; # of conn.: {runningModel.GetNumberOfJunctions():n0}\r\n" +
                    $"Simulation time: {Util.TimeSpanToString(ts)}";
                modelOutputControl.SetRunningModel(modelSimulator.LastSimulation, runningModel);
                modelOutputControl.CompleteRun(modelSimulator.LastSimulation.RunParam);
                if (list.Count > 0)
                    TextDisplayer.Display($"Run time stats", list, saveFileText);
            }
        }
        private bool PreRun()
        {
            SaveLastRunSettings();
            runStart = DateTime.Now;
            lRunTime.Text = "";
            UseWaitCursor = true;
            progressBarRun.Value = 0;
            SetProgressVisibility(true);
            timerRun.Enabled = true;

            runningModel = modelControl.GetModel() as RunningModel;
            if (runningModel == null) return false;
            modelOutputControl.SetRunningModel(null, runningModel);

            List<string> errors = [];
            List<string> warnings = [];
            if (!runningModel.CheckValues(ref errors, ref warnings))
            {
                if (errors.Count > 0)
                {
                    MessageBox.Show($"There are errors in the model. Please correct them before running a simulation: \r\n" +
                        $"{string.Join("\r\n", errors)}", "Error");
                    return false;
                }
                if (warnings.Count > 0)
                {
                    if (MessageBox.Show($"There are some issues in the model. Do you want to run as-is?\r\n" +
                        $"{string.Join("\r\n", warnings)}", "Data Errors", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                        return false;
                }
            }
            if (runningModel.Settings.JunctionLevelTracking)
            {
                int iMax = simulationSettings.iMax;
                long numConn = runningModel.GetNumberOfJunctions();
                long memoryRequired = numConn * iMax * 8;//number of bytes
                memoryRequired /= 1024 * 1024 * 1024;
                if (memoryRequired > GlobalSettings.MemoryWarningLimit)
                {
                    string msg = $"There are {numConn:n0} connections, which would require more than {memoryRequired} GB extra memory for junction based current tracking.\r\n" +
                        $"Do you want to turn off current tracking to minimize memory problems? " +
                        $"Plotting the currents at a specific junction will regenerate the required data.";

                    DialogResult dlg = MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNoCancel);
                    if (dlg == DialogResult.Yes)
                        runningModel.Settings.JunctionLevelTracking = false;
                    else if (dlg == DialogResult.No)
                        runningModel.Settings.JunctionLevelTracking = true;
                    else
                        return false;
                }
            }
            runParam = simulationSettings.GetValues();
            runParam.TrackJunctionCurrent = runningModel.Settings.JunctionLevelTracking;
            if (modelSimulator?.SimulationList.Count > 0)
            {
                foreach (Simulation oldSim in modelSimulator?.SimulationList)
                {
                    if (oldSim.DBLink != null)
                    {
                        string dbname = oldSim.DBLink.DatabaseName;
                        FileUtil.DeleteFile(dbname, allExtensions: true);
                    }
                }
            }

            btnStart.Visible = false;
            btnResume.Visible = false;
            btnPause.Visible = true;
            btnStop.Visible = true;
            return true;
        }

        private void RunSimulationFromModel()
        {
            modelSimulator = new(runningModel, runParam, numSimulations, parallelRun, completeRunsAction);
            modelSimulator.Run();
        }
        private void PauseSimulation()
        {
            modelSimulator?.InterruptRun();
            btnPause.Visible = btnStart.Visible = false;
            btnResume.Visible = btnStop.Visible = true;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (modelSimulator?.ModelRun ?? false)
            {
                if (MessageBox.Show("Do you want to run the stimulation? Old simulation values will be lost.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            if (!PreRun())
                return;
            numSimulations = 1;
            parallelRun = false;
            RunSimulationFromModel();
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            PauseSimulation();
        }
        private void btnResume_Click(object sender, EventArgs e)
        {
            if (modelSimulator == null)
                return;
            timerRun.Enabled = true;
            SetProgressVisibility(true);
            progressBarRun.Value = (int)(modelSimulator.GetProgress() * progressBarRun.Maximum);
            lProgress.Text = $"Progress: {modelSimulator.GetStatus()}...";
            modelSimulator.ResumeRun();
            btnResume.Visible = btnStart.Visible = false;
            btnPause.Visible = btnStop.Visible = true;
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            bool interrupt = modelSimulator.GetProgress() > GlobalSettings.ProgressLimitToDiscard / 100;
            string msg = $"Do you want to stop the stimulation?\r\n" +
                (interrupt ? "You can access the values till the point it was executed." :
                "You will lose all the simulation data.");
            if (MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if (interrupt && !modelSimulator.Interrupted)
                PauseSimulation();
            else
                modelSimulator?.CancelRun();

            btnPause.Visible = btnResume.Visible = false;
            btnStart.Visible = true;
            btnStop.Visible = false;
            return;
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            if (simulationDBWriter != null)
            {
                progressBarRun.Value = (int)(simulationDBWriter.GetProgress() * progressBarRun.Maximum);
                lProgress.Text = $"Saving simulation summary results to the database.";
            }
            else if (modelSimulator != null)
            {
                progressBarRun.Value = (int)(modelSimulator.GetProgress() * progressBarRun.Maximum);
                lProgress.Text = $"Progress: {modelSimulator.GetStatus()}...";
            }
        }

        #endregion


        private void SetCurrentMode(RunMode mode, string name)
        {
            bool prevCollapsed = splitMain.Panel2Collapsed;
            miFileSaveSimulationResults.Visible =
            miToolsGenerateStatsData.Visible =
            miToolsMemoryFlush.Visible =
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
            List<string> errors = [];
            List<string> warnings = [];
            if (!mb.CheckValues(ref errors, ref warnings))
            {
                if (MessageBox.Show($"There are some errors/issues in the model. Do you want to save as-is?\r\n" +
                    $"{string.Join("\r\n", errors.Concat(warnings))}", "Data Errors/Warnings", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
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
            List<string> errorList = [];
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
                    ModelFile.SaveToExcel(saveFileExcel.FileName, mb, errorList);
                    lastFileName = saveFileExcel.FileName;
                    this.Text = $"SiliFish {Path.GetFileNameWithoutExtension(saveFileExcel.FileName)}";
                    modelControl.ModelUpdated = false;
                    string filename = saveFileExcel.FileName;
                    UtilWindows.DisplaySavedFile(filename);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            finally
            {
                if (errorList.Count > 0)
                    MessageBox.Show("");
            }
        }

        private bool CheckModelTemplate()
        {
            modelTemplate = modelControl.GetModel() as ModelTemplate;
            if (modelTemplate == null)
            {
                MessageBox.Show("There is no model template loaded.", "Error");
                return false;
            }
            List<string> errors = [];
            List<string> warnings = [];
            if (!modelTemplate.CheckValues(ref errors, ref warnings))
            {
                UseWaitCursor = false;
                if (errors.Count > 0)
                {
                    MessageBox.Show($"There are errors in the template file. Please correct them before generating a model: \r\n" +
                        $"{string.Join("\r\n", errors)}", "Error");
                    return false;
                }
                if (warnings.Count > 0)
                {
                    if (MessageBox.Show($"There are some issues in the model. Do you want to generate the running model as-is?\r\n" +
                        $"{string.Join("\r\n", warnings)}", "Data Errors", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                        return false;
                }
            }
            return true;
        }
        private void GenerateAndRunModelsFromTemplate(int numOfModels = 1)
        {
            if (!CheckModelTemplate())
                return;
            UseWaitCursor = true;
            if (parallelRun)
            {
                int oldSeed = modelTemplate.Settings.Seed;
                for (int i = 0; i < numOfModels; i++)
                {
                    MainForm mf = new(new RunningModel(modelTemplate))
                    {
                        WindowState = FormWindowState.Maximized
                    };
                    mf.Show();
                    mf.numSimulations = 1;
                    if (!mf.PreRun())
                        return;
                    Thread thread = new(mf.RunSimulationFromModel);
                    thread.Start();
                    modelTemplate.Settings.Seed += 1;//to have different results 
                }
                modelTemplate.Settings.Seed = oldSeed;
            }
            else
            {
                MainForm mf = new(new RunningModel(modelTemplate))
                {
                    WindowState = FormWindowState.Maximized
                };
                mf.Show();
                mf.numSimulations = numOfModels;
                mf.parallelRun = parallelRun;
                mf.RunSimulationFromModel();
            }

            UseWaitCursor = false;
        }
        private void btnGenerateModel_Click(object sender, EventArgs e)
        {
            if (!CheckModelTemplate())
                return;
            UseWaitCursor = true;
            MainForm mf = new(new RunningModel(modelTemplate))
            {
                WindowState = FormWindowState.Maximized
            };
            mf.Show();

            UseWaitCursor = false;
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

        private void modelOutputControl_PlottingSelection(object sender, EventArgs e)
        {
            if (e is SelectedUnitArgs selectedUnitArgs)
                modelControl.SetSelection(selectedUnitArgs.unitsSelected);
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

        private bool SaveSimulationResultsToDBStart()
        {
            try
            {
                using SFDataContext dataContext = new();
                //check whether the database is blank - by checking the presence of Models table
                using var connection = new SqliteConnection(dataContext.Database.GetDbConnection().ConnectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = "PRAGMA table_info(Models);";
                using var reader = command.ExecuteReader();
                bool tableExists = reader.HasRows;

                if (!tableExists) return false;
                simulationDBWriter = new(dataContext.DbFileName, modelSimulator, completeSaveSimulationAction, abortSaveSimulationAction);
                btnStart.Enabled = btnResume.Enabled = btnPause.Enabled = btnStop.Enabled = false;
                UseWaitCursor = true;
                progressBarRun.Value = 0;
                SetProgressVisibility(true);
                timerRun.Enabled = true;
                return true;
            }
            catch (Exception exc)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
                return false;
            }
        }

        private void completeSaveSimulationAction()
        {
            Invoke(SaveSimulationResultsToDBEnd);
        }

        private void abortSaveSimulationAction()
        {
            Invoke(SaveSimulationResultsToDBAborted);
        }
        private void SaveSimulationResultsToDBEnd()
        {
            UseWaitCursor = false;
            timerRun.Enabled = false;
            progressBarRun.Value = progressBarRun.Maximum;
            SetProgressVisibility(false);
            btnStart.Enabled = btnResume.Enabled = btnPause.Enabled = btnStop.Enabled = false;
            string savedSimulations = $"\r\nSimulation id(s): {string.Join(',', simulationDBWriter.SimulationIds)}";
            string savedModels = string.Join(',', simulationDBWriter.ModelJsons);
            if (!string.IsNullOrEmpty(savedModels))
                savedModels = $"\r\nRelated models saved as {savedModels}";
            MessageBox.Show($"Simulation summary results are saved to {new SFDataContext().DbFileName}{savedSimulations}{savedModels}");
            simulationDBWriter = null;
            miFileSaveSimulationResults.Text = $"Save Simulation Results (last saved at {DateTime.Now:ddd} {DateTime.Now:t})";
        }

        private void SaveSimulationResultsToDBAborted()
        {
            UseWaitCursor = false;
            timerRun.Enabled = false;
            progressBarRun.Value = progressBarRun.Maximum;
            SetProgressVisibility(false);
            btnStart.Enabled = btnResume.Enabled = btnPause.Enabled = btnStop.Enabled = true;
            MessageBox.Show($"There was a problem in saving the simulation to {new SFDataContext().DbFileName}");
            simulationDBWriter = null;
        }
        private void miFileSaveSimulationResults_Click(object sender, EventArgs e)
        {
            try
            {
                if (modelSimulator == null || !modelSimulator.ModelRun || modelSimulator.LastSimulation == null)
                {
                    MessageBox.Show("There is no simulation to be saved.");
                    return;
                }
                //get the latest name and description, as can be updated while the simulation is run
                ModelBase updatedModel = modelControl.GetModel();
                modelSimulator.UpdateNameAndDecription(updatedModel.ModelName, updatedModel.ModelDescription);
                if (!SaveSimulationResultsToDBStart())
                {
                    MessageBox.Show("There is a problem with connecting to the database or the database is corrupt.");
                    return;
                }
                simulationDBWriter.Run();
            }
            catch (Exception exc)
            {
                MessageBox.Show($"There is a problem in saving the simulation results.\r\n{exc.Message}", "Error");
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
            }
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
                        List<Difference> diffs = currentModel.DiffersFrom(mb);
                        if (diffs == null || diffs.Count == 0)
                            MessageBox.Show("Models are identical", "");
                        else
                        {
                            diffs.Insert(0, new Difference("Model name", "Current", "Compared to"));
                            TextDisplayer.Display($"Comparing {currentModel.ModelName} to {mb.ModelName}", diffs, saveFileText);
                        }
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
            ControlContainer frmDynamics = new()
            {
                WindowState = FormWindowState.Maximized
            };
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
        private void miToolsStatsSpikeCounts_Click(object sender, EventArgs e)
        {
            if (!StatsWarning()) return;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                if (SimulationFile.SaveSpikeCounts(modelSimulator.LastSimulation, saveFileCSV.FileName))
                {
                    string filename = saveFileCSV.FileName;
                    UtilWindows.DisplaySavedFile(filename);
                }
            }
        }
        private void miToolsStatsSpikeStats_Click(object sender, EventArgs e)
        {
            if (!StatsWarning()) return;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileCSV.FileName;
                if (SimulationFile.SaveSpikeFreqStats(modelSimulator.LastSimulation, filename))
                {
                    UtilWindows.DisplaySavedFile(filename);
                }
            }
        }

        private void miToolsStatsSpikes_Click(object sender, EventArgs e)
        {
            if (!StatsWarning()) return;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                if (SimulationFile.SaveSpikes(modelSimulator.LastSimulation, saveFileCSV.FileName))
                {
                    string filename = saveFileCSV.FileName;
                    UtilWindows.DisplaySavedFile(filename);
                }
            }
        }

        private void miToolsStatsEpisodes_Click(object sender, EventArgs e)
        {
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                if (SimulationFile.SaveEpisodes(modelSimulator.LastSimulation, saveFileCSV.FileName))
                {
                    string filename = saveFileCSV.FileName;
                    UtilWindows.DisplaySavedFile(filename);
                }
            }
        }

        private void miToolsStatsMembranePotentials_Click(object sender, EventArgs e)
        {
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                if (SimulationFile.SaveMembranePotentials(modelSimulator.LastSimulation, saveFileCSV.FileName))
                {
                    string filename = saveFileCSV.FileName;
                    UtilWindows.DisplaySavedFile(filename);
                }
            }
        }

        private void miToolsStatsCurrents_Click(object sender, EventArgs e)
        {
            //TODO create a class similar to SimulationDBWriter and use it to save all the stats info
            //to be able to use the progress bar or to be able to cancel the saving
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                if (SimulationFile.SaveCurrents(modelSimulator.LastSimulation, saveFileCSV.FileName))
                {
                    string filename = saveFileCSV.FileName;
                    UtilWindows.DisplaySavedFile(filename);
                }
            }
        }
        private void miToolsStatsFull_Click(object sender, EventArgs e)
        {
            if (!StatsWarning()) return;
            if (saveFileExcel.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Membrane potential and (if tracked) current information will be saved as seperate csv files for file maintainability.", "Information");
                if (SimulationFile.SaveFullStats(modelSimulator.LastSimulation, saveFileExcel.FileName))
                {
                    string folder = Path.GetDirectoryName(saveFileExcel.FileName);
                    Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", folder);
                }
            }
        }

        private void miToolsMultipleRun_Click(object sender, EventArgs e)
        {
            try
            {
                MultipleRunDialog runTimeStatsDialog = new(runTimeStats: false, simulationSettings.GetValues());
                runTimeStatsDialog.ShowDialog();
                numSimulations = runTimeStatsDialog.NumOfSimulations;
                if (numSimulations <= 0) return;
                simulationSettings.SetValues(runTimeStatsDialog.RunParam);
                parallelRun = true;
                Application.DoEvents();
                if (!PreRun())
                {
                    if (modelTemplate != null)
                        GenerateAndRunModelsFromTemplate(numSimulations);
                }
                else
                {
                    RunSimulationFromModel();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        private void miToolsRunTimeStats_Click(object sender, EventArgs e)
        {
            try
            {
                MultipleRunDialog runTimeStatsDialog = new(runTimeStats: true, simulationSettings.GetValues());
                runTimeStatsDialog.ShowDialog();
                numSimulations = runTimeStatsDialog.NumOfSimulations;
                if (numSimulations <= 0) return;
                simulationSettings.SetValues(runTimeStatsDialog.RunParam);
                parallelRun = false;
                Application.DoEvents();
                if (!PreRun())
                {
                    if (modelTemplate != null)
                        GenerateAndRunModelsFromTemplate(numSimulations);
                }
                else
                {
                    RunSimulationFromModel();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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

        private void miToolsMemoryFlush_Click(object sender, EventArgs e)
        {
            if (Model is RunningModel runningModel)
            {
                double cleared = runningModel.MemoryFlush();
                if (cleared > 0)
                    MessageBox.Show($"{cleared / (1024 * 1024): #.##} megabytes are cleared from memory.", "Info");
                else
                    MessageBox.Show("There are no structures created for plotting purposes to clear from memory.", "Info");

            }
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

        private void miFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}