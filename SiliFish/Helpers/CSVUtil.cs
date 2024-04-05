using GeneticSharp;
using SiliFish.ModelUnits.Architecture;
using SiliFish.ModelUnits.Cells;
using SiliFish.ModelUnits.Stim;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Helpers
{
    public static class CSVUtil
    {
        public static string CSVEncode(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            return s.Replace(",", ";").Replace("\r\n", ";").Replace("\n", ";");
        }
        public static string GetCSVLine(List<string> values)
        {
            if (values == null || values.Count == 0)
                return string.Empty;
            StringBuilder sb = new();
            foreach (string value in values)
            {
                if (value != null && value.Contains(','))
                    sb.Append("\"" + value.Replace("\"", "\"\"") + "\"");
                else
                    sb.Append(value ?? string.Empty);
                sb.Append(',');
            }
            return sb.ToString()[..^1];
        }

    }
}
