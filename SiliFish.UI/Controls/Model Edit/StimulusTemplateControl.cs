﻿using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Stim;
using SiliFish.UI.EventArguments;

namespace SiliFish.UI.Controls
{
    public partial class StimulusTemplateControl : UserControl
    {
        private StimulusTemplate Stimulus;
        private bool autoGenerateName = true;
        public StimulusTemplateControl()
        {
            InitializeComponent();
            ddSagittalPosition.DataSource = Enum.GetNames(typeof(SagittalPlane));
        }

        public override string ToString()
        {
            string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return GetStimulus().ToString() + activeStatus;
        }

        public void SetStimulus(List<CellPoolTemplate> pools, StimulusTemplate stim)
        {
            ddTargetPool.Items.Clear();

            if (pools == null || pools.Count == 0) return;

            stim ??= new();
            Stimulus = stim;
            string name = stim.GeneratedName();
            autoGenerateName = name == stim.Name;
            eName.Text = stim.Name;
            stimControl.SetStimulusSettings(Stimulus.Settings);
            ddTargetPool.Items.AddRange([.. pools]);
            ddTargetPool.Text = stim.TargetPool;
            if (ddTargetPool.Text == "")
                ddTargetPool.Text = stim.TargetPool + " (inactive)";
            if (string.IsNullOrEmpty(stim.TargetSomite) || stim.TargetSomite.StartsWith("All"))
            {
                cbAllSomites.Checked = true;
            }
            else
            {
                cbAllSomites.Checked = false;
                eTargetSomites.ReadOnly = false;
                eTargetSomites.Text = stim.TargetSomite;
            }
            if (string.IsNullOrEmpty(stim.TargetCell) || stim.TargetCell.StartsWith("All"))
            {
                cbAllCells.Checked = true;
            }
            else
            {
                cbAllCells.Checked = false;
                eTargetCells.ReadOnly = false;
                eTargetCells.Text = stim.TargetCell;
            }
            if (Enum.TryParse(typeof(SagittalPlane), stim.LeftRight, out object sp))
                ddSagittalPosition.Text = sp.ToString();
            else
            {
                SagittalPlane sagittal = stim.LeftRight.GetValueFromName(SagittalPlane.Both);
                ddSagittalPosition.Text = sagittal.ToString();
            }
            eDelayPerSomite.Text = stim.DelayPerSomite.ToString("0.##");
            eDelaySagittal.Text = stim.DelaySagittal.ToString("0.##");
            cbActive.Checked = stim.Active;

            if (stim.TimeLine_ms != null)
                timeLineControl.SetTimeLine(stim.TimeLine_ms);
        }

        public StimulusTemplate GetStimulus()
        {
            SagittalPlane sagPlane = SagittalPlane.Both;
            if (ddSagittalPosition.Text == "Left")
                sagPlane = SagittalPlane.Left;
            else if (ddSagittalPosition.Text == "Right")
                sagPlane = SagittalPlane.Right;
            else if (ddSagittalPosition.Text is "Left/Right" or "Both")
                sagPlane = SagittalPlane.Both;
            double.TryParse(eDelayPerSomite.Text, out double delayPerSomite);
            double.TryParse(eDelaySagittal.Text, out double delaySagittal);

            Stimulus = new StimulusTemplate()
            {
                Name = eName.Text,
                Settings = stimControl.GetStimulusSettings(),
                TargetPool = ddTargetPool.Text,
                TargetSomite = cbAllSomites.Checked ? "All somites" : eTargetSomites.Text,
                TargetCell = cbAllCells.Checked ? "All cells" : eTargetCells.Text,
                LeftRight = sagPlane.ToString(),
                DelayPerSomite = delayPerSomite,
                DelaySagittal = delaySagittal,
                TimeLine_ms = timeLineControl.GetTimeLine(),
                Active = cbActive.Checked
            };
            return Stimulus;
        }

        private void cbAllSomites_CheckedChanged(object sender, EventArgs e)
        {
            eTargetSomites.ReadOnly = cbAllSomites.Checked;
            eTargetSomites.Visible = !cbAllSomites.Checked;
            lRange.Visible = !cbAllSomites.Checked || !cbAllCells.Checked;
        }

        private void cbAllCells_CheckedChanged(object sender, EventArgs e)
        {
            eTargetCells.ReadOnly = cbAllCells.Checked;
            eTargetCells.Visible = !cbAllCells.Checked;
            lRange.Visible = !cbAllSomites.Checked || !cbAllCells.Checked;
        }

        internal void CheckValues(object sender, EventArgs args)
        {
            CheckValuesArgs checkValuesArgs = args as CheckValuesArgs;
            checkValuesArgs.Errors = [];
            if (ddTargetPool.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("No target pool selected.");
            if (ddSagittalPosition.SelectedIndex < 0)
                checkValuesArgs.Errors.Add("Sagittal position not defined.");
            checkValuesArgs.Errors.AddRange(stimControl.CheckValues());
        }

        internal void SetSourcePool(CellPoolTemplate selectedPoolTemplate)
        {
            ddTargetPool.SelectedItem = selectedPoolTemplate;
        }

        private void ddTargetPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateName();
        }
        private void UpdateName()
        {
            if (Stimulus == null) return;
            if (autoGenerateName)
                eName.Text = Stimulus.GeneratedName();
        }

        private void eName_Leave(object sender, EventArgs e)
        {
            autoGenerateName = string.IsNullOrEmpty(eName.Text) || eName.Text == Stimulus?.GeneratedName();
        }
    }
}
