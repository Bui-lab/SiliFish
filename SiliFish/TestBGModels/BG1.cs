using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebrafish.DataTypes;
using Zebrafish.Extensions;
using Zebrafish.ModelUnits;
using Zebrafish.PredefinedModels;

namespace Zebrafish.TestBGModels
{
    /// <summary>
    /// Swimming episodes: several seconds, upto a minute + long breaks in between
    /// Tail beat freq >= 60-70 Hz
    /// Adult: <3Hz, 3-8 Hz, >8 Hz
    /// Larval: 20-40 Hz, 40-60 Hz, 60-80 Hz
    /// 
    /// pMN: primary born - dorsal - ballistic movements
    /// sMN - secondary born - distributed dorsal ventral. More populated in the dorsal regions than caudal - swimming
    /// dorsal sMN: fast swimming - lower resistance
    /// ventral sMN: slow simming, high resistance
    /// 
    /// V0: contralateral projections to MN
    /// 70% inhibitory: glycine, GABA, or both
    /// Dorsal V0d: inhibitory. In fast swimming, activated by contralateral projections of fast V2a
    /// Ventral V0v: excitatory, commissural. Rhythmic (slow, intermediate, fast) and non-rhythmic. 
    ///     Slow, intermediate: descending and bifurcating. Fast: ascending projections
    ///     
    /// V1: circumferential ascending
    /// glycinergic, some also GABAergic
    /// In phase activity with MNs
    /// lateral-medial plane: number and location change during development - Higashima 2004
    /// Ipsilateral to ventral MNs and other inhibitory INs
    /// ablation of V1 neurons prolongs the swimming durations, especially in fast swimming
    /// involved in the inhibition of slow INs and slow MNs during fast swimming 
    /// Fast type V1 INs are active during the initial phase of fast swimming, 
    /// Slow type V1 INs active during the late phase of slow swimming
    /// V1 INs shape V2a recruitment, which also influences the swimming speed in zebrafish
    /// 
    /// Earlier born V2as that are more dorsally located are recruited during fast swimming. Ipsilateral excitatory descending connections to MNs.
    /// Late-born and ventrally located V2as are involved in slow swimming.
    /// V2as to MN: both electrical and synaptic connections
    /// V2a INs: necessary and sufficient for swimming
    /// excitatory V2a neurons have ipsilateral monosynaptic inputs to MNs
    /// ablation of V2a INs increases the delay between the segments in the rostrocaudal axis, 
    ///     a decrease in the bursting pattern, and a higher stimulation is required to trigger the swimming activity
    ///There is a difference between ablation of the dorsal versus ventrally located V2a.
    ///Dorsal V2as are shown to be more involved in fast swimming
    ///
    /// Adult zebrafish: slow, intermediate, and fast V2a, projecting to slow, intermediate, and fast MNs 
    ///     with stronger connections to the MNs in the same group, and weaker connections to the MNs in the neighboring group.
    ///     No connections between the slow V2as and fast MNs.
    ///The slow V2as had the lowest threshold; the fast V2as had the highest.
    ///
    /// Larval zebrafish: Type I V2as make strong connections to other excitatory V2a and inhibitory V0ds; timing of the swimming.
    /// Type II V2a make connections to MNs - amplitude of swimming.Type II V2as also make connections to V0d INS but with different dynamics than type I V2as.
    /// - only descending projections (V2a-D) and both descending and supraspinal projections(V2a-B). 
    /// - bursting and non-bursting types based on their firing patterns.
    /// Bursting V2as have descending projections to the dendrites of slow MNs, while non-bursting V2a have weak connections to the somas of fast MNs.
    /// 
    /// V2b; alternating extensor and flexor muscles
    /// direct connections to MNs and different subgroups project to different MNs
    /// V2b activation lowers the swimming frequency, and its suppression causes faster swimming
    /// V2b is involved in speed control by ipsilateral inhibition
    /// In zebrafish embryos, V2b neurons are GABAergic. 
    /// In 5-dpf larvae, all V2b neurons release glycine(V2b-gly), and half also release GABA(V2b-mixed). 
    /// V2b-mixed INS are slightly more ventral than V2b-gly.both normally distributed across the dorsal-ventral axis.
    /// V2b-mixed provide inhibition to slow motor neurons that are ventrally located, while V2b-gly inhibit dorsally located fast MNs
    /// 
    /// V0d and dI6 INs together constitute the commissural bifurcating longitudinal (CoBL) neurons, the inhibitory INs that project contralaterally
    /// dI6: the acceleration of zebrafish swimming
    /// both ipsilateral and contralateral projections to MNs
    /// works synergistically with V0d INs to coordinate left, right movements in zebrafish swimming
    /// </summary>
    public class BG1: BeatAndGlideModel
    {
        protected int nV0d = 15;
        protected CellPool L_V0d, R_V0d;

