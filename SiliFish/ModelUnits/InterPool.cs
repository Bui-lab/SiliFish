using SiliFish.DataTypes;

namespace SiliFish.ModelUnits
{
    public class InterPool
    {
        internal CellPool poolSource, poolTarget;
        internal CellReach reach;
        internal SynapseParameters synapseParameters;
        internal bool IsChemical { get { return synapseParameters != null; } }
        internal TimeLine timeLine;
    }

}
