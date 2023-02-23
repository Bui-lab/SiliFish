using SiliFish.Extensions;
using SiliFish.ModelUnits.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.ModelUnits.Stim
{
    public class Stimuli
    {
        public List<Stimulus> ListOfStimulus { get; set; } = new();

        public Stimuli()
        {
        }

        [JsonIgnore]
        public double MinValue { get { return ListOfStimulus.Any() ? ListOfStimulus.Min(s => s.MinValue) : 0; } }
        [JsonIgnore]
        public double MaxValue { get { return ListOfStimulus.Any() ? ListOfStimulus.Max(s => s.MaxValue) : 0; } }
        [JsonIgnore]
        public bool HasStimulus { get { return ListOfStimulus.Any(); } }

        public double GetStimulus(int timeIndex)
        {
            return ListOfStimulus.Any() ? ListOfStimulus.Sum(s => s[timeIndex]) : 0;
        }

        public double[] GetStimulusArray(int nmax)
        {
            double[] ret = new double[nmax];
            foreach (Stimulus s in ListOfStimulus)
                ret = ret.AddArray(s.GetValues(nmax));
            return ret;
        }
        public virtual void InitForSimulation(RunParam runParam, Random rand)
        {
            foreach (Stimulus s in ListOfStimulus)
                s.InitForSimulation(runParam, rand);
        }
        public void Add(Stimulus stim)
        {
            if (stim == null)
                return;
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
