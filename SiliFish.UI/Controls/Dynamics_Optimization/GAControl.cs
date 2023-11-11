using GeneticSharp;
using OxyPlot;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Stim;
using SiliFish.Repositories;
using SiliFish.Services.Optimization;
using SiliFish.UI.EventArguments;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.Gaming.XboxLive.Storage;

namespace SiliFish.UI.Controls
{

    public partial class GAControl : UserControl
    {
        private static string GAFileDefaultFolder;
        private string coreType;

        private bool exhaustiveSearch = false;

        private bool optimizationCancelled = false;
        private DateTime optimizationStartTime;
        private TimeSpan optimizationDuration;

        private CoreSolver solver;
        private CoreSolverOutput solverOutput;

        private List<CoreSolver> exhaustiveSolverList;
        private int exhaustiveSolverIterator;
        private CoreSolverOutput exhaustiveSolverOutput;
        private CoreSolver exhaustiveBestSolver;
        private List<(double Fitness, Dictionary<string, double> Parameters)> exhaustiveBestParameters;
        private int exhaustiveBestSolverIterator;


        private Dictionary<string, double> minValues;
        private Dictionary<string, double> maxValues;
        private TargetRheobaseFunction targetRheobaseFunction;
        private List<FitnessFunction> fitnessFunctions;//All fitness functions except the target rheobase
        private ProgressForm optimizationProgress;

        public event EventHandler OnCompleteOptimization;
        public event EventHandler OnTriggerOptimization;
        public event EventHandler OnLoadParams;
        public event EventHandler OnGetParams;
        public event EventHandler ContentChanged;

        public double DeltaT { get; set; }
        public string CoreType
        {
            get { return coreType; }
            set
            {
                coreType = value;
                if (solver?.Settings != null)
                    solver.Settings.CoreType = value;
            }
        }
        public Dictionary<string, double> Parameters { get; set; }

        public GAControl()
        {
            InitializeComponent();
            ddGASelection.Items.AddRange(GeneticAlgorithmExtension.GetSelectionBases().ToArray());
            ddGASelection.SelectedIndex = 0;
            ddGACrossOver.Items.AddRange(GeneticAlgorithmExtension.GetCrossoverBases().ToArray());
            ddGACrossOver.SelectedIndex = 0;
            ddGAMutation.Items.AddRange(GeneticAlgorithmExtension.GetMutationBases().ToArray());
            ddGAMutation.SelectedIndex = 0;
            ddGAReinsertion.Items.AddRange(GeneticAlgorithmExtension.GetReinsertionBases().ToArray());
            ddGAReinsertion.SelectedIndex = 0;
            ddGATermination.Items.AddRange(GeneticAlgorithmExtension.GetTerminationBases().ToArray());
            ddGATermination.SelectedIndex = 0;
        }

        private void RunOptimize()
        {
            if (solver == null) return;
            exhaustiveSearch = false;
            optimizationStartTime = DateTime.Now;
            optimizationCancelled = false;
            solverOutput = solver.Optimize(single:true).Outputs[0];
            Invoke(CompleteOptimization);
        }

