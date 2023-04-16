using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using System;

namespace SiliFish.Services.Optimization
{
    public class TargetRheobaseFunction : FitnessFunction
    {
        /// <summary>
        /// Fitness is equal to weight if rheobase is within [ValueMin, ValueMax]
        /// Fitness is equal to 0 if rheobase <= ValueMin - range or rheobase >= ValueMax + range
        /// Where range is ValueMax-ValueMin or ValueMin/2 (if ValueMin==ValueMax)
        /// In other places, it is calculated by the tangent
        /// </summary>
        /// <param name="core"></param>
        /// <param name="rheobase"></param>
        /// <returns></returns>
        public double CalculateFitness(CellCoreUnit core, out double rheobase)
        {
            rheobase = core.CalculateRheoBase(maxRheobase: 1000, sensitivity: Math.Pow(0.1, 3), infinity_ms: GlobalSettings.RheobaseInfinity, dt: 0.1);
            if (rheobase < 0) return 0;
            if (ValueMin <= rheobase && ValueMax >= rheobase)
                return Weight;
            double range = ValueMax - ValueMin;
            if (range < GlobalSettings.Epsilon)
                range = ValueMin / 2;
            if (rheobase <= ValueMin - range || rheobase >= ValueMax + range)
                return 0;
            double diff = rheobase < ValueMin ? (rheobase - (ValueMin - range)) : (ValueMax + range - rheobase);
            return Weight * diff / range;
        }

        public TargetRheobaseFunction()
        {
            MinMaxExists = true;
            CurrentRequired = false;
            ModeExists = false;
        }
    }

}
