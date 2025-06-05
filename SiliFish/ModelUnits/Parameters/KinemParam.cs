using SiliFish.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SiliFish.ModelUnits.Parameters
{
    public class KinemParam
    {
        #region Swim Dynamics
        [Description("Damping Coefficient"), DisplayName("Damping Coef"), Category("Swim Dynamics")]
        public double Zeta { get; set; } = 3.0; //#damping constant , high zeta =0.5/ low = 0.1

        [Description("Natural oscillation frequency"), DisplayName("w0"), Category("Swim Dynamics")]
        public double w0 { get; set; } = 2.5; //20Hz = 125.6

        [Description("If set to false, membrane potential values are used for animation."),
            DisplayName("Use Muscle Tension"), Category("Swim Dynamics")]
        public bool UseMuscleTension { get; set; } = false;

        [Description("Used if 'Use Muscle Tension' is set to false. " +
            "If non-zero, (α + β * R) is used as 'Conversion Coefficient')"),
            DisplayName("Alpha"), Category("Swim Dynamics")]
        public double Alpha { get; set; } = 0;

        [Description("Used if 'Use Muscle Tension' is set to false. " +
            "If non-zero, (α + β * R) is used as 'Conversion Coefficient')"),
            DisplayName("Beta"), Category("Swim Dynamics")]
        public double Beta { get; set; } = 0;

        [Description("Used if 'Use Muscle Tension' is set to false. " +
            "Coefficient to convert membrane potential to driving force for the oscillation"), DisplayName("Potential Conversion Coef"), Category("Swim Dynamics")]
        public double ConvCoefPotential { get; set; } = 0.1;

        [Description("Used if 'Use Muscle Tension' is set to true. " +
            "Coefficient to convert muscle tension to driving force for the oscillation"), DisplayName("Tension Conversion Coef"), Category("Swim Dynamics")]
        public double ConvCoefTension { get; set; } = 0.1;

        [Description("The distance considered as a move from the center line. Used only for tail based episode calculation."),
            DisplayName("Boundary"), Category("Swim Dynamics")]
        public double Boundary { get; set; } = 0.5;

        //same as the time range that will be looked ahead to detect motion

        [Description("In ms. The duration required considered to be an episode break."), DisplayName("Episode Break"), Category("Swim Dynamics")]
        public int EpisodeBreak { get; set; } = 100;

        #endregion

        [Browsable(false)]
        public double ConvCoef => UseMuscleTension ? ConvCoefTension : ConvCoefPotential;

        public KinemParam() { }

        public KinemParam Clone()
        {
            return (KinemParam)MemberwiseClone();
        }
        public Dictionary<string, object> BackwardCompatibility(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || !paramExternal.Keys.Any(k => k.StartsWith("Kinematics.")))
                return paramExternal;
            Zeta = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Damping Coef", Zeta);
            w0 = paramExternal.ReadDoubleAndRemoveKey("Kinematics.w0", w0);
            ConvCoefPotential = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Potential Conversion Coef", ConvCoefPotential);
            ConvCoefTension = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Tension Conversion Coef", ConvCoefTension);
            Alpha = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Alpha", Alpha);
            Beta = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Beta", Beta);
            Boundary = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Boundary", Boundary);
            EpisodeBreak = paramExternal.ReadIntegerAndRemoveKey("Kinematics.Delay", EpisodeBreak);
            return paramExternal;
        }

        public string GetAnimationDetails()
        {
            double convCoef = UseMuscleTension ? ConvCoefTension : ConvCoefPotential;
            string details = $"Damping coef: {Zeta}; Natural Oscillation Freq: {w0:0.###}\r\n" +
                $"Conv. Coef: {convCoef:0.###}; Alpha {Alpha:0.###}; Beta {Beta:0.###}";
            return details;
        }

    }
}
