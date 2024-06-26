﻿using SiliFish.DataTypes;
using SiliFish.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits.Architecture
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

        [JsonIgnore, Browsable(false)]
        public double SomiteLength => SpinalRostralCaudalDistance / NumberOfSomites;

        public ModelDimensions Clone()
        {
            return (ModelDimensions)MemberwiseClone();
        }
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

        public bool CheckConsistency(out string error)
        {
            error = "";

            if (BodyDorsalVentralDistance < SpinalBodyPosition + SpinalDorsalVentralDistance)
                error = "Musculoskeletal dorsal-ventral height has to be greater than spinal dorsal-ventral height + spinal body position\r\n";

            if (BodyMedialLateralDistance < SpinalMedialLateralDistance)
                error += "Musculoskeletal medial-lateral width has to be greater than spinal medial-lateral width";

            return string.IsNullOrEmpty(error);
        }

        public List<Difference> DiffersFrom(ModelDimensions other)
        {
            List<Difference> differences = [];
            if (NumberOfSomites != other.NumberOfSomites)
                differences.Add(new Difference("Number of somites", NumberOfSomites, other.NumberOfSomites));
            if (SupraSpinalRostralCaudalDistance != other.SupraSpinalRostralCaudalDistance)
                differences.Add(new Difference("Supra-spinal rostrocaudal distance", SupraSpinalRostralCaudalDistance, other.SupraSpinalRostralCaudalDistance));
            if (SupraSpinalDorsalVentralDistance != other.SupraSpinalDorsalVentralDistance)
                differences.Add(new Difference("Supra-spinal dorsoventral distance", SupraSpinalDorsalVentralDistance, other.SupraSpinalDorsalVentralDistance));
            if (SupraSpinalMedialLateralDistance != other.SupraSpinalMedialLateralDistance)
                differences.Add(new Difference("Supra-spinal mediolateral distance", SupraSpinalMedialLateralDistance, other.SupraSpinalMedialLateralDistance));
            if (SpinalRostralCaudalDistance != other.SpinalRostralCaudalDistance)
                differences.Add(new Difference("Spinal rostrocaudal distance", SpinalRostralCaudalDistance, other.SpinalRostralCaudalDistance));
            if (SpinalDorsalVentralDistance != other.SpinalDorsalVentralDistance)
                differences.Add(new Difference("Spinal dorsoventral distance", SpinalDorsalVentralDistance, other.SpinalDorsalVentralDistance));
            if (SpinalMedialLateralDistance != other.SpinalMedialLateralDistance)
                differences.Add(new Difference("Spinal mediolateral distance", SpinalMedialLateralDistance, other.SpinalMedialLateralDistance));
            if (SpinalBodyPosition != other.SpinalBodyPosition)
                differences.Add(new Difference("Spinal body position", SpinalBodyPosition, other.SpinalBodyPosition));
            if (BodyMedialLateralDistance != other.BodyMedialLateralDistance)
                differences.Add(new Difference("Body mediolateral distance", BodyMedialLateralDistance, other.BodyMedialLateralDistance));
            if (BodyDorsalVentralDistance != other.BodyDorsalVentralDistance)
                differences.Add(new Difference("Body dorsoventral distance", BodyDorsalVentralDistance, other.BodyDorsalVentralDistance));
            if (differences.Count != 0)
                return differences;
            return null;
        }
    }
}
