using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiliFish.ModelUnits.Junction
{
    public class JunctionBase: ModelUnitBase
    {
        public CellReach CellReach { get; set; } = new();
        public SynapseParameters SynapseParameters { get; set; }
        public bool IsChemical { get { return SynapseParameters != null; } set { } }






    }
}
