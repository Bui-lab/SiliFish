using System.Collections.Generic;
using System.Drawing;
using SiliFish.DataTypes;
using SiliFish.Extensions;
using SiliFish.ModelUnits;

namespace SiliFish.PredefinedModels
{
    public class DoubleCoilModel : PredefinedModel
    {
        int nIC = 5, nMN = 10, nV0d = 10, nV0v = 10, nV2a = 10, nMuscle = 10;
        CellPool L_IC, R_IC, L_MN, R_MN, L_V0d, R_V0d, L_V0v, R_V0v, L_V2a, R_V2a;
        CellPool L_Muscle, R_Muscle;

        protected int tasyncdelay = 0;

        #region Weight parameters
        double IC_IC_gap_weight = 0.0001;
        double IC_MN_gap_weight = 0.03;
        double IC_V0d_gap_weight = 0.05;
        double IC_V0v_gap_weight = 0.00005;
        double IC_V2a_gap_weight = 0.15;
        
        double MN_MN_gap_weight = 0.07;
        double MN_V0d_gap_weight=0.0001 ;
        double MN_V0v_gap_weight= 0.0001;
        
        double V0d_V0d_gap_weight = 0.04;
        
        double V0v_V0v_gap_weight= 0.05;
        
        double V2a_MN_gap_weight = 0.005;
        double V2a_V2a_gap_weight = 0.005;

        double MN_Muscle_syn_weight = 0.02;
        double V0d_MN_syn_weight = 2.0;
        double V0d_IC_syn_weight = 2.0;
        double V0d_V2a_syn_weight = 2.0 ;
        double V0v_IC_syn_weight= 0.175;
        double V2a_V0v_syn_weight= 0.04;
        
        #endregion

        #region Upper range parameters for neuron to neuron/muscle cell reach
        double rangeMin = 0.2; // lower range value
        double rangeIC_MN = 10;
        double rangeIC_V0d = 10;
        double rangeIC_V0v = 10;
        double rangeIC_V2a = 10;
        double rangeMN_MN = 6.5;
        double rangeMN_V0d = 1.5;
        double rangeMN_V0v = 1.5;
        double rangeV0d_IC = 20;
        double rangeV0d_MN = 8;
        double rangeV0d_V0d = 3.5;
        double rangeV0v_V0v = 3.5;
        double rangeV0d_V2a = 8;
        double rangeV0v_IC_min = 6;
        double rangeV0v_IC_max = 20;
        double rangeV2a_MN = 3.5;
        double rangeV2a_V0v_asc = 4;
        double rangeV2a_V0v_desc = 10;
        double rangeV2a_V2a = 3.5;
        double rangeMN_Muscle = 1;
        #endregion

        public DoubleCoilModel()
        {
            taur = 0.5;
            taud = 1;
            vth = -15;
            stim_value1 = 35;
            sigma_gap = 0;
            sigma_chem = 0;
            E_ach = 120;
            E_glu = 0;
            E_gly = -58;
            cv = 1;
            CellPool.rangeNoiseMultiplier = GetRangeNoiseMultiplier;
            CellPool.gapWeightNoiseMultiplier = GetGapWeightNoiseMultiplier;
            CellPool.synWeightNoiseMultiplier = GetSynWeightNoiseMultiplier;

            tasyncdelay = 150;
        }

        public void SetNumberOfCells(int nic, int nmn, int nv0d, int nv0v, int nv2a, int nmuscle)
        {
            nIC = nic;
            nMN = nmn;
            nV0d = nv0d;
            nV0v = nv0v;
            nV2a = nv2a;
            nMuscle = nmuscle;
        }
        private static double GetXNoise()
        {
            return rand.Gauss(1, 0.01, 0.9, 1.1);
        }

        protected override void InitNeurons()
        {
            MembraneDynamics ic_dyn = new() { a = 0.0002, b = 0.5, c = -40, d = 5, vmax = 0, vr = -60, vt = -45, k = 0.3, Cm = 50 };
            L_IC = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "IC", SagittalPlane.Left, 1, Color.Brown);
            R_IC = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "IC", SagittalPlane.Right, 1, Color.Brown);
            TimeLine tlLeft = new();
            tlLeft.AddTimeRange(this.tshutoff);
            Stimulus stimLeft = new(stim_mode, tlLeft, stim_value1, stim_value2);
            TimeLine tlRight= new();
            tlRight.AddTimeRange(this.tasyncdelay);
            Stimulus stimRight = new(stim_mode, tlRight, stim_value1, stim_value2);