        private void CheckResult(CoreSolverOutput currentOutput)
        {
            if (exhaustiveBestParameters.Any(bp => string.Join(",", bp.Item2) == string.Join(",", currentOutput.Values)))
                return;
            if (exhaustiveSolverOutput == null || exhaustiveSolverOutput.Fitness <= currentOutput.Fitness)
            {
                exhaustiveSolverOutput = currentOutput;
                exhaustiveBestSolver = solver;
                Invoke(TriggerOptimizationLoop);
            }
            if (exhaustiveBestParameters.Count < GlobalSettings.GeneticAlgorithmExhaustiveSolutionCount)
                exhaustiveBestParameters.Add((currentOutput.Fitness, currentOutput.Values));
            else
            {
                double minFitness = exhaustiveBestParameters.Min(ebp => ebp.Fitness);
                if (minFitness < currentOutput.Fitness)
                {
                    var toRemove = exhaustiveBestParameters.Find(ebp => ebp.Fitness == minFitness);
                    exhaustiveBestParameters.Remove(toRemove);
                    exhaustiveBestParameters.Add((currentOutput.Fitness, currentOutput.Values));
                }
            }

        }
        private void RunOptimizeExhaustive()
        {
            if (exhaustiveSolverList == null || !exhaustiveSolverList.Any()) return;
            exhaustiveSearch = true;
            exhaustiveBestParameters = new();
            optimizationStartTime = DateTime.Now;
            optimizationCancelled = false;
            exhaustiveSolverOutput = null;
            exhaustiveBestSolver = null;
            exhaustiveSolverIterator = 0;
            foreach (CoreSolver iterSolver in exhaustiveSolverList)
            {
                if (optimizationCancelled) break;
                solver = iterSolver;
                exhaustiveSolverIterator++;
                List<CoreSolverOutput> currentOutputs = solver.Optimize(single: false).Outputs;
                foreach(CoreSolverOutput output in currentOutputs)
                    CheckResult(output);
                if (exhaustiveBestParameters.Min(ebp => ebp.Fitness) >= iterSolver.Settings.TargetFitness - GlobalSettings.Epsilon
                    && exhaustiveBestParameters.Count == GlobalSettings.GeneticAlgorithmExhaustiveSolutionCount)
                    break;

            }
            solver = exhaustiveBestSolver;
            solverOutput = exhaustiveSolverOutput;
            Invoke(CompleteExhaustiveOptimization);
        }
        private void TriggerOptimizationLoop()
        {
            OnTriggerOptimization?.Invoke(this, new OptimizationEventArgs()
            {
                Fitness = exhaustiveSolverOutput.Fitness,
                Parameters = exhaustiveSolverOutput.Values
            });
            lOptimizationOutput.Text = $"Best fitness: {exhaustiveSolverOutput.Fitness}";
        }

        private void CompleteExhaustiveOptimization()
        {
            if (exhaustiveBestParameters.Any())
            {
                btnOptimizePrev.Visible =
                btnOptimizeNext.Visible = true;
                btnOptimizePrev.Enabled = false;
                btnOptimizeNext.Enabled = exhaustiveBestParameters.Count > 1;
                exhaustiveBestSolverIterator = 0;
                linkExportExhOptRes.Visible = true;
            }
            else
            {
                btnOptimizePrev.Visible =
                btnOptimizeNext.Visible = false;
                linkExportExhOptRes.Visible = false;
            }
            CompleteOptimization();
        }
        private void CompleteOptimization()
        {
            optimizationDuration = DateTime.Now - optimizationStartTime;
            timerOptimization.Enabled = false;
            exhaustiveSolverList = null;
            exhaustiveSolverIterator = 0;
            btnOptimize.Enabled = btnOptimizeExhaustive.Enabled = true;
            if (solverOutput == null) return;
            if (exhaustiveSearch && exhaustiveBestParameters.Any())
                Parameters = exhaustiveBestParameters[0].Parameters;
            else
                Parameters = solverOutput.Values;
            if (optimizationProgress != null && optimizationProgress.Visible)
                optimizationProgress.Close();
            optimizationProgress = null;
            OnCompleteOptimization?.Invoke(this, EventArgs.Empty);
            if (exhaustiveSearch && exhaustiveBestParameters.Any())
                lOptimizationOutput.Text = $"Current fitness: {exhaustiveBestParameters[0].Fitness}\r\n" +
                $"Opimization duration: {optimizationDuration:g}";
            else
                lOptimizationOutput.Text = $"Latest fitness: {solverOutput.Fitness}\r\n" +
                $"Opimization duration: {optimizationDuration:g}";
            if (!string.IsNullOrEmpty(solverOutput.ErrorMessage))
                lOptimizationOutput.Text += $"\r\nOptimization ran with errors: {solverOutput.ErrorMessage}";
        }

