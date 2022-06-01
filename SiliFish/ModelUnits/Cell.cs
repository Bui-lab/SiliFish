using System;
using System.Collections.Generic;
using System.Linq;
using SiliFish.DataTypes;
using SiliFish.Extensions;

namespace SiliFish.ModelUnits
{
    public class Cell
    {
        public CellPool CellPool;
        public string CellGroup { get; set; }
        public int Sequence { get; set; }
        public Stimulus Stimulus = null;
        public FrontalPlane PositionDorsalVentral = FrontalPlane.NotSet;
        public TransversePlane PositionAnteriorPosterior = TransversePlane.NotSet;
        public SagittalPlane PositionLeftRight = SagittalPlane.NotSet;

        public Coordinate coordinate;
        public double X { get { return coordinate.X; } set { } }
        public double Y { get { return coordinate.Y; } set { } }
        public double Z { get { return coordinate.Z; } set { } }

        public double[] V; //Membrane potential vector

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
        public string ID { get { return Position + "_" + CellGroup + "_" + Sequence.ToString(); } set { } }
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


        public virtual void AddChemicalJunction(ChemicalJunction jnc)
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
    }

    public class Neuron : Cell
    {

        readonly Izhikevich_9P Core;
        public double u; //keeps the current u value
        public List<GapJunction> GapJunctions;
        public List<ChemicalJunction> Terminals; //keeps the list of all synapses the current cells extends to
        public List<ChemicalJunction> Synapses; //keeps the list of all synapses targeting the current cell

        /// <summary>
        /// Used as a template
        /// </summary>
        public Neuron(string group, int seq, double cv, TimeLine timeline = null)
        {
            CellGroup = group;
            Sequence = seq;
            Core = new Izhikevich_9P(null, 0, 0);
            GapJunctions = new List<GapJunction>();
            Synapses = new List<ChemicalJunction>();
            Terminals = new List<ChemicalJunction>();
            TimeLine = timeline;
            ConductionVelocity = cv;
        }
        public Neuron(CellPoolTemplate cellTemp, int seq, double cv)
            :this(cellTemp.CellGroup, seq, cv, cellTemp.TimeLine)
        {
            Parameters = cellTemp.Parameters;            
        }

        /// <summary>
        /// neuron constructor called from predefined models
        /// </summary>
        public Neuron(string group, int seq, MembraneDynamics dyn, double init_v, double init_u,
            Coordinate coor, double cv, Stimulus stim = null, TimeLine timeline = null)
        {
            CellGroup = group;
            Sequence = seq;
            PositionLeftRight = coor.Y < 0 ? SagittalPlane.Left : SagittalPlane.Right;
            Core = new Izhikevich_9P(dyn, init_v, init_u);
            coordinate = coor;
            Stimulus = stim;
            GapJunctions = new List<GapJunction>();
            Synapses = new List<ChemicalJunction>();
            Terminals = new List<ChemicalJunction>();
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

        public ChemicalJunction CreateChemicalJunction(Cell postCell, SynapseParameters param, double conductance, DistanceMode distanceMode)
        {
            ChemicalJunction jnc = new(this, postCell, param, conductance, distanceMode);
            if (jnc == null) return null;
            Terminals.Add(jnc);
            postCell.AddChemicalJunction(jnc);
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
        public override void AddChemicalJunction(ChemicalJunction jnc)
        {
            Synapses.Add(jnc);
        }

        public void InitVectors(int nmax)
        {
            V = new double[nmax];
            bool spike = false;
            V[0] = Core.GetNextVal(0, ref spike);
            foreach (ChemicalJunction jnc in this.Synapses)
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

        public double CalculateRheoBase(double maxI = 100, double sensitivity = 0.001, int infinity = 300)//SOURCE: https://en.wikipedia.org/wiki/Rheobase
        {

            return Core.CalculateRheoBase(maxI, sensitivity, infinity);
        }


        public override string GetInstanceParams()
        {
            return Core.GetInstanceParams();
        }
    }

    public class MuscleCell : Cell
    {


        readonly Leaky_Integrator Core;
        public double R { get { return Core.R; } set { } }
        public List<ChemicalJunction> EndPlates; //keeps the list of all synapses targeting the current cell

        /// <summary>
        /// Used as a template
        /// </summary>

        public MuscleCell(string group, int seq, TimeLine timeline = null)
        {
            CellGroup = group;
            Sequence = seq;
            Core = new Leaky_Integrator(0, 0, 0);
            EndPlates = new List<ChemicalJunction>();
            TimeLine = timeline;
        }
        public MuscleCell(CellPoolTemplate cellTemp, int seq)
            : this(cellTemp.CellGroup, seq, cellTemp.TimeLine)
        {
            Parameters = cellTemp.Parameters;
        }
        public MuscleCell(string group, int seq, double R, double C, double init_v, Coordinate coor, TimeLine timeline = null)
        {
            CellGroup = group;
            Sequence = seq;
            PositionLeftRight = coor.Y < 0 ? SagittalPlane.Left : SagittalPlane.Right;
            Core = new Leaky_Integrator(R, C, init_v);
            EndPlates = new List<ChemicalJunction>();
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
        
        public override void AddChemicalJunction(ChemicalJunction jnc)
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
            foreach (ChemicalJunction jnc in this.EndPlates)
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

        public override string GetInstanceParams()
        {
            return Core.GetInstanceParams();
        }

    }


}
