using SiliFish.Definitions;
using SiliFish.Services.Optimization;

namespace Controls
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
                return new TargetRheobaseFunction() { 
                    Weight = double.Parse(eFitnessWeight.Text.ToString()),
                    TargetRheobaseMin = double.Parse(eMinRheobase.Text.ToString()),
                    TargetRheobaseMax = double.Parse(eMaxRheobase.Text.ToString())
                };

            if (ddFitnessFunction.Text == FitnessFunctionOptions.FiringDelay.ToString())
                return new FiringFitnessFunction()
                {
                    Weight = double.Parse(eFitnessWeight.Text.ToString()),
                    CurrentValueOrRheobaseMultiplier = double.Parse(eCurrentApplied.Text.ToString()),
                    RheobaseBased = ddCurrentSelection.Text =="x Rheobase",
                    FitnessFunctionOptions = FitnessFunctionOptions.FiringDelay,
                    TargetDelay = (FiringDelay)Enum.Parse(typeof(FiringDelay), ddFiringValues.Text)
                };
           
            if (ddFitnessFunction.Text == FitnessFunctionOptions.FiringPattern.ToString())
                return new FiringFitnessFunction()
                {
                    Weight = double.Parse(eFitnessWeight.Text.ToString()),
                    CurrentValueOrRheobaseMultiplier = double.Parse(eCurrentApplied.Text.ToString()),
                    RheobaseBased = ddCurrentSelection.Text == "x Rheobase",
                    FitnessFunctionOptions = FitnessFunctionOptions.FiringPattern,
                    TargetPattern = (FiringPattern)Enum.Parse(typeof(FiringPattern), ddFiringValues.Text)
                };

            if (ddFitnessFunction.Text == FitnessFunctionOptions.FiringRhythm.ToString())
                return new FiringFitnessFunction()
                {
                    Weight = double.Parse(eFitnessWeight.Text.ToString()),
                    CurrentValueOrRheobaseMultiplier = double.Parse(eCurrentApplied.Text.ToString()),
                    RheobaseBased = ddCurrentSelection.Text == "x Rheobase",
                    FitnessFunctionOptions = FitnessFunctionOptions.FiringRhythm,
                    TargetRhythm = (FiringRhythm)Enum.Parse(typeof(FiringRhythm), ddFiringValues.Text)
                };

            return null;
        }

        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            removeClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
