using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Junction;
using SiliFish.ModelUnits.Parameters;
using SiliFish.ModelUnits.Stim;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Cells
{
    [JsonDerivedType(typeof(Cell), typeDiscriminator: "cell")]
    [JsonDerivedType(typeof(Neuron), typeDiscriminator: "neuron")]
    [JsonDerivedType(typeof(MuscleCell), typeDiscriminator: "musclecell")]
    public class Cell : ModelUnitBase
    {
        public CellCoreUnit Core { get; set; }
        public CellPool CellPool;
        public string CellGroup { get; set; }
        public int Sequence { get; set; }
        public int Somite { get; set; } = -1;
        
        [JsonPropertyOrder(3)]
        public Stimuli Stimuli { get; set; } = new();

        #region Position Members and Properties
        /*public FrontalPlane PositionDorsalVentral { get; set; } = FrontalPlane.NotSet;
        public TransversePlane PositionAnteriorPosterior { get; set; } = TransversePlane.NotSet;
        */
        public SagittalPlane PositionLeftRight { get; set; } = SagittalPlane.Both;

        public Coordinate coordinate;
        public double X { get => coordinate.X; set => coordinate.X = value; }
        public double Y { get => coordinate.Y; set => coordinate.Y = value; }
        public double Z { get => coordinate.Z; set => coordinate.Z = value; }
        #endregion

        #region Connection Properties
        [JsonPropertyOrder(1)]
        public List<GapJunction> LeavingGapJunctions
        {
            get
            {
                return GapJunctions.Where(jnc => jnc.Cell1 == this).ToList();
            }
            set => GapJunctions = value;
        }
        [JsonIgnore]
        public List<GapJunction> GapJunctions { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<JunctionBase> Projections
        {
            get
            {
                return GapJunctions;
            }
        }

        #endregion

        #region Simulation Values
        [JsonIgnore]
        public double[] V; //Membrane potential vector
        #endregion
        [JsonIgnore]
        public virtual double RestingMembranePotential { get { throw new NotImplementedException(); } }

        public string Name { get { return CellGroup + "_" + Sequence.ToString(); } set { } }
        public override string ID
        {
            get
            {
                string s = Somite > 0 ? "_" + Somite.ToString() : "";
                return $"{Position}_{CellGroup}{s}_{Sequence}";
            }
        }
        public string Position
        {
            get
            {
                string FTS =
                    /*(PositionDorsalVentral == FrontalPlane.Ventral ? "V" : PositionDorsalVentral == FrontalPlane.Dorsal ? "D" : "") +
                    (PositionAnteriorPosterior == TransversePlane.Posterior ? "P" : PositionAnteriorPosterior == TransversePlane.Anterior ? "A" : PositionAnteriorPosterior == TransversePlane.Central ? "C" : "") +
                    */
                    (PositionLeftRight == SagittalPlane.Left ? "L" : PositionLeftRight == SagittalPlane.Right ? "R" : "");
                return FTS;
            }
        }

        public double ConductionVelocity { get; set; }


        public Cell()
        {
        }

        public override string ToString()
        {
            return ID;
        }
        public virtual void LinkObjects(RunningModel model, CellPool pool)
        {
            CellPool = pool;
            Core = CellCoreUnit.CreateCore(Core.CoreType, Core.Parameters);
            foreach (GapJunction jnc in GapJunctions.Where(j => j.Cell1 == null))//To prevent double linking
            {
                jnc.Cell1 = this;
                jnc.LinkObjects(model);
            }
        }

        #region Sort Functions
        public virtual void SortJunctions()
        {
            GapJunctions.Sort();
        }
        public virtual void SortJunctionsBySource()
        {
            GapJunctions = GapJunctions.OrderBy(jnc => jnc.Cell1.ID).ToList();
        }
        public virtual void SortJunctionsByTarget()
        {
            GapJunctions = GapJunctions.OrderBy(jnc => jnc.Cell2.ID).ToList();
        }

        #endregion              

        #region Stimulus Functions
        public double GetStimulus(int timeIndex, Random rand)
        {
            return Stimuli.GenerateStimulus(timeIndex, rand);
        }

        public void AddStimulus(Stimulus stim)
        {
            Stimuli.Add(stim);
        }
        public void RemoveStimulus(Stimulus stim)
        {
            Stimuli.Remove(stim);
        }
        #endregion

        #region Connection Functions
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


        #endregion

        #region RunTime
        public virtual void InitDataVectors(int nmax)
        {
            V = new double[nmax];
            Stimuli.InitDataVectors(nmax);
            foreach (GapJunction jnc in GapJunctions)
                jnc.InitDataVectors(nmax);
        }
        internal bool IsAlive(int timepoint)
        {
            double t_ms = RunParam.GetTimeOfIndex(timepoint);
            return TimeLine_ms?.IsActive(t_ms) ?? true;
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
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }
        public virtual void CalculateMembranePotential(int t)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Simulation Stats
        public double MinStimulusValue() { return Stimuli.MinValue; }
        public double MaxStimulusValue() { return Stimuli.MaxValue; }
        public double MinPotentialValue(int iStart = 0, int iEnd = -1)
        {
            return V?.MinValue(iStart, iEnd) ?? 0;
        }
        public double MaxPotentialValue(int iStart = 0, int iEnd = -1)
        {
            return V?.MaxValue(iStart, iEnd) ?? 0;
        }
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

        public virtual (Dictionary<string, Color>, Dictionary<string, List<double>>) GetGapCurrents()
        {
            Dictionary<string, Color> colors = new();
            Dictionary<string, List<double>> AffarentCurrents = new();
            foreach (GapJunction jnc in GapJunctions.Where(j => j.Cell2 == this))
            {
                colors.TryAdd(jnc.Cell1.ID, jnc.Cell1.CellPool.Color);
                AffarentCurrents.AddObject(jnc.Cell1.ID, jnc.InputCurrent.ToList());
            }
            foreach (GapJunction jnc in GapJunctions.Where(j => j.Cell1 == this))
            {
                colors.TryAdd(jnc.Cell2.ID, jnc.Cell2.CellPool.Color);
                AffarentCurrents.AddObject(jnc.Cell2.ID, jnc.InputCurrent.Select(d => -d).ToList());
            }
            return (colors, AffarentCurrents);
        }
        public virtual (Dictionary<string, Color>, Dictionary<string, List<double>>) GetIncomingSynapticCurrents()
        {
            throw new NotImplementedException();
        }
        public virtual (Dictionary<string, Color>, Dictionary<string, List<double>>) GetOutgoingSynapticCurrents()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}