        private void CreateSolver()
        {
            solver = null;
            ReadMinMaxParamValues();
            ReadFitnessFunctions();
            if (!int.TryParse(eMinChromosome.Text, out int minPopulation))
                minPopulation = 50;
            if (!int.TryParse(eMaxChromosome.Text, out int maxPopulation))
                maxPopulation = 50;
            double? targetFitness = null;
            if (cbTargetFitness.Checked && double.TryParse(eTargetFitness.Text, out double d))
                targetFitness = d;
            int? maxGeneration = null;
            if (cbMaxGeneration.Checked && int.TryParse(eMaxGeneration.Text, out int i))
                maxGeneration = i;
            string customTermination = cbCustomTermination.Checked ? ddGATermination.Text : "";
            string customTerminationParam = cbCustomTermination.Checked ? eTerminationParameter.Text : "";

            CoreSolverSettings settings = new()
            {
                DeltaT = DeltaT,
                SelectionType = ddGASelection.Text,
                CrossOverType = ddGACrossOver.Text,
                MutationType = ddGAMutation.Text,
                ReinsertionType = ddGAReinsertion.Text,
                MaxGeneration = maxGeneration,
                TargetFitness = targetFitness,
                TerminationType = customTermination,
                TerminationParam = customTerminationParam,
                MinPopulationSize = minPopulation,
                MaxPopulationSize = maxPopulation,
                CoreType = CoreType,
                TargetRheobaseFunction = targetRheobaseFunction,
                FitnessFunctions = fitnessFunctions.Select(ff => ff as FitnessFunction).ToList(),
                ParamValues = Parameters,
                MinValueDictionary = minValues,
                MaxValueDictionary = maxValues
            };
            solver = new() { Settings = settings };
        }
        private void CreateExhaustiveSolverList()
        {
            exhaustiveSolverList = new();
            ReadMinMaxParamValues();
            ReadFitnessFunctions();
            if (!int.TryParse(eMinChromosome.Text, out int minPopulation))
                minPopulation = 50;
            if (!int.TryParse(eMaxChromosome.Text, out int maxPopulation))
                maxPopulation = 50;
            double? targetFitness = null;
            if (cbTargetFitness.Checked && double.TryParse(eTargetFitness.Text, out double d))
                targetFitness = d;
            int? maxGeneration = null;
            if (cbMaxGeneration.Checked && int.TryParse(eMaxGeneration.Text, out int i))
                maxGeneration = i;
            string customTermination = cbCustomTermination.Checked ? ddGATermination.Text : "";
            string customTerminationParam = cbCustomTermination.Checked ? eTerminationParameter.Text : "";

            foreach (var sb in GeneticAlgorithmExtension.GetSelectionBases())
            {
                string selection = sb.ToString();
                foreach (var co in GeneticAlgorithmExtension.GetCrossoverBases())
                {
                    string crossover = co.ToString();
                    foreach (var mb in GeneticAlgorithmExtension.GetMutationBases())
                    {
                        string mutation = mb.ToString();
                        foreach (var rb in GeneticAlgorithmExtension.GetReinsertionBases())
                        {
                            string reinsertion = rb.ToString();
                            CoreSolverSettings settings = new()
                            {
                                DeltaT = DeltaT,
                                SelectionType = selection,
                                CrossOverType = crossover,
                                MutationType = mutation,
                                ReinsertionType = reinsertion,
                                MaxGeneration = maxGeneration,
                                TargetFitness = targetFitness,
                                TerminationType = customTermination,
                                TerminationParam = customTerminationParam,
                                MinPopulationSize = minPopulation,
                                MaxPopulationSize = maxPopulation,
                                CoreType = CoreType,
                                TargetRheobaseFunction = targetRheobaseFunction,
                                FitnessFunctions = fitnessFunctions.Select(ff => ff as FitnessFunction).ToList(),
                                ParamValues = Parameters,
                                MinValueDictionary = minValues,
                                MaxValueDictionary = maxValues
                            };
                            exhaustiveSolverList.Add(new CoreSolver() { Settings = settings });
                        }
                    }
                }
            }
        }

