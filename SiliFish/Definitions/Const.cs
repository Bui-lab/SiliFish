using SiliFish.DynamicUnits;

namespace SiliFish.Definitions
{
    public class Const
    {
        public static double Epsilon = 0.00001;
        public static int RheobaseInfinity = 500;
        public static string DecimalPointFormat = "0.0###";
        public static double[] RheobaseTestMultipliers = new double[] { 1, 1.1, 1.5 };//, 2 };
        public static double GeneticAlgorithmMinValue = -100;
        public static double GeneticAlgorithmMaxValue = 100;
        public static UnitOfMeasure UoM { get; set; } = UnitOfMeasure.milliVolt_picoAmpere_GigaOhm_picoFarad;//Used from data structures that don't have direct access to the model
        public static string DefaultNeuronCore = typeof(Izhikevich_9P).Name;
        public static string DefaultMuscleCore = typeof(Leaky_Integrator).Name;

    }
}
