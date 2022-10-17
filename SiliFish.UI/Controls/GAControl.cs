using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.Helpers;
using SiliFish.Services.Optimization;
using System.Data;

namespace SiliFish.UI.Controls
{
    public partial class GAControl : UserControl
    {
        public string CoreType { get; set; }
        public Dictionary<string, double> Parameters { get; set; }
        private List<FitnessFunctionControl> FitnessControls;
        private CoreSolver Solver;
        Dictionary<string, double> SolverBestValues;
        private Dictionary<string, double> minValues;
        private Dictionary<string, double> maxValues;
        private TargetRheobaseFunction targetRheobaseFunction;
        private List<FiringFitnessFunction> firingFitnessFunctions;
        private ProgressForm optimizationProgress;
        private event EventHandler onCompleteOptimization;
        public event EventHandler OnCompleteOptimization { add => onCompleteOptimization += value; remove => onCompleteOptimization -= value; }

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
            FitnessControls = new();
        }

        public void ResetParameters(Dictionary<string, double> parameters)
        {
            Parameters = parameters;
            dgMinMaxValues.Rows.Clear();
            dgMinMaxValues.RowCount = parameters.Count;
            int rowIndex = 0;
            foreach (string key in parameters.Keys)
            {
                dgMinMaxValues[colParameter.Index, rowIndex++].Value = key;
            }
        }