        double MN_V0d_gap_weight = 0.0001;
        double V0d_V0d_gap_weight = 0.04;
        double V0d_MN_syn_weight = 2.0;
        double V0d_V2a_syn_weight = 2.0;

        double rangeMN_V0d = 1.5;
        double rangeV0d_MN = 8;
        double rangeV0d_V0d = 3.5;
        double rangeV0d_V2a = 8;

        protected override void InitNeurons()
        {
            /*The existing model contains some of the IN subpopulations, like V0d and V0v INs. 
             * Even these subpopulations can further be divided into numerous groups based on their projections, neurotransmitter phenotypes, or intrinsic properties. 
             * Some of them will be distinct populations, like V2b-gly and V2b-mixed, or V2a-descending and V2a-bifurcating. 
             * The others will be blended into these distinct populations, like early-born V2as and late-born V2as, to model the continuity in their properties. 
             * This will result in variability in the neuronal subgroups rather than having them representing a group of neurons with identical intrinsic properties.*/

            /*The distribution of MNs and INs across rostrocaudal or dorsal-ventral axes is not uniform. 
             * For example, V2as are more concentrated on the rostral and caudal ends, whereas MNs are concentrated in the middle ?ADULT or LARVA???. 
             * In larva zebrafish, the slow and fast muscle fibres are located more dorsally and medially, respectively. 
             */

            /*, it was shown that there are more connections from faster to slower modules of V2as in adult zebrafish than vice versa. 
             * Therefore, assigning probabilities to projections between two neuronal groups will increase the power of the models generated.*/
            MembraneDynamics mn_dyn = new() { a = 0.5, b = 0.01, c = -55, d = 100, vmax = 10, vr = -65, vt = -58, k = 0.5, Cm = 20 };
            CellPoolTemplate mn_temp = new() { CellGroup="MN", CellType= "Neuron", ColumnIndex2D = 5, NumOfCells = nMN};
            double minX = 5;
            double maxX = 5 + 1.6 * nMN;
            mn_temp.XDistribution = new BimodalDistribution(minX, maxX, 7, 1, 10, 1, 0.5);
            mn_temp.YDistribution = new UniformDistribution(0, -3);
            mn_temp.ZDistribution = new UniformDistribution(-3, 3);
            Dictionary<string, object> paramDict = new();
            paramDict.Add("Izhikevich_9P.a", 0.5);
            paramDict.Add("Izhikevich_9P.b", 0.01);
            paramDict.Add("Izhikevich_9P.c", -55);
            paramDict.Add("Izhikevich_9P.d", 100);
            paramDict.Add("Izhikevich_9P.vmax", new UniformDistribution(8,12));//10
            paramDict.Add("Izhikevich_9P.vr", new GaussianDistribution(-65, 5));//-65
            paramDict.Add("Izhikevich_9P.vt", -58);
            paramDict.Add("Izhikevich_9P.k", 0.5);
            paramDict.Add("Izhikevich_9P.Cm", 20);
            mn_temp.Parameters = paramDict;
            mn_temp.PositionLeftRight = SagittalPlane.Left;
            L_MN = new CellPool("MN", SagittalPlane.Left, 5);
            L_MN.GenerateCells(mn_temp, SagittalPlane.Left);
            
            
            R_MN = new CellPool("MN", SagittalPlane.Right, 5);
            mn_temp.YDistribution = new UniformDistribution(0, 3);
            mn_temp.PositionLeftRight = SagittalPlane.Right;
            R_MN.GenerateCells(mn_temp, SagittalPlane.Right);

            /*
    public CellTemplate TemplateCell { get; set; } = new CellTemplate();
    public SagittalPlane PositionLeftRight { get; set; } = SagittalPlane.NotSet;
    public object? XDistribution { get; set; }*/

/*            for (int i = 0; i < nMN; i++)
            {
                L_MN.AddCell(new Neuron("MN", seq: i, mn_dyn, init_v: -70, init_u: -14, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), y: -1)));
                R_MN.AddCell(new Neuron("MN", seq: i, mn_dyn, init_v: -70, init_u: -14, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), y: 1)));
            }
*/
            NeuronPools.Add(L_MN);
            NeuronPools.Add(R_MN);

            MembraneDynamics di6_dyn = new() { a = 0.1, b = 0.002, c = -55, d = 4, vmax = 10, vr = -60, vt = -54, k = 0.3, Cm = 10 };
            L_dI6 = new CellPool("dI6", SagittalPlane.Left, 1);
            R_dI6 = new CellPool("dI6", SagittalPlane.Right, 1);

            for (int i = 0; i < ndI6; i++)
            {
                L_dI6.AddCell(new Neuron("dI6", seq: i, di6_dyn, init_v: -70, init_u: -14, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: -1)));
                R_dI6.AddCell(new Neuron("dI6", seq: i, di6_dyn, init_v: -70, init_u: -14, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: 1)));
            }
            NeuronPools.Add(L_dI6);
            NeuronPools.Add(R_dI6);

