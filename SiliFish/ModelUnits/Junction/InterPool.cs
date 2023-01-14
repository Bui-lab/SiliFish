using SiliFish.DataTypes;
using SiliFish.ModelUnits.Cells;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits
{
    public class InterPool
    {
        //Source and Target properties added for Json
        public string Source { get; set; }
        public string Target { get; set; }

        [JsonIgnore]
        internal CellPool SourcePool;
        [JsonIgnore]
        internal CellPool TargetPool;
        public CellReach Reach { get; set; }
        public SynapseParameters SynapseParameters { get; set; }
        internal bool IsChemical { get { return SynapseParameters != null; } set { } }
        [JsonIgnore]
        public TimeLine TimeLine { get; }

        public InterPool()
        { }
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


