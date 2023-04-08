using OxyPlot;
using SiliFish.Definitions;
using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Stim;
using SiliFish.UI.EventArguments;
using SiliFish.UI.Extensions;
using System.Data;
using Windows.Devices.HumanInterfaceDevice;

namespace SiliFish.UI.Controls
{
    public partial class StimulusControl : UserControl
    {
        private Stimulus Stimulus;

        public override string ToString()
        {
            string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return $"{Stimulus}{activeStatus}";
        }
        public StimulusControl()
        {
            InitializeComponent();
        }

        public void SetStimulus(Stimulus stim)
        {
            Stimulus = stim;
            if (stim != null)
            {
                eTargetCell.Text = stim.TargetCell.ID;
                stimControl.SetStimulusSettings(stim.Settings);
                cbActive.Checked = stim.Active;

                if (stim.TimeLine_ms != null)
                    timeLineControl.SetTimeLine(stim.TimeLine_ms);
            }
        }
        public Stimulus GetStimulus()
        {
            Stimulus = new()
            {
                TargetCell = Stimulus.TargetCell,
                Settings = stimControl.GetStimulusSettings(),
                TimeLine_ms = timeLineControl.GetTimeLine(),
                Active = cbActive.Checked,
            };
            return Stimulus;
        }

        internal void CheckValues(object sender, EventArgs args)
        {
            CheckValuesArgs checkValuesArgs = args as CheckValuesArgs;
            checkValuesArgs.Errors = new();
            checkValuesArgs.Errors.AddRange(stimControl.CheckValues());
        }

        internal void SetTargetCell(Cell selectedCell)
        {
            eTargetCell.Text= selectedCell.ID;
        }
    }
}
