using System;
using System.Collections.Generic;
using System.Linq;
using SiliFish.DataTypes;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;

namespace SiliFish.ModelUnits
{
    public class Cell
    {
        public CellPool CellPool;
        public string CellGroup { get; set; }
        public int Sequence { get; set; }
        public int Somite { get; set; }
        public Stimulus Stimulus = null;
        public FrontalPlane PositionDorsalVentral = FrontalPlane.NotSet;
        public TransversePlane PositionAnteriorPosterior = TransversePlane.NotSet;
        public SagittalPlane PositionLeftRight = SagittalPlane.NotSet;

        public Coordinate coordinate;
        public double X { get { return coordinate.X; } set { } }
        public double Y { get { return coordinate.Y; } set { } }
        public double Z { get { return coordinate.Z; } set { } }

        public double[] V; //Membrane potential vector

        public double MinStimulusValue { get { return Stimulus?.MinValue ?? 0; } }
        public double MaxStimulusValue { get { return Stimulus?.MaxValue ?? 0; } }
        public double MinPotentialValue { get { return V?.Min() ?? 0; } }
        public double MaxPotentialValue { get { return V?.Max() ?? 0; } }

        public virtual double MinCurrentValue { get { throw (new NotImplementedException()); } }
        public virtual double MaxCurrentValue { get { throw (new NotImplementedException()); } }


        protected TimeLine TimeLine = null;
        internal bool IsAlive(int time)
        {
            return TimeLine?.IsActive(time) ?? true;
        }
        public double GetStimulus(int t, Random rand)
        {
            return Stimulus?.getStimulus(t, rand) ?? 0;
        }
        public virtual Dictionary<string, object> Parameters
        {
            get { throw (new NotImplementedException()); }
            set { throw (new NotImplementedException()); }
        }
        public virtual (double[], double[]) DynamicsTest(double[] I)
        {
            throw new NotImplementedException();
        }

        public string Name { get { return CellGroup + "_" + Sequence.ToString(); } set { } }
        public string ID
        {
            get
            {
                string s = Somite >= 0 ? "_" + Somite.ToString() : "";
                return $"{Position}_{CellGroup}{s}_{Sequence}";
            }
            set { }
        }
        public string Position
        {
            get
            {
                string FTS =
                    (PositionDorsalVentral == FrontalPlane.Ventral ? "V" : PositionDorsalVentral == FrontalPlane.Dorsal ? "D" : "") +
                    (PositionAnteriorPosterior == TransversePlane.Posterior ? "P" : PositionAnteriorPosterior == TransversePlane.Anterior ? "A" : PositionAnteriorPosterior == TransversePlane.Central ? "C" : "") +
                    (PositionLeftRight == SagittalPlane.Left ? "L" : PositionLeftRight == SagittalPlane.Right ? "R" : "")
                    ;
                return FTS;
            }
        }

        public double ConductionVelocity { get; set; }
        public Cell()
        {
        }


        public virtual void AddChemicalSynapse(ChemicalSynapse jnc)
        {
            throw (new NotImplementedException());
        }

        public virtual (double, double) GetConnectionRange()
        {
            throw (new NotImplementedException());
        }

        public virtual string GetInstanceParams()
        {
            throw new NotImplementedException();
        }

        public virtual void CalculateMembranePotential(int t)
        {
            throw new NotImplementedException();
        }

    }

    public class Neuron : Cell
    {

        readonly Izhikevich_9P Core;
        public double u; //keeps the current u value
        public List<GapJunction> GapJunctions;
        public List<ChemicalSynapse> Terminals; //keeps the list of all synapses the current cells extends to
        public List<ChemicalSynapse> Synapses; //keeps the list of all synapses targeting the current cell


        public override double MinCurrentValue
        {
            get
            {
                double cur1 = GapJunctions != null && GapJunctions.Any() ? GapJunctions.Min(jnc => jnc.InputCurrent.Min()) : 0;
                double cur2 = Terminals != null && Terminals.Any() ? Terminals.Min(jnc => jnc.InputCurrent.Min()) : 0;
                double cur3 = Synapses != null && Synapses.Any() ? Synapses.Min(jnc => jnc.InputCurrent.Min()) : 0;
                return Math.Min(cur1, Math.Min(cur2, cur3));
            }
        }
        public override double MaxCurrentValue
        {
            get
            {
                double cur1 = GapJunctions!=null && GapJunctions.Any() ? GapJunctions.Max(jnc => jnc.InputCurrent.Max()) : 0;
                double cur2 = Terminals != null && Terminals.Any() ? Terminals.Max(jnc => jnc.InputCurrent.Max()) : 0;
                double cur3 = Synapses != null && Synapses.Any() ? Synapses.Max(jnc => jnc.InputCurrent.Max()) : 0;
                return Math.Max(cur1, Math.Max(cur2, cur3));
            }
        }

