using System.Drawing;

namespace SiliFish.Extensions
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color color)
        {
            return "0x" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
        public static Color FromHex(this Color _, string hex)
        {
            hex = hex.Replace("'", "");
            if (hex.StartsWith("0x") && hex.Length == 8)
            {
                int r, g, b;
                r = int.Parse(hex[2..4], System.Globalization.NumberStyles.HexNumber);
                g = int.Parse(hex[4..6], System.Globalization.NumberStyles.HexNumber);
                b = int.Parse(hex[6..8], System.Globalization.NumberStyles.HexNumber);
                return Color.FromArgb(r, g, b);
            }
            if (hex.StartsWith("#") && hex.Length == 7)
            {
                int r, g, b;
                r = int.Parse(hex[1..3], System.Globalization.NumberStyles.HexNumber);
                g = int.Parse(hex[3..5], System.Globalization.NumberStyles.HexNumber);
                b = int.Parse(hex[5..7], System.Globalization.NumberStyles.HexNumber);
                return Color.FromArgb(r, g, b);
            }

            return Color.Black;
        }
        public static string ToRGBQuoted(this Color color)
        {
            return "'#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2") + "'";
        }
    }
}
