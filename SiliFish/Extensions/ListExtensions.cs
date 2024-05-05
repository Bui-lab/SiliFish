using System.Collections.Generic;

namespace SiliFish.Extensions
{
    public static class ListExtensions
    {
        public static bool Equivalent<T>(this List<T> thisList, List<T> secondList)
        {
            if (thisList?.Count != secondList?.Count)
                return false;
            for (int i = 0;  i< thisList?.Count; i++)
            {
                if (!thisList[i].Equals(secondList[i])) return false;

            }
            return true;
        }
        public static bool IsEmpty(this List<string> thisList)
        {
            for (int i = 0; i < thisList?.Count; i++)
            {
                if (!string.IsNullOrEmpty(thisList[i])) return false;

            }
            return true;
        }
    }
}
