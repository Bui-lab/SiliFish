using SiliFish.Definitions;
using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Stim
{
    public class Stimuli
    {
        public List<Stimulus> ListOfStimulus { get; set; } = [];

        [JsonIgnore]
        public double MinValue { get { return ListOfStimulus.Count != 0 ? ListOfStimulus.Min(s => s.MinValue) : 0; } }
        [JsonIgnore]
        public double MaxValue { get { return ListOfStimulus.Count != 0 ? ListOfStimulus.Max(s => s.MaxValue) : 0; } }
        [JsonIgnore]
        public bool HasStimulus { get { return ListOfStimulus.Count != 0; } }
        public Stimuli()
        {
        }

        public double GetStimulus(int timeIndex)
        {
            return ListOfStimulus.Where(s => s.Active).Any() ? ListOfStimulus.Where(s => s.Active).Sum(s => s[timeIndex]) : 0;
        }

        public double[] GetStimulusArray(int nmax)
        {
            double[] ret = new double[nmax];
            foreach (Stimulus s in ListOfStimulus.Where(s => s.Active))
                ret = ret.SumUp(s.GetValues(nmax));
            return ret;
        }
        public virtual void InitForSimulation(RunParam runParam, Random rand)
        {
            foreach (Stimulus s in ListOfStimulus.Where(s => s.Active))
                s.InitForSimulation(runParam, rand);
        }
        public void Add(Stimulus stim)
        {
            if (stim == null)
                return;
            if (!ListOfStimulus.Contains(stim))
                ListOfStimulus.Add(stim);
        }
        public void Remove(Stimulus stim)
        {
            if (stim == null || !ListOfStimulus.Contains(stim))
                return;
            ListOfStimulus.Remove(stim);
        }

        public void Clear()
        {
            ListOfStimulus?.Clear();
        }

        internal void Sort()
        {
            ListOfStimulus.Sort();
        }
    }
}
