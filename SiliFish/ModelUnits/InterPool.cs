using SiliFish.DataTypes;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits
{
    public class InterPool
    {
        //Source and Target properties added for Json
        public string Source { get { return SourcePool?.ID ?? ""; } }
        public string Target { get { return TargetPool?.ID ?? ""; } }

        [JsonIgnore]
        internal CellPool SourcePool;
        [JsonIgnore]
        internal CellPool TargetPool;
        public CellReach Reach { get; }
        public SynapseParameters SynapseParameters { get; }
        internal bool IsChemical { get { return SynapseParameters != null; } }
        public TimeLine TimeLine { get; }
        public InterPool(CellPool pool1, CellPool pool2, CellReach cr, SynapseParameters synPar = null, TimeLine timeline = null)
        {
            SourcePool = pool1; 
            TargetPool = pool2; 
            Reach = cr; 
            SynapseParameters = synPar;
            TimeLine = timeline;
        }
    }
}


