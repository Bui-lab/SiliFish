﻿using SiliFish.DynamicUnits;
using SiliFish.Extensions;
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
        private void RunOptimize()
        {
            if (Solver == null) return;
            SolverBestValues = Solver.Optimize();
            Invoke(CompleteOptimization);
        }

        private void CompleteOptimization()
        {
            timerOptimization.Enabled = false;
            linkOptimize.Enabled = true;
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
            Solver = new((Type)ddGASelection.SelectedItem,
                (Type)ddGACrossOver.SelectedItem,
                (Type)ddGAMutation.SelectedItem,
                (Type)ddGAReinsertion.SelectedItem,
                (Type)ddGATermination.SelectedItem);
            Solver.SetOptimizationSettings(minPopulation, maxPopulation,
                CoreType,
                Parameters,
                targetRheobaseFunction,
                firingFitnessFunctions,
                minValues, maxValues);
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
            linkOptimize.Enabled = true;
        }

        private void linkSuggestMinMax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DynamicUnit core = DynamicUnit.CreateCore(CoreType, Parameters);

            if (core == null) return;

            (Dictionary<string, double> MinValues, Dictionary<string, double> MaxValues) = core.GetSuggestedMinMaxValues();

            dgMinMaxValues.Rows.Clear();
            dgMinMaxValues.RowCount = Parameters.Count;
            int rowIndex = 0;
            foreach (string key in Parameters.Keys)
            {
                dgMinMaxValues[colParameter.Index, rowIndex].Value = key;
                dgMinMaxValues[colMinValue.Index, rowIndex].Value = MinValues[key];
                dgMinMaxValues[colMaxValue.Index, rowIndex].Value = MaxValues[key];
                rowIndex++;
            }
        }
        private void linkOptimize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateSolver();
            timerOptimization.Enabled = true;
            linkOptimize.Enabled = false;
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
    }
}