            MembraneDynamics v0v_dyn = new() { a = 0.01, b = 0.002, c = -55, d = 2, vmax = 8, vr = -60, vt = -54, k = 0.3, Cm = 10 };
            L_V0v = new CellPool("V0v", SagittalPlane.Left, 2);
            R_V0v = new CellPool("V0v", SagittalPlane.Right, 2);

            for (int i = 0; i < nV0v; i++)
            {
                L_V0v.AddCell(new Neuron("V0v", seq: i, v0v_dyn, init_v: -70, init_u: -14, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: -1)));
                R_V0v.AddCell(new Neuron("V0v", seq: i, v0v_dyn, init_v: -70, init_u: -14, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: 1)));
            }
            NeuronPools.Add(L_V0v);
            NeuronPools.Add(R_V0v);

            MembraneDynamics v2a_dyn = new() { a = 0.1, b = 0.002, c = -55, d = 4, vmax = 10, vr = -60, vt = -54, k = 0.3, Cm = 10 };
            L_V2a = new CellPool("V2a", SagittalPlane.Left, 3);
            R_V2a = new CellPool("V2a", SagittalPlane.Right, 3);

            Stimulus stim = new(stim_mode, (int)(this.tshutoff / SwimmingModel.dt), this.MaxIndex, stim_value1, stim_value2);

            for (int i = 0; i < nV2a; i++)
            {
                L_V2a.AddCell(new Neuron("V2a", seq: i, v2a_dyn, init_v: -64, init_u: -16, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: -1), stim: stim));
                R_V2a.AddCell(new Neuron("V2a", seq: i, v2a_dyn, init_v: -64, init_u: -16, new Coordinate(x: 5.1 + 1.6 * i * GetXNoise(), y: 1), stim: stim));
            }
            NeuronPools.Add(L_V2a);
            NeuronPools.Add(R_V2a);

            MembraneDynamics v1_dyn = new() { a = 0.1, b = 0.002, c = -55, d = 4, vmax = 10, vr = -60, vt = -54, k = 0.3, Cm = 10 };
            L_V1 = new CellPool("V1", SagittalPlane.Left, 4);
            R_V1 = new CellPool("V1", SagittalPlane.Right, 4);
            for (int i = 0; i < nV1; i++)
            {
                L_V1.AddCell(new Neuron("V1", seq: i, v1_dyn, init_v: -64, init_u: -16, new Coordinate(x: 7.1 + 1.6 * i * GetXNoise(), -1)));
                R_V1.AddCell(new Neuron("V1", seq: i, v1_dyn, init_v: -64, init_u: -16, new Coordinate(x: 7.1 + 1.6 * i * GetXNoise(), 1)));
            }
            NeuronPools.Add(L_V1);
            NeuronPools.Add(R_V1);

            L_Muscle = new CellPool("Muscle", SagittalPlane.Left, 6);
            R_Muscle = new CellPool("Muscle", SagittalPlane.Right, 6);
            for (int i = 0; i < nMuscle; i++)
            {
                L_Muscle.AddCell(new MuscleCell("Muscle", i, R: 1, C: 3, init_v: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), -1)));
                R_Muscle.AddCell(new MuscleCell("Muscle", i, R: 1, C: 3, init_v: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), 1)));
            }
            MuscleCellPools.Add(L_Muscle);
            MuscleCellPools.Add(R_Muscle); 
            
            MembraneDynamics v0d_dyn = new() { a = 0.02, b = 0.1, c = -30, d = 3.75, vmax = 10, vr = -60, vt = -45, k = 0.05, Cm = 20 };
            L_V0d = new CellPool("V0d", SagittalPlane.Left, 1);
            R_V0d = new CellPool("V0d", SagittalPlane.Right, 1);
            for (int i = 0; i < nV0d; i++)
            {
                L_V0d.AddCell(new Neuron("V0d", seq: i, v0d_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), -1)));
                R_V0d.AddCell(new Neuron("V0d", seq: i, v0d_dyn, init_v: -65, init_u: 0, new Coordinate(x: 5.0 + 1.6 * i * GetXNoise(), 1)));
            }
            NeuronPools.Add(L_V0d);
            NeuronPools.Add(R_V0d);
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

            TimeLine span = new();
            span.AddTimeRange((int)(this.tshutoff / SwimmingModel.dt));

            //Ipsilateral glutamatergic projections from V2a to V2a
            SynapseParameters GluSynapse = new() { TauD = taud, TauR = taur, VTh = vth, E_rev = E_glu, G_syn = g_syn };
            cr = new() { AscendingReach = 0, DescendingReach = rangeV2a_V2a_syn, MinReach = rangeMin, Weight = V2a_V2a_syn_weight };
            PoolToPoolChemJunction(L_V2a, L_V2a, cr, GluSynapse, span);
            PoolToPoolChemJunction(R_V2a, R_V2a, cr, GluSynapse, span);

            //Ipsilateral glutamatergic projections from V2a to MN
            cr = new() { AscendingReach = rangeV2a_MN_asc, DescendingReach = rangeV2a_MN_desc, MinReach = rangeMin, Weight = V2a_MN_syn_weight };
            PoolToPoolChemJunction(L_V2a, L_MN, cr, GluSynapse, span);
            PoolToPoolChemJunction(R_V2a, R_MN, cr, GluSynapse, span);

            //Ipsilateral glutamatergic projections from V2a to dI6
            cr = new() { AscendingReach = 0, DescendingReach = rangeV2a_dI6, MinReach = -1, Weight = V2a_dI6_syn_weight };
            PoolToPoolChemJunction(L_V2a, L_dI6, cr, GluSynapse, span);
            PoolToPoolChemJunction(R_V2a, R_dI6, cr, GluSynapse, span);

            //Ipsilateral glutamatergic projections from V2a to V1
            cr = new() { AscendingReach = 0, DescendingReach = rangeV2a_V1, MinReach = -1, Weight = V2a_V1_syn_weight };
            PoolToPoolChemJunction(L_V2a, L_V1, cr, GluSynapse, span);
            PoolToPoolChemJunction(R_V2a, R_V1, cr, GluSynapse, span);

            //Ipsilateral glutamatergic projections from V2a to V0v 
            cr = new() { AscendingReach = rangeV2a_V0v_asc, DescendingReach = rangeV2a_V0v_desc, MinReach = rangeMin, Weight = V2a_V0v_syn_weight };
            PoolToPoolChemJunction(L_V2a, L_V0v, cr, GluSynapse, span);
            PoolToPoolChemJunction(R_V2a, R_V0v, cr, GluSynapse, span);

            SynapseParameters GlySynapse = new() { TauD = taud, TauR = taur, VTh = vth, E_rev = E_gly, G_syn = g_syn };

            //Ipsilateral glycinergic projections from V1 to MN 
            cr = new() { AscendingReach = rangeV1_MN, DescendingReach = 0, MinReach = -1, Weight = V1_MN_syn_weight };
            PoolToPoolChemJunction(L_V1, L_MN, cr, GlySynapse, span);
            PoolToPoolChemJunction(R_V1, R_MN, cr, GlySynapse, span);

            //Ipsilateral glycinergic projections from V1 to V2a
            cr = new() { AscendingReach = rangeV1_V2a, DescendingReach = 0, MinReach = -1, Weight = V1_V2a_syn_weight };
            PoolToPoolChemJunction(L_V1, L_V2a, cr, GlySynapse, span);
            PoolToPoolChemJunction(R_V1, R_V2a, cr, GlySynapse, span);

            //Ipsilateral glycinergic projections from V1 to V0v
            cr = new() { AscendingReach = rangeV1_V0v, DescendingReach = 0, MinReach = -1, Weight = V1_V0v_syn_weight };
            PoolToPoolChemJunction(L_V1, L_V0v, cr, GlySynapse, span);
            PoolToPoolChemJunction(R_V1, R_V0v, cr, GlySynapse, span);

            //Ipsilateral glycinergic projections from V1 to dI6
            cr = new() { AscendingReach = rangeV1_dI6, DescendingReach = 0, MinReach = -1, Weight = V1_dI6_syn_weight };
            PoolToPoolChemJunction(L_V1, L_dI6, cr, GlySynapse, span);
            PoolToPoolChemJunction(R_V1, R_dI6, cr, GlySynapse, span);

            //Contralateral glycinergic projections from dI6 to MN 
            cr = new() { AscendingReach = rangedI6_MN_asc, DescendingReach = rangedI6_MN_desc, MinReach = rangeMin, Weight = dI6_MN_syn_weight };
            PoolToPoolChemJunction(L_dI6, R_MN, cr, GlySynapse, span);
            PoolToPoolChemJunction(R_dI6, L_MN, cr, GlySynapse, span);

            //Contralateral glycinergic projections from dI6 to V2a 
            cr = new() { AscendingReach = rangedI6_V2a_asc, DescendingReach = rangedI6_V2a_desc, MinReach = rangeMin, Weight = dI6_V2a_syn_weight };
            PoolToPoolChemJunction(L_dI6, R_V2a, cr, GlySynapse, span);
            PoolToPoolChemJunction(R_dI6, L_V2a, cr, GlySynapse, span);

            //Contralateral glycinergic projections from dI6 to dI6 
            cr = new() { AscendingReach = rangedI6_dI6_asc, DescendingReach = rangedI6_dI6_desc, MinReach = rangeMin, Weight = dI6_dI6_syn_weight };
            PoolToPoolChemJunction(L_dI6, R_dI6, cr, GlySynapse, span);
            PoolToPoolChemJunction(R_dI6, L_dI6, cr, GlySynapse, span);

            //Contralateral glutamatergic projections from V0v to V2a 
            cr = new() { AscendingReach = rangeV0v_V2a_asc, DescendingReach = rangeV0v_V2a_desc, MinReach = rangeMin, Weight = V0v_V2a_syn_weight };
            PoolToPoolChemJunction(L_V0v, R_V2a, cr, GluSynapse, span);
            PoolToPoolChemJunction(R_V0v, L_V2a, cr, GluSynapse, span);


            //Ipsilateral cholinergic projections from MN to Muscle cells
            SynapseParameters AChSynapse = new() { TauD = taud, TauR = taur, VTh = vth, E_rev = E_ach, G_syn = g_syn };
            cr = new() { AscendingReach = rangeMN_Muscle, DescendingReach = rangeMN_Muscle, MinReach = -1, Weight = MN_Muscle_syn_weight, FixedDuration_ms = 10 };
            PoolToPoolChemJunction(L_MN, L_Muscle, cr, AChSynapse);
            PoolToPoolChemJunction(R_MN, R_Muscle, cr, AChSynapse);


            //MN to V0d gap junctions
            cr = new() { AscendingReach = rangeMN_V0d, DescendingReach = rangeMN_V0d, MinReach = -1, Weight = MN_V0d_gap_weight };
            PoolToPoolGapJunction(L_MN, L_V0d, cr);
            PoolToPoolGapJunction(R_MN, R_V0d, cr);

            //V0d to V0d gap junctions
            cr = new() { AscendingReach = rangeV0d_V0d, DescendingReach = rangeV0d_V0d, MinReach = rangeMin, Weight = V0d_V0d_gap_weight };
            PoolToPoolGapJunction(L_V0d, L_V0d, cr);
            PoolToPoolGapJunction(R_V0d, R_V0d, cr);

            //Contralateral glycinergic projections from V0d to MN 
            cr = new() { AscendingReach = rangeV0d_MN, DescendingReach = rangeV0d_MN, MinReach = rangeMin, Weight = V0d_MN_syn_weight };
            PoolToPoolChemJunction(L_V0d, R_MN, cr, GlySynapse, span);
            PoolToPoolChemJunction(R_V0d, L_MN, cr, GlySynapse, span);


            //Contralateral glycinergic projections from V0d to V2a 
            cr = new() { AscendingReach = rangeV0d_V2a, DescendingReach = rangeV0d_V2a, MinReach = rangeMin, Weight = V0d_V2a_syn_weight };
            PoolToPoolChemJunction(L_V0d, R_V2a, cr, GlySynapse, span);
            PoolToPoolChemJunction(R_V0d, L_V2a, cr, GlySynapse, span);
        }

        public override Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = base.GetParameters();

            paramDict.Add("CellNum.nV0d", nV0d);

            paramDict.Add("Weight.MN_V0d_gap_weight", MN_V0d_gap_weight);
            paramDict.Add("Weight.V0d_V0d_gap_weight", V0d_V0d_gap_weight);
            paramDict.Add("Weight.V0d_MN_syn_weight", V0d_MN_syn_weight);
            paramDict.Add("Weight.V0d_V2a_syn_weight", V0d_V2a_syn_weight);

            paramDict.Add("Range.rangeMN_V0d", rangeMN_V0d);
            paramDict.Add("Range.rangeV0d_MN", rangeV0d_MN);
            paramDict.Add("Range.rangeV0d_V0d", rangeV0d_V0d);
            paramDict.Add("Range.rangeV0d_V2a", rangeV0d_V2a);
            return paramDict;
        }

        public override void SetParameters(Dictionary<string, object> paramExternal)
        {
            base.SetParameters(paramExternal);

            nV0d = paramExternal.Read("CellNum.nV0d", nV0d);

            MN_V0d_gap_weight = paramExternal.Read("Weight.MN_V0d_gap_weight", MN_V0d_gap_weight);
            V0d_V0d_gap_weight = paramExternal.Read("Weight.V0d_V0d_gap_weight", V0d_V0d_gap_weight);
            V0d_MN_syn_weight = paramExternal.Read("Weight.V0d_MN_syn_weight", V0d_MN_syn_weight);
            V0d_V2a_syn_weight = paramExternal.Read("Weight.V0d_V2a_syn_weight", V0d_V2a_syn_weight);

            rangeMN_V0d = paramExternal.Read("Range.rangeMN_V0d", rangeMN_V0d);
            rangeV0d_MN = paramExternal.Read("Range.rangeV0d_MN", rangeV0d_MN);
            rangeV0d_V0d = paramExternal.Read("Range.rangeV0d_V0d", rangeV0d_V0d);
            rangeV0d_V2a = paramExternal.Read("Range.rangeV0d_V2a", rangeV0d_V2a);
        }
    }
}
