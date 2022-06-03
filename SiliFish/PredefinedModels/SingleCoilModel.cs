using System.Collections.Generic;
using System.Drawing;
using SiliFish.DataTypes;
using SiliFish.Extensions;
using SiliFish.ModelUnits;

namespace SiliFish.PredefinedModels
{
    public class SingleCoilModel : PredefinedModel
    {
        protected override string Name { get { return "SingleCoil"; } }

        int nIC = 5, nMN = 10, nV0d = 10, nMuscle = 10;
        CellPool L_IC, R_IC, L_MN, R_MN, L_V0d, R_V0d;
        CellPool L_Muscle, R_Muscle;


        #region Weight parameters
        double IC_IC_gap_weight = 0.001;
        double IC_MN_gap_weight = 0.04;
        double IC_V0d_gap_weight = 0.05;
        double MN_MN_gap_weight = 0.1;
        double V0d_V0d_gap_weight = 0.04;
        double MN_V0d_gap_weight = 0.01;
        double V0d_MN_syn_weight = 2;
        double V0d_IC_syn_weight = 2;
        double MN_Muscle_syn_weight = 0.015;
        #endregion

        #region Upper range parameters for neuron to neuron/muscle cell reach
        double rangeMin = 0.2;//lower range value
        double rangeIC_MN = 10;
        double rangeIC_V0d = 10;
        double rangeMN_MN = 6.5;
        double rangeV0d_V0d = 3.5;
        double rangeMN_V0d = 1.5;
        double rangeV0d_MN = 8;
        double rangeV0d_IC = 20;
        double rangeMN_Muscle = 1;
        #endregion
        public SingleCoilModel()
        {
            taur = 0.5;
            taud = 1;
            vth = -15;
            E_ach = 120;
            E_gly = -45;
            cv = 4;
            stim_value1 = 50;
            CellPool.rangeNoiseMultiplier = GetRangeNoiseMultiplier;
            CellPool.gapWeightNoiseMultiplier = GetGapWeightNoiseMultiplier;
            CellPool.synWeightNoiseMultiplier = GetSynWeightNoiseMultiplier;
        }
        public void SetNumberOfCells(int nic, int nmn, int nv0d, int nmuscle)
        {
            nIC = nic;
            nMN = nmn;
            nV0d = nv0d;
            nMuscle = nmuscle;
        }
        private double GetXNoise()
        {
            return rand.Gauss(1, 0.01, 0.9, 1.1);
        }
        protected override void InitNeurons()
        {
            MembraneDynamics ic_dyn = new() { a = 0.0005, b = 0.5, c = -30, d = 5, vmax = 0, vr = -60, vt = -45, k = 0.05, Cm = 50 };
            L_IC = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "IC", SagittalPlane.Left, 1, Color.Brown);
            R_IC = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "IC", SagittalPlane.Right, 1, Color.Brown);
            Stimulus stim = new(stim_mode, this.tshutoff, -1, stim_value1, stim_value2);

            for (int i = 0; i < nIC; i++)
            {
                L_IC.AddCell(new Neuron("IC", seq: i, ic_dyn, init_v: -65, init_u: 0, new Coordinate(x: 1.0 * GetXNoise(), -1), cv: cv, stim: stim));
                R_IC.AddCell(new Neuron("IC", seq: i, ic_dyn, init_v: -65, init_u: 0, new Coordinate(x: 1.0 * GetXNoise(), 1), cv: cv));
            }
            NeuronPools.Add(L_IC);
            NeuronPools.Add(R_IC);