        /// <summary>
        /// Used as a template
        /// </summary>
        public Neuron(string group, int somite, int seq, double cv, TimeLine timeline = null)
        {
            CellGroup = group;
            Somite = somite;
            Sequence = seq;
            Core = new Izhikevich_9P(null, 0, 0);
            GapJunctions = new List<GapJunction>();
            Synapses = new List<ChemicalSynapse>();
            Terminals = new List<ChemicalSynapse>();
            TimeLine = timeline;
            ConductionVelocity = cv;
        }
        public Neuron(CellPoolTemplate cellTemp, int somite, int seq, double cv)
            :this(cellTemp.CellGroup, somite, seq, cv, cellTemp.TimeLine)
        {
            Parameters = cellTemp.Parameters;            
        }

        /// <summary>
        /// neuron constructor called from predefined models
        /// </summary>
        public Neuron(string group, int seq, MembraneDynamics dyn, double sigma_dyn, double init_v, double init_u,
            Coordinate coor, double cv, Stimulus stim = null, TimeLine timeline = null)
        {
            CellGroup = group;
            Sequence = seq;
            PositionLeftRight = coor.Y < 0 ? SagittalPlane.Left : SagittalPlane.Right;
            if (sigma_dyn > 0)
                dyn = dyn.RandomizedDynamics(sigma_dyn);

            Core = new Izhikevich_9P(dyn, init_v, init_u);
            coordinate = coor;
            Stimulus = stim;
            GapJunctions = new List<GapJunction>();
            Synapses = new List<ChemicalSynapse>();
            Terminals = new List<ChemicalSynapse>();
            TimeLine = timeline;
            ConductionVelocity = cv;
        }

        public override Dictionary<string, object> Parameters
        {
            get { return Core.GetParameters(); }
            set
            {
                if (value == null || value.Count == 0)
                    return;
                Core.SetParameters(value);
            }
        }
        public GapJunction CreateGapJunction(Neuron n2, double weight, DistanceMode distanceMode)
        {
            GapJunction jnc = new(weight, this, n2, distanceMode);
            if (jnc == null) return null;
            GapJunctions.Add(jnc);
            n2.GapJunctions.Add(jnc);
            return jnc;
        }

        public ChemicalSynapse CreateChemicalSynapse(Cell postCell, SynapseParameters param, double conductance, DistanceMode distanceMode)
        {
            ChemicalSynapse jnc = new(this, postCell, param, conductance, distanceMode);
            if (jnc == null) return null;
            Terminals.Add(jnc);
            postCell.AddChemicalSynapse(jnc);
            return jnc;
        }

        public override (double, double) GetConnectionRange()
        {
            double? maxWeight1 = GapJunctions.Select(j => j.Conductance).DefaultIfEmpty(0).Max();
            double? maxWeight2 = Synapses.Select(j => j.Conductance).DefaultIfEmpty(0).Max();
            double? minWeight1 = GapJunctions.Where(j => j.Conductance > 0).Select(j => j.Conductance).DefaultIfEmpty(999).Min();
            double? minWeight2 = Synapses.Where(j => j.Conductance > 0).Select(j => j.Conductance).DefaultIfEmpty(999).Min();
            double maxWeight = Math.Max(maxWeight1 ?? 0, maxWeight2 ?? 0);
            double minWeight = Math.Min(minWeight1 ?? 999, minWeight2 ?? 999);
            return (minWeight, maxWeight);
        }
        public override void AddChemicalSynapse(ChemicalSynapse jnc)
        {
            Synapses.Add(jnc);
        }

        public void InitVectors(int nmax)
        {
            V = new double[nmax];
            bool spike = false;
            V[0] = Core.GetNextVal(0, ref spike);
            foreach (ChemicalSynapse jnc in this.Synapses)
                jnc.InitVectors(nmax);
            foreach (GapJunction jnc in this.GapJunctions)
                jnc.InitVectors(nmax);
        }

        public void NextStep(int t, double stim)
        {
            bool spike = false;
            V[t] = Core.GetNextVal(stim, ref spike);
        }

        public override (double[], double[]) DynamicsTest(double[] I)
        {
            return Core.SolveODE(I);
        }

        public double CalculateRheoBase(double dt, double maxI = 100, double sensitivity = 0.001, int infinity = 300)//SOURCE: https://en.wikipedia.org/wiki/Rheobase
        {

            return Core.CalculateRheoBase(maxI, sensitivity, infinity, dt);
        }


        public override string GetInstanceParams()
        {
            return Core.GetInstanceParams();
        }

