using SiliFish.ModelUnits;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            Regex possibleDate = new("^\\d+-\\d+$");//Values like 1-5 are taken as January 5th by Excel
            foreach (string value in values)
            {
                string prefix = "";
                if (value != null)
                {
                    Match m = possibleDate.Match(value);//to prevent Excel taking it as a date
                    if (m.Success)
                        prefix = "'";
                    if (value != null && value.Contains(','))
                        sb.Append(prefix + "\"" + value.Replace("\"", "\"\"") + "\"");
                    else
                        sb.Append(prefix + value);
                }
                else
                    sb.Append(prefix + (value ?? string.Empty));
                sb.Append(',');
            }
            return sb.ToString()[..^1];
        }

    }
}