            for (int i = 0; i < nIC; i++)
            {
                L_IC.AddCell(new Neuron("IC", seq: i, ic_dyn, init_v: -65, init_u: 0, new Coordinate(x: 1.0 * GetXNoise(), -1), cv: cv, stim: stimLeft));
                R_IC.AddCell(new Neuron("IC", seq: i, ic_dyn, init_v: -65, init_u: 0, new Coordinate(x: 1.0 * GetXNoise(), 1), cv: cv, stim: stimRight));
            }
            NeuronPools.Add(L_IC);
            NeuronPools.Add(R_IC);

            MembraneDynamics mn_dyn = new() { a = 0.5, b = 0.1, c = -50, d = 100, vmax = 10, vr = -60, vt = -50, k = 0.05, Cm = 20 };
            L_MN = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "MN", SagittalPlane.Left, 4, Color.Red);
            R_MN = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "MN", SagittalPlane.Right, 4, Color.Red);
            for (int i = 0; i < nMN; i++)
            {
                L_MN.AddCell(new Neuron("MN", seq: i, mn_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), -1), cv: cv));
                R_MN.AddCell(new Neuron("MN", seq: i, mn_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), 1), cv: cv));
            }
            NeuronPools.Add(L_MN);
            NeuronPools.Add(R_MN);

            MembraneDynamics v0d_dyn = new() { a = 0.02, b = 0.1, c = -30, d = 3.75, vmax = 10, vr = -60, vt = -45, k = 0.05, Cm = 20 };
            L_V0d = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "V0d", SagittalPlane.Left, 1, Color.Green);
            R_V0d = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "V0d", SagittalPlane.Right, 1, Color.Green);
            for (int i = 0; i < nV0d; i++)
            {
                L_V0d.AddCell(new Neuron("V0d", seq: i, v0d_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), -1), cv: cv));
                R_V0d.AddCell(new Neuron("V0d", seq: i, v0d_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), 1), cv: cv));
            }
            NeuronPools.Add(L_V0d);
            NeuronPools.Add(R_V0d);

            MembraneDynamics v0v_dyn = new() { a = 0.02, b = 0.1, c = -30, d = 11.6, vmax = 10, vr = -60, vt = -45, k = 0.05, Cm = 20 };
            L_V0v = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "V0v", SagittalPlane.Left, 2, Color.Blue);
            R_V0v = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "V0v", SagittalPlane.Right, 2, Color.Blue);
            for (int i = 0; i < nV0v; i++)
            {
                L_V0v.AddCell(new Neuron("V0v", seq: i, v0v_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), -1), cv: cv));
                R_V0v.AddCell(new Neuron("V0v", seq: i, v0v_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), 1), cv: cv));
            }
            NeuronPools.Add(L_V0v);
            NeuronPools.Add(R_V0v);

            MembraneDynamics v2a_dyn = new() { a = 0.5, b = 0.1, c = -50, d = 100, vmax = 10, vr = -60, vt = -45, k = 0.05, Cm = 20 };
            L_V2a = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "V2a", SagittalPlane.Left, 3, Color.RebeccaPurple);
            R_V2a = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord,  "V2a", SagittalPlane.Right, 3, Color.RebeccaPurple);
            for (int i = 0; i < nV2a; i++)
            {
                L_V2a.AddCell(new Neuron("V2a", seq: i, v2a_dyn, init_v: -64, init_u: -16, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), -1), cv: cv));
                R_V2a.AddCell(new Neuron("V2a", seq: i, v2a_dyn, init_v: -64, init_u: -16, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), 1), cv: cv));
            }
            NeuronPools.Add(L_V2a);
            NeuronPools.Add(R_V2a);

            L_Muscle = new CellPool(this, CellType.MuscleCell, BodyLocation.Body,  "Muscle", SagittalPlane.Left, 5, Color.Purple);
            R_Muscle = new CellPool(this, CellType.MuscleCell, BodyLocation.Body,  "Muscle", SagittalPlane.Right, 5, Color.Purple);
            for (int i = 0; i < nMuscle; i++)
            {
                L_Muscle.AddCell(new MuscleCell("Muscle", i, R: 50, C: 5.0, init_v: 0, new Coordinate(x: 5.0 + 1.6 * i, -1)));
                R_Muscle.AddCell(new MuscleCell("Muscle", i, R: 50, C: 5.0, init_v: 0, new Coordinate(x: 5.0 + 1.6 * i, 1)));
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

            //IC to V0v gap junctions
            cr = new() { AscendingReach = 0, DescendingReach = rangeIC_V0v, MinReach = rangeMin, Weight = IC_V0v_gap_weight };
            PoolToPoolGapJunction(L_IC, L_V0v, cr);
            PoolToPoolGapJunction(R_IC, R_V0v, cr);

            //IC to V2a gap junctions
            cr = new() { AscendingReach = 0, DescendingReach = rangeIC_V2a, MinReach = rangeMin, Weight = IC_V2a_gap_weight };
            PoolToPoolGapJunction(L_IC, L_V2a, cr);
            PoolToPoolGapJunction(R_IC, R_V2a, cr);

            //MN to MN gap junctions
            cr = new() { AscendingReach = rangeMN_MN, DescendingReach = rangeMN_MN, MinReach = rangeMin, Weight = MN_MN_gap_weight };
            PoolToPoolGapJunction(L_MN, L_MN, cr);
            PoolToPoolGapJunction(R_MN, R_MN, cr);

            //MN to V0d gap junctions
            cr = new() { AscendingReach = rangeMN_V0d, DescendingReach = rangeMN_V0d, MinReach = -1, Weight = MN_V0d_gap_weight };
            PoolToPoolGapJunction(L_MN, L_V0d, cr);
            PoolToPoolGapJunction(R_MN, R_V0d, cr);

            //MN to V0v gap junctions
            cr = new() { AscendingReach = rangeMN_V0v, DescendingReach = rangeMN_V0v, MinReach = -1, Weight = MN_V0v_gap_weight };
            PoolToPoolGapJunction(L_MN, L_V0v, cr);
            PoolToPoolGapJunction(R_MN, R_V0v, cr);

            //V0d to V0d gap junctions
            cr = new() { AscendingReach = rangeV0d_V0d, DescendingReach = rangeV0d_V0d, MinReach = rangeMin, Weight = V0d_V0d_gap_weight };
            PoolToPoolGapJunction(L_V0d, L_V0d, cr);
            PoolToPoolGapJunction(R_V0d, R_V0d, cr);

            //V0v to V0v gap junctions
            cr = new() { AscendingReach = rangeV0v_V0v, DescendingReach = rangeV0v_V0v, MinReach = rangeMin, Weight = V0v_V0v_gap_weight };
            PoolToPoolGapJunction(L_V0v, L_V0v, cr);
            PoolToPoolGapJunction(R_V0v, R_V0v, cr);

            //V2a to V2a gap junctions
            cr = new() { AscendingReach = rangeV2a_V2a, DescendingReach = rangeV2a_V2a, MinReach = rangeMin, Weight = V2a_V2a_gap_weight };
            PoolToPoolGapJunction(L_V2a, L_V2a, cr);
            PoolToPoolGapJunction(R_V2a, R_V2a, cr);

            //V2a to MN gap junctions
            cr = new() { AscendingReach = rangeV2a_MN, DescendingReach = rangeV2a_MN, MinReach = rangeMin, Weight = V2a_MN_gap_weight };
            PoolToPoolGapJunction(L_V2a, L_MN, cr);
            PoolToPoolGapJunction(R_V2a, R_MN, cr);


            TimeLine span = new();
            span.AddTimeRange(this.tshutoff);

            //Contralateral glycinergic projections from V0d to MN 
            SynapseParameters GlySynapse = new() { TauD = taud, TauR = taur, VTh = vth, E_rev = E_gly };
            cr = new() { AscendingReach = rangeV0d_MN, DescendingReach = rangeV0d_MN, MinReach = rangeMin, Weight = V0d_MN_syn_weight };
            PoolToPoolChemSynapse(L_V0d ,R_MN, cr, GlySynapse, span);
            PoolToPoolChemSynapse(R_V0d ,L_MN, cr, GlySynapse, span);

            //Contralateral glycinergic projections from V0d to IC 
            cr = new() { AscendingReach = rangeV0d_IC, DescendingReach = rangeV0d_IC, MinReach = rangeMin, Weight = V0d_IC_syn_weight };
            PoolToPoolChemSynapse(L_V0d ,R_IC, cr, GlySynapse, span);
            PoolToPoolChemSynapse(R_V0d ,L_IC, cr, GlySynapse, span);

            //Contralateral glycinergic projections from V0d to V2a 
            cr = new() { AscendingReach = rangeV0d_V2a, DescendingReach = rangeV0d_V2a, MinReach = rangeMin, Weight = V0d_V2a_syn_weight };
            PoolToPoolChemSynapse(L_V0d ,R_V2a, cr, GlySynapse, span);
            PoolToPoolChemSynapse(R_V0d ,L_V2a, cr, GlySynapse, span);

            //Contralateral glutamatergic projections from V0v to IC 
            SynapseParameters GluSynapse = new() { TauD = taud, TauR = taur, VTh = vth, E_rev = E_glu };
            cr = new() { AscendingReach = rangeV0v_IC_max, DescendingReach = rangeV0v_IC_max, MinReach = rangeV0v_IC_min, Weight = V0v_IC_syn_weight };
            PoolToPoolChemSynapse(L_V0v ,R_IC, cr, GluSynapse, span);
            PoolToPoolChemSynapse(R_V0v ,L_IC, cr, GluSynapse, span);

            //Pisilateral glutamatergic projections from V2a to V0v 
            cr = new() { AscendingReach = rangeV2a_V0v_asc, DescendingReach = rangeV2a_V0v_desc, MinReach = rangeMin, Weight = V2a_V0v_syn_weight };
            PoolToPoolChemSynapse(L_V2a,L_V0v, cr, GluSynapse, span);
            PoolToPoolChemSynapse(R_V2a,R_V0v, cr, GluSynapse, span);

            //Ipsilateral cholinergic projections from MN to Muscle cells
            SynapseParameters AChSynapse = new() { TauD = taud, TauR = taur, VTh = vth, E_rev = E_ach };
            cr = new() { AscendingReach = rangeMN_Muscle, DescendingReach = rangeMN_Muscle, MinReach = -1, Weight = MN_Muscle_syn_weight, FixedDuration_ms = 10 };
            PoolToPoolChemSynapse(L_MN,L_Muscle, cr, AChSynapse);
            PoolToPoolChemSynapse(R_MN,R_Muscle, cr, AChSynapse);
        }

        public override Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = base.GetParameters();

            paramDict.Add("Dynamic.tasyncdelay", tasyncdelay);

            paramDict.Add("CellNum.nIC", nIC);
            paramDict.Add("CellNum.nMN", nMN);
            paramDict.Add("CellNum.nV0d", nV0d);
            paramDict.Add("CellNum.nV0v", nV0v);
            paramDict.Add("CellNum.nV2a", nV2a);
            paramDict.Add("CellNum.nMuscle", nMuscle);

            paramDict.Add("Weight.IC_IC_gap_weight",IC_IC_gap_weight);
            paramDict.Add("Weight.IC_MN_gap_weight",IC_MN_gap_weight);
            paramDict.Add("Weight.IC_V0d_gap_weight", IC_V0d_gap_weight);
            paramDict.Add("Weight.IC_V0v_gap_weight", IC_V0v_gap_weight);
            paramDict.Add("Weight.IC_V2a_gap_weight", IC_V2a_gap_weight);
            paramDict.Add("Weight.MN_MN_gap_weight",MN_MN_gap_weight);
            paramDict.Add("Weight.MN_V0d_gap_weight",MN_V0d_gap_weight);
            paramDict.Add("Weight.MN_V0v_gap_weight", MN_V0v_gap_weight);
            paramDict.Add("Weight.V0d_V0d_gap_weight", V0d_V0d_gap_weight);
            paramDict.Add("Weight.V0v_V0v_gap_weight", V0v_V0v_gap_weight);
            paramDict.Add("Weight.V2a_MN_gap_weight", V2a_MN_gap_weight);
            paramDict.Add("Weight.V2a_V2a_gap_weight", V2a_V2a_gap_weight);
            paramDict.Add("Weight.MN_Muscle_syn_weight",MN_Muscle_syn_weight);
            paramDict.Add("Weight.V0d_MN_syn_weight",V0d_MN_syn_weight);
            paramDict.Add("Weight.V0d_IC_syn_weight",V0d_IC_syn_weight);
            paramDict.Add("Weight.V0d_V2a_syn_weight", V0d_V2a_syn_weight);
            paramDict.Add("Weight.V0v_IC_syn_weight", V0v_IC_syn_weight);
            paramDict.Add("Weight.V2a_V0v_syn_weight", V2a_V0v_syn_weight);

            paramDict.Add("Range.rangeMin",rangeMin);
            paramDict.Add("Range.rangeIC_MN",rangeIC_MN);
            paramDict.Add("Range.rangeIC_V0d",rangeIC_V0d);
            paramDict.Add("Range.rangeIC_V0v", rangeIC_V0v);
            paramDict.Add("Range.rangeIC_V2a", rangeIC_V2a);
            paramDict.Add("Range.rangeMN_MN",rangeMN_MN);
            paramDict.Add("Range.rangeMN_V0d",rangeMN_V0d);
            paramDict.Add("Range.rangeMN_V0v", rangeMN_V0v);
            paramDict.Add("Range.rangeV0d_IC", rangeV0d_IC);
            paramDict.Add("Range.rangeV0d_MN",rangeV0d_MN);
            paramDict.Add("Range.rangeV0d_V0d", rangeV0d_V0d);
            paramDict.Add("Range.rangeV0v_V0v", rangeV0v_V0v);
            paramDict.Add("Range.rangeV0d_V2a", rangeV0d_V2a);
            paramDict.Add("Range.rangeV0v_IC_min", rangeV0v_IC_min);
            paramDict.Add("Range.rangeV0v_IC_max", rangeV0v_IC_max);
            paramDict.Add("Range.rangeV2a_MN", rangeV2a_MN);
            paramDict.Add("Range.rangeV2a_V0v_asc", rangeV2a_V0v_asc);
            paramDict.Add("Range.rangeV2a_V0v_desc", rangeV2a_V0v_desc);
            paramDict.Add("Range.rangeV2a_V2a", rangeV2a_V2a);
            paramDict.Add("Range.rangeMN_Muscle", rangeMN_Muscle);
            return paramDict;
        }

        public override void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            base.SetParameters(paramExternal);

            tasyncdelay = paramExternal.Read("Dynamic.tasyncdelay", tasyncdelay);

            nIC = paramExternal.Read("CellNum.nIC", nIC);
            nMN = paramExternal.Read("CellNum.nMN", nMN);
            nV0d = paramExternal.Read("CellNum.nV0d", nV0d);
            nV0v = paramExternal.Read("CellNum.nV0v", nV0v);
            nV2a = paramExternal.Read("CellNum.nV2a", nV2a);
            nMuscle = paramExternal.Read("CellNum.nMuscle", nMuscle);

            IC_IC_gap_weight = paramExternal.Read("Weight.IC_IC_gap_weight", IC_IC_gap_weight);
            IC_MN_gap_weight = paramExternal.Read("Weight.IC_MN_gap_weight", IC_MN_gap_weight);
            IC_V0d_gap_weight = paramExternal.Read("Weight.IC_V0d_gap_weight", IC_V0d_gap_weight);
            IC_V0v_gap_weight = paramExternal.Read("Weight.IC_V0v_gap_weight", IC_V0v_gap_weight);
            IC_V2a_gap_weight = paramExternal.Read("Weight.IC_V2a_gap_weight", IC_V2a_gap_weight);
            MN_MN_gap_weight = paramExternal.Read("Weight.MN_MN_gap_weight", MN_MN_gap_weight);
            MN_V0d_gap_weight = paramExternal.Read("Weight.MN_V0d_gap_weight", MN_V0d_gap_weight);
            MN_V0v_gap_weight = paramExternal.Read("Weight.MN_V0v_gap_weight", MN_V0v_gap_weight);
            V0d_V0d_gap_weight = paramExternal.Read("Weight.V0d_V0d_gap_weight", V0d_V0d_gap_weight);
            V0v_V0v_gap_weight = paramExternal.Read("Weight.V0v_V0v_gap_weight", V0v_V0v_gap_weight);
            V2a_MN_gap_weight = paramExternal.Read("Weight.V2a_MN_gap_weight", V2a_MN_gap_weight);
            V2a_V2a_gap_weight = paramExternal.Read("Weight.V2a_V2a_gap_weight", V2a_V2a_gap_weight);
            MN_Muscle_syn_weight = paramExternal.Read("Weight.MN_Muscle_syn_weight", MN_Muscle_syn_weight);
            V0d_MN_syn_weight = paramExternal.Read("Weight.V0d_MN_syn_weight", V0d_MN_syn_weight);
            V0d_IC_syn_weight = paramExternal.Read("Weight.V0d_IC_syn_weight", V0d_IC_syn_weight);
            V0d_V2a_syn_weight = paramExternal.Read("Weight.V0d_V2a_syn_weight", V0d_V2a_syn_weight);
            V0v_IC_syn_weight = paramExternal.Read("Weight.V0v_IC_syn_weight", V0v_IC_syn_weight);
            V2a_V0v_syn_weight = paramExternal.Read("Weight.V2a_V0v_syn_weight", V2a_V0v_syn_weight);

            rangeMin = paramExternal.Read("Range.rangeMin", rangeMin);
            rangeIC_MN = paramExternal.Read("Range.rangeIC_MN", rangeIC_MN);
            rangeIC_V0d = paramExternal.Read("Range.rangeIC_V0d", rangeIC_V0d);
            rangeIC_V0v = paramExternal.Read("Range.rangeIC_V0v", rangeIC_V0v);
            rangeIC_V2a = paramExternal.Read("Range.rangeIC_V2a", rangeIC_V2a);
            rangeMN_MN = paramExternal.Read("Range.rangeMN_MN", rangeMN_MN);
            rangeMN_V0d = paramExternal.Read("Range.rangeMN_V0d", rangeMN_V0d);
            rangeMN_V0v = paramExternal.Read("Range.rangeMN_V0v", rangeMN_V0v);
            rangeV0d_IC = paramExternal.Read("Range.rangeV0d_IC", rangeV0d_IC);
            rangeV0d_MN = paramExternal.Read("Range.rangeV0d_MN", rangeV0d_MN);
            rangeV0d_V0d = paramExternal.Read("Range.rangeV0d_V0d", rangeV0d_V0d);
            rangeV0v_V0v = paramExternal.Read("Range.rangeV0v_V0v", rangeV0v_V0v);
            rangeV0d_V2a = paramExternal.Read("Range.rangeV0d_V2a", rangeV0d_V2a);
            rangeV0v_IC_min = paramExternal.Read("Range.rangeV0v_IC_min", rangeV0v_IC_min);
            rangeV0v_IC_max = paramExternal.Read("Range.rangeV0v_IC_max", rangeV0v_IC_max);
            rangeV2a_MN = paramExternal.Read("Range.rangeV2a_MN", rangeV2a_MN);
            rangeV2a_V0v_asc = paramExternal.Read("Range.rangeV2a_V0v_asc", rangeV2a_V0v_asc);
            rangeV2a_V0v_desc = paramExternal.Read("Range.rangeV2a_V0v_desc", rangeV2a_V0v_desc);
            rangeV2a_V2a = paramExternal.Read("Range.rangeV2a_V2a", rangeV2a_V2a);
            rangeMN_Muscle = paramExternal.Read("Range.rangeMN_Muscle", rangeMN_Muscle);
        }

    }
}