        public void CalculateNeuronalOutputs(int t)
        {
            try
            {
                foreach (ChemicalSynapse syn in Terminals)
                {
                    syn.NextStep(t);
                }

                foreach (GapJunction jnc in GapJunctions.Where(j => j.Cell1 == this)) //to prevent double call
                {
                    jnc.NextStep(t);
                }
            }
            catch (Exception ex)
            {
                SwimmingModel.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }

        public override void CalculateMembranePotential(int t)
        {
            try
            {
                double ISyn = 0, IGap = 0, stim = 0;
                if (IsAlive(t))
                {
                    foreach (ChemicalSynapse syn in Synapses)
                    {
                        ISyn += syn.GetSynapticCurrent(t);
                    }
                    foreach (GapJunction jnc in GapJunctions)
                    {
                        IGap += jnc.GetGapCurrent(this, t);
                    }
                    stim = GetStimulus(t, SwimmingModel.rand);
                }

                NextStep(t, stim + ISyn + IGap);
            }
            catch (Exception ex)
            {
                SwimmingModel.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

    }

    public class MuscleCell : Cell
    {


        readonly Leaky_Integrator Core;
        public double R { get { return Core.R; } set { } }
        public List<ChemicalSynapse> EndPlates; //keeps the list of all synapses targeting the current cell

        public override double MinCurrentValue
        {
            get
            {
                return EndPlates?.Min(jnc => jnc.InputCurrent.Min()) ?? 0;
            }
        }
        public override double MaxCurrentValue
        {
            get
            {
                return EndPlates?.Max(jnc => jnc.InputCurrent.Max()) ?? 0;
            }
        }
        /// <summary>
        /// Used as a template
        /// </summary>

        public MuscleCell(string group, int somite, int seq, TimeLine timeline = null)
        {
            CellGroup = group;
            Somite = somite;
            Sequence = seq;
            Core = new Leaky_Integrator(0, 0, 0);
            EndPlates = new List<ChemicalSynapse>();
            TimeLine = timeline;
        }
        public MuscleCell(CellPoolTemplate cellTemp, int somite, int seq)
            : this(cellTemp.CellGroup, somite, seq, cellTemp.TimeLine)
        {
            Parameters = cellTemp.Parameters;
        }
        public MuscleCell(string group, int seq, double R, double C, double init_v, double sigma_dyn, Coordinate coor, TimeLine timeline = null)
        {
            CellGroup = group;
            Sequence = seq;
            PositionLeftRight = coor.Y < 0 ? SagittalPlane.Left : SagittalPlane.Right;
            if (sigma_dyn > 0)
            {
                R *= SwimmingModel.rand.Gauss(1, sigma_dyn);
                C *= SwimmingModel.rand.Gauss(1, sigma_dyn);
                init_v *= SwimmingModel.rand.Gauss(1, sigma_dyn);
            }
            Core = new Leaky_Integrator(R, C, init_v);
            EndPlates = new List<ChemicalSynapse>();
            coordinate = coor;
            TimeLine = timeline;
        }

         public override Dictionary<string, object> Parameters
        {
            get { return Core.GetParameters(); }
            set
            {
                if (value == null || value.Count == 0)
                    return;
                Core.SetParameters(value);
            }
        }       
        
        public override void AddChemicalSynapse(ChemicalSynapse jnc)
        {
            EndPlates.Add(jnc);
        }

        public override (double, double) GetConnectionRange()
        {
            double maxWeight = EndPlates.Select(j => j.Conductance).DefaultIfEmpty(0).Max();
            double minWeight = EndPlates.Where(j => j.Conductance > 0).Select(j => j.Conductance).DefaultIfEmpty(999).Min();
            return (minWeight, maxWeight);
        }

        public void InitVectors(int nmax)
        {
            V = new double[nmax];
            V[0] = Core.GetNextVal(0);
            foreach (ChemicalSynapse jnc in this.EndPlates)
                jnc.InitVectors(nmax);
        }

        public void NextStep(int t, double stim)
        {
            V[t] = Core.GetNextVal(stim);
        }

        public override (double[], double[]) DynamicsTest(double[] I)
        {
            return (Core.SolveODE(I), null);
        }

        public override void CalculateMembranePotential(int t)
        {
            double ISyn = 0, stim = 0;
            if (IsAlive(t))
            {
                foreach (ChemicalSynapse syn in EndPlates)
                {
                    ISyn += syn.GetSynapticCurrent(t);
                }
                stim = GetStimulus(t, SwimmingModel.rand);
            }
            NextStep(t, stim + ISyn);
        }

        public override string GetInstanceParams()
        {
            return Core.GetInstanceParams();
        }

    }


}
