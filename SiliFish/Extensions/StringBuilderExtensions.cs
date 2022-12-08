using System.Linq;

using System.Text;

namespace SiliFish.Extensions
{
    public static class StringBuilderExtensions
    {
        public static void Replace(this StringBuilder thisStrBuilder, string oldStr, string newStr, char wrap)
        {
            if (!newStr.StartsWith(wrap))
                newStr = wrap + newStr;
            if (!newStr.EndsWith(wrap))
                newStr += wrap;
            thisStrBuilder.Replace(oldStr, newStr);
        }



    }
}