            MembraneDynamics v0d_dyn = new() { a = 0.5, b = 0.01, c = -50, d = 0.2, vmax = 10, vr = -60, vt = -45, k = 0.05, Cm = 20 };
            L_V0d = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "V0d", SagittalPlane.Left, 1, Color.Green);
            R_V0d = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "V0d", SagittalPlane.Right, 1, Color.Green);
            for (int i = 0; i < nV0d; i++)
            {
                L_V0d.AddCell(new Neuron("V0d", seq: i, v0d_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), -1), cv: cv));
                R_V0d.AddCell(new Neuron("V0d", seq: i, v0d_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), 1), cv: cv));
            }
            NeuronPools.Add(L_V0d);
            NeuronPools.Add(R_V0d);

            L_MN = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "MN", SagittalPlane.Left, 2, Color.Red);
            R_MN = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "MN", SagittalPlane.Right, 2, Color.Red);
            MembraneDynamics mn_dyn = new() { a = 0.5, b = 0.1, c = -50, d = 0.2, vmax = 10, vr = -60, vt = -45, k = 0.05, Cm = 20 };
            for (int i = 0; i < nMN; i++)
            {
                L_MN.AddCell(new Neuron("MN", seq: i, mn_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), -1), cv: cv));
                R_MN.AddCell(new Neuron("MN", seq: i, mn_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), 1), cv: cv));
            }
            NeuronPools.Add(L_MN);
            NeuronPools.Add(R_MN);

            L_Muscle = new CellPool(this, CellType.MuscleCell, BodyLocation.Body,  "Muscle", SagittalPlane.Left, 3, Color.Purple);
            R_Muscle = new CellPool(this, CellType.MuscleCell, BodyLocation.Body,  "Muscle", SagittalPlane.Right, 3, Color.Purple);
            for (int i = 0; i < nMuscle; i++)
            {
                L_Muscle.AddCell(new MuscleCell("Muscle", i, 25.0, 10.0, 0, new Coordinate(x: 5.0 + 1.6 * i, -1)));
                R_Muscle.AddCell(new MuscleCell("Muscle", i, 25.0, 10.0, 0, new Coordinate(x: 5.0 + 1.6 * i, 1)));
            }
            MuscleCellPools.Add(L_Muscle);
            MuscleCellPools.Add(R_Muscle);
        }

        protected override void InitSynapsesAndGapJunctions()
        {
            L_IC.FormKernel(IC_IC_gap_weight);
            R_IC.FormKernel(IC_IC_gap_weight);

            //IC to MN gap junctions
            CellReach cr = new() { AscendingReach = 0, DescendingReach = rangeIC_MN, MinReach = rangeMin, Weight = IC_MN_gap_weight };
            PoolToPoolGapJunction(L_IC, L_MN, cr);
            PoolToPoolGapJunction(R_IC, R_MN, cr);

            //IC to V0d gap junctions
            cr = new() { AscendingReach = 0, DescendingReach = rangeIC_V0d, MinReach = rangeMin, Weight = IC_V0d_gap_weight };
            PoolToPoolGapJunction(L_IC, L_V0d, cr);
            PoolToPoolGapJunction(R_IC, R_V0d, cr);

            //MN to MN gap junctions
            cr = new() { AscendingReach = rangeMN_MN, DescendingReach = rangeMN_MN, MinReach = rangeMin, Weight = MN_MN_gap_weight };
            PoolToPoolGapJunction(L_MN, L_MN, cr);
            PoolToPoolGapJunction(R_MN, R_MN, cr);

            //MN to V0d gap junctions
            cr = new() { AscendingReach = rangeMN_V0d, DescendingReach = rangeMN_V0d, MinReach = -1, Weight = MN_V0d_gap_weight };
            PoolToPoolGapJunction(L_MN, L_V0d, cr);
            PoolToPoolGapJunction(R_MN, R_V0d, cr);


            //V0d to V0d gap junctions
            cr = new() { AscendingReach = rangeV0d_V0d, DescendingReach = rangeV0d_V0d, MinReach = rangeMin, Weight = V0d_V0d_gap_weight };
            PoolToPoolGapJunction(L_V0d, L_V0d, cr);
            PoolToPoolGapJunction(R_V0d, R_V0d, cr);

            //Ipsilateral cholinergic projections from MN to Muscle cells
            SynapseParameters AChSynapse = new() { TauD = taud, TauR = taur, VTh = vth, E_rev = E_ach };
            cr = new() { AscendingReach = rangeMN_Muscle, DescendingReach = rangeMN_Muscle, MinReach = -1, Weight = MN_Muscle_syn_weight, FixedDuration_ms = 10 };
            PoolToPoolChemSynapse(L_MN, L_Muscle, cr, AChSynapse);
            PoolToPoolChemSynapse(R_MN, R_Muscle, cr, AChSynapse);

            TimeLine span = new();
            span.AddTimeRange(this.tshutoff);

            //Contralateral glycinergic projections from V0d to MN 
            SynapseParameters GlySynapse = new() { TauD = taud, TauR = taur, VTh = vth, E_rev = E_gly };
            cr = new() { AscendingReach = rangeV0d_MN, DescendingReach = rangeV0d_MN, MinReach = -1, Weight = V0d_MN_syn_weight };
            PoolToPoolChemSynapse(L_V0d, R_MN, cr, GlySynapse, span);
            PoolToPoolChemSynapse(R_V0d, L_MN, cr, GlySynapse, span);


            //Contralateral glycinergic projections from V0d to IC 
            cr = new() { AscendingReach = rangeV0d_IC, DescendingReach = rangeV0d_IC, MinReach = rangeMin, Weight = V0d_IC_syn_weight };
            PoolToPoolChemSynapse(L_V0d, R_IC, cr, GlySynapse, span);
            PoolToPoolChemSynapse(R_V0d, L_IC, cr, GlySynapse, span);

        }

        public override Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = base.GetParameters();

            paramDict.Add("CellNum.nIC", nIC);
            paramDict.Add("CellNum.nMN", nMN);
            paramDict.Add("CellNum.nV0d", nV0d);
            paramDict.Add("CellNum.nMuscle", nMuscle);

            paramDict.Add("Weight.IC_IC_gap_weight", IC_IC_gap_weight);
            paramDict.Add("Weight.IC_MN_gap_weight", IC_MN_gap_weight);
            paramDict.Add("Weight.IC_V0d_gap_weight", IC_V0d_gap_weight);
            paramDict.Add("Weight.MN_MN_gap_weight", MN_MN_gap_weight);
            paramDict.Add("Weight.MN_V0d_gap_weight", MN_V0d_gap_weight);
            paramDict.Add("Weight.MN_Muscle_syn_weight", MN_Muscle_syn_weight);
            paramDict.Add("Weight.V0d_MN_syn_weight", V0d_MN_syn_weight);
            paramDict.Add("Weight.V0d_IC_syn_weight", V0d_IC_syn_weight);
            paramDict.Add("Weight.V0d_V0d_gap_weight", V0d_V0d_gap_weight);
            paramDict.Add("Range.rangeMin", rangeMin);
            paramDict.Add("Range.rangeIC_MN", rangeIC_MN);
            paramDict.Add("Range.rangeIC_V0d", rangeIC_V0d);
            paramDict.Add("Range.rangeMN_MN", rangeMN_MN);
            paramDict.Add("Range.rangeV0d_V0d", rangeV0d_V0d);
            paramDict.Add("Range.rangeMN_V0d", rangeMN_V0d);
            paramDict.Add("Range.rangeV0d_MN", rangeV0d_MN);
            paramDict.Add("Range.rangeV0d_IC", rangeV0d_IC);
            paramDict.Add("Range.rangeMN_Muscle", rangeMN_Muscle);
            return paramDict;
        }
        public override void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            base.SetParameters(paramExternal);

            nIC = paramExternal.Read("CellNum.nIC", nIC);
            nMN = paramExternal.Read("CellNum.nMN", nMN);
            nV0d = paramExternal.Read("CellNum.nV0d", nV0d);
            nMuscle = paramExternal.Read("CellNum.nMuscle", nMuscle);

            IC_IC_gap_weight = paramExternal.Read("Weight.IC_IC_gap_weight", IC_IC_gap_weight);
            IC_MN_gap_weight = paramExternal.Read("Weight.IC_MN_gap_weight", IC_MN_gap_weight);
            IC_V0d_gap_weight = paramExternal.Read("Weight.IC_V0d_gap_weight", IC_V0d_gap_weight);
            MN_MN_gap_weight = paramExternal.Read("Weight.MN_MN_gap_weight", MN_MN_gap_weight);
            MN_V0d_gap_weight = paramExternal.Read("Weight.MN_V0d_gap_weight", MN_V0d_gap_weight);
            MN_Muscle_syn_weight = paramExternal.Read("Weight.MN_Muscle_syn_weight", MN_Muscle_syn_weight);
            V0d_MN_syn_weight = paramExternal.Read("Weight.V0d_MN_syn_weight", V0d_MN_syn_weight);
            V0d_IC_syn_weight = paramExternal.Read("Weight.V0d_IC_syn_weight", V0d_IC_syn_weight);
            V0d_V0d_gap_weight = paramExternal.Read("Weight.V0d_V0d_gap_weight", V0d_V0d_gap_weight);
            rangeMin = paramExternal.Read("Range.rangeMin", rangeMin);
            rangeIC_MN = paramExternal.Read("Range.rangeIC_MN", rangeIC_MN);
            rangeIC_V0d = paramExternal.Read("Range.rangeIC_V0d", rangeIC_V0d);
            rangeMN_MN = paramExternal.Read("Range.rangeMN_MN", rangeMN_MN);
            rangeMN_V0d = paramExternal.Read("Range.rangeMN_V0d", rangeMN_V0d);
            rangeV0d_MN = paramExternal.Read("Range.rangeV0d_MN", rangeV0d_MN);
            rangeMN_Muscle = paramExternal.Read("Range.rangeMN_Muscle", rangeMN_Muscle);
        }
    }
}