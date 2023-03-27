namespace SiliFish.UI.Extensions
{
    public static class NumericUpDownExtensions
    {
        public static void SetValue(this NumericUpDown eNum, double d)
        {
            try
            {
                decimal dec = (decimal)Math.Round(d, eNum.DecimalPlaces);
                if (dec < eNum.Minimum)
                    eNum.Minimum = dec;
                if (dec > eNum.Maximum)
                    eNum.Maximum = dec;
                eNum.Value = dec;
            }
            catch { }
        }

    }
}
