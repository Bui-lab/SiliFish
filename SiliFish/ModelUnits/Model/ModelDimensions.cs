using SiliFish.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SiliFish.ModelUnits.Model
{
    public class ModelDimensions
    {
        [DisplayName("Number of Somites"), Category("General")]
        public int NumberOfSomites { get; set; } = 0;

        [DisplayName("Rostral-Caudal"), Category("Supra Spinal")]
        public double SupraSpinalRostralCaudalDistance { get; set; }

        [DisplayName("Dorsal-Ventral"), Category("Supra Spinal")]
        public double SupraSpinalDorsalVentralDistance { get; set; }

        [DisplayName("Medial-Lateral"), Category("Supra Spinal")]
        public double SupraSpinalMedialLateralDistance { get; set; }

        [DisplayName("Rostral-Caudal"), Category("Spinal")]
        public double SpinalRostralCaudalDistance { get; set; }

        [DisplayName("Dorsal-Ventral"), Category("Spinal")]
        public double SpinalDorsalVentralDistance { get; set; }

        [DisplayName("Medial-Lateral"), Category("Spinal")]
        public double SpinalMedialLateralDistance { get; set; }

        [DisplayName("Body Position"), Category("Spinal")]
        public double SpinalBodyPosition { get; set; }

        [DisplayName("Medial-Lateral"), Category("Musculo-Skeletal")]
        public double BodyMedialLateralDistance { get; set; }

        [DisplayName("Dorsal-Ventral"), Category("Musculo-Skeletal")]
        public double BodyDorsalVentralDistance { get; set; }

        public Dictionary<string, object> BackwardCompatibility(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || !paramExternal.Keys.Any(k => k.StartsWith("General.")))
                return paramExternal;
            NumberOfSomites = paramExternal.ReadIntegerAndRemoveKey("General.NumberOfSomites", NumberOfSomites);
            SupraSpinalRostralCaudalDistance = paramExternal.ReadDoubleAndRemoveKey("General.SupraSpinalRostralCaudalDistance", SupraSpinalRostralCaudalDistance);
            SupraSpinalDorsalVentralDistance = paramExternal.ReadDoubleAndRemoveKey("General.SupraSpinalDorsalVentralDistance", SupraSpinalDorsalVentralDistance);
            SupraSpinalMedialLateralDistance = paramExternal.ReadDoubleAndRemoveKey("General.SupraSpinalMedialLateralDistance", SupraSpinalMedialLateralDistance);
            SpinalRostralCaudalDistance = paramExternal.ReadDoubleAndRemoveKey("General.SpinalRostralCaudalDistance", SpinalRostralCaudalDistance);
            SpinalDorsalVentralDistance = paramExternal.ReadDoubleAndRemoveKey("General.SpinalDorsalVentralDistance", SpinalDorsalVentralDistance);
            SpinalMedialLateralDistance = paramExternal.ReadDoubleAndRemoveKey("General.SpinalMedialLateralDistance", SpinalMedialLateralDistance);
            SpinalBodyPosition = paramExternal.ReadDoubleAndRemoveKey("General.SpinalBodyPosition", SpinalBodyPosition);
            BodyDorsalVentralDistance = paramExternal.ReadDoubleAndRemoveKey("General.BodyDorsalVentralDistance", BodyDorsalVentralDistance);
            BodyMedialLateralDistance = paramExternal.ReadDoubleAndRemoveKey("General.BodyMedialLateralDistance", BodyMedialLateralDistance);
            return paramExternal;
        }
    }
}
