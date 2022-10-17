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
            if (function == FitnessFunctionOptions.TargetRheobase.ToString())
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
                if (function == FitnessFunctionOptions.FiringDelay.ToString())
                    ddFiringValues.DataSource = Enum.GetNames(typeof(FiringDelay));
                else if (function == FitnessFunctionOptions.FiringRhythm.ToString())
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
                    TargetRheobaseMin = double.Parse(eMinRheobase.Text.ToString()),
                    TargetRheobaseMax = double.Parse(eMaxRheobase.Text.ToString())
                };

            FiringFitnessFunction fff = new()
            {
                Weight = double.Parse(eFitnessWeight.Text.ToString()),
                CurrentValueOrRheobaseMultiplier = double.Parse(eCurrentApplied.Text.ToString()),
                RheobaseBased = ddCurrentSelection.Text == "x Rheobase",
            };
            if (ddFitnessFunction.Text == FitnessFunctionOptions.FiringDelay.ToString())
            {
                fff.FitnessFunctionOptions = FitnessFunctionOptions.FiringDelay;
                fff.TargetDelay = (FiringDelay)Enum.Parse(typeof(FiringDelay), ddFiringValues.Text);
            }

            else if (ddFitnessFunction.Text == FitnessFunctionOptions.FiringPattern.ToString())
            {
                fff.FitnessFunctionOptions = FitnessFunctionOptions.FiringPattern;
                fff.TargetPattern = (FiringPattern)Enum.Parse(typeof(FiringPattern), ddFiringValues.Text);
            }

            else if (ddFitnessFunction.Text == FitnessFunctionOptions.FiringRhythm.ToString())
            {
                fff.FitnessFunctionOptions = FitnessFunctionOptions.FiringRhythm;
                fff.TargetRhythm = (FiringRhythm)Enum.Parse(typeof(FiringRhythm), ddFiringValues.Text);
            }

            return fff;
        }
        public void SetFitnessFunction(FitnessFunction fitnessFunction)
        {
            if (fitnessFunction is TargetRheobaseFunction trf)
            {
                ddFitnessFunction.Text = FitnessFunctionOptions.TargetRheobase.ToString();
                eFitnessWeight.Text = trf.Weight.ToString();
                eMinRheobase.Text = trf.TargetRheobaseMin.ToString();
                eMaxRheobase.Text = trf.TargetRheobaseMax.ToString();
                return;
            }
            if (fitnessFunction is FiringFitnessFunction fff)
            {
                eFitnessWeight.Text = fff.Weight.ToString();
                eCurrentApplied.Text = fff.CurrentValueOrRheobaseMultiplier.ToString();
                ddCurrentSelection.Text = fff.RheobaseBased ? "x Rheobase" : "Fixed Current";
                ddFitnessFunction.Text = fff.FitnessFunctionOptions.ToString();
                switch (fff.FitnessFunctionOptions)
                {
                    case FitnessFunctionOptions.FiringDelay:
                        ddFiringValues.Text = fff.TargetDelay.ToString();
                        break;
                    case FitnessFunctionOptions.FiringRhythm:
                        ddFiringValues.Text = fff.TargetRhythm.ToString();
                        break;
                    case FitnessFunctionOptions.FiringPattern:
                        ddFiringValues.Text = fff.TargetPattern.ToString();
                        break;
                }
            }
        }

        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            removeClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
