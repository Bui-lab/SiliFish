using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.Extensions;
using SiliFish.Helpers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

namespace SiliFish.Services.Plotting
{
    public class RasterPlotGenerator : EmbeddedResourceReader
    {
        private static StringBuilder AddHeader(string mainTitle, double width, double height)
        {
            StringBuilder html = new(ReadEmbeddedText("SiliFish.Resources.AmChartRaster.html"));
            html.Replace("__STYLE_SHEET__", ReadEmbeddedText("SiliFish.Resources.StyleSheet.css"));

            if (height < GlobalSettings.PlotHeight)
                height = GlobalSettings.PlotHeight;
            html.Replace("__TITLE__", HttpUtility.HtmlEncode(mainTitle));
            html.Replace("__WIDTH__", width.ToString());
            html.Replace("__HEIGHT__", height.ToString());
            return html;
        }

        private static StringBuilder AddSection(XYDataSet dataPoints,
            int index, Color color, string ID)
        {
            StringBuilder jshtml = new(ReadEmbeddedText("SiliFish.Resources.AmChartRasterJSTemplate.html"));
            jshtml.Replace("__NAME__", HttpUtility.HtmlEncode(ID));
            jshtml.Replace("__INDEX__", index.ToString());
            jshtml.Replace("__DATA_X__", string.Join(',', dataPoints.XValues));
            jshtml.Replace("__DATA_Y__", string.Join(',', Enumerable.Repeat(index, dataPoints.XValues.Length)));
            string title = '"' + HttpUtility.HtmlEncode(dataPoints.Title) + '"';
            jshtml.Replace("__DATA_ID__", string.Join(',', Enumerable.Repeat(title, dataPoints.XValues.Length)));
            jshtml.Replace("__COLOR__", color.ToRGBQuoted());
            return jshtml;
        }

        public static string GenerateRasterPlotHTML(List<List<XYDataSet>> dataPoints, List<string> IDs, List<Color> colors,
            string mainTitle, double width, double height)
        {
            StringBuilder html = AddHeader(mainTitle, width, height);
            StringBuilder scripts = new();
            if (Util.CheckOnlineStatus("https://cdn.amcharts.com/lib/5/index.js"))
            {
                scripts.AppendLine("<script src = \"https://cdn.amcharts.com/lib/5/index.js\" ></script>");
                scripts.AppendLine("<script src = \"https://cdn.amcharts.com/lib/5/xy.js\" ></script>");
                scripts.AppendLine("<script src = \"https://cdn.amcharts.com/lib/5/plugins/exporting.js\" ></script>");
            }
            else
            {
                scripts.AppendLine(HTMLUtil.CreateScriptHTML("SiliFish.Resources.amcharts-bundle.js"));
            }
            html.Replace("__AMCHART5_SCRIPTS__", scripts.ToString());

            StringBuilder jshtml = new();
            int chartIndex = 0;
            List<string> seriesList = [];
            List<string> groupList = [];
            for (int i = 0; i < dataPoints.Count; i++)
            {
                List<XYDataSet> dataset = dataPoints[i];
                Color color = colors[i];
                string ID = IDs[i];
                string groupLabel = $"{{ name: \"{ID}\", start: {chartIndex}, end: __END__ }}";
                bool seriesAdded = false;
                foreach (XYDataSet ds in dataset)
                {
                    //Display all - if (ds.XValues.Length == 0) continue;
                    seriesAdded = true;
                    seriesList.Add($"series{chartIndex}");
                    StringBuilder chartBuilder = AddSection(ds, chartIndex++, color, ID);
                    jshtml.Append(chartBuilder);
                }
                if (seriesAdded)
                {
                    groupLabel = groupLabel.Replace("__END__", (chartIndex - 1).ToString());
                    groupList.Add(groupLabel);
                }
            }
            html.Replace("__LABEL_GROUPS__", string.Join(',', groupList));
            html.Replace("__SNAP_TO_SERIES__", string.Join(',', seriesList));
            html.Replace("__JAVASCRIPT_HTML__", jshtml.ToString());
            return html.ToString();
        }
    }
}