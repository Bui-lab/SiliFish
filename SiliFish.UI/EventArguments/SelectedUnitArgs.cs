using SiliFish.ModelUnits;
using SiliFish.ModelUnits.Architecture;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiliFish.UI.EventArguments
{
    internal class SelectedUnitArgs : EventArgs
    {
        internal List<ModelUnitBase> unitsSelected = [];
        internal bool enforce;
    }

}
