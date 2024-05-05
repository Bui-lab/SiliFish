namespace SiliFish.UI.EventArguments
{
    internal class OptimizationEventArgs : EventArgs
    {
        internal double Fitness;
        internal Dictionary<string, double> Parameters;
    }

}
