using SiliFish.ModelUnits;

namespace SiliFish.UI.EventArguments
{
    internal class SelectedUnitArgs : EventArgs
    {
        internal List<ModelUnitBase> unitsSelected = [];
        internal bool enforce;
    }

}
