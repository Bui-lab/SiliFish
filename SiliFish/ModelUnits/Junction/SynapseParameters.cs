﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.ModelUnits.Junction
{
    public class SynapseParameters
    {
        public double TauD { get; set; }
        public double TauR { get; set; }
        public double VTh { get; set; }
        public double E_rev { get; set; }

        public SynapseParameters() { }
        public SynapseParameters(SynapseParameters sp)
        {
            if (sp == null) return;
            TauD = sp.TauD;
            TauR = sp.TauR;
            VTh = sp.VTh;
            E_rev = sp.E_rev;
        }

        internal object GetTooltip()
        {
            return $"Tau D: {TauD:0.###}\r\n" +
                $"Tau R: {TauR:0.###}\r\n" +
                $"V thresh: {VTh:0.###}\r\n" +
                $"E rev: {E_rev:0.###}";
        }
    }

}