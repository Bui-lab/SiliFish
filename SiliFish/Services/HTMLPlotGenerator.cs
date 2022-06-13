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

        private static string PlotMembranePotentials(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,
            int iStart, int iEnd, string filename = "", int nSample = 0)
        {
            LineChartGenerator lc = new();
            List<string> charts = new();
            if (cells != null)
                charts.AddRange(lc.CreatePotentialsMultiPlot(cells, TimeOffset, iStart, iEnd));
            if (pools != null)
                charts.AddRange(lc.CreatePotentialsMultiPlot(pools, TimeOffset, iStart, iEnd, nSample));

            return lc.PlotCharts(filename, "Potentials", charts, numColumns: 2);
        }

        private static string PlotCurrents(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,
            int iStart, int iEnd, bool includeGap = true, bool includeChem = true, string filename = "", int nSample = 0)
        {
            LineChartGenerator lc = new();
            List<string> charts = new();
            if (cells != null)
                charts.AddRange(lc.CreateCurrentsMultiPlot(cells, TimeOffset, iStart, iEnd, includeGap: includeGap, includeChem: includeChem));
            if (pools != null)
                charts.AddRange(lc.CreateCurrentsMultiPlot(pools, TimeOffset, iStart, iEnd, includeGap: includeGap, includeChem: includeChem, nSample: nSample));
            return lc.PlotCharts(filename, "Currents", charts, numColumns: 2);
        }

        private static string PlotStimuli(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,
            int iStart, int iEnd, string filename = "", int nSample = 0)
        {            
            LineChartGenerator lc = new();
            List<string> charts = new();
            if (cells != null)
                charts.AddRange(lc.CreateStimuliMultiPlot(cells, TimeOffset, iStart, iEnd));
            if (pools != null)
                charts.AddRange(lc.CreateStimuliMultiPlot(pools, TimeOffset, iStart, iEnd, nSample: nSample));
            return lc.PlotCharts(filename, "Stimuli", charts, numColumns: 2);
        }

        private static string PlotFullDynamics(double[] TimeOffset, List<Cell> cells, List<CellPool> pools,
            int iStart, int iEnd, string filename = "", int nSample = 0)
        {
            LineChartGenerator lc = new();
            List<string> charts = new();
            if (cells != null)
            {
                charts.AddRange(lc.CreatePotentialsMultiPlot(cells, TimeOffset, iStart, iEnd));
                charts.AddRange(lc.CreateCurrentsMultiPlot(cells, TimeOffset, iStart, iEnd, includeGap: true, includeChem: true));
                charts.AddRange(lc.CreateStimuliMultiPlot(cells, TimeOffset, iStart, iEnd));
            }
            if (pools != null)
            {
                charts.AddRange(lc.CreatePotentialsMultiPlot(pools, TimeOffset, iStart, iEnd, nSample));
                charts.AddRange(lc.CreateCurrentsMultiPlot(pools, TimeOffset, iStart, iEnd, includeGap: true, includeChem: true, nSample: nSample));
                charts.AddRange(lc.CreateStimuliMultiPlot(pools, TimeOffset, iStart, iEnd, nSample: nSample));
            }
            return lc.PlotCharts(filename, "Full Dynamics", charts, numColumns: 2);
        }


        public static string Plot( PlotType PlotType, double[] TimeArray, List<Cell> Cells, List<CellPool> Pools, int tStart = 0, int tEnd = -1, int tSkip = 0, int nSample = 0)
        {
            if ((Cells == null || !Cells.Any()) &&
                (Pools == null || !Pools.Any()))
                return "";
            double dt = RunParam.dt;
            int iStart = (int)((tStart + tSkip) / dt);
            int iEnd = (int)((tEnd + tSkip) / dt);
            int offset = tSkip;
            if (iEnd < iStart || iEnd >= TimeArray.Length)
                iEnd = TimeArray.Length - 1;

            double[] TimeOffset = offset > 0 ? (TimeArray.Select(t => t - offset).ToArray()) : TimeArray;
            
            
            string PlotHTML = "";
            switch (PlotType)
            {
                case PlotType.MembPotential:
                    PlotHTML = PlotMembranePotentials(TimeOffset, Cells, Pools, iStart, iEnd, nSample: nSample);
                    break;
                case PlotType.Current:
                    PlotHTML = PlotCurrents(TimeOffset, Cells, Pools, iStart, iEnd, includeGap: true, includeChem: true, nSample: nSample);
                    break;
                case PlotType.GapCurrent:
                    PlotHTML = PlotCurrents(TimeOffset, Cells, Pools, iStart, iEnd, includeGap: true, includeChem: false, nSample: nSample);
                    break;
                case PlotType.ChemCurrent:
                    PlotHTML = PlotCurrents(TimeOffset, Cells, Pools, iStart, iEnd, includeGap: false, includeChem: true, nSample: nSample);
                    break;
                case PlotType.Stimuli:
                    PlotHTML = PlotStimuli(TimeOffset, Cells, Pools, iStart, iEnd, nSample: nSample);
                    break;
                case PlotType.FullDyn:
                    PlotHTML = PlotFullDynamics(TimeOffset, Cells, Pools, iStart, iEnd, nSample: nSample);
                    break;
            }
            return PlotHTML;
        }

    }
}
