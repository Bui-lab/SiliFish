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
        public CellReach CellReach { get; set; }
        public SynapseParameters SynapseParameters { get; set; }
        public bool IsChemical { get { return SynapseParameters != null; } set { } }

        private string poolSource, poolTarget;
        public string PoolSource
        {
            get { return poolSource; }
            set
            {
                bool rename = GeneratedName() == Name;
                poolSource = value;
                if (rename) Name = GeneratedName();
            }
        }
        public string PoolTarget
        {
            get { return poolTarget; }
            set
            {
                bool rename = GeneratedName() == Name;
                poolTarget = value;
                if (rename) Name = GeneratedName();
            }
        }

        private string _Name;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_Name))
                    _Name = GeneratedName();
                return _Name;
            }
            set { _Name = value; }
        }

        public string GeneratedName()
        {
            return String.Format("{0}-->{1}", (string.IsNullOrEmpty(PoolSource) ? "__" : PoolSource),
                (string.IsNullOrEmpty(PoolTarget) ? "__" : PoolTarget));
        }


    }
}
