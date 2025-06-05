using SiliFish.Definitions;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Junction;
using SiliFish.Services.Plotting.PlotSelection;
using System.Collections.Generic;
using System.Linq;

namespace SiliFish.Services.Plotting.PlotGenerators
{
    internal class PlotGeneratorJunctions : PlotGeneratorBase
    {
        protected readonly List<JunctionBase> Junctions;
        protected readonly List<InterPool> InterPools;
        protected readonly List<CellPool> Pools;

        public PlotGeneratorJunctions(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
            List<JunctionBase> junctions, PlotSelectionInterface plotSelection) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq, plotSelection)
        {
            Junctions = junctions;
        }
        public PlotGeneratorJunctions(PlotGenerator plotGenerator, double[] timeArray, int iStart, int iEnd, int groupSeq,
            List<InterPool> interpools, List<CellPool> pools, PlotSelectionInterface plotSelection) :
            base(plotGenerator, timeArray, iStart, iEnd, groupSeq, plotSelection)
        {
            InterPools = interpools;
            Pools = pools;
        }
        protected override void CreateCharts(PlotType _)
        {
            CreateCharts();
        }
        protected override void CreateCharts()
        {
            List<Cell> cells = [];
            List<GapJunction> gapJunctions = [];
            List<ChemicalSynapse> synapses = [];
            if (Junctions != null)
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
            if (InterPools != null)
            {
                foreach (InterPool interpool in InterPools)
                {
                    CellPool source = Pools.FirstOrDefault(c => c.ID == interpool.SourcePool);
                    CellPool target = Pools.FirstOrDefault(c => c.ID == interpool.TargetPool);
                    List<Cell> targetCells = null;
                    if (plotSelection is PlotSelectionMultiCells ps)
                        targetCells = target.GetCells(ps, iStart, iEnd).ToList();//to limit the plot to a select set of target cells

                    List<GapJunction> gjlist = null;
                    List<ChemicalSynapse> synlist = null;
                    if (targetCells != null && targetCells.Count != 0)
                    {
                        gjlist = source.GetGapJunctionsTo(targetCells);
                        synlist = source.GetChemicalSynapsesTo(targetCells);
                    }
                    else
                    {
                        List<JunctionBase> junctions = source.GetJunctionsTo(interpool.TargetPool);
                        gjlist = junctions.Where(j => j is GapJunction).Select(j => j as GapJunction).ToList();
                        synlist = junctions.Where(j => j is ChemicalSynapse).Select(j => j as ChemicalSynapse).ToList();
                    }
                    if (gjlist.Count > 0)
                    {
                        gapJunctions.AddRange(gjlist);
                        cells.AddRange(gapJunctions.Select(j => j.Cell1).DistinctBy(c => c.ID));
                        cells.AddRange(gapJunctions.Select(j => j.Cell2).DistinctBy(c => c.ID));
                    }
                    if (synlist.Count > 0)
                    {
                        synapses.AddRange(synlist);
                        cells.AddRange(synlist.Select(j => j.PreNeuron).DistinctBy(c => c.ID));
                        cells.AddRange(synlist.Select(j => j.PostCell).DistinctBy(c => c.ID));
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