        public void ResetParameters(Dictionary<string, double> parameters, Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues)
        {
            Parameters = parameters;
            dgMinMaxValues.Rows.Clear();
            dgMinMaxValues.RowCount = parameters.Count;
            int rowIndex = 0;
            foreach (string key in parameters.Keys)
            {
                dgMinMaxValues[colParameter.Index, rowIndex].Value = key;
                if (MinValues.TryGetValue(key, out double minValue))
                    dgMinMaxValues[colMinValue.Index, rowIndex].Value = minValue;
                if (MaxValues.TryGetValue(key, out double maxValue))
                    dgMinMaxValues[colMaxValue.Index, rowIndex].Value = maxValue;
                rowIndex++;
            }
        }
        //TODO 3 version of reset parameters - fix
        public void ResetParameters(Dictionary<string, double> parameters, double[] MinValues, double[] MaxValues)
        {
            Parameters = parameters;
            dgMinMaxValues.Rows.Clear();
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
        private void RunOptimize()
        {
            if (Solver == null) return;
            SolverBestValues = Solver.Optimize();
            Invoke(CompleteOptimization);
        }

        private void CompleteOptimization()
        {
            timerOptimization.Enabled = false;
            btnOptimize.Enabled = true;
            Parameters = SolverBestValues;
            if (optimizationProgress != null && optimizationProgress.Visible)
                optimizationProgress.Close();
            optimizationProgress = null;
            onCompleteOptimization?.Invoke(this, EventArgs.Empty);
        }
        private void CreateSolver()
        {
            Solver = null;
            ReadMinMaxParamValues();
            ReadFitnessValues();
            if (!int.TryParse(eMinChromosome.Text, out int minPopulation))
                minPopulation = 50;
            if (!int.TryParse(eMaxChromosome.Text, out int maxPopulation))
                maxPopulation = 50;
            CoreSolverSettings settings = new()
            {
                SelectionType = ddGASelection.Text,
                CrossOverType = ddGACrossOver.Text,
                MutationType = ddGAMutation.Text,
                ReinsertionType = ddGAReinsertion.Text,
                TerminationType = ddGATermination.Text,
                TerminationParam = eTerminationParameter.Text,
                MinPopulationSize = minPopulation,
                MaxPopulationSize = maxPopulation,

                CoreType = CoreType,
                TargetRheobaseFunction = targetRheobaseFunction,
                FitnessFunctions = firingFitnessFunctions,
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

        private void ReadFitnessValues()
        {
            List<FitnessFunction> fitnessFunctions = FitnessControls.Select(fc => fc.GetFitnessFunction()).ToList();
            targetRheobaseFunction = fitnessFunctions.FirstOrDefault(fc => fc is TargetRheobaseFunction) as TargetRheobaseFunction;
            fitnessFunctions.RemoveAll(fc => fc is TargetRheobaseFunction);
            firingFitnessFunctions = fitnessFunctions.Select(fc => fc as FiringFitnessFunction).ToList();
        }

        private void ProgressForm_StopRunClicked(object sender, EventArgs e)
        {
            timerOptimization.Enabled = false;
            Solver?.CancelOptimization();
            btnOptimize.Enabled = true;
        }

        private void linkSuggestMinMax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DynamicUnit core = DynamicUnit.CreateCore(CoreType, Parameters);

            if (core == null) return;

            (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) = core.GetSuggestedMinMaxValues();

            ResetParameters(Parameters, MinValues, MaxValues);
        }
        private void timerOptimization_Tick(object sender, EventArgs e)
        {
            if (Solver == null) return;
            eOptimizationOutput.AppendText(Solver.ProgressText + "\r\n");
            optimizationProgress.Progress = Solver.Progress;
        }

        private void linkAddFitnessFunction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FitnessFunctionControl fitness = new();
            fitness.BorderStyle = BorderStyle.FixedSingle;
            pfGAParams.Controls.Add(fitness);
            pfGAParams.Controls.SetChildIndex(linkAddFitnessFunction, pfGAParams.Controls.Count - 1);
            fitness.RemoveClicked += Fitness_RemoveClicked;
            FitnessControls.Add(fitness);
        }

        private void Fitness_RemoveClicked(object sender, EventArgs e)
        {
            pfGAParams.Controls.Remove(sender as FitnessFunctionControl);
            pfGAParams.Controls.SetChildIndex(linkAddFitnessFunction, pfGAParams.Controls.Count - 1);
            FitnessControls.Remove(sender as FitnessFunctionControl);
        }

        private void ddGATermination_SelectedIndexChanged(object sender, EventArgs e)
        {
            lGATerminationParameter.Text = GeneticAlgorithmExtension.GetTerminationParameter(ddGATermination.Text);
        }

        private void btnOptimize_Click(object sender, EventArgs e)
        {
            CreateSolver();
            timerOptimization.Enabled = true;
            btnOptimize.Enabled = false;
            eOptimizationOutput.Text = "";
            optimizationProgress = new()
            {
                Text = "Optimization",
                Progress = 0
            };
            optimizationProgress.StopRunClicked += ProgressForm_StopRunClicked;
            optimizationProgress.Show();
            Task.Run(RunOptimize);
        }

        private void linkLoadGAParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileJson.ShowDialog() == DialogResult.OK)
            {
                string JSONString = Util.ReadFromFile(openFileJson.FileName);
                if (string.IsNullOrEmpty(JSONString))
                    return;
                Solver = new();
                Solver.Settings = (CoreSolverSettings)Util.CreateObjectFromJSON(typeof(CoreSolverSettings), JSONString);
                if (Solver != null)
                {
                    eMinChromosome.Text = Solver.Settings.MinPopulationSize.ToString();
                    eMaxChromosome.Text = Solver.Settings.MaxPopulationSize.ToString();
                    ResetParameters(Solver.Settings.ParamValues, Solver.Settings.MinValues, Solver.Settings.MaxValues);

/*                    ReadFitnessValues();
                    if (!int.TryParse(eMinChromosome.Text, out int minPopulation))
                        minPopulation = 50;
                    if (!int.TryParse(eMaxChromosome.Text, out int maxPopulation))
                        maxPopulation = 50;
                    Solver = new((Type)ddGASelection.SelectedItem,
                        (Type)ddGACrossOver.SelectedItem,
                        (Type)ddGAMutation.SelectedItem,
                        (Type)ddGAReinsertion.SelectedItem,
                        (Type)ddGATermination.SelectedItem,
                        eTerminationParameter.Text);
                    Solver.SetOptimizationSettings(minPopulation,
                        maxPopulation,
                        CoreType,
                        Parameters,
                        targetRheobaseFunction,
                        firingFitnessFunctions,
                        minValues, maxValues);*/
                }
            }

        }

        private void linkSaveGAParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateSolver();
            string JSONString = Util.CreateJSONFromObject(Solver.Settings);
            if (saveFileJson.ShowDialog() == DialogResult.OK)
            {
                Util.SaveToFile(saveFileJson.FileName, JSONString);
            }
        }
    }
}
