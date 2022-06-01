using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SiliFish.Extensions;
using SiliFish.ModelUnits;

namespace SiliFish.UI.Controls
{
    public partial class InterPoolControl : UserControl
    {
        private event EventHandler interPoolChanged;
        public event EventHandler InterPoolChanged { add => interPoolChanged += value; remove => interPoolChanged -= value; }

        private bool autoGenerateName = true;
        private InterPoolTemplate interPoolTemplate;
        private Dictionary<string, object> Parameters;
        public override string ToString()
        {
            string activeStatus = !cbActive.Checked ? " (inactive)" :
                !timeLineControl.GetTimeLine().IsBlank() ? " (timeline)" :
                "";
            return String.Format("{0}-->{1} [{2}]{3}", ddSourcePool.Text, ddTargetPool.Text, ddJunctionType.Text, activeStatus);
        }
        public InterPoolControl()
        {
            InitializeComponent();
            ddAxonReachMode.DataSource = Enum.GetNames(typeof(AxonReachMode));
            ddDistanceMode.DataSource = Enum.GetNames(typeof(DistanceMode));
            //ddJunctionType is manually loaded as not all of them are displayed
            ddJunctionType.Items.Clear();
            ddJunctionType.Items.Add(JunctionType.Gap);
            ddJunctionType.Items.Add(JunctionType.Synapse);
            ddJunctionType.Items.Add(JunctionType.NMJ);
        }

        private void FillJunctionTypes()
        {
            if (ddSourcePool.SelectedItem is CellPoolTemplate source
                && ddTargetPool.SelectedItem is CellPoolTemplate target)
            {
                if (source.CellType == CellType.MuscleCell)
                {
                    ddJunctionType.Items.Clear();
                    ddJunctionType.Items.Add(JunctionType.Gap);
                    ddJunctionType.SelectedItem = JunctionType.Gap;
                }
                else //Neuron
                {
                    JunctionType prevType = JunctionType.NotSet;
                    if (ddJunctionType.SelectedItem != null)
                        prevType = (JunctionType)ddJunctionType.SelectedItem;
                    ddJunctionType.Items.Clear();
                    ddJunctionType.Items.Add(JunctionType.Gap);
                    if (target.CellType == CellType.MuscleCell)
                        ddJunctionType.Items.Add(JunctionType.NMJ);
                    else
                        ddJunctionType.Items.Add(JunctionType.Synapse);
                    if (ddJunctionType.Items.Contains(prevType))
                        ddJunctionType.SelectedItem = prevType;
                }
            }
        }
        private void UpdateName()
        {
            if (interPoolTemplate == null) return;
            if (autoGenerateName)
                eName.Text = interPoolTemplate.GeneratedName();
        }
        private void ddSourcePool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillJunctionTypes();
            if (!ddSourcePool.Focused)
                return;
            if (Parameters != null && Parameters.Any() && ddSourcePool.SelectedItem is CellPoolTemplate pool)
            {
                switch (pool.NTMode)
                {
                    case NeuronClass.Glycinergic:
                        numEReversal.Value = Parameters.Read("Dynamic.E_gly", numEReversal.Value);
                        break;
                    case NeuronClass.GABAergic:
                        numEReversal.Value = Parameters.Read("Dynamic.E_gaba", numEReversal.Value);
                        break;
                    case NeuronClass.Glutamatergic:
                        numEReversal.Value = Parameters.Read("Dynamic.E_glu", numEReversal.Value);
                        break;
                    case NeuronClass.Cholinergic:
                        numEReversal.Value = Parameters.Read("Dynamic.E_ach", numEReversal.Value);
                        break;
                    default:
                        break;
                }
            }
            interPoolTemplate.PoolSource = ddSourcePool.Text;
            UpdateName();
            interPoolChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddTargetPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillJunctionTypes();
            interPoolTemplate.PoolTarget = ddTargetPool.Text;
            UpdateName();
            interPoolChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddJunctionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gSynapse.Visible = ddJunctionType.Text != "Gap";
            if (interPoolTemplate == null || !ddJunctionType.Focused) return;
            interPoolTemplate.JunctionType = (JunctionType)Enum.Parse(typeof(JunctionType), ddJunctionType.Text);
            UpdateName();
            interPoolChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ddAxonReachMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateName();
            interPoolChanged?.Invoke(this, EventArgs.Empty);
        }
        public void SetInterPoolTemplate(List<CellPoolTemplate> pools, InterPoolTemplate interPool, SwimmingModelTemplate modelTemplate)
        {
            Parameters = modelTemplate.Parameters;
            ddSourcePool.Items.Clear();
            ddTargetPool.Items.Clear();

            if (pools == null || pools.Count == 0)
                return;
            ddSourcePool.Items.AddRange(pools.ToArray());
            ddTargetPool.Items.AddRange(pools.ToArray());
            if (interPool != null)
            {
                interPoolTemplate = interPool;
                JunctionType jnc = interPool.JunctionType;
                ddSourcePool.Text = interPool.PoolSource;
                ddTargetPool.Text = interPool.PoolTarget;
                ddAxonReachMode.Text = interPool.AxonReachMode.ToString();
                interPool.JunctionType = jnc; //target pool change can initialize the junc type list and update incoming info
                ddJunctionType.Text = interPool.JunctionType.ToString();
                ddDistanceMode.Text = interPool.DistanceMode.ToString();

                string name = interPool.GeneratedName();
                autoGenerateName = name == interPool.Name;
                eName.Text = interPool.Name;
                eDescription.Text = interPool.Description;
                numMinReach.Value = (decimal)interPool.CellReach.MinReach;
                numMaxReach.Value = (decimal)interPool.CellReach.MaxReach;
                numAscReach.Value = (decimal)interPool.CellReach.AscendingReach;
                numDescReach.Value = (decimal)interPool.CellReach.DescendingReach;
                numConductance.Value = (decimal)interPool.CellReach.Weight;
                numDelay.Value = (decimal)interPool.CellReach.Delay_ms;
                eFixedDuration.Text = interPool.CellReach.FixedDuration_ms?.ToString() ?? "";
                if (interPool.SynapseParameters != null)
                {
                    numTauD.Value = (decimal)interPool.SynapseParameters.TauD;
                    numTauR.Value = (decimal)interPool.SynapseParameters.TauR;
                    numVthreshold.Value = (decimal)interPool.SynapseParameters.VTh;
                    numEReversal.Value = (decimal)interPool.SynapseParameters.E_rev;
                }
                cbActive.Checked = interPool.JncActive;
                timeLineControl.SetTimeLine(interPool.TimeLine);
            }
            else
                interPoolTemplate = new();
        }

