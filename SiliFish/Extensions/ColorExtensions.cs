namespace SiliFish.Extensions
{
    public static class ColorExtensions
    {
        public static string ToHex(this System.Drawing.Color color)
        {
            return "0x" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
        public static string ToRGB(this System.Drawing.Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
    }
}
