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
            ddFitnessFunction.DataSource = FitnessFunction.TypeMap.Keys.ToList();
        }

        private void ddFitnessFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            FitnessFunction ff = (FitnessFunction)Activator.CreateInstance(FitnessFunction.TypeMap[ddFitnessFunction.Text]);
            int height = pFitnessFunction.Height + 35;
            pMinMax.Visible = ff.MinMaxExists;
            height += pMinMax.Visible ? pMinMax.Height : 0;
            pCurrentRequired.Visible = ff.CurrentRequired;
            height += pCurrentRequired.Visible ? pCurrentRequired.Height : 0;
            pFiringOption.Visible = ff.ModeExists;
            height += pFiringOption.Visible ? pFiringOption.Height : 0;
            Height = height;
            ddFiringOption.DataSource = ff.GetFiringOptions();
        }

        public FitnessFunction GetFitnessFunction()
        {
            FitnessFunction ff = (FitnessFunction)Activator.CreateInstance(FitnessFunction.TypeMap[ddFitnessFunction.Text]);
            ff.Weight = double.Parse(eFitnessWeight.Text.ToString());
            if (double.TryParse(eMinValue.Text.ToString(), out double d))
                ff.ValueMin = d;
            if (double.TryParse(eMaxValue.Text.ToString(), out d))
                ff.ValueMax = d;
            if (double.TryParse(eCurrentApplied.Text.ToString(), out d))
                ff.CurrentValueOrRheobaseMultiplier = d;
            ff.RheobaseBased = ddCurrentSelection.Text == "x Rheobase";
            if (ff is FiringPatternFunction fpf && !string.IsNullOrEmpty(ddFiringOption.Text))
                fpf.TargetPattern = (FiringPattern)Enum.Parse(typeof(FiringPattern), ddFiringOption.Text);
            else if (ff is FiringRhythmFunction frf && !string.IsNullOrEmpty(ddFiringOption.Text))
                frf.TargetRhythm = (FiringRhythm)Enum.Parse(typeof(FiringRhythm), ddFiringOption.Text);
            return ff;
        }
        public void SetFitnessFunction(FitnessFunction fitnessFunction)
        {
            if (fitnessFunction == null) return;
            ddFitnessFunction.Text = fitnessFunction.GetType().Name;
            eFitnessWeight.Text = fitnessFunction.Weight.ToString();
            eMinValue.Text = fitnessFunction.ValueMin.ToString();
            eMaxValue.Text = fitnessFunction.ValueMax.ToString();
            eCurrentApplied.Text = fitnessFunction.CurrentValueOrRheobaseMultiplier.ToString();
            ddCurrentSelection.Text = fitnessFunction.RheobaseBased ? "x Rheobase" : "Fixed Current";
            if (fitnessFunction is FiringRhythmFunction frf)
                ddFiringOption.Text = frf.TargetRhythm.ToString();
            else if (fitnessFunction is FiringPatternFunction fpf)
                ddFiringOption.Text = fpf.TargetPattern.ToString();
        }

        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            removeClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
