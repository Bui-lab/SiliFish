using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiliFish.UI.EventArguments
{
    internal class SelectedUnitArgs : EventArgs
    {
        internal ModelUnitBase unitSelected;
        internal bool enforce;
    }

}
