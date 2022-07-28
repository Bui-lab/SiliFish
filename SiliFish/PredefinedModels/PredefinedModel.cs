using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliFish.Extensions;

namespace SiliFish.PredefinedModels
{
    public class PredefinedModel : SwimmingModel
    {
        protected StimulusMode stim_mode = StimulusMode.Step;

        protected int tStimStart_ms = 0; //in ms;

        protected double stim_value1 = 8; //stimulus value if step current, mean value if gaussian, start value if ramp
        protected double stim_value2 = 8; //obselete if step current, SD value if gaussian, end value if ramp

        protected double sigma_range = 0; //a variance for gaussian randomization
        protected double sigma_chem = 0; //a variance for gaussian randomization
        protected double sigma_gap = 0; //a variance for gaussian randomization

        protected virtual double GetSynWeightNoiseMultiplier()
        {
            if (sigma_chem > 0)
                return rand.Gauss(1, this.sigma_chem, 0, sigma_chem * 10);
            return 1;
        }
        protected virtual double GetRangeNoiseMultiplier()
        {
            if (sigma_range > 0)
                return rand.Gauss(1, this.sigma_range, 0, sigma_range * 10);
            return 1;
        }

        protected virtual double GetGapWeightNoiseMultiplier()
        {
            if (sigma_gap > 0)
                return rand.Gauss(1, this.sigma_gap, 0, sigma_gap * 10);
            return 1;
        }

        public override Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = base.GetParameters();

            paramDict.Add("Dynamic.Stimulus Start (ms)", tStimStart_ms);
            paramDict.Add("Dynamic.Stimulus Value 1", stim_value1);
            paramDict.Add("Dynamic.Stimulus Value 2", stim_value2);
            paramDict.Add("Variability.Sigma Range", sigma_range);
            paramDict.Add("Variability.Sigma Gap", sigma_gap);
            paramDict.Add("Variability.Sigma Chem", sigma_chem);

            return paramDict;
        }

        public override Dictionary<string, object> GetParameterDesc()
        {
            Dictionary<string, object> paramDescDict = base.GetParameterDesc();
            paramDescDict.Add("Dynamic.Stimulus Value 1",
                "Stimulus value in case of 'step' stimulus is applied\r\n" +
                "Initial value for 'ramp' stimulus\r\n" +
                "Mean value for 'Gaussian' stimulus");
            paramDescDict.Add("Dynamic.Stimulus Value 2",
                "Obsolete in case of 'step' stimulus is applied\r\n" +
                "Final value for 'ramp' stimulus\r\n" +
                "Standard deviation for 'Gaussian' stimulus");

            paramDescDict.Add("Variability.Sigma Range", "Standard deviation noise that will be added to range parameters.");
            paramDescDict.Add("Variability.Sigma Gap", "Standard deviation noise that will be added to gap junction parameters.");
            paramDescDict.Add("Variability.Sigma Chem", "Standard deviation noise that will be added to synapse parameters.");

            return paramDescDict;
        }
        public override void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            base.SetParameters(paramExternal);
            tStimStart_ms = paramExternal.Read("Dynamic.Stimulus Start (ms)", tStimStart_ms);
            stim_value1 = paramExternal.Read("Dynamic.Stimulus Value 1", stim_value1);
            stim_value2 = paramExternal.Read("Dynamic.Stimulus Value 2", stim_value2);
            sigma_range = paramExternal.Read("Variability.Sigma Range", sigma_range);
            sigma_gap = paramExternal.Read("Variability.Sigma Gap", sigma_gap);
            sigma_chem = paramExternal.Read("Variability.Sigma Chem", sigma_chem);
        }

        public void SetConstants(double stim0 = 8, double sigma_range = 0, double sigma_chem = 0, double sigma_gap = 0, double E_glu = 0, double E_gly = -70, double dt = 0.1, double cv = 0.55)
        {
            this.stim_value1 = stim0;
            this.sigma_range = sigma_range;
            this.sigma_gap = sigma_gap;
            this.sigma_chem = sigma_chem;
            this.E_glu = E_glu;
            this.E_gly = E_gly;
            runParam.dt = 
                RunParam.static_dt  = dt;
            this.cv = cv;
        }

        public void SetStimulusMode(StimulusMode stimMode)
        {
            stim_mode = stimMode;
        }
}
}
