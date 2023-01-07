using SiliFish.Definitions;
using System.Linq;

namespace SiliFish.ModelUnits.Model
{
    public class CustomSwimmingModel : SwimmingModel
    {
        public CustomSwimmingModel()
        {
        }
        public CustomSwimmingModel(SwimmingModelTemplate swimmingModelTemplate)
        {
            neuronPools.Clear();
            musclePools.Clear();
            gapPoolConnections.Clear();
            chemPoolConnections.Clear();

            initialized = false;

            if (swimmingModelTemplate == null) return;

            ModelName = swimmingModelTemplate.ModelName;
            ModelDescription = swimmingModelTemplate.ModelDescription;
            ModelDimensions = swimmingModelTemplate.ModelDimensions;
            CurrentSettings.Settings = swimmingModelTemplate.Settings;
            SetParameters(swimmingModelTemplate.Parameters);

            #region Generate pools and cells
            foreach (CellPoolTemplate pool in swimmingModelTemplate.CellPoolTemplates.Where(cp => cp.Active))
            {
                if (pool.PositionLeftRight == SagittalPlane.Both || pool.PositionLeftRight == SagittalPlane.Left)
                {
                    if (pool.CellType == CellType.Neuron)
                        neuronPools.Add(new CellPool(this, pool, SagittalPlane.Left));
                    else if (pool.CellType == CellType.MuscleCell)
                        musclePools.Add(new CellPool(this, pool, SagittalPlane.Left));
                }
                if (pool.PositionLeftRight == SagittalPlane.Both || pool.PositionLeftRight == SagittalPlane.Right)
                {
                    if (pool.CellType == CellType.Neuron)
                        neuronPools.Add(new CellPool(this, pool, SagittalPlane.Right));
                    else if (pool.CellType == CellType.MuscleCell)
                        musclePools.Add(new CellPool(this, pool, SagittalPlane.Right));
                }
            }
            #endregion

            #region Generate Gap Junctions and Chemical Synapses
            foreach (InterPoolTemplate jncTemp in swimmingModelTemplate.InterPoolTemplates.Where(ip => ip.Active))
            {
                CellPool leftSource = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == jncTemp.PoolSource && np.PositionLeftRight == SagittalPlane.Left);
                CellPool rightSource = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == jncTemp.PoolSource && np.PositionLeftRight == SagittalPlane.Right);

                CellPool leftTarget = neuronPools.Union(musclePools).FirstOrDefault(mp => mp.CellGroup == jncTemp.PoolTarget && mp.PositionLeftRight == SagittalPlane.Left);
                CellPool rightTarget = neuronPools.Union(musclePools).FirstOrDefault(mp => mp.CellGroup == jncTemp.PoolTarget && mp.PositionLeftRight == SagittalPlane.Right);

                if (jncTemp.ConnectionType == ConnectionType.Synapse || jncTemp.ConnectionType == ConnectionType.NMJ)
                {
                    if (jncTemp.AxonReachMode == AxonReachMode.Ipsilateral || jncTemp.AxonReachMode == AxonReachMode.Bilateral)
                    {
                        PoolToPoolChemSynapse(leftSource, leftTarget, jncTemp);
                        PoolToPoolChemSynapse(rightSource, rightTarget, jncTemp);
                    }
                    if (jncTemp.AxonReachMode == AxonReachMode.Contralateral || jncTemp.AxonReachMode == AxonReachMode.Bilateral)
                    {
                        PoolToPoolChemSynapse(leftSource, rightTarget, jncTemp);
                        PoolToPoolChemSynapse(rightSource, leftTarget, jncTemp);
                    }
                }
                else if (jncTemp.ConnectionType == ConnectionType.Gap)
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
            if (swimmingModelTemplate.AppliedStimuli?.Count > 0)
            {
                foreach (StimulusTemplate stimulus in swimmingModelTemplate.AppliedStimuli.Where(stim => stim.Active))
                {
                    Stimulus stim = new(stimulus.StimulusSettings, stimulus.TimeLine_ms);
                    if (stimulus.LeftRight.Contains("Left"))
                    {
                        CellPool target = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == stimulus.TargetPool && np.PositionLeftRight == SagittalPlane.Left);
                        target?.ApplyStimulus(stim, stimulus.TargetSomite, stimulus.TargetCell);
                    }
                    if (stimulus.LeftRight.Contains("Right"))
                    {
                        CellPool target = neuronPools.Union(musclePools).FirstOrDefault(np => np.CellGroup == stimulus.TargetPool && np.PositionLeftRight == SagittalPlane.Right);
                        target?.ApplyStimulus(stim, stimulus.TargetSomite, stimulus.TargetCell);
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
            InitDataVectors(nmax);
            if (!neuronPools.Any(p => p.GetCells().Any()) &&
                !musclePools.Any(p => p.GetCells().Any()))
                return;
            initialized = true;
        }
    }
}
