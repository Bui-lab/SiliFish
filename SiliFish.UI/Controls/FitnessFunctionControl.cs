using SiliFish.Definitions;
using SiliFish.Services.Optimization;

namespace SiliFish.UI.Controls
{
    public partial class FitnessFunctionControl : UserControl
    {
        private event EventHandler removeClicked;
        public event EventHandler RemoveClicked
        {
            add
            {
                removeClicked += value;
                linkRemove.Click += value;
            }
            remove
            {
                removeClicked -= value;
                linkRemove.Click -= value;
            }
        }

        public FitnessFunctionControl()
        {
            InitializeComponent();
            ddFitnessFunction.DataSource = Enum.GetNames(typeof(FitnessFunctionOptions));
        }

        private void ddFitnessFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string function = ddFitnessFunction.Text;
            if (function == FitnessFunctionOptions.TargetRheobase.ToString())// || function == FitnessFunctionOptions.FiringDelay.ToString())
            {
                pTargetRheobase.Visible = true;
                pFiringRelated.Visible = false;
                this.Height = pFitnessFunction.Height + pTargetRheobase.Height + 35;
            }
            else
            {
                pTargetRheobase.Visible = false;
                pFiringRelated.Visible = true;
                this.Height = pFitnessFunction.Height + pFiringRelated.Height + 35;
                lFiringValues.Text = function;
                if (function == FitnessFunctionOptions.FiringRhythm.ToString())
                    ddFiringValues.DataSource = Enum.GetNames(typeof(FiringRhythm));
                else if (function == FitnessFunctionOptions.FiringPattern.ToString())
                    ddFiringValues.DataSource = Enum.GetNames(typeof(FiringPattern));
            }
        }

        public FitnessFunction GetFitnessFunction()
        {
            if (ddFitnessFunction.Text == FitnessFunctionOptions.TargetRheobase.ToString())
                return new TargetRheobaseFunction()
                {
                    Weight = double.Parse(eFitnessWeight.Text.ToString()),
                    TargetRheobaseMin = double.Parse(eMinValue.Text.ToString()),
                    TargetRheobaseMax = double.Parse(eMaxValue.Text.ToString())
                };
            /*TODO if (ddFitnessFunction.Text == FitnessFunctionOptions.FiringDelay.ToString())
                 return new FiringDelayFunction()
                 {
                     Weight = double.Parse(eFitnessWeight.Text.ToString()),
                     FiringDelayMin = double.Parse(eMinValue.Text.ToString()),
                     FiringDelayMax = double.Parse(eMaxValue.Text.ToString())
                 };*/

            if (ddFitnessFunction.Text == FitnessFunctionOptions.FiringPattern.ToString())
            {
                FiringPatternFunction fpf = new()
                {
                    Weight = double.Parse(eFitnessWeight.Text.ToString()),
                    CurrentValueOrRheobaseMultiplier = double.Parse(eCurrentApplied.Text.ToString()),
                    RheobaseBased = ddCurrentSelection.Text == "x Rheobase",
                    TargetPattern = (FiringPattern)Enum.Parse(typeof(FiringPattern), ddFiringValues.Text)
                };
                return fpf;
            }

            if (ddFitnessFunction.Text == FitnessFunctionOptions.FiringRhythm.ToString())
            {
                FiringRhythmFunction fpf = new()
                {
                    Weight = double.Parse(eFitnessWeight.Text.ToString()),
                    CurrentValueOrRheobaseMultiplier = double.Parse(eCurrentApplied.Text.ToString()),
                    RheobaseBased = ddCurrentSelection.Text == "x Rheobase",
                    TargetRhythm = (FiringRhythm)Enum.Parse(typeof(FiringRhythm), ddFiringValues.Text)
                };
                return fpf;
            }

            return null;
        }
        public void SetFitnessFunction(FitnessFunction fitnessFunction)
        {
            if (fitnessFunction is TargetRheobaseFunction trf)
            {
                ddFitnessFunction.Text = FitnessFunctionOptions.TargetRheobase.ToString();
                eFitnessWeight.Text = trf.Weight.ToString();
                eMinValue.Text = trf.TargetRheobaseMin.ToString();
                eMaxValue.Text = trf.TargetRheobaseMax.ToString();
                return;
            }
            /*TODO if (fitnessFunction is FiringDelayFunction fdf)
            {
                ddFitnessFunction.Text = FitnessFunctionOptions.FiringDelay.ToString();
                eFitnessWeight.Text = fdf.Weight.ToString();
                eMinValue.Text = fdf.FiringDelayMin.ToString();
                eMaxValue.Text = fdf.FiringDelayMax.ToString();
                return;
            }*/
            if (fitnessFunction is FiringFitnessFunction fff)
            {
                ddFitnessFunction.Text = fff.FitnessFunctionOption.ToString();
                eFitnessWeight.Text = fff.Weight.ToString();
                eCurrentApplied.Text = fff.CurrentValueOrRheobaseMultiplier.ToString();
                ddCurrentSelection.Text = fff.RheobaseBased ? "x Rheobase" : "Fixed Current";
                if (fff is FiringRhythmFunction frf)
                    ddFiringValues.Text = frf.TargetRhythm.ToString();
                else if (fff is FiringPatternFunction fpf)
                    ddFiringValues.Text = fpf.TargetPattern.ToString();
            }
        }

        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            removeClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
