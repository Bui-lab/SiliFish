using SiliFish.Definitions;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.ModelUnits.Model
{
    public class SwimmingModelTemplate
    {
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }

        public ModelDimensions ModelDimensions { get; set; } = new();

        public Settings Settings { get; set; } = new();

        public List<CellPoolTemplate> CellPoolTemplates { get; set; } = new();
        public List<InterPoolTemplate> InterPoolTemplates { get; set; } = new();
        public Dictionary<string, object> Parameters { get; set; }
        public List<StimulusTemplate> AppliedStimuli { get; set; } = new();

        public void ClearLists()
        {
            CellPoolTemplates.Clear();
            InterPoolTemplates.Clear();
            Parameters?.Clear();
            AppliedStimuli.Clear();
        }
        //Needs to be run after created from JSON
        public void LinkObjects()
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
        public void CopyConnectionsOfCellPool(CellPoolTemplate poolSource, CellPoolTemplate poolTarget)
        {
            List<InterPoolTemplate> iptNewList = new();
            foreach (InterPoolTemplate ipt in InterPoolTemplates.Where(t => t.PoolSource == poolSource.CellGroup))
            {
                InterPoolTemplate iptCopy = new(ipt);
                iptCopy.PoolSource = poolTarget.CellGroup;
                iptNewList.Add(iptCopy);
            }
            foreach (InterPoolTemplate ipt in InterPoolTemplates.Where(t => t.PoolTarget == poolSource.CellGroup))
            {
                InterPoolTemplate iptCopy = new(ipt);
                iptCopy.PoolTarget = poolTarget.CellGroup;
                iptNewList.Add(iptCopy);
            }
            InterPoolTemplates.AddRange(iptNewList);
        }
        public void RenameCellPool(string oldName, string newName)
        {
            if (oldName == null || newName == null || oldName == newName)
                return;
            if (CellPoolTemplates.Any(p => p.CellGroup == oldName))
                return;
            foreach (InterPoolTemplate ip in InterPoolTemplates.Where(ip => ip.PoolSource == oldName))
                ip.PoolSource = newName;
            foreach (InterPoolTemplate ip in InterPoolTemplates.Where(ip => ip.PoolTarget == oldName))
                ip.PoolTarget = newName;
            foreach (StimulusTemplate stim in AppliedStimuli.Where(s => s.TargetPool == oldName))
                stim.TargetPool = newName;
        }

        public void RemoveCellPool(CellPoolTemplate cpt)
        {
            InterPoolTemplates.RemoveAll(ipt => ipt.PoolSource == cpt.CellGroup);
            InterPoolTemplates.RemoveAll(ipt => ipt.PoolTarget == cpt.CellGroup);
            AppliedStimuli.RemoveAll(s => s.TargetPool == cpt.CellGroup);
            CellPoolTemplates.Remove(cpt);
        }

        public void BackwardCompatibility()
        {
            if (Parameters == null || !Parameters.Any())
                return;
            if (Parameters.ContainsKey("General.Name"))
            {
                ModelName = Parameters["General.Name"].ToString();
                Parameters.Remove("General.Name");
            }
            if (Parameters.ContainsKey("General.Description"))
            {
                ModelDescription = Parameters["General.Description"].ToString();
                Parameters.Remove("General.Description");
            }
            Parameters = ModelDimensions.BackwardCompatibility(Parameters);
        }

    }

}
