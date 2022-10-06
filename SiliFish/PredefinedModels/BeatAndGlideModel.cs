using System.Collections.Generic;
using System.Drawing;
using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.ModelUnits;

namespace SiliFish.PredefinedModels
{
    public class BeatAndGlideModel : PredefinedModel
    {
        protected int nMN = 15, ndI6 = 15, nV0v = 15, nV2a = 15, nV1 = 15, nMuscle = 15;
        protected CellPool L_MN, R_MN, L_dI6, R_dI6, L_V0v, R_V0v, L_V2a, R_V2a, L_V1, R_V1;
        protected CellPool L_Muscle, R_Muscle;

        #region Weight parameters
        //New version of SiliFish uses the max gap current, rather than summation.
        //Therefore gap weights for the predefined models are modified to reflect that
        protected double MN_MN_gap_weight = 0.01; //previous value: 0.005;
        protected double dI6_dI6_gap_weight = 0.08; //previous value: 0.04;
        protected double V0v_V0v_gap_weight = 0.01; //previous value: 0.05;
        protected double V2a_V2a_gap_weight = 0.005;
        protected double V2a_MN_gap_weight = 0.01; //previous value: 0.005;
        protected double MN_dI6_gap_weight = 0.0002; //previous value: 0.0001;
        protected double MN_V0v_gap_weight = 0.01; //previous value: 0.005;

        protected double V2a_V2a_syn_weight = 0.3;
        protected double V2a_MN_syn_weight = 0.5;
        protected double V2a_dI6_syn_weight = 0.5;
        protected double V2a_V1_syn_weight = 0.5;
        protected double V2a_V0v_syn_weight = 0.3;
        protected double V1_MN_syn_weight = 1.0;
        protected double V1_V2a_syn_weight = 0.5;
        protected double V1_V0v_syn_weight = 0.1;
        protected double V1_dI6_syn_weight = 0.2;
        protected double dI6_MN_syn_weight = 1.5;
        protected double dI6_V2a_syn_weight = 1.5;
        protected double dI6_dI6_syn_weight = 0.25;
        protected double V0v_V2a_syn_weight = 0.4;
        protected double MN_Muscle_syn_weight = 0.1;

        #endregion

        #region Upper range parameters for neuron to neuron/muscle cell reach
        protected double rangeMin = 0.2; // lower range value
        protected double rangeMN_MN = 4.5;
        protected double rangedI6_dI6 = 3.5;
        protected double rangeV0v_V0v = 3.5;
        protected double rangeV2a_V2a_gap = 3.5;
        protected double rangeV2a_MN = 3.5;
        protected double rangeMN_dI6 = 1.5;
        protected double rangeMN_V0v = 1.5;
        protected double rangeV2a_V2a_syn = 10;
        protected double rangeV2a_MN_asc = 4;
        protected double rangeV2a_MN_desc = 10;
        protected double rangeV2a_dI6 = 10;
        protected double rangeV2a_V1 = 10;
        protected double rangeV2a_V0v_asc = 4;
        protected double rangeV2a_V0v_desc = 10;
        protected double rangeV1_MN = 4;
        protected double rangeV1_V2a = 4;
        protected double rangeV1_V0v = 4;
        protected double rangeV1_dI6 = 4;
        protected double rangedI6_MN_desc = 5;
        protected double rangedI6_MN_asc = 2;
        protected double rangedI6_V2a_desc = 5;
        protected double rangedI6_V2a_asc = 2;
        protected double rangedI6_dI6_desc = 5;
        protected double rangedI6_dI6_asc = 2;
        protected double rangeV0v_V2a_desc = 5;
        protected double rangeV0v_V2a_asc = 2;
        protected double rangeMN_Muscle = 1;
        #endregion