        private void ReadMinMaxParamValues()
        {
            minValues = new();
            maxValues = new();
            foreach (DataGridViewRow row in dgMinMaxValues.Rows)
            {
                string param = row.Cells[colParameter.Index].Value?.ToString();
                double value = Parameters[param];
                if (!string.IsNullOrEmpty(row.Cells[colMinValue.Index].Value?.ToString())
                    && double.TryParse(row.Cells[colMinValue.Index].Value.ToString(), out double dmin))
                    minValues.Add(param, dmin);
                else minValues.Add(param, value);
                if (!string.IsNullOrEmpty(row.Cells[colMaxValue.Index].Value?.ToString())
                    && double.TryParse(row.Cells[colMaxValue.Index].Value.ToString(), out double dmax))
                    maxValues.Add(param, dmax);
                else maxValues.Add(param, value);
            }
        }

        private void ReadFitnessFunctions()
        {
            List<FitnessFunction> fitnessFunctions = new();
            for (int rowind = 0; rowind < dgFitnessFunctions.RowCount; rowind++)
            {
                fitnessFunctions.Add(dgFitnessFunctions.Rows[rowind].Tag as FitnessFunction);
            }
            targetRheobaseFunction = fitnessFunctions.FirstOrDefault(fc => fc is TargetRheobaseFunction) as TargetRheobaseFunction;
            fitnessFunctions.RemoveAll(fc => fc is TargetRheobaseFunction);
            this.fitnessFunctions = fitnessFunctions;
        }

        private void ProgressForm_StopRunClicked(object sender, EventArgs e)
        {
            timerOptimization.Enabled = false;
            optimizationCancelled = true;
            solver?.CancelOptimization();
            if (exhaustiveBestSolver != null)
            {
                solver = exhaustiveBestSolver;
            }
            btnOptimize.Enabled = btnOptimizeExhaustive.Enabled = true;
        }

        private void linkSuggestMinMax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CellCore core = CellCore.CreateCore(CoreType, Parameters, DeltaT);

            if (core == null) return;

            (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) = core.GetSuggestedMinMaxValues();

            ResetParameters(Parameters, MinValues, MaxValues);
        }
        private void timerOptimization_Tick(object sender, EventArgs e)
        {
            if (solver == null) return;
            string progress = solver.ProgressText;
            if (exhaustiveSolverIterator > 0)
                progress = $"{progress}\r\n" +
                    $"~.~.~.~.~.~.~.~.~.~.~.~.~.~\r\n" +
                    $"Exhaustive search {exhaustiveSolverIterator}/{exhaustiveSolverList.Count}\r\n" +
                    $"Best overall fitness: {exhaustiveSolverOutput?.Fitness}";
            optimizationProgress.Progress = solver.Progress;
            optimizationProgress.ProgressText = progress;
        }

        private void FitnessFunctionToGrid(int rowInd, FitnessFunction fitnessFunction)
        {
            if (rowInd >= dgFitnessFunctions.RowCount || fitnessFunction == null) return;
            dgFitnessFunctions.Rows[rowInd].Tag = fitnessFunction;
            dgFitnessFunctions[colFFMode.Index, rowInd].Value = fitnessFunction.FitnessFunctionType;
            dgFitnessFunctions[colFFWeight.Index, rowInd].Value = fitnessFunction.Weight;
            dgFitnessFunctions[colFFFunction.Index, rowInd].Value = fitnessFunction.Details;
        }
        private void AddFitnessFunctionRow(FitnessFunction fitnessFunction = null)
        {
            if (fitnessFunction == null) return;
            dgFitnessFunctions.Rows.Add();
            int rowInd = dgFitnessFunctions.RowCount - 1;
            FitnessFunctionToGrid(rowInd, fitnessFunction);
        }

