using SiliFish.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SiliFish.ModelUnits.Parameters
{
    public class KinemParam
    {
        [Description("Damping Coefficient"), DisplayName("Damping Coef"), Category("Animation")]
        public double Zeta { get; set; } = 3.0; //#damping constant , high zeta =0.5/ low = 0.1

        [Description("Natural oscillation frequency"), DisplayName("w0"), Category("Animation")]
        public double w0 { get; set; } = 2.5; //20Hz = 125.6

        [Description("If set to false, membrane potential values are used for animation.") , 
            DisplayName("Use Muscle Tension"), Category("Animation")]
        public bool UseMuscleTension { get; set; } = false;

        [Description("Obsolete if 'Tension' values are used for animation." +
            "If non-zero, (α + β * R) is used as 'Conversion Coefficient')"), 
            DisplayName("Alpha"), Category("Animation")]
        public double Alpha { get; set; } = 0;

        [Description("Obsolete if 'Tension' values are used for animation." +
            "If non-zero, (α + β * R) is used as 'Conversion Coefficient')"), 
            DisplayName("Beta"), Category("Animation")]
        public double Beta { get; set; } = 0;

        [Description("Obsolete if 'Tension' values are used for animation." +
            "Coefficient to convert membrane potential to driving force for the oscillation"), DisplayName("Conversion Coef"), Category("Animation")]
        public double ConvCoef { get; set; } = 0.1;

        [Description("The distance considered as a move from the center line."), DisplayName("Boundary"), Category("Swim Statistics")]
        public double Boundary { get; set; } = 0.5;

        [Description("The time range that will be looked ahead to detect motion."), DisplayName("Delay"), Category("Swim Statistics")]
        public int Delay { get; set; } = 1000;

        [Description("In ms. The duration required considered to be a burst break."), DisplayName("Burst Break"), Category("MN based kinematics")]
        public int BurstBreak { get; set; } = 10;

        [Description("In ms. The duration required considered to be an episode break."), DisplayName("Episode Break"), Category("MN based kinematics")]
        public int EpisodeBreak { get; set; } = 100;

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
            ConvCoef = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Conversion Coef", ConvCoef);
            Alpha = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Alpha", Alpha);
            Beta = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Beta", Beta);
            Boundary = paramExternal.ReadDoubleAndRemoveKey("Kinematics.Boundary", Boundary);
            Delay = paramExternal.ReadIntegerAndRemoveKey("Kinematics.Delay", Delay);
            return paramExternal;
        }

        public string GetAnimationDetails()
        {
            string details = $"Damping coef: {Zeta}\r\n" +
                $"w0: {w0:0.###}\r\n" +
                $"Conv. Coef: {ConvCoef:0.###}\r\n" +
                $"Alpha {Alpha:0.###}\r\n" +
                $"Beta {Beta:0.###}";
            return details;
        }
    }
}
