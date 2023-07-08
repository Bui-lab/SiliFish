using System;
using System.Collections.Generic;
using System.Text;

namespace SiliFish.UI.EventArguments
{
    internal class OptimizationEventArgs : EventArgs
    {
        internal double Fitness;
        internal Dictionary<string, double> Parameters;
    }

}
