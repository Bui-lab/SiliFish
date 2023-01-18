using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.ModelUnits.Architecture
{
    public class ModelTemplate : ModelBase
    {
        public List<CellPoolTemplate> CellPoolTemplates { get; set; } = new();
        public List<InterPoolTemplate> InterPoolTemplates { get; set; } = new();
        public List<StimulusTemplate> AppliedStimuli { get; set; } = new();

        public ModelTemplate() { }

        public override List<CellPoolTemplate> GetCellPools()
        {
            return CellPoolTemplates.Select(cp => (CellPoolTemplate)cp).ToList();
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
            InterPoolTemplates.RemoveAll(ipt => ipt.PoolSource == cellPool.CellGroup);
            InterPoolTemplates.RemoveAll(ipt => ipt.PoolTarget == cellPool.CellGroup);
            AppliedStimuli.RemoveAll(s => s.TargetPool == cellPool.CellGroup);
            return CellPoolTemplates.Remove(cellPool);
        }

        public override List<object> GetProjections()
        {
            return InterPoolTemplates.Select(ip => (object)ip).ToList();
        }


        public override bool AddJunction(JunctionBase jnc) 
        {
            if (jnc is InterPoolTemplate ipt && !InterPoolTemplates.Contains(ipt))
            { 
                InterPoolTemplates.Add(ipt);
                return true;
            }
            return false;
        }
        public override bool RemoveJunction(JunctionBase jnc) 
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
            InterPoolTemplates = InterPoolTemplates.OrderBy(jnc => jnc.ConnectionType.ToString()).ToList();
        }
        public override void SortJunctionsBySource()
        {
            InterPoolTemplates = InterPoolTemplates.OrderBy(jnc => jnc.PoolSource).ToList();
        }

        public override void SortJunctionsByTarget()
        {
            InterPoolTemplates = InterPoolTemplates.OrderBy(jnc => jnc.PoolTarget).ToList();
        }

        public override List<StimulusBase> GetStimuli()
        {
            return AppliedStimuli.Select(stim => (StimulusBase)stim).ToList();
        }

        public override void AddStimulus(StimulusBase stim)
        {
            AppliedStimuli.Add(stim as StimulusTemplate);
        }

        public override void RemoveStimulus(StimulusBase stim)
        {
            AppliedStimuli.Remove(stim as StimulusTemplate);
        }

        public void ClearLists()
        {
            CellPoolTemplates.Clear();
            InterPoolTemplates.Clear();
            Parameters?.Clear();
            AppliedStimuli.Clear();
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
            jnc.linkedSource = CellPoolTemplates.FirstOrDefault(t => t.CellGroup == jnc.PoolSource);
            jnc.linkedTarget = CellPoolTemplates.FirstOrDefault(t => t.CellGroup == jnc.PoolTarget);
        }
        public string CheckTemplate()
        {
            if (CellPoolTemplates.GroupBy(p => p.ToString()).Any(c => c.Count() > 1))
                return "Cell pool names have to be unique";
            if (InterPoolTemplates.GroupBy(p => p.ToString()).Any(c => c.Count() > 1))
                return "Gap junction and synapse names have to be unique";
            return "";
        }
        public override void CopyConnectionsOfCellPool(CellPoolTemplate poolSource, CellPoolTemplate poolCopyTo)
        {
            List<InterPoolTemplate> iptNewList = new();
            foreach (InterPoolTemplate ipt in InterPoolTemplates.Where(t => t.PoolSource == poolSource.CellGroup))
            {
                InterPoolTemplate iptCopy = new(ipt)
                {
                    PoolSource = poolCopyTo.CellGroup
                };
                iptNewList.Add(iptCopy);
            }
            foreach (InterPoolTemplate ipt in InterPoolTemplates.Where(t => t.PoolTarget == poolSource.CellGroup))
            {
                InterPoolTemplate iptCopy = new(ipt)
                {
                    PoolTarget = poolCopyTo.CellGroup
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
            foreach (InterPoolTemplate ip in InterPoolTemplates.Where(ip => ip.PoolSource == oldName))
                ip.PoolSource = newName;
            foreach (InterPoolTemplate ip in InterPoolTemplates.Where(ip => ip.PoolTarget == oldName))
                ip.PoolTarget = newName;
            foreach (StimulusTemplate stim in AppliedStimuli.Where(s => s.TargetPool == oldName))
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


            foreach (CellPoolTemplate cpt in CellPoolTemplates)
            {
                cpt.ConductionVelocity ??= new Constant_NoDistribution(CurrentSettings.Settings.cv);
            }
        }

    }

}
