using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliFish.Extensions;
using SiliFish.ModelUnits;

namespace SiliFish
{

    public class SwimmingModelTemplate
    {
        public List<CellPoolTemplate> Pools { get; set; } = new();
        public List<InterPoolTemplate> InterPools { get; set; } = new();
        public Dictionary<string, object> Parameters { get; set; }
        public List<AppliedStimulus> Stimuli { get; set; } = new();

        public string CheckTemplate()
        {
            if (Pools.GroupBy(p => p.ToString()).Any(c => c.Count() > 1))
                return "Cell pool names have to be unique";
            return "";
        }
        public void RenameCellPool(string oldName, string newName)
        {
            if (oldName == null || newName == null || oldName == newName)
                return;
            if (Pools.Any(p => p.CellGroup == oldName))
                return;
            foreach (InterPoolTemplate ip in InterPools.Where(ip => ip.PoolSource == oldName))
                ip.PoolSource = newName;
            foreach (InterPoolTemplate ip in InterPools.Where(ip => ip.PoolTarget == oldName))
                ip.PoolTarget = newName;
        }

        public string ModelName
        {
            get
            {
                return (string)Parameters?.Read("General.Name", "") ?? "";
            }
        }
        public string ModelDescription
        {
            get
            {
                return (string)Parameters?.Read("General.Description", "") ?? "";
            }
        }

    }
    public class CustomSwimmingModel: SwimmingModel
    {
        public CustomSwimmingModel(SwimmingModelTemplate swimmingModelTemplate)
        {
            NeuronPools.Clear();
            MuscleCellPools.Clear();
            PoolConnections.Clear();

            initialized = false;

            if (swimmingModelTemplate == null) return;

            SetParameters(swimmingModelTemplate.Parameters);

            #region Generate pools and cells
            foreach (CellPoolTemplate pool in swimmingModelTemplate.Pools.Where(cp => cp.Active))
            {
                if (pool.PositionLeftRight == SagittalPlane.Both || pool.PositionLeftRight == SagittalPlane.Left)
                {
                    if (pool.CellType == CellType.Neuron)
                        NeuronPools.Add(new CellPool(this, pool, SagittalPlane.Left));
                    else if (pool.CellType == CellType.MuscleCell)
                        MuscleCellPools.Add(new CellPool(this, pool, SagittalPlane.Left));
                }
                if (pool.PositionLeftRight == SagittalPlane.Both || pool.PositionLeftRight == SagittalPlane.Right)
                {
                    if (pool.CellType == CellType.Neuron)
                        NeuronPools.Add(new CellPool(this, pool, SagittalPlane.Right));
                    else if (pool.CellType == CellType.MuscleCell)
                        MuscleCellPools.Add(new CellPool(this, pool, SagittalPlane.Right));
                }
            }
            #endregion

            #region Generate Junctions
            foreach (InterPoolTemplate jncTemp in swimmingModelTemplate.InterPools.Where(ip => ip.Active))
            {
                CellPool leftSource = NeuronPools.FirstOrDefault(np => np.CellGroup == jncTemp.PoolSource && np.PositionLeftRight == SagittalPlane.Left);
                CellPool rightSource = NeuronPools.FirstOrDefault(np => np.CellGroup == jncTemp.PoolSource && np.PositionLeftRight == SagittalPlane.Right);

                CellPool leftTarget = NeuronPools.Union(MuscleCellPools).FirstOrDefault(mp => mp.CellGroup == jncTemp.PoolTarget && mp.PositionLeftRight == SagittalPlane.Left);
                CellPool rightTarget = NeuronPools.Union(MuscleCellPools).FirstOrDefault(mp => mp.CellGroup == jncTemp.PoolTarget && mp.PositionLeftRight == SagittalPlane.Right);

                if (jncTemp.JunctionType == JunctionType.Synapse || jncTemp.JunctionType == JunctionType.NMJ)
                {
                    if (jncTemp.AxonReachMode == AxonReachMode.Ipsilateral || jncTemp.AxonReachMode == AxonReachMode.Bilateral)
                    {
                        PoolToPoolChemJunction(leftSource, leftTarget, jncTemp);
                        PoolToPoolChemJunction(rightSource, rightTarget, jncTemp);
                    }
                    if (jncTemp.AxonReachMode == AxonReachMode.Contralateral || jncTemp.AxonReachMode == AxonReachMode.Bilateral)
                    {
                        PoolToPoolChemJunction(leftSource, rightTarget, jncTemp);
                        PoolToPoolChemJunction(rightSource, leftTarget, jncTemp);
                    }
                }
                else if (jncTemp.JunctionType == JunctionType.Gap)
                {
                    if (jncTemp.AxonReachMode == AxonReachMode.Ipsilateral || jncTemp.AxonReachMode == AxonReachMode.Bilateral)
                    {
                        PoolToPoolGapJunction(leftSource, leftTarget, jncTemp);
                        PoolToPoolGapJunction(rightSource, rightTarget, jncTemp);
                    }
                    if (jncTemp.AxonReachMode == AxonReachMode.Contralateral || jncTemp.AxonReachMode == AxonReachMode.Bilateral)
                    {
                        PoolToPoolGapJunction(leftSource, rightTarget, jncTemp);
                        PoolToPoolGapJunction(rightSource, leftTarget, jncTemp);
                    }
                }
            }
            #endregion

            #region Generate Stimuli
            if (swimmingModelTemplate.Stimuli?.Count > 0)
            {
                foreach (AppliedStimulus stimulus in swimmingModelTemplate.Stimuli)
                {
                    if (stimulus.LeftRight.Contains("Left"))
                    {
                        CellPool target = NeuronPools.Union(MuscleCellPools).FirstOrDefault(np => np.CellGroup == stimulus.Target && np.PositionLeftRight == SagittalPlane.Left);
                        target?.ApplyStimulus(stimulus.Stimulus_ms);
                    }
                    if (stimulus.LeftRight.Contains("Right"))
                    {
                        CellPool target = NeuronPools.Union(MuscleCellPools).FirstOrDefault(np => np.CellGroup == stimulus.Target && np.PositionLeftRight == SagittalPlane.Right);
                        target?.ApplyStimulus(stimulus.Stimulus_ms);
                    }
                }
            }
            #endregion
        }

        protected override void InitNeurons()
        {
            //base.InitNeurons();
        }

        protected override void InitSynapsesAndGapJunctions()
        {
            //base.InitSynapsesAndGapJunctions();
        }
        protected override void InitStructures(int nmax)
        {
            this.Time = new double[nmax];
            InitMembranePotentials(nmax);
            initialized = true;
        }
    }
}
