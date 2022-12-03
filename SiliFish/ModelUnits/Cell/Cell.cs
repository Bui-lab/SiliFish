using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits
{
    public class Cell
    {
        protected CellCoreUnit Core;
        public CellPool CellPool;

        public string CellGroup { get; set; }
        public int Sequence { get; set; }
        public int Somite { get; set; } = -1;
        public Stimuli Stimuli = new();
        public FrontalPlane PositionDorsalVentral = FrontalPlane.NotSet;
        public TransversePlane PositionAnteriorPosterior = TransversePlane.NotSet;
        public SagittalPlane PositionLeftRight = SagittalPlane.Both;

        public Coordinate coordinate;
        public double X { get { return coordinate.X; } set { } }
        public double Y { get { return coordinate.Y; } set { } }
        public double Z { get { return coordinate.Z; } set { } }

        public List<GapJunction> GapJunctions;

        [JsonIgnore]

        public double[] V; //Membrane potential vector
        [JsonIgnore]
        public double MinStimulusValue { get { return Stimuli.MinValue; } }
        [JsonIgnore]
        public double MaxStimulusValue { get { return Stimuli.MaxValue; } }

        public double MinPotentialValue(int iStart = 0, int iEnd = -1)
        {
            return V?.MinValue(iStart, iEnd) ?? 0;
        }
        public double MaxPotentialValue(int iStart = 0, int iEnd = -1)
        {
            return V?.MaxValue(iStart, iEnd) ?? 0;
        }

        [JsonIgnore]
        public virtual double RestingMembranePotential { get { throw new NotImplementedException(); } }

        public virtual List<int> GetSpikeIndices(int iStart = 0, int iEnd = -1)
        {
            throw new NotImplementedException();
        }
        public virtual double MinCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return GapJunctions != null && GapJunctions.Any() ? GapJunctions.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
        }
        public virtual double MaxCurrentValue(int iStart = 0, int iEnd = -1)
        {
            return GapJunctions != null && GapJunctions.Any() ? GapJunctions.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
        }


        protected TimeLine TimeLine_ms = null;
        internal bool IsAlive(int timepoint)
        {
            double t_ms = RunParam.GetTimeOfIndex(timepoint);
            return TimeLine_ms?.IsActive(t_ms) ?? true;
        }
        public double GetStimulus(int timeIndex, Random rand)
        {
            return Stimuli.GenerateStimulus(timeIndex, rand);
        }
        public virtual Dictionary<string, double> Parameters
        {
            get { throw (new NotImplementedException()); }
            set { throw (new NotImplementedException()); }
        }

        public string Name { get { return CellGroup + "_" + Sequence.ToString(); } set { } }
        public string ID
        {
            get
            {
                string s = Somite > 0 ? "_" + Somite.ToString() : "";
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

        public virtual void InitDataVectors(int nmax)
        {
            V = new double[nmax];
            Stimuli.InitDataVectors(nmax);
            foreach (GapJunction jnc in GapJunctions)
                jnc.InitDataVectors(nmax);
        }

        public GapJunction CreateGapJunction(Cell n2, double weight, DistanceMode distanceMode)
        {
            GapJunction jnc = new(weight, this, n2, distanceMode);
            if (jnc == null) return null;
            GapJunctions.Add(jnc);
            n2.GapJunctions.Add(jnc);
            return jnc;
        }

        public virtual void AddChemicalSynapse(ChemicalSynapse jnc)
        {
            throw (new NotImplementedException());
        }

        public virtual (double, double) GetConnectionRange()
        {
            double? maxWeight1 = GapJunctions.Select(j => j.Conductance).DefaultIfEmpty(0).Max();
            double? minWeight1 = GapJunctions.Where(j => j.Conductance > 0).Select(j => j.Conductance).DefaultIfEmpty(999).Min();
            return (minWeight1 ?? 0, maxWeight1 ?? 999);
        }
        public virtual void CalculateCellularOutputs(int t)
        {
            try
            {
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


        public virtual void CalculateMembranePotential(int t)
        {
            throw new NotImplementedException();
        }

    }

    public class Neuron : Cell
    {
        public List<ChemicalSynapse> Terminals; //keeps the list of all synapses the current cells extends to
        public List<ChemicalSynapse> Synapses; //keeps the list of all synapses targeting the current cell

        public override double RestingMembranePotential { get { return (Core?.Vr) ?? 0; } }
        /// <summary>
        /// Used as a template
        /// </summary>
        public Neuron(string coreType, string group, int somite, int seq, double cv, TimeLine timeline = null)
        {
            CellGroup = group;
            Somite = somite;
            Sequence = seq;
            Core = CellCoreUnit.CreateCore(coreType, null);
            GapJunctions = new List<GapJunction>();
            Synapses = new List<ChemicalSynapse>();
            Terminals = new List<ChemicalSynapse>();
            TimeLine_ms = timeline;
            ConductionVelocity = cv;
        }
        public Neuron(CellPoolTemplate cellTemp, int somite, int seq, double cv)
            : this(cellTemp.CoreType, cellTemp.CellGroup, somite, seq, cv, cellTemp.TimeLine_ms)
        {
        }

        /// <summary>
        /// neuron constructor called from hardcoded models
        /// </summary>
        public Neuron(string group, string coreType, int somite, int seq, Dictionary<string, double> coreParams, 
            Coordinate coor, double cv, Stimulus stim = null, TimeLine timeline = null)
        {
            CellGroup = group;
            Somite = somite;
            Sequence = seq;
            PositionLeftRight = coor.Y < 0 ? SagittalPlane.Left : SagittalPlane.Right;

            Core = CellCoreUnit.CreateCore(coreType, coreParams);

            coordinate = coor;
            if (stim != null)
                Stimuli.Add(stim);
            GapJunctions = new List<GapJunction>();
            Synapses = new List<ChemicalSynapse>();
            Terminals = new List<ChemicalSynapse>();
            TimeLine_ms = timeline;
            ConductionVelocity = cv;
        }

        public override double MinCurrentValue(int iStart = 0, int iEnd = -1)
        {
            double cur1 = base.MinCurrentValue(iStart, iEnd);
            double cur2 = Synapses != null && Synapses.Any() ? Synapses.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
            double cur3 = Terminals != null && Terminals.Any() ? Terminals.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
            return Math.Min(Math.Min(cur1, cur2), cur3);
        }
        public override double MaxCurrentValue(int iStart = 0, int iEnd = -1)
        {
            double cur1 = base.MaxCurrentValue(iStart, iEnd);
            double cur2 = Synapses != null && Synapses.Any() ? Synapses.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
            double cur3 = Terminals != null && Terminals.Any() ? Terminals.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
            return Math.Max(Math.Max(cur1, cur2), cur3);
        }

        public override List<int> GetSpikeIndices(int iStart = 0, int iEnd = -1)
        {
            return V.GetPeakIndices(Core.Vmax, iStart, iEnd);
        }

        public override Dictionary<string, double> Parameters
        {
            get { return Core.GetParameters(); }
            set
            {
                if (value == null || value.Count == 0)
                    return;
                Core.SetParameters(value);
            }
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
            (double minWeight1, double maxWeight1) = base.GetConnectionRange();
            double maxWeight2 = Synapses.Select(j => j.Conductance).DefaultIfEmpty(0).Max();
            double minWeight2 = Synapses.Where(j => j.Conductance > 0).Select(j => j.Conductance).DefaultIfEmpty(999).Min();
            double maxWeight = Math.Max(maxWeight1, maxWeight2);
            double minWeight = Math.Min(minWeight1, minWeight2);
            return (minWeight, maxWeight);
        }
        public override void AddChemicalSynapse(ChemicalSynapse jnc)
        {
            Synapses.Add(jnc);
        }

        public override void InitDataVectors(int nmax)
        {
            base.InitDataVectors(nmax);
            V[0] = Core.Vr;
            foreach (ChemicalSynapse jnc in this.Synapses)
                jnc.InitDataVectors(nmax);

        }

        public void NextStep(int t, double stim)
        {
            bool spike = false;
            V[t] = Core.GetNextVal(stim, ref spike);
        }

        public override void CalculateCellularOutputs(int t)
        {
            try
            {
                base.CalculateCellularOutputs(t);
                foreach (ChemicalSynapse syn in Terminals)
                {
                    syn.NextStep(t);
                }
            }
            catch (Exception ex)
            {
                SwimmingModel.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }

        public override void CalculateMembranePotential(int timeIndex)
        {
            try
            {
                double ISyn = 0, IGap = 0, stim = 0;
                if (IsAlive(timeIndex))
                {
                    foreach (ChemicalSynapse syn in Synapses)
                    {
                        ISyn += syn.GetSynapticCurrent(timeIndex);
                    }
                    foreach (GapJunction jnc in GapJunctions)
                    {
                        IGap += jnc.GetGapCurrent(this, timeIndex);
                    }
                    stim = GetStimulus(timeIndex, SwimmingModel.rand);
                }

                NextStep(timeIndex, stim + ISyn + IGap);
            }
            catch (Exception ex)
            {
                SwimmingModel.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

    }

    public class MuscleCell : Cell
    {
        public double R { get { return (Core as Leaky_Integrator).R; } set { } }
        [JsonIgnore]
        public double[] RelativeTension { get { return (Core as Leaky_Integrator).CalculateRelativeTension(V); } }
        [JsonIgnore]
        public double[] Tension { get { return (Core as Leaky_Integrator).CalculateTension(V); } }
        public override double RestingMembranePotential { get { return (Core?.Vr) ?? 0; } }

        public List<ChemicalSynapse> EndPlates; //keeps the list of all synapses targeting the current cell

        /// <summary>
        /// Used as a template
        /// </summary>
        public MuscleCell(string coreType, string group, int somite, int seq, TimeLine timeline = null)
        {
            CellGroup = group;
            Somite = somite;
            Sequence = seq;
            Core = CellCoreUnit.CreateCore(coreType, null);
            EndPlates = new List<ChemicalSynapse>();
            GapJunctions = new List<GapJunction>();
            TimeLine_ms = timeline;
        }
        public MuscleCell(CellPoolTemplate cellTemp, int somite, int seq)
            : this(cellTemp.CoreType, cellTemp.CellGroup, somite, seq, cellTemp.TimeLine_ms)
        {
        }

        /// <summary>
        /// Constructor called from predefined models
        /// </summary>
        public MuscleCell(string group, int seq, double R, double C, double Vr, double sigma_dyn, Coordinate coor, TimeLine timeline = null)
        {
            CellGroup = group;
            Sequence = seq;
            PositionLeftRight = coor.Y < 0 ? SagittalPlane.Left : SagittalPlane.Right;
            if (sigma_dyn > 0)
            {
                R *= SwimmingModel.rand.Gauss(1, sigma_dyn);
                C *= SwimmingModel.rand.Gauss(1, sigma_dyn);
            }
            Core = new Leaky_Integrator(R, C, Vr);
            EndPlates = new List<ChemicalSynapse>();
            GapJunctions = new List<GapJunction>();
            coordinate = coor;
            TimeLine_ms = timeline;
        }

        public override double MinCurrentValue(int iStart = 0, int iEnd = -1)
        {
            double cur1 = base.MinCurrentValue(iStart, iEnd);
            double cur2 = EndPlates?.Count > 0 ? EndPlates.Min(jnc => jnc.InputCurrent.MinValue(iStart, iEnd)) : 0;
            return Math.Min(cur1, cur2);
        }
        public override double MaxCurrentValue(int iStart = 0, int iEnd = -1)
        {
            double cur1 = base.MaxCurrentValue(iStart, iEnd);
            double cur2 = EndPlates?.Count > 0 ? EndPlates.Max(jnc => jnc.InputCurrent.MaxValue(iStart, iEnd)) : 0;
            return Math.Max(cur1, cur2);
        }

        public override List<int> GetSpikeIndices(int iStart = 0, int iEnd = -1)
        {
            return V.GetPeakIndices(Core.Vr * 1.1, iStart, iEnd);//URGENT this is a random multiplier 
        }

        public override Dictionary<string, double> Parameters
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
            (double minWeight1, double maxWeight1) = base.GetConnectionRange();
            double maxWeight2 = EndPlates.Select(j => j.Conductance).DefaultIfEmpty(0).Max();
            double minWeight2 = EndPlates.Where(j => j.Conductance > 0).Select(j => j.Conductance).DefaultIfEmpty(999).Min();

            double maxWeight = Math.Max(maxWeight1, maxWeight2);
            double minWeight = Math.Min(minWeight1, minWeight2);
            return (minWeight, maxWeight);
        }

        public override void InitDataVectors(int nmax)
        {
            base.InitDataVectors(nmax);
            V[0] = Core.Vr;
            foreach (ChemicalSynapse jnc in this.EndPlates)
                jnc.InitDataVectors(nmax);
        }

        public void NextStep(int t, double stim)
        {
            bool spike = false;
            V[t] = Core.GetNextVal(stim, ref spike);
        }

        public override void CalculateMembranePotential(int timeIndex)
        {
            double ISyn = 0, IGap = 0, stim = 0;
            if (IsAlive(timeIndex))
            {
                foreach (ChemicalSynapse syn in EndPlates)
                {
                    ISyn += syn.GetSynapticCurrent(timeIndex);
                }
                foreach (GapJunction jnc in GapJunctions)
                {
                    IGap += jnc.GetGapCurrent(this, timeIndex);
                }
                stim = GetStimulus(timeIndex, SwimmingModel.rand);
            }
            NextStep(timeIndex, stim + ISyn + IGap);
        }
    }


}