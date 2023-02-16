using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Architecture;
using SiliFish.Repositories;
using SiliFish.Services.Optimization;
using System.Data;

namespace SiliFish.UI.Controls
{
    public partial class GAControl : UserControl
    {
        private static string GAFileDefaultFolder;
        private string coreType;
        private CoreSolver Solver;
        private CoreSolverOutput SolverOutput;
        private Dictionary<string, double> minValues;
        private Dictionary<string, double> maxValues;
        private TargetRheobaseFunction targetRheobaseFunction;
        private List<FitnessFunction> fitnessFunctions;//All fitness functions except the target rheobase
        private ProgressForm optimizationProgress;

        public event EventHandler OnCompleteOptimization;
        public event EventHandler OnLoadParams;
        public event EventHandler OnGetParams;
        public event EventHandler ContentChanged;

        public double DeltaT { get; set; }
        public double DeltaTEuler { get; set; }

        public string CoreType
        {
            get { return coreType; }
            set
            {
                coreType = value;
                if (Solver?.Settings != null)
                    Solver.Settings.CoreType = value;
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
            if (Solver == null) return;
            SolverOutput = Solver.Optimize();
            Invoke(CompleteOptimization);
        }

        private void CompleteOptimization()
        {
            timerOptimization.Enabled = false;
            btnOptimize.Enabled = true;
            if (SolverOutput == null) return;
            Parameters = SolverOutput.BestValues;
            if (optimizationProgress != null && optimizationProgress.Visible)
                optimizationProgress.Close();
            optimizationProgress = null;
            OnCompleteOptimization?.Invoke(this, EventArgs.Empty);
            lOptimizationOutput.Text = $"Latest fitness: {SolverOutput.BestFitness}";
            if (!string.IsNullOrEmpty(SolverOutput.ErrorMessage))
                lOptimizationOutput.Text += $"\r\nOptimization ran with errors: {SolverOutput.ErrorMessage}";
        }

        private void CreateSolver()
        {
            Solver = null;
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
                DeltaTEuler = DeltaTEuler,
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
            Solver = new() { Settings = settings };
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
            List<FitnessFunction> fitnessFunctions =new();
            for (int rowind=0; rowind<dgFitnessFunctions.RowCount; rowind++)
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
            Solver?.CancelOptimization();
            btnOptimize.Enabled = true;
        }

        private void linkSuggestMinMax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CellCoreUnit core = CellCoreUnit.CreateCore(CoreType, Parameters, DeltaT, DeltaTEuler);

            if (core == null) return;

            (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) = core.GetSuggestedMinMaxValues();

            ResetParameters(Parameters, MinValues, MaxValues);
        }
        private void timerOptimization_Tick(object sender, EventArgs e)
        {
            if (Solver == null) return;
            string progress = Solver.ProgressText;
            optimizationProgress.Progress = Solver.Progress;
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
            if (Solver.Settings.TargetRheobaseFunction!=null)
                AddFitnessFunctionRow(Solver.Settings.TargetRheobaseFunction);
            foreach (FitnessFunction fitnessFunction in Solver.Settings.FitnessFunctions)
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
        private void ddGATermination_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolTip.SetToolTip(lGATerminationParameter, GeneticAlgorithmExtension.GetTerminationParameter(ddGATermination.Text));
        }

        private void btnOptimize_Click(object sender, EventArgs e)
        {
            if (!cbTargetFitness.Checked && !cbMaxGeneration.Checked && !cbCustomTermination.Checked)
            {
                MessageBox.Show("Select at least one termination mode.");
                return;
            }
            CreateSolver();
            timerOptimization.Enabled = true;
            btnOptimize.Enabled = false;
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

        private void linkLoadGAParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileJson.InitialDirectory = GAFileDefaultFolder;
            if (openFileJson.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Solver = new()
                    {
                        Settings = GeneticAlgorithmFile.Load(openFileJson.FileName)
                    };

                    coreType = Solver.Settings.CoreType;
                    eMinChromosome.Text = Solver.Settings.MinPopulationSize.ToString();
                    eMaxChromosome.Text = Solver.Settings.MaxPopulationSize.ToString();
                    ResetParameters(Solver.Settings.ParamValues, Solver.Settings.MinValues, Solver.Settings.MaxValues);
                    LoadFitnessFunctions();
                    ddGASelection.Text = Solver.Settings.SelectionType;
                    ddGACrossOver.Text = Solver.Settings.CrossOverType;
                    ddGAMutation.Text = Solver.Settings.MutationType;
                    ddGAReinsertion.Text = Solver.Settings.ReinsertionType;
                    cbMaxGeneration.Checked = Solver.Settings.MaxGeneration != null;
                    eMaxGeneration.Text = Solver.Settings.MaxGeneration?.ToString();
                    cbTargetFitness.Checked = Solver.Settings.TargetFitness != null;
                    eTargetFitness.Text = Solver.Settings.TargetFitness?.ToString();
                    cbCustomTermination.Checked = !string.IsNullOrEmpty(Solver.Settings.TerminationType);
                    ddGATermination.Text = Solver.Settings.TerminationType;
                    eTerminationParameter.Text = Solver.Settings.TerminationParam;
                    ContentChangedArgs args = new()
                    { Caption = $"GA File: {Path.GetFileNameWithoutExtension(openFileJson.FileName)}" };
                    ContentChanged?.Invoke(this, args);
                }
                catch
                {
                    MessageBox.Show("Selected file is not a valid Genetic Algorithm Parameters file.");
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
                GeneticAlgorithmFile.Save(saveFileJson.FileName, Solver.Settings);
                GAFileDefaultFolder = Path.GetDirectoryName(saveFileJson.FileName);
            }
        }

        private void btnCalculateFitness_Click(object sender, EventArgs e)
        {
            OnGetParams.Invoke(this, EventArgs.Empty);
            ReadFitnessFunctions();
            CellCoreUnit core = CellCoreUnit.CreateCore(CoreType, Parameters, DeltaT, DeltaTEuler);
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
    }
}
