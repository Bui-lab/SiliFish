using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.Services.Plotting.PlotSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorJunctions : PlotGeneratorBase
    {
        protected readonly List<JunctionBase> Junctions;

        public PlotGeneratorJunctions(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
            List<JunctionBase> junctions, PlotSelectionInterface plotSelection) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq, plotSelection)
        {
            Junctions = junctions;
        }
        protected override void CreateCharts(PlotType _)
        {
            CreateCharts();
        }
        protected override void CreateCharts()
        {
            foreach (var jncGroup in Junctions.GroupBy(j => j is GapJunction gj ? gj.Cell2.ID : j is ChemicalSynapse syn ? syn.PostCell.ID : ""))
            {
                List<Cell> cells = [];
                List<GapJunction> gapJunctions = [];
                List<ChemicalSynapse> synapses = [];
                foreach (JunctionBase junction in jncGroup)
                {
                    foreach (JunctionBase jnc in Junctions)
                    {
                        if (jnc is GapJunction gj)
                        {
                            cells.Add(gj.Cell1);
                            cells.Add(gj.Cell2);
                            gapJunctions.Add(gj);
                        }
                        else if (jnc is ChemicalSynapse syn)
                        {
                            cells.Add(syn.PreNeuron);
                            cells.Add(syn.PostCell);
                            synapses.Add(syn);
                        }
                    }
                }
                PlotGeneratorMembranePotentials plotGeneratorMP = new(plotGenerator, timeArray, iStart, iEnd, 0,
                    cells.DistinctBy(c => c.ID).ToList(), plotSelection);
                plotGeneratorMP.CreateCharts(charts);

                PlotGeneratorCurrentsOfJunctions plotGeneratorCurrentsOfJunctions = new(plotGenerator, timeArray, iStart, iEnd, 1, gapJunctions, synapses, true);
                plotGeneratorCurrentsOfJunctions.CreateCharts(charts);
            }
        }
    }
}
