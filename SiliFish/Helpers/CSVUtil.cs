using SiliFish.ModelUnits;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static string ReadCSVLine(StreamReader sr)
        {
            string line = sr.ReadLine();
            string pattern = @"(^|,)'(\d+)-(\d+)($|,)";
            MatchCollection matches = Regex.Matches(line, pattern);

            foreach (Match match in matches.OrderByDescending(m=>m.Index))
            {
                if (match.Value.StartsWith('\''))
                    line = line.Remove(match.Index, 1);
                else if (match.Value.StartsWith(",'"))
                    line = line.Remove(match.Index + 1, 1);
            }
            return line;
        }
        public static string WriteCSVLine(List<string> values)
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
