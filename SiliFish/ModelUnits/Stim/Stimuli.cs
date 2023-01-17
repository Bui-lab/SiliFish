using SiliFish.Extensions;
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
        public List<Stimulus> stimuli { get; set; }

        public Stimuli()
        {
            stimuli = new();
        }

        [JsonIgnore]
        public double MinValue { get { return stimuli.Any() ? stimuli.Min(s => s.MinValue) : 0; } }
        [JsonIgnore]
        public double MaxValue { get { return stimuli.Any() ? stimuli.Max(s => s.MaxValue) : 0; } }
        [JsonIgnore]
        public bool HasStimulus { get { return stimuli.Any(); } }
        public double GenerateStimulus(int timeIndex, Random rand)
        {
            return stimuli.Any() ? stimuli.Sum(s => s.generateStimulus(timeIndex, rand)) : 0;
        }

        public double GetStimulus(int timeIndex)
        {
            return stimuli.Any() ? stimuli.Sum(s => s[timeIndex]) : 0;
        }

        public double[] GetStimulusArray(int nmax)
        {
            double[] ret = new double[nmax];
            foreach (Stimulus s in stimuli)
                ret = ret.AddArray(s.getValues(nmax));
            return ret;
        }
        public virtual void InitDataVectors(int nmax)
        {
            foreach (Stimulus s in stimuli)
                s.InitDataVectors(nmax);
        }
        public void Add(Stimulus stim)
        {
            if (stim == null)
                return;
            stimuli.Add(stim);
        }
        public void Remove(Stimulus stim)
        {
            if (stim == null || !stimuli.Contains(stim))
                return;
            stimuli.Remove(stim);
        }
    }
}
