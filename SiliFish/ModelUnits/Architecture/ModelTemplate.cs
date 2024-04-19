using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Architecture
{
    public class ModelTemplate : ModelBase
    {
        [JsonPropertyOrder(1)]
        public List<CellPoolTemplate> CellPoolTemplates { get; set; } = [];
        [JsonPropertyOrder(1)]
        public List<InterPoolTemplate> InterPoolTemplates { get; set; } = [];
        [JsonPropertyOrder(1)]
        public List<StimulusTemplate> StimulusTemplates { get; set; } = [];

        public ModelTemplate() { }

        public override List<Difference> DiffersFrom(ModelBase other)
        {
            List<Difference> differences = base.DiffersFrom(other) ?? [];
            if (other is ModelTemplate om)
            {
                List<Difference> diffs = ModelUnitBase.ListDiffersFrom(CellPoolTemplates.Select(c => c as ModelUnitBase).ToList(),
                    om.CellPoolTemplates.Select(c => c as ModelUnitBase).ToList());
                if (diffs != null)
                    differences.AddRange(diffs);

                diffs = ModelUnitBase.ListDiffersFrom(InterPoolTemplates.Select(c => c as ModelUnitBase).ToList(),
                    om.InterPoolTemplates.Select(c => c as ModelUnitBase).ToList());
                if (diffs != null)
                    differences.AddRange(diffs);

                diffs = ModelUnitBase.ListDiffersFrom(StimulusTemplates.Select(c => c as ModelUnitBase).ToList(),
                    om.StimulusTemplates.Select(c => c as ModelUnitBase).ToList());
                if (diffs != null)
                    differences.AddRange(diffs);
            }
            else
            {
                //TODO error handling differences.Add("Models not comparable.");
            }
            if (differences.Count != 0)
                return differences;
            return null;
        }
        public void BackwardCompatibility_Stimulus()
        {
            try
            {
                if (StimulusTemplates == null) return;
                foreach (StimulusTemplate stim in StimulusTemplates.Where(s => s.Settings.Mode == StimulusMode.Sinusoidal))
                {
                    if (stim.Settings.Frequency == null || stim.Settings.Frequency == 0)
                    { 
                        stim.Settings.Frequency = stim.Settings.Value2;
                        stim.Settings.Value2 = 0;
                    } 
                }
                foreach (StimulusTemplate stim in StimulusTemplates.Where(s => s.Settings.Mode == StimulusMode.Pulse))
                {
                    if (stim.Settings.Frequency == null || stim.Settings.Frequency == 0)
                    {
                        stim.Settings.Frequency = stim.Settings.Value2;
                        stim.Settings.Value2 = 1;//set to 1 by default
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        #region CellPools
        public override List<CellPoolTemplate> GetCellPools()
        {
            return CellPoolTemplates;
        }
        public override bool AddCellPool(CellPoolTemplate cellPool)
        {
            if (cellPool is CellPoolTemplate cp)
            {
                CellPoolTemplates.Add(cp);
                return true;
            }
            return false;
        }
        public override bool RemoveCellPool(CellPoolTemplate cellPool)
        {
            InterPoolTemplates.RemoveAll(ipt => ipt.SourcePool == cellPool.CellGroup);
            InterPoolTemplates.RemoveAll(ipt => ipt.TargetPool == cellPool.CellGroup);
            StimulusTemplates.RemoveAll(s => s.TargetPool == cellPool.CellGroup);
            return CellPoolTemplates.Remove(cellPool);
        }
        public override bool UpdateCellPool(CellPoolTemplate cellPoolNew)
        {
            CellPoolTemplate cellPoolOld = CellPoolTemplates.FirstOrDefault(cp=>cp.CellGroup == cellPoolNew.CellGroup);
            CellPoolTemplates.Remove(cellPoolOld);
            CellPoolTemplates.Add(cellPoolNew);
            return true;
        }


        #endregion

        #region Projections

        public bool HasConnections()
        {
            return InterPoolTemplates.Count != 0;
        }

        public override List<InterPoolBase> GetChemicalProjections()
        {
            return InterPoolTemplates
                .Where(ip => ip.ConnectionType is ConnectionType.Synapse or ConnectionType.NMJ)
                .Select(ip => (InterPoolBase)ip).ToList();
        }

        public override List<InterPoolBase> GetGapProjections()
        {
            return InterPoolTemplates
                .Where(ip => ip.ConnectionType is ConnectionType.Gap)
                .Select(ip => (InterPoolBase)ip).ToList();
        }
        public override bool AddJunction(InterPoolBase jnc) 
        {
            if (jnc is InterPoolTemplate ipt && !InterPoolTemplates.Contains(ipt))
            { 
                InterPoolTemplates.Add(ipt);
                return true;
            }
            return false;
        }
        public override bool RemoveJunction(InterPoolBase jnc) 
        {
            if (jnc is InterPoolTemplate ipt && InterPoolTemplates.Contains(ipt))
            {
                InterPoolTemplates.Remove(ipt);
                return true;
            }
            return false;
        }
        public override void SortJunctions() 
        {
            InterPoolTemplates.Sort();
        }
        public override void SortJunctionsByType() 
        {
            InterPoolTemplates = [.. InterPoolTemplates.OrderBy(jnc => jnc.ConnectionType.ToString())];
        }
        public override void SortJunctionsBySource()
        {
            InterPoolTemplates = [.. InterPoolTemplates.OrderBy(jnc => jnc.SourcePool)];
        }

        public override void SortJunctionsByTarget()
        {
            InterPoolTemplates = [.. InterPoolTemplates.OrderBy(jnc => jnc.TargetPool)];
        }

        public void RemoveJunctions(bool gap, bool chem)
        {
            if (gap && chem)
            {
                InterPoolTemplates.Clear();
                return;
            }
            else if (gap)
                InterPoolTemplates.RemoveAll(jnc => jnc.ConnectionType == ConnectionType.Gap);
            else if (chem)
                InterPoolTemplates.RemoveAll(jnc => jnc.ConnectionType != ConnectionType.Gap);
        }
        public void RemoveJunctionsOf(CellPoolTemplate cpt, bool gap, bool chemin, bool chemout)
        {
            if (cpt == null)
            {
                RemoveJunctions(gap, chemin || chemout);
                return;
            }
            if (gap)
                InterPoolTemplates.RemoveAll(jnc => jnc.ConnectionType == ConnectionType.Gap &&
                    (jnc.TargetPool == cpt.CellGroup || jnc.SourcePool == cpt.CellGroup));
            if (chemin)
                InterPoolTemplates.RemoveAll(jnc => jnc.ConnectionType != ConnectionType.Gap &&
                    jnc.TargetPool == cpt.CellGroup);
            if (chemout)
                InterPoolTemplates.RemoveAll(jnc => jnc.ConnectionType != ConnectionType.Gap &&
                    jnc.SourcePool == cpt.CellGroup);
        }
        #endregion

        #region Stimulus
        public bool HasStimulus()
        {
            return StimulusTemplates.Count != 0;
        }
        public override List<StimulusBase> GetStimuli()
        {
            return StimulusTemplates.Select(stim => (StimulusBase)stim).ToList();
        }

        public override void AddStimulus(StimulusBase stim)
        {
            StimulusTemplates.Add(stim as StimulusTemplate);
        }

        public override void RemoveStimulus(StimulusBase stim)
        {
            StimulusTemplates.Remove(stim as StimulusTemplate);
        }

        public override void UpdateStimulus(StimulusBase stim, StimulusBase stim2)
        {
            int ind = StimulusTemplates.IndexOf(stim as StimulusTemplate);
            if (ind != -1)
                StimulusTemplates[ind] = stim2 as StimulusTemplate;
            else
                AddStimulus(stim2);
        }

        public void ClearStimuli()
        {
            StimulusTemplates.Clear();
        }

        public void ClearStimuli(string cellPool)
        {
            StimulusTemplates.RemoveAll(stim => stim.TargetPool == cellPool);
        }
        #endregion
        public void ClearLists()
        {
            CellPoolTemplates.Clear();
            InterPoolTemplates.Clear();
            Parameters?.Clear();
            StimulusTemplates.Clear();
        }

        //Needs to be run after created from JSON
        public override void LinkObjects()
        {
            foreach (InterPoolTemplate jnc in InterPoolTemplates)
            {
                LinkObjects(jnc);
            }
        }
        public void LinkObjects(InterPoolTemplate jnc)
        {
            jnc.linkedSource = CellPoolTemplates.FirstOrDefault(t => t.CellGroup == jnc.SourcePool);
            jnc.linkedTarget = CellPoolTemplates.FirstOrDefault(t => t.CellGroup == jnc.TargetPool);
        }

        public override bool CheckValues(ref List<string> errors)
        {
            errors ??= [];
            int preCount = errors.Count;
            base.CheckValues(ref errors);
            var v = CellPoolTemplates.GroupBy(p => p.ToString()).Where(c => c.Count() > 1);
            foreach (string cpt in v.Select(gr => gr.Key).Distinct())
                errors.Add($"Cell pool names have to be unique: {cpt}");
            var v2 = InterPoolTemplates.GroupBy(p => p.ID).Where(c => c.Count() > 1);
            foreach (string ipt in v2.Select(gr => gr.Key).Distinct())
                errors.Add($"Gap junction and synapse names have to be unique: {ipt}");
            foreach (CellPoolTemplate cpt in CellPoolTemplates)
                cpt.CheckValues(ref errors);
            foreach (InterPoolTemplate ipt in InterPoolTemplates)
                ipt.CheckValues(ref errors);
            return errors.Count == preCount;
        }
        public override void CopyConnectionsOfCellPool(CellPoolTemplate poolSource, CellPoolTemplate poolCopyTo)
        {
            List<InterPoolTemplate> iptNewList = [];
            foreach (InterPoolTemplate ipt in InterPoolTemplates.Where(t => t.SourcePool == poolSource.CellGroup))
            {
                InterPoolTemplate iptCopy = new(ipt)
                {
                    SourcePool = poolCopyTo.CellGroup
                };
                iptNewList.Add(iptCopy);
            }
            foreach (InterPoolTemplate ipt in InterPoolTemplates.Where(t => t.TargetPool == poolSource.CellGroup))
            {
                InterPoolTemplate iptCopy = new(ipt)
                {
                    TargetPool = poolCopyTo.CellGroup
                };
                iptNewList.Add(iptCopy);
            }
            InterPoolTemplates.AddRange(iptNewList);
        }
        public bool RenameCellPool(string oldName, string newName)
        {
            if (oldName == null || newName == null || oldName == newName)
                return true;
            if (CellPoolTemplates.Any(p => p.CellGroup == oldName))
                return false;
            foreach (InterPoolTemplate ip in InterPoolTemplates.Where(ip => ip.SourcePool == oldName))
                ip.SourcePool = newName;
            foreach (InterPoolTemplate ip in InterPoolTemplates.Where(ip => ip.TargetPool == oldName))
                ip.TargetPool = newName;
            foreach (StimulusTemplate stim in StimulusTemplates.Where(s => s.TargetPool == oldName))
                stim.TargetPool = newName;
            return true;
        }

        public override void SortCellPools()
        {
            CellPoolTemplates.Sort();
        }
        public override void BackwardCompatibility()
        {
            base.BackwardCompatibility();
            bool checkAxonLength = string.Compare(Version, "2.4.0") < 0;//axon length added to stimulus is added on 2.4.0
            foreach (CellPoolTemplate cpt in CellPoolTemplates)
            {
                cpt.BackwardCompatibility();
                if (checkAxonLength)
                    cpt.BackwardCompatibilityAxonLength(this);
            }
            foreach (InterPoolTemplate ip in InterPoolTemplates)
            {
                ip.BackwardCompatibility();
            }
            if (string.Compare(Version, "2.2.4") < 0)//frequency to stimulus is added on 2.2.4
            {
                BackwardCompatibility_Stimulus();
            }
        }

    }

}
