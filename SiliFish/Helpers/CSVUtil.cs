using GeneticSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Helpers
{
    public static class CSVUtil
    {
        public static string GetCSVLine(List<string> values)
        {
            if (values == null || values.Count == 0)
                return string.Empty;
            StringBuilder sb = new();
            foreach (string value in values)
            {
                if (value != null && value.Contains(","))
                    sb.Append("\"" + value.Replace("\"", "\"\"") + "\"");
                else
                    sb.Append(value ?? string.Empty);
                sb.Append(',');
            }
            return sb.ToString()[..^1];
        }
    }
}