        public InterPoolTemplate GetInterPoolTemplate()
        {
            interPoolTemplate.PoolSource = ddSourcePool.Text;
            interPoolTemplate.PoolTarget= ddTargetPool.Text;
            
            double? fixedDuration = null;
            if (!string.IsNullOrEmpty(eFixedDuration.Text) && double.TryParse(eFixedDuration.Text, out double fd))
                fixedDuration = fd;
            interPoolTemplate.CellReach = new CellReach
            {
                MinReach = (double)numMinReach.Value,
                MaxReach = (double)numMaxReach.Value,
                AscendingReach = (double)numAscReach.Value,
                DescendingReach = (double)numDescReach.Value,
                Weight = (double)numConductance.Value,
                Delay_ms = (double)numDelay.Value,
                FixedDuration_ms = fixedDuration
            };

            interPoolTemplate.AxonReachMode = (AxonReachMode)Enum.Parse(typeof(AxonReachMode), ddAxonReachMode.Text);
            interPoolTemplate.JunctionType = ddJunctionType.SelectedItem != null ? (JunctionType)ddJunctionType.SelectedItem : JunctionType.NotSet;
            interPoolTemplate.DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), ddDistanceMode.Text);
            interPoolTemplate.Name = eName.Text;
            interPoolTemplate.Description = eDescription.Text;

            if (interPoolTemplate.JunctionType != JunctionType.Gap)
            {
                interPoolTemplate.SynapseParameters = new SynapseParameters
                {
                    TauD = (double)numTauD.Value,
                    TauR = (double)numTauR.Value,
                    VTh = (double)numVthreshold.Value,
                    E_rev = (double)numEReversal.Value,
                };
            }
            interPoolTemplate.JncActive = cbActive.Checked;
            interPoolTemplate.TimeLine = timeLineControl.GetTimeLine();
            return interPoolTemplate;
        }

        private void eName_Leave(object sender, EventArgs e)
        {
            autoGenerateName = string.IsNullOrEmpty(eName.Text) || eName.Text == interPoolTemplate.GeneratedName();
        }

        private void cbActive_CheckedChanged(object sender, EventArgs e)
        {
            interPoolChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