        private void linkAddFitnessFunction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddFitnessFunctionRow(OpenFitnessFunctionDialog(null));
        }

        private void ClearFitnessControls()
        {
            dgFitnessFunctions.Rows.Clear();
        }

        private void LoadFitnessFunctions()
        {
            ClearFitnessControls();
            if (solver.Settings.TargetRheobaseFunction != null)
                AddFitnessFunctionRow(solver.Settings.TargetRheobaseFunction);
            foreach (FitnessFunction fitnessFunction in solver.Settings.FitnessFunctions)
                AddFitnessFunctionRow(fitnessFunction);
        }

        private static FitnessFunction OpenFitnessFunctionDialog(FitnessFunction fitnessFunction)
        {
            FitnessFunctionControl fitnessControl = new();
            fitnessControl.SetFitnessFunction(fitnessFunction);
            ControlContainer frmControl = new();
            frmControl.AddControl(fitnessControl, null);
            frmControl.Text = fitnessFunction?.FitnessFunctionType;
            if (frmControl.ShowDialog() == DialogResult.OK)
            {
                fitnessFunction = fitnessControl.GetFitnessFunction();
                return fitnessFunction;
            }
            return null;
        }
        private void dgFitnessFunctions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == colFFEdit.Index)
            {
                FitnessFunction fitnessFunction = dgFitnessFunctions.Rows[e.RowIndex].Tag as FitnessFunction;
                FitnessFunctionToGrid(e.RowIndex, OpenFitnessFunctionDialog(fitnessFunction));
            }
            else if (e.ColumnIndex == colFFDelete.Index)
                dgFitnessFunctions.Rows.RemoveAt(e.RowIndex);
        }
        private void dgFitnessFunctions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                dgFitnessFunctions.AutoResizeColumns();
                return;
            }
            FitnessFunction fitnessFunction = dgFitnessFunctions.Rows[e.RowIndex].Tag as FitnessFunction;
            FitnessFunctionToGrid(e.RowIndex, OpenFitnessFunctionDialog(fitnessFunction));
        }

        private void dgFitnessFunctions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //weight can be directly editted on the grid
            if (e.RowIndex < 0 || e.ColumnIndex != colFFWeight.Index)
                return;
            FitnessFunction fitnessFunction = dgFitnessFunctions.Rows[e.RowIndex].Tag as FitnessFunction;
            if (double.TryParse(dgFitnessFunctions[colFFWeight.Index, e.RowIndex].Value?.ToString(), out double d))
            {
                fitnessFunction.Weight = d;
                dgFitnessFunctions.Rows[e.RowIndex].Tag = fitnessFunction;
            }
        }

        private void ddGATermination_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolTip.SetToolTip(lGATerminationParameter, GeneticAlgorithmExtension.GetTerminationParameter(ddGATermination.Text));
        }

        private void btnOptimize_Click(object sender, EventArgs e)
        {
            if (!cbTargetFitness.Checked && !cbMaxGeneration.Checked && !cbCustomTermination.Checked)
            {
                MessageBox.Show("Select at least one termination mode.", "Warning");
                return;
            }
            CreateSolver();
            timerOptimization.Enabled = true;
            btnOptimize.Enabled = btnOptimizeExhaustive.Enabled = false;
            btnOptimizePrev.Enabled = btnOptimizeNext.Enabled = false;
            optimizationProgress = new()
            {
                Text = "Optimization",
                Progress = 0,
                ProgressText = ""
            };
            optimizationProgress.StopRunClicked += ProgressForm_StopRunClicked;
            optimizationProgress.Show();
            Task.Run(RunOptimize);
        }
        private void btnOptimizeFull_Click(object sender, EventArgs e)
        {
            if (!cbTargetFitness.Checked && !cbMaxGeneration.Checked && !cbCustomTermination.Checked)
            {
                MessageBox.Show("Select at least one termination mode.", "Warning");
                return;
            }
            CreateExhaustiveSolverList();
            timerOptimization.Enabled = true;
            btnOptimize.Enabled = btnOptimizeExhaustive.Enabled = false;
            optimizationProgress = new()
            {
                Text = "Optimization",
                Progress = 0,
                ProgressText = ""
            };
            optimizationProgress.StopRunClicked += ProgressForm_StopRunClicked;
            optimizationProgress.Show();
            Task.Run(RunOptimizeExhaustive);
        }
        private void linkLoadGAParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileJson.InitialDirectory = GAFileDefaultFolder;
            if (openFileJson.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    solver = new()
                    {
                        Settings = GeneticAlgorithmFile.Load(openFileJson.FileName)
                    };
                    if (solver.Settings == null)
                        throw new Exception();
                    coreType = solver.Settings.CoreType;
                    eMinChromosome.Text = solver.Settings.MinPopulationSize.ToString();
                    eMaxChromosome.Text = solver.Settings.MaxPopulationSize.ToString();
                    ResetParameters(solver.Settings.ParamValues, solver.Settings.MinValues, solver.Settings.MaxValues);
                    LoadFitnessFunctions();
                    ddGASelection.Text = solver.Settings.SelectionType;
                    ddGACrossOver.Text = solver.Settings.CrossOverType;
                    ddGAMutation.Text = solver.Settings.MutationType;
                    ddGAReinsertion.Text = solver.Settings.ReinsertionType;
                    cbMaxGeneration.Checked = solver.Settings.MaxGeneration != null;
                    eMaxGeneration.Text = solver.Settings.MaxGeneration?.ToString();
                    cbTargetFitness.Checked = solver.Settings.TargetFitness != null;
                    eTargetFitness.Text = solver.Settings.TargetFitness?.ToString();
                    cbCustomTermination.Checked = !string.IsNullOrEmpty(solver.Settings.TerminationType);
                    ddGATermination.Text = solver.Settings.TerminationType;
                    eTerminationParameter.Text = solver.Settings.TerminationParam;
                    ContentChangedArgs args = new()
                    { Caption = $"GA File: {Path.GetFileNameWithoutExtension(openFileJson.FileName)}" };
                    ContentChanged?.Invoke(this, args);
                }
                catch
                {
                    MessageBox.Show("Selected file is not a valid Genetic Algorithm Parameters file.", "Error");
                    return;
                }
                OnLoadParams?.Invoke(this, EventArgs.Empty);
            }
        }

        private void linkSaveGAParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateSolver();
            saveFileJson.InitialDirectory = GAFileDefaultFolder;
            if (saveFileJson.ShowDialog() == DialogResult.OK)
            {
                GeneticAlgorithmFile.Save(saveFileJson.FileName, solver.Settings);
                GAFileDefaultFolder = Path.GetDirectoryName(saveFileJson.FileName);
                ContentChangedArgs args = new()
                { Caption = $"GA File: {Path.GetFileNameWithoutExtension(saveFileJson.FileName)}" };
                ContentChanged?.Invoke(this, args);
            }
        }

        private void btnCalculateFitness_Click(object sender, EventArgs e)
        {
            OnGetParams.Invoke(this, EventArgs.Empty);
            ReadFitnessFunctions();
            CellCore core = CellCore.CreateCore(CoreType, Parameters, DeltaT);
            double fitness = CoreFitness.Evaluate(targetRheobaseFunction, fitnessFunctions, core);
            lOptimizationOutput.Text = $"Snapshot fitness: {fitness}";
        }

        private void cbTargetFitness_CheckedChanged(object sender, EventArgs e)
        {
            eTargetFitness.Visible = cbTargetFitness.Checked;
        }

        private void cbMaxGeneration_CheckedChanged(object sender, EventArgs e)
        {
            eMaxGeneration.Visible = cbMaxGeneration.Checked;
        }

        private void cbCustomTermination_CheckedChanged(object sender, EventArgs e)
        {
            ddGATermination.Visible = lGATerminationParameter.Visible = eTerminationParameter.Visible =
                cbCustomTermination.Checked;
        }
        public void ResetParameters(Dictionary<string, double> parameters,
        Dictionary<string, double> MinValues = null, Dictionary<string, double> MaxValues = null)
        {
            Parameters = parameters;
            dgMinMaxValues.Rows.Clear();
            if (Parameters == null) return;
            dgMinMaxValues.RowCount = parameters.Count;
            int rowIndex = 0;
            foreach (string key in parameters.Keys)
            {
                dgMinMaxValues[colParameter.Index, rowIndex].Value = key;
                if (MinValues != null && MinValues.TryGetValue(key, out double minValue))
                    dgMinMaxValues[colMinValue.Index, rowIndex].Value = minValue;
                if (MaxValues != null && MaxValues.TryGetValue(key, out double maxValue))
                    dgMinMaxValues[colMaxValue.Index, rowIndex].Value = maxValue;
                rowIndex++;
            }
        }
        public void ResetParameters(Dictionary<string, double> parameters, double[] MinValues, double[] MaxValues)
        {
            Parameters = parameters;
            dgMinMaxValues.Rows.Clear();
            if (Parameters == null) return;
            dgMinMaxValues.RowCount = parameters.Count;
            int rowIndex = 0;
            foreach (string key in parameters.Keys)
            {
                dgMinMaxValues[colParameter.Index, rowIndex].Value = key;
                dgMinMaxValues[colMinValue.Index, rowIndex].Value = MinValues[rowIndex];
                dgMinMaxValues[colMaxValue.Index, rowIndex].Value = MaxValues[rowIndex];
                rowIndex++;
            }
        }

        private void btnOptimizePrev_Click(object sender, EventArgs e)
        {
            if (exhaustiveBestSolverIterator <= 0) return;
            btnOptimizeNext.Enabled = true;
            Parameters = exhaustiveBestParameters[--exhaustiveBestSolverIterator].Parameters;
            OnCompleteOptimization?.Invoke(this, EventArgs.Empty);
            double fitness = exhaustiveBestParameters[exhaustiveBestSolverIterator].Fitness;
            lOptimizationOutput.Text = $"Solution {exhaustiveBestSolverIterator + 1} fitness: {fitness}";
            if (exhaustiveBestSolverIterator == 0)
                btnOptimizePrev.Enabled = false;
        }

        private void btnOptimizeNext_Click(object sender, EventArgs e)
        {
            if (exhaustiveBestSolverIterator >= exhaustiveBestParameters.Count - 1) return;
            btnOptimizePrev.Enabled = true;
            Parameters = exhaustiveBestParameters[++exhaustiveBestSolverIterator].Parameters;
            OnCompleteOptimization?.Invoke(this, EventArgs.Empty);
            double fitness = exhaustiveBestParameters[exhaustiveBestSolverIterator].Fitness;
            lOptimizationOutput.Text = $"Solution {exhaustiveBestSolverIterator + 1} fitness: {fitness}";
            if (exhaustiveBestSolverIterator == exhaustiveBestParameters.Count - 1)
                btnOptimizeNext.Enabled = false;
        }

        private void linkExportExhOptRes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (exhaustiveBestParameters.Any() == false) return;
            saveFileCSV.InitialDirectory = GAFileDefaultFolder;
            if (saveFileCSV.ShowDialog() == DialogResult.OK)
            {
                using FileStream fs = File.Open(saveFileCSV.FileName, FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                sw.WriteLine(string.Join(",", "Solution", string.Join(",", exhaustiveBestParameters[0].Parameters.Keys), "Fitness", "Rheobase"));
                int iter = 0;
                foreach (var bp in exhaustiveBestParameters)
                {
                    CellCore core = CellCore.CreateCore(CoreType, bp.Parameters, DeltaT);
                    double rheobase = core.CalculateRheoBase(maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity_ms: GlobalSettings.RheobaseInfinity, dt: 0.1);
                    sw.WriteLine(string.Join(",", ++iter, string.Join(",", bp.Parameters.Values), bp.Fitness, rheobase));
                }
                FileUtil.ShowFile(saveFileCSV.FileName);
            }

        }
    }
}
