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

namespace SiliFish.UI
{
    public partial class MainForm : Form
    {
        private static bool showAboutForm = true;
        private DateTime runStart;
        private string modelFileDefaultFolder, lastFileName;
        private ModelTemplate ModelTemplate = null;
        private RunningModel RunningModel = null;
        public ModelBase Model
        {
            get
            {
                return (ModelBase)ModelTemplate ?? RunningModel;
            }
        }

        public MainForm()
        {
            try
            {
                InitializeComponent();
                ModelTemplate = new();
                modelControl.SetModel(ModelTemplate);
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
                ModelTemplate = null;
                RunningModel = model;

                modelControl.SetModel(model);
                modelOutputControl.SetRunningModel(model);
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
            if (Model == null) return;
            edt.Value = (decimal)RunningModel.RunParam.DeltaT;
            edtEuler.Value = (decimal)RunningModel.RunParam.DeltaTEuler;
            eSkip.Value = RunningModel.RunParam.SkipDuration;
            eTimeEnd.Value = RunningModel.RunParam.MaxTime;
        }
        private void RefreshModel()
        {
            RunningModel = modelControl.GetModel() as RunningModel;
            modelOutputControl.SetRunningModel(RunningModel);
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
                ModelTemplate = null;
                RunningModel = null;
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
            GlobalSettings.LastRunSettings[ldtEuler.Name] = edtEuler.Text;
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
                    edtEuler.Text = GlobalSettings.LastRunSettings[ldtEuler.Name];
                }
                catch { }
            }
        }
        #region Settings
        private void linkOpenOutputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", GlobalSettings.OutputFolder);
            }
            catch { }
        }

        private void linkOpenTempFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", GlobalSettings.TempFolder);
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

        private void btnSettings_Click(object sender, EventArgs e)
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

        #endregion

        #region Simulation
        private void RunModel()
        {
            try
            {
                RunningModel.RunModel();
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
                $"Model: {RunningModel.ModelName}\r\n" +
                $"# of cells: {RunningModel.GetNumberOfCells():n0}; # of conn.: {RunningModel.GetNumberOfConnections():n0}\r\n" +
                $"Simulation time: {Util.TimeSpanToString(ts)}";

            modelOutputControl.SetRunningModel(RunningModel);
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
            SaveLastRunSettings();
            runStart = DateTime.Now;
            lRunTime.Text = "";
            UseWaitCursor = true;
            progressBarRun.Value = 0;
            progressBarRun.Visible = true;
            timerRun.Enabled = true;

            RefreshModel();
            if (RunningModel == null) return;
            List<string> errors = new();
            if (!RunningModel.CheckValues(ref errors))
            {
                MessageBox.Show($"There are errors in the model. Please correct them before running a simulation: \r\n" +
                    $"{string.Join("\r\n", errors)}", "Error");
                return;
            }
            RunningModel.JunctionCurrentTrackingOn = GlobalSettings.JunctionLevelTracking;
            if (GlobalSettings.JunctionLevelTracking)
            {
                int iMax = (int)(eTimeEnd.Value / edt.Value);
                long numConn = RunningModel.GetNumberOfConnections();
                long memoryRequired = numConn * iMax * 8;//number of bytes
                memoryRequired /= (1024 * 1024 * 1024);
                if (memoryRequired > GlobalSettings.MemoryWarningLimit)
                {
                    string msg = $"There are {numConn:n0} connections, which would require more than {memoryRequired} GB extra memory for junction based current tracking.\r\n" +
                        $"Do you want to turn off current tracking to minimize memory problems? You will not be able to plot the currents at a specific junction.";

                    DialogResult dlg = MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNoCancel);
                    if (dlg == DialogResult.Yes)
                        RunningModel.JunctionCurrentTrackingOn = false;
                    else if (dlg == DialogResult.No)
                        RunningModel.JunctionCurrentTrackingOn = true;
                    else
                        return;
                }
            }
            RunningModel.RunParam = new()
            {
                SkipDuration = (int)eSkip.Value,
                MaxTime = (int)eTimeEnd.Value,
                DeltaT = (double)edt.Value,
                DeltaTEuler = (double)edtEuler.Value,
                TrackJunctionCurrent = RunningModel.JunctionCurrentTrackingOn
            };
            btnRun.Text = "Stop Run";
            Task.Run(RunModel);
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            if (RunningModel == null) return;
            progressBarRun.Value = (int)((RunningModel?.GetProgress() ?? 0) * progressBarRun.Maximum);
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
                ModelFile.SaveToFile(RunningModel, saveFileCSV.FileName);
            }
        }

        #endregion

        ControlContainer frmDynamics;
        private void btnCellularDynamics_Click(object sender, EventArgs e)
        {
            DynamicsTestControl dynControl = new("Izhikevich_9P", null, testMode: true);
            frmDynamics = new();
            frmDynamics.AddControl(dynControl, null);
            dynControl.ContentChanged += frmDynamics.ChangeCaption;
            frmDynamics.Text = "Cellular Dynamics Test";
            frmDynamics.SaveVisible = false;
            frmDynamics.Show();
        }


        private void SetCurrentMode(RunMode mode, string name)
        {
            bool prevCollapsed = splitMain.Panel2Collapsed;
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

        private void SetModel(ModelBase mb, string name, bool readFromControl = false)
        {
            if (mb is ModelTemplate)
            {
                ModelTemplate = mb as ModelTemplate;
                if (!readFromControl) //to prevent recursive setting
                    modelControl.SetModel(ModelTemplate);
                RunningModel = null;
                SetCurrentMode(RunMode.Template, name);
            }
            else
            {
                RunningModel = mb as RunningModel;
                if (!readFromControl)
                    modelControl.SetModel(RunningModel);
                modelOutputControl.SetRunningModel(RunningModel);
                ModelTemplate = null;
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
        private void linkSaveModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveModelAsJSON();
        }

        private void linkExportModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveModelAsExcel();
        }


        private void linkImportModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void btnGenerateModel_Click(object sender, EventArgs e)
        {
            if (ModelTemplate == null)
            {
                MessageBox.Show("There is no model template loaded.", "Error");
                return;
            }
            UseWaitCursor = true;
            List<string> errors = new();
            if (!ModelTemplate.CheckValues(ref errors))
            {
                UseWaitCursor = false;
                MessageBox.Show($"There are errors in the template file. Please correct them before generating a model: \r\n" +
                    $"{string.Join("\r\n", errors)}", "Error");
                return;
            }
            MainForm mf = new(new RunningModel(ModelTemplate));
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

        private void btnNewModel_Click(object sender, EventArgs e)
        {
            MainForm mf = new();
            mf.Show();
        }

        private void btnClearModel_Click(object sender, EventArgs e)
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
    }
}