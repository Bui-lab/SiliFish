using SiliFish.ModelUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Services
{
    public class HTMLPlotGenerator
    {

        private static string PlotMembranePotentials(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,CellSelectionStruct cellSelection,
            int iStart, int iEnd, string filename = "")
        {
            LineChartGenerator lc = new();
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
                charts.AddRange(lc.CreatePotentialsMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
            if (pools != null)
                charts.AddRange(lc.CreatePotentialsMultiPlot(pools, cellSelection, TimeOffset, iStart, iEnd, ref chartindex));

            return lc.PlotCharts(filename, "Potentials", charts, numColumns: 2);
        }

        private static string PlotCurrents(double[] TimeOffset, List<Cell> cells, List<CellPool> pools, CellSelectionStruct cellSelection,
            int iStart, int iEnd, bool includeGap = true, bool includeChem = true, string filename = "")
        {
            LineChartGenerator lc = new();
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
                charts.AddRange(lc.CreateCurrentsMultiPlot(cells, TimeOffset, iStart, iEnd, includeGap: includeGap, includeChem: includeChem, ref chartindex));
            if (pools != null)
                charts.AddRange(lc.CreateCurrentsMultiPlot(pools, cellSelection,TimeOffset,  iStart, iEnd, includeGap: includeGap, includeChem: includeChem, ref chartindex));
            return lc.PlotCharts(filename, "Currents", charts, numColumns: 2);
        }

        private static string PlotStimuli(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,
            int iStart, int iEnd,
            CellSelectionStruct cellSelection, string filename = "")
        {            
            LineChartGenerator lc = new();
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
                charts.AddRange(lc.CreateStimuliMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
            if (pools != null)
                charts.AddRange(lc.CreateStimuliMultiPlot(pools, TimeOffset, iStart, iEnd, cellSelection, ref chartindex));
            return lc.PlotCharts(filename, "Stimuli", charts, numColumns: 2);
        }

        private static string PlotFullDynamics(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,
            int iStart, int iEnd,
            CellSelectionStruct cellSelection, string filename = "")
        {
            LineChartGenerator lc = new();
            List<string> charts = new();
            int chartindex = 0;
            if (cells != null)
            {
                charts.AddRange(lc.CreatePotentialsMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
                charts.AddRange(lc.CreateCurrentsMultiPlot(cells, TimeOffset, iStart, iEnd, includeGap: true, includeChem: true, ref chartindex));
                charts.AddRange(lc.CreateStimuliMultiPlot(cells, TimeOffset, iStart, iEnd, ref chartindex));
            }
            if (pools != null)
            {
                charts.AddRange(lc.CreatePotentialsMultiPlot(pools, cellSelection, TimeOffset, iStart, iEnd, ref chartindex));
                charts.AddRange(lc.CreateCurrentsMultiPlot(pools, cellSelection, TimeOffset, iStart, iEnd, includeGap: true, includeChem: true, ref chartindex));
                charts.AddRange(lc.CreateStimuliMultiPlot(pools, TimeOffset, iStart, iEnd, cellSelection, ref chartindex));
            }
            return lc.PlotCharts(filename, "Full Dynamics", charts, numColumns: 2);
        }


        public static string Plot( PlotType PlotType, double[] TimeArray, List<Cell> Cells, List<CellPool> Pools,
            CellSelectionStruct cellSelection, 
            double dt, int tStart = 0, int tEnd = -1, int tSkip = 0)
        {
            if ((Cells == null || !Cells.Any()) &&
                (Pools == null || !Pools.Any()))
                return "";
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);
            if (iEnd < iStart || iEnd >= TimeArray.Length)
                iEnd = TimeArray.Length - 1;
  
            
            string PlotHTML = "";
            //URGENT remaining PlotTypes
            switch (PlotType)
            {
                case PlotType.MembPotential:
                    PlotHTML = PlotMembranePotentials(TimeArray, Cells, Pools, cellSelection, iStart, iEnd);
                    break;
                case PlotType.Current:
                    PlotHTML = PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, includeGap: true, includeChem: true);
                    break;
                case PlotType.GapCurrent:
                    PlotHTML = PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, includeGap: true, includeChem: false);
                    break;
                case PlotType.ChemCurrent:
                    PlotHTML = PlotCurrents(TimeArray, Cells, Pools, cellSelection, iStart, iEnd, includeGap: false, includeChem: true);
                    break;
                case PlotType.Stimuli:
                    PlotHTML = PlotStimuli(TimeArray, Cells, Pools, iStart, iEnd, cellSelection);
                    break;
                case PlotType.FullDyn:
                    PlotHTML = PlotFullDynamics(TimeArray, Cells, Pools, iStart, iEnd, cellSelection);
                    break;
            }
            return PlotHTML;
        }

    }
}
