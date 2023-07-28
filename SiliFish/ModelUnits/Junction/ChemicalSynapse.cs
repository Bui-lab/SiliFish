using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Helpers;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Drawing;
using System.Collections.Generic;
using SiliFish.Services;
using System.Xml.Linq;
using OfficeOpenXml.ExternalReferences;
using OfficeOpenXml.Core.Worksheet.Fill;
using System.Reflection;
using SiliFish.DynamicUnits.JncCore;
using System.Text;

namespace SiliFish.ModelUnits.Junction
{
    public class ChemicalSynapse : JunctionBase
    {
        private int duration; //The number of time units (dt) it will take for the current to travel from one neuron to the other
                              //calculated at InitForSimulation, and used only during simulation

        

        protected override int nMax => PreNeuron.V.Length;


        public Neuron PreNeuron;
        public Cell PostCell; //can be a neuron or a muscle cell

        [JsonIgnore]
        public override string ID { get { return $"{PreNeuron.ID} → {PostCell.ID}; Conductance: {Weight:0.#####}"; } }

        public override List<string> ExportValues()
        {
            return ListBuilder.Build<string>(
            ConnectionType.Synapse, Core.SynapseType,
            csvExportParamValues,

            PreNeuron.ID, PostCell.ID,
                DistanceMode,
                Weight, FixedDuration_ms, Delay_ms,
                Active,
                TimeLine_ms?.ExportValues());
        }

        public override void ImportValues(List<string> values)
        {
            try
            {
                int iter = 1;//junction type is already read before junction creation
                string coreType = values[iter++].Trim();

                if (values.Count < ColumnNames.Count - TimeLine.ColumnNames.Count) return;
                Dictionary<string, double> parameters = new();
                for (int i = 1; i <= SynapseCore.CoreParamMaxCount; i++)
                {
                    if (iter > values.Count - 2) break;
                    string paramkey = values[iter++].Trim();
                    if (double.TryParse(values[iter++].Trim(), out double paramvalue) && !string.IsNullOrEmpty(paramkey))
                    {
                        parameters.Add(paramkey, paramvalue);
                    }
                }
                Core = SynapseCore.CreateCore(coreType, parameters, Weight, 0.1, 0.1);//TODO constant values here - FIX!!!!

                Source = values[iter++].Trim();
                Target = values[iter++].Trim();

                DistanceMode = (DistanceMode)Enum.Parse(typeof(DistanceMode), values[iter++]);
                Weight = double.Parse(values[iter++]);

                if (double.TryParse(values[iter++], out double d))
                    FixedDuration_ms = d;
                if (double.TryParse(values[iter++], out double dd))
                    Delay_ms = dd;
                Active = bool.Parse(values[iter++]);
                if (iter < values.Count)
                    TimeLine_ms.ImportValues(new[] { values[iter++] }.ToList());
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public ChemicalSynapse()
        { }
        public ChemicalSynapse(Neuron preN, Cell postN, string coreType, Dictionary<string, double> synParams, double conductance, DistanceMode distmode)
        {
            Core = SynapseCore.CreateCore(coreType, synParams, conductance,  preN.Model.RunParam.DeltaT, preN.Model.RunParam.DeltaTEuler);

            PreNeuron = preN;
            PostCell = postN;
            Source = preN.ID;
            Target = postN.ID;
            DistanceMode = distmode;
            Weight = conductance;
        }


        public ChemicalSynapse(ChemicalSynapse syn): base(syn)
        {
            Core = SynapseCore.CreateCore(syn.Core);
            Core = new SimpleSyn(syn.Core as SimpleSyn);
            PreNeuron = syn.PreNeuron;
            PostCell = syn.PostCell;
        }

        internal bool IsActive(int timepoint)
        {
            double t_ms = PreNeuron.Model.RunParam.GetTimeOfIndex(timepoint);
            return TimeLine_ms?.IsActive(t_ms) ?? true;
        }


        public override bool CheckValues(ref List<string> errors)
        {
            base.CheckValues(ref errors);
            Core.CheckValues(ref errors);
            return errors.Count == 0;
        }
        public override void LinkObjects()
        {
            PreNeuron.Terminals.Add(this);
            if (PostCell is MuscleCell mc)
                mc.EndPlates.Add(this);
            else if (PostCell is Neuron n)
                n.Synapses.Add(this);
        }
        public override void UnlinkObjects()
        {
            PreNeuron.Terminals.Remove(this);
            if (PostCell is MuscleCell mc)
                mc.EndPlates.Remove(this);
            else if (PostCell is Neuron n)
                n.Synapses.Remove(this);
        }

        public override void LinkObjects(RunningModel model)
        {
            PreNeuron ??= model.GetCell(Source) as Neuron;
            Source ??= PreNeuron.ID;
            if (!PreNeuron.Terminals.Contains(this))
                PreNeuron.Terminals.Add(this);
            
            PostCell = model.GetCell(Target);
            if (PostCell is Neuron n)
                n.Synapses.Add(this);
            else if (PostCell is MuscleCell m)
                m.EndPlates.Add(this);
            
            Core.DeltaTEuler = model.RunParam.DeltaTEuler;
            Core.DeltaT = model.RunParam.DeltaT;
        }
        public override string ToString()
        {
            string activeStatus = Active && TimeLine_ms.IsBlank() ? "" : Active ? " (timeline)" : " (inactive)";
            return $"{ID}{activeStatus}";
        }
        public override void InitForSimulation(int nmax, bool trackCurrent)
        {
            base.InitForSimulation(nmax, trackCurrent);
            Core.InitForSimulation(Weight);
            
            RunningModel model = PreNeuron.Model;
            if (FixedDuration_ms != null)
                duration = (int)(FixedDuration_ms / model.RunParam.DeltaT);
            else
            {
                double distance = Util.Distance(PreNeuron.Coordinate, PostCell.Coordinate, DistanceMode);
                int delay = (int)((Delay_ms ?? model.Settings.synaptic_delay) / model.RunParam.DeltaT);
                duration = Math.Max((int)Math.Round(distance / (PreNeuron.ConductionVelocity * model.RunParam.DeltaT)), 1);
                duration += delay;
            }
        }

        public void SetFixedDuration(double dur)
        {
            FixedDuration_ms = dur;
        }
        public void SetDelay(double? delay)
        {
            Delay_ms = delay;
        }
        public void SetTimeLine(TimeLine span)
        {
            TimeLine_ms = span;
        }
        public override void NextStep(int tIndex)
        {
            if (!IsActive(tIndex))
            {
                if (inputCurrent != null)
                    inputCurrent[tIndex] = 0;
                return;
            }
            RunningModel model = PreNeuron.Model;

            int tt = duration;
            double vPreSynapse = tt <= tIndex ? PreNeuron.V[tIndex - tt] : PreNeuron.RestingMembranePotential;
            double vPost = tIndex > 0 ? PostCell.V[tIndex - 1] : PostCell.RestingMembranePotential;
            double t_t0 = PreNeuron.LastSpike >= 0 ? (tIndex - PreNeuron.LastSpike) * model.RunParam.DeltaT : 0;//Time since the last spike
            double ISyn = Core.GetNextVal(vPreSynapse, vPost, t_t0);
            if (inputCurrent != null)
                inputCurrent[tIndex] = ISyn;
        }

    }

}