        public BeatAndGlideModel()
        {
            taur = 0.5;
            taud = 1;
            vth = -15;
            stim_value1 = 2.89;
            stim_value2 = 0;
            sigma_gap = 0;
            sigma_chem = 0;
            E_ach = 120;
            E_glu = 0;
            E_gly = -70;
            cv = 0.80;
            tStimStart_ms = 200;
            CellPool.rangeNoiseMultiplier = GetRangeNoiseMultiplier;
            CellPool.gapWeightNoiseMultiplier = GetGapWeightNoiseMultiplier;
            CellPool.synWeightNoiseMultiplier = GetSynWeightNoiseMultiplier;
        }

        protected static double GetXNoise()
        {
            return rand.Gauss(1, 0.01, 0.9, 1.1);
        }

        protected override void InitNeurons()
        {

            MembraneDynamics mn_dyn = new() { a = 0.5, b = 0.01, c = -55, d = 100, Vmax = 10, Vr = -65, Vt = -58, k = 0.5, Cm = 20 };
            L_MN = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord, "MN", SagittalPlane.Left, 5, Color.Red);
            R_MN = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord, "MN", SagittalPlane.Right, 5, Color.Red);

            for (int i = 0; i < nMN; i++)
            {
                L_MN.AddCell(new Neuron("MN", seq: i + 1, mn_dyn, sigma_dyn, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), y: -1), cv: cv));
                R_MN.AddCell(new Neuron("MN", seq: i + 1, mn_dyn, sigma_dyn, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), y: 1), cv: cv));
            }

            neuronPools.Add(L_MN);
            neuronPools.Add(R_MN);

            MembraneDynamics di6_dyn = new() { a = 0.1, b = 0.002, c = -55, d = 4, Vmax = 10, Vr = -60, Vt = -54, k = 0.3, Cm = 10 };
            L_dI6 = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord, "dI6", SagittalPlane.Left, 1, Color.YellowGreen);
            R_dI6 = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord, "dI6", SagittalPlane.Right, 1, Color.YellowGreen);

            for (int i = 0; i < ndI6; i++)
            {
                L_dI6.AddCell(new Neuron("dI6", seq: i + 1, di6_dyn, sigma_dyn, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: -1), cv: cv));
                R_dI6.AddCell(new Neuron("dI6", seq: i + 1, di6_dyn, sigma_dyn, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: 1), cv: cv));
            }
            neuronPools.Add(L_dI6);
            neuronPools.Add(R_dI6);

            MembraneDynamics v0v_dyn = new() { a = 0.01, b = 0.002, c = -55, d = 2, Vmax = 8, Vr = -60, Vt = -54, k = 0.3, Cm = 10 };
            L_V0v = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord, "V0v", SagittalPlane.Left, 2, Color.Blue);
            R_V0v = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord, "V0v", SagittalPlane.Right, 2, Color.Blue);

            for (int i = 0; i < nV0v; i++)
            {
                L_V0v.AddCell(new Neuron("V0v", seq: i + 1, v0v_dyn, sigma_dyn, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: -1), cv: cv));
                R_V0v.AddCell(new Neuron("V0v", seq: i + 1, v0v_dyn, sigma_dyn, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: 1), cv: cv));
            }
            neuronPools.Add(L_V0v);
            neuronPools.Add(R_V0v);

            MembraneDynamics v2a_dyn = new() { a = 0.1, b = 0.002, c = -55, d = 4, Vmax = 10, Vr = -60, Vt = -54, k = 0.3, Cm = 10 };
            L_V2a = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord, "V2a", SagittalPlane.Left, 3, Color.RebeccaPurple);
            R_V2a = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord, "V2a", SagittalPlane.Right, 3, Color.RebeccaPurple);


            double multiplier = GetStimulusNoiseMultiplier();
            double multiplier2 = stim_mode == StimulusMode.Step ? 0 : multiplier; //to prevent stim_value2 to be used as noise, as there is already a stig_stim for it in predefined models.

            for (int i = 0; i < nV2a; i++)
            {
                TimeLine tl = new();
                tl.AddTimeRange(this.tStimStart_ms + 32 * i);
                Stimulus stim = new(new StimulusSettings(stim_mode, stim_value1 * multiplier, stim_value2 * multiplier2), tl);
                L_V2a.AddCell(new Neuron("V2a", seq: i + 1, v2a_dyn, sigma_dyn, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: -1), cv: cv, stim: stim));
                R_V2a.AddCell(new Neuron("V2a", seq: i + 1, v2a_dyn, sigma_dyn, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: 1), cv: cv, stim: stim));
            }
            neuronPools.Add(L_V2a);
            neuronPools.Add(R_V2a);

            MembraneDynamics v1_dyn = new() { a = 0.1, b = 0.002, c = -55, d = 4, Vmax = 10, Vr = -60, Vt = -54, k = 0.3, Cm = 10 };
            L_V1 = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord, "V1", SagittalPlane.Left, 4, Color.Yellow);
            R_V1 = new CellPool(this, CellType.Neuron, BodyLocation.SpinalCord, "V1", SagittalPlane.Right, 4, Color.Yellow);
            for (int i = 0; i < nV1; i++)
            {
                L_V1.AddCell(new Neuron("V1", seq: i + 1, v1_dyn, sigma_dyn, new Coordinate(x: 7.1 + 1.6 * i * GetXNoise(), -1), cv: cv));
                R_V1.AddCell(new Neuron("V1", seq: i + 1, v1_dyn, sigma_dyn, new Coordinate(x: 7.1 + 1.6 * i * GetXNoise(), 1), cv: cv));
            }
            neuronPools.Add(L_V1);
            neuronPools.Add(R_V1);

            L_Muscle = new CellPool(this, CellType.MuscleCell, BodyLocation.Body, "Muscle", SagittalPlane.Left, 6, Color.Purple);
            R_Muscle = new CellPool(this, CellType.MuscleCell, BodyLocation.Body, "Muscle", SagittalPlane.Right, 6, Color.Purple);
            for (int i = 0; i < nMuscle; i++)
            {
                L_Muscle.AddCell(new MuscleCell("Muscle", seq: i + 1, R: 1, C: 3, Vr: 0, sigma_dyn, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), -1)));
                R_Muscle.AddCell(new MuscleCell("Muscle", seq: i + 1, R: 1, C: 3, Vr: 0, sigma_dyn, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), 1)));
            }
            musclePools.Add(L_Muscle);
            musclePools.Add(R_Muscle);
        }


        protected override void InitSynapsesAndGapJunctions()
        {
            //MN to MN gap junctions
            CellReach cr = new() { AscendingReach = rangeMN_MN, DescendingReach = rangeMN_MN, MinReach = rangeMin, Weight = MN_MN_gap_weight };
            PoolToPoolGapJunction(L_MN, L_MN, cr);
            PoolToPoolGapJunction(R_MN, R_MN, cr);

            //MN to dI6 gap junctions
            cr = new() { AscendingReach = rangeMN_dI6, DescendingReach = rangeMN_dI6, MinReach = -1, Weight = MN_dI6_gap_weight };
            PoolToPoolGapJunction(L_MN, L_dI6, cr);
            PoolToPoolGapJunction(R_MN, R_dI6, cr);

            //dI6 to dI6 gap junctions
            cr = new() { AscendingReach = rangedI6_dI6, DescendingReach = rangedI6_dI6, MinReach = rangeMin, Weight = dI6_dI6_gap_weight };
            PoolToPoolGapJunction(L_dI6, L_dI6, cr);
            PoolToPoolGapJunction(R_dI6, R_dI6, cr);

            //V0v to V0v gap junctions
            cr = new() { AscendingReach = rangeV0v_V0v, DescendingReach = rangeV0v_V0v, MinReach = rangeMin, Weight = V0v_V0v_gap_weight };
            PoolToPoolGapJunction(L_V0v, L_V0v, cr);
            PoolToPoolGapJunction(R_V0v, R_V0v, cr);

            //V2a to V2a gap junctions
            cr = new() { AscendingReach = rangeV2a_V2a_gap, DescendingReach = rangeV2a_V2a_gap, MinReach = rangeMin, Weight = V2a_V2a_gap_weight };
            PoolToPoolGapJunction(L_V2a, L_V2a, cr);
            PoolToPoolGapJunction(R_V2a, R_V2a, cr);

            //V2a to MN gap junctions
            cr = new() { AscendingReach = rangeV2a_MN, DescendingReach = rangeV2a_MN, MinReach = rangeMin, Weight = V2a_MN_gap_weight };
            PoolToPoolGapJunction(L_V2a, L_MN, cr);
            PoolToPoolGapJunction(R_V2a, R_MN, cr);

            //MN to V0v gap junctions
            cr = new() { AscendingReach = rangeMN_V0v, DescendingReach = rangeMN_V0v, MinReach = -1, Weight = MN_V0v_gap_weight };
            PoolToPoolGapJunction(L_MN, L_V0v, cr);
            PoolToPoolGapJunction(R_MN, R_V0v, cr);

            //Ipsilateral glutamatergic projections from V2a to V2a
            SynapseParameters GluSynapse = new() { TauD = taud, TauR = taur, VTh = vth, E_rev = E_glu };
            cr = new() { AscendingReach = 0, DescendingReach = rangeV2a_V2a_syn, MinReach = rangeMin, Weight = V2a_V2a_syn_weight };
            PoolToPoolChemSynapse(L_V2a, L_V2a, cr, GluSynapse);
            PoolToPoolChemSynapse(R_V2a, R_V2a, cr, GluSynapse);

            //Ipsilateral glutamatergic projections from V2a to MN
            cr = new() { AscendingReach = rangeV2a_MN_asc, DescendingReach = rangeV2a_MN_desc, MinReach = rangeMin, Weight = V2a_MN_syn_weight };
            PoolToPoolChemSynapse(L_V2a, L_MN, cr, GluSynapse);
            PoolToPoolChemSynapse(R_V2a, R_MN, cr, GluSynapse);

            //Ipsilateral glutamatergic projections from V2a to dI6
            cr = new() { AscendingReach = 0, DescendingReach = rangeV2a_dI6, MinReach = -1, Weight = V2a_dI6_syn_weight };
            PoolToPoolChemSynapse(L_V2a, L_dI6, cr, GluSynapse);
            PoolToPoolChemSynapse(R_V2a, R_dI6, cr, GluSynapse);

            //Ipsilateral glutamatergic projections from V2a to V1
            cr = new() { AscendingReach = 0, DescendingReach = rangeV2a_V1, MinReach = -1, Weight = V2a_V1_syn_weight };
            PoolToPoolChemSynapse(L_V2a, L_V1, cr, GluSynapse);
            PoolToPoolChemSynapse(R_V2a, R_V1, cr, GluSynapse);

            //Ipsilateral glutamatergic projections from V2a to V0v 
            cr = new() { AscendingReach = rangeV2a_V0v_asc, DescendingReach = rangeV2a_V0v_desc, MinReach = rangeMin, Weight = V2a_V0v_syn_weight };
            PoolToPoolChemSynapse(L_V2a, L_V0v, cr, GluSynapse);
            PoolToPoolChemSynapse(R_V2a, R_V0v, cr, GluSynapse);

            SynapseParameters GlySynapse = new() { TauD = taud, TauR = taur, VTh = vth, E_rev = E_gly };

            //Ipsilateral glycinergic projections from V1 to MN 
            cr = new() { AscendingReach = rangeV1_MN, DescendingReach = 0, MinReach = -1, Weight = V1_MN_syn_weight };
            PoolToPoolChemSynapse(L_V1, L_MN, cr, GlySynapse);
            PoolToPoolChemSynapse(R_V1, R_MN, cr, GlySynapse);

            //Ipsilateral glycinergic projections from V1 to V2a
            cr = new() { AscendingReach = rangeV1_V2a, DescendingReach = 0, MinReach = -1, Weight = V1_V2a_syn_weight };
            PoolToPoolChemSynapse(L_V1, L_V2a, cr, GlySynapse);
            PoolToPoolChemSynapse(R_V1, R_V2a, cr, GlySynapse);

            //Ipsilateral glycinergic projections from V1 to V0v
            cr = new() { AscendingReach = rangeV1_V0v, DescendingReach = 0, MinReach = -1, Weight = V1_V0v_syn_weight };
            PoolToPoolChemSynapse(L_V1, L_V0v, cr, GlySynapse);
            PoolToPoolChemSynapse(R_V1, R_V0v, cr, GlySynapse);

            //Ipsilateral glycinergic projections from V1 to dI6
            cr = new() { AscendingReach = rangeV1_dI6, DescendingReach = 0, MinReach = -1, Weight = V1_dI6_syn_weight };
            PoolToPoolChemSynapse(L_V1, L_dI6, cr, GlySynapse);
            PoolToPoolChemSynapse(R_V1, R_dI6, cr, GlySynapse);

            //Contralateral glycinergic projections from dI6 to MN 
            cr = new() { AscendingReach = rangedI6_MN_asc, DescendingReach = rangedI6_MN_desc, MinReach = rangeMin, Weight = dI6_MN_syn_weight };
            PoolToPoolChemSynapse(L_dI6, R_MN, cr, GlySynapse);
            PoolToPoolChemSynapse(R_dI6, L_MN, cr, GlySynapse);

            //Contralateral glycinergic projections from dI6 to V2a 
            cr = new() { AscendingReach = rangedI6_V2a_asc, DescendingReach = rangedI6_V2a_desc, MinReach = rangeMin, Weight = dI6_V2a_syn_weight };
            PoolToPoolChemSynapse(L_dI6, R_V2a, cr, GlySynapse);
            PoolToPoolChemSynapse(R_dI6, L_V2a, cr, GlySynapse);

            //Contralateral glycinergic projections from dI6 to dI6 
            cr = new() { AscendingReach = rangedI6_dI6_asc, DescendingReach = rangedI6_dI6_desc, MinReach = rangeMin, Weight = dI6_dI6_syn_weight };
            PoolToPoolChemSynapse(L_dI6, R_dI6, cr, GlySynapse);
            PoolToPoolChemSynapse(R_dI6, L_dI6, cr, GlySynapse);

            //Contralateral glutamatergic projections from V0v to V2a 
            cr = new() { AscendingReach = rangeV0v_V2a_asc, DescendingReach = rangeV0v_V2a_desc, MinReach = rangeMin, Weight = V0v_V2a_syn_weight };
            PoolToPoolChemSynapse(L_V0v, R_V2a, cr, GluSynapse);
            PoolToPoolChemSynapse(R_V0v, L_V2a, cr, GluSynapse);


            //Ipsilateral cholinergic projections from MN to Muscle cells
            SynapseParameters AChSynapse = new() { TauD = taud, TauR = taur, VTh = vth, E_rev = E_ach };
            cr = new() { AscendingReach = rangeMN_Muscle, DescendingReach = rangeMN_Muscle, MinReach = -1, Weight = MN_Muscle_syn_weight, FixedDuration_ms = 10 };
            PoolToPoolChemSynapse(L_MN, L_Muscle, cr, AChSynapse);
            PoolToPoolChemSynapse(R_MN, R_Muscle, cr, AChSynapse);
        }

        public override Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = base.GetParameters();

            paramDict.Add("Cell Num.ndI6", ndI6);
            paramDict.Add("Cell Num.nV0v", nV0v);
            paramDict.Add("Cell Num.nV1", nV1);
            paramDict.Add("Cell Num.nV2a", nV2a);
            paramDict.Add("Cell Num.nMN", nMN);
            paramDict.Add("Cell Num.nMuscle", nMuscle);

            paramDict.Add("Weight.MN_MN_gap_weight", MN_MN_gap_weight);
            paramDict.Add("Weight.dI6_dI6_gap_weight", dI6_dI6_gap_weight);
            paramDict.Add("Weight.V0v_V0v_gap_weight", V0v_V0v_gap_weight);
            paramDict.Add("Weight.V2a_V2a_gap_weight", V2a_V2a_gap_weight);
            paramDict.Add("Weight.V2a_MN_gap_weight", V2a_MN_gap_weight);
            paramDict.Add("Weight.MN_dI6_gap_weight", MN_dI6_gap_weight);
            paramDict.Add("Weight.MN_V0v_gap_weight", MN_V0v_gap_weight);

            paramDict.Add("Weight.V2a_V2a_syn_weight", V2a_V2a_syn_weight);
            paramDict.Add("Weight.V2a_MN_syn_weight", V2a_MN_syn_weight);
            paramDict.Add("Weight.V2a_dI6_syn_weight", V2a_dI6_syn_weight);
            paramDict.Add("Weight.V2a_V1_syn_weight", V2a_V1_syn_weight);
            paramDict.Add("Weight.V2a_V0v_syn_weight", V2a_V0v_syn_weight);
            paramDict.Add("Weight.V1_MN_syn_weight", V1_MN_syn_weight);
            paramDict.Add("Weight.V1_V2a_syn_weight", V1_V2a_syn_weight);
            paramDict.Add("Weight.V1_V0v_syn_weight", V1_V0v_syn_weight);
            paramDict.Add("Weight.V1_dI6_syn_weight", V1_dI6_syn_weight);
            paramDict.Add("Weight.dI6_MN_syn_weight", dI6_MN_syn_weight);
            paramDict.Add("Weight.dI6_V2a_syn_weight", dI6_V2a_syn_weight);
            paramDict.Add("Weight.dI6_dI6_syn_weight", dI6_dI6_syn_weight);
            paramDict.Add("Weight.V0v_V2a_syn_weight", V0v_V2a_syn_weight);
            paramDict.Add("Weight.MN_Muscle_syn_weight", MN_Muscle_syn_weight);

            paramDict.Add("Range.rangeMin", rangeMin);
            paramDict.Add("Range.rangeMN_MN", rangeMN_MN);
            paramDict.Add("Range.rangedI6_dI6", rangedI6_dI6);
            paramDict.Add("Range.rangeV0v_V0v", rangeV0v_V0v);
            paramDict.Add("Range.rangeV2a_V2a_gap", rangeV2a_V2a_gap);
            paramDict.Add("Range.rangeV2a_MN", rangeV2a_MN);
            paramDict.Add("Range.rangeMN_dI6", rangeMN_dI6);
            paramDict.Add("Range.rangeMN_V0v", rangeMN_V0v);
            paramDict.Add("Range.rangeV2a_V2a_syn", rangeV2a_V2a_syn);
            paramDict.Add("Range.rangeV2a_MN_asc", rangeV2a_MN_asc);
            paramDict.Add("Range.rangeV2a_MN_desc", rangeV2a_MN_desc);
            paramDict.Add("Range.rangeV2a_dI6", rangeV2a_dI6);
            paramDict.Add("Range.rangeV2a_V1", rangeV2a_V1);
            paramDict.Add("Range.rangeV2a_V0v_asc", rangeV2a_V0v_asc);
            paramDict.Add("Range.rangeV2a_V0v_desc", rangeV2a_V0v_desc);
            paramDict.Add("Range.rangeV1_MN", rangeV1_MN);
            paramDict.Add("Range.rangeV1_V2a", rangeV1_V2a);
            paramDict.Add("Range.rangeV1_V0v", rangeV1_V0v);
            paramDict.Add("Range.rangeV1_dI6", rangeV1_dI6);
            paramDict.Add("Range.rangedI6_MN_desc", rangedI6_MN_desc);
            paramDict.Add("Range.rangedI6_MN_asc", rangedI6_MN_asc);
            paramDict.Add("Range.rangedI6_V2a_desc", rangedI6_V2a_desc);
            paramDict.Add("Range.rangedI6_V2a_asc", rangedI6_V2a_asc);
            paramDict.Add("Range.rangedI6_dI6_desc", rangedI6_dI6_desc);
            paramDict.Add("Range.rangedI6_dI6_asc", rangedI6_dI6_asc);
            paramDict.Add("Range.rangeV0v_V2a_desc", rangeV0v_V2a_desc);
            paramDict.Add("Range.rangeV0v_V2a_asc", rangeV0v_V2a_asc);
            paramDict.Add("Range.rangeMN_Muscle", rangeMN_Muscle);
            return paramDict;
        }

        public override void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            base.SetParameters(paramExternal);
            initialized = false;

            ndI6 = paramExternal.Read("Cell Num.ndI6", ndI6);
            nV0v = paramExternal.Read("Cell Num.nV0v", nV0v);
            nV1 = paramExternal.Read("Cell Num.nV1", nV1);
            nV2a = paramExternal.Read("Cell Num.nV2a", nV2a);
            nMN = paramExternal.Read("Cell Num.nMN", nMN);
            nMuscle = paramExternal.Read("Cell Num.nMuscle", nMuscle);

            MN_MN_gap_weight = paramExternal.Read("Weight.MN_MN_gap_weight", MN_MN_gap_weight);
            dI6_dI6_gap_weight = paramExternal.Read("Weight.dI6_dI6_gap_weight", dI6_dI6_gap_weight);
            V0v_V0v_gap_weight = paramExternal.Read("Weight.V0v_V0v_gap_weight", V0v_V0v_gap_weight);
            V2a_V2a_gap_weight = paramExternal.Read("Weight.V2a_V2a_gap_weight", V2a_V2a_gap_weight);
            V2a_MN_gap_weight = paramExternal.Read("Weight.V2a_MN_gap_weight", V2a_MN_gap_weight);
            MN_dI6_gap_weight = paramExternal.Read("Weight.MN_dI6_gap_weight", MN_dI6_gap_weight);
            MN_V0v_gap_weight = paramExternal.Read("Weight.MN_V0v_gap_weight", MN_V0v_gap_weight);

            V2a_V2a_syn_weight = paramExternal.Read("Weight.V2a_V2a_syn_weight", V2a_V2a_syn_weight);
            V2a_MN_syn_weight = paramExternal.Read("Weight.V2a_MN_syn_weight", V2a_MN_syn_weight);
            V2a_dI6_syn_weight = paramExternal.Read("Weight.V2a_dI6_syn_weight", V2a_dI6_syn_weight);
            V2a_V1_syn_weight = paramExternal.Read("Weight.V2a_V1_syn_weight", V2a_V1_syn_weight);
            V2a_V0v_syn_weight = paramExternal.Read("Weight.V2a_V0v_syn_weight", V2a_V0v_syn_weight);
            V1_MN_syn_weight = paramExternal.Read("Weight.V1_MN_syn_weight", V1_MN_syn_weight);
            V1_V2a_syn_weight = paramExternal.Read("Weight.V1_V2a_syn_weight", V1_V2a_syn_weight);
            V1_V0v_syn_weight = paramExternal.Read("Weight.V1_V0v_syn_weight", V1_V0v_syn_weight);
            V1_dI6_syn_weight = paramExternal.Read("Weight.V1_dI6_syn_weight", V1_dI6_syn_weight);
            dI6_MN_syn_weight = paramExternal.Read("Weight.dI6_MN_syn_weight", dI6_MN_syn_weight);
            dI6_V2a_syn_weight = paramExternal.Read("Weight.dI6_V2a_syn_weight", dI6_V2a_syn_weight);
            dI6_dI6_syn_weight = paramExternal.Read("Weight.dI6_dI6_syn_weight", dI6_dI6_syn_weight);
            V0v_V2a_syn_weight = paramExternal.Read("Weight.V0v_V2a_syn_weight", V0v_V2a_syn_weight);
            MN_Muscle_syn_weight = paramExternal.Read("Weight.MN_Muscle_syn_weight", MN_Muscle_syn_weight);

            rangeMin = paramExternal.Read("Range.rangeMin", rangeMin);
            rangeMN_MN = paramExternal.Read("Range.rangeMN_MN", rangeMN_MN);
            rangedI6_dI6 = paramExternal.Read("Range.rangedI6_dI6", rangedI6_dI6);
            rangeV0v_V0v = paramExternal.Read("Range.rangeV0v_V0v", rangeV0v_V0v);
            rangeV2a_V2a_gap = paramExternal.Read("Range.rangeV2a_V2a_gap", rangeV2a_V2a_gap);
            rangeV2a_MN = paramExternal.Read("Range.rangeV2a_MN", rangeV2a_MN);
            rangeMN_dI6 = paramExternal.Read("Range.rangeMN_dI6", rangeMN_dI6);
            rangeMN_V0v = paramExternal.Read("Range.rangeMN_V0v", rangeMN_V0v);
            rangeV2a_V2a_syn = paramExternal.Read("Range.rangeV2a_V2a_syn", rangeV2a_V2a_syn);
            rangeV2a_MN_asc = paramExternal.Read("Range.rangeV2a_MN_asc", rangeV2a_MN_asc);
            rangeV2a_MN_desc = paramExternal.Read("Range.rangeV2a_MN_desc", rangeV2a_MN_desc);
            rangeV2a_dI6 = paramExternal.Read("Range.rangeV2a_dI6", rangeV2a_dI6);
            rangeV2a_V1 = paramExternal.Read("Range.rangeV2a_V1", rangeV2a_V1);
            rangeV2a_V0v_asc = paramExternal.Read("Range.rangeV2a_V0v_asc", rangeV2a_V0v_asc);
            rangeV2a_V0v_desc = paramExternal.Read("Range.rangeV2a_V0v_desc", rangeV2a_V0v_desc);
            rangeV1_MN = paramExternal.Read("Range.rangeV1_MN", rangeV1_MN);
            rangeV1_V2a = paramExternal.Read("Range.rangeV1_V2a", rangeV1_V2a);
            rangeV1_V0v = paramExternal.Read("Range.rangeV1_V0v", rangeV1_V0v);
            rangeV1_dI6 = paramExternal.Read("Range.rangeV1_dI6", rangeV1_dI6);
            rangedI6_MN_desc = paramExternal.Read("Range.rangedI6_MN_desc", rangedI6_MN_desc);
            rangedI6_MN_asc = paramExternal.Read("Range.rangedI6_MN_asc", rangedI6_MN_asc);
            rangedI6_V2a_desc = paramExternal.Read("Range.rangedI6_V2a_desc", rangedI6_V2a_desc);
            rangedI6_V2a_asc = paramExternal.Read("Range.rangedI6_V2a_asc", rangedI6_V2a_asc);
            rangedI6_dI6_desc = paramExternal.Read("Range.rangedI6_dI6_desc", rangedI6_dI6_desc);
            rangedI6_dI6_asc = paramExternal.Read("Range.rangedI6_dI6_asc", rangedI6_dI6_asc);
            rangeV0v_V2a_desc = paramExternal.Read("Range.rangeV0v_V2a_desc", rangeV0v_V2a_desc);
            rangeV0v_V2a_asc = paramExternal.Read("Range.rangeV0v_V2a_asc", rangeV0v_V2a_asc);
            rangeMN_Muscle = paramExternal.Read("Range.rangeMN_Muscle", rangeMN_Muscle);

        }

    }
}
