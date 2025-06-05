using System;

namespace SiliFish.Extensions
{
    public static class StringExtensions
    {
        public static (double rangeStart, double rangeEnd) ParseRange(this string strRange, double defStart = 0, double defEnd = -1)
        {
            double rangeStart;
            double rangeEnd = defEnd;
            int sep = strRange.IndexOf('-');
            if (sep == -1)
            {
                if (!double.TryParse(strRange.Trim(), out rangeStart))
                    rangeStart = defStart;
            }
            else
            {
                if (!double.TryParse(strRange.AsSpan(0, sep).Trim(), out rangeStart))
                    rangeStart = defStart;
                else if (!double.TryParse(strRange.AsSpan(sep + 1).Trim(), out rangeEnd))
                    rangeEnd = defEnd;
            }
            return (rangeStart, rangeEnd);
        }
    }
}
