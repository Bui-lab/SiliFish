using System;
using System.Collections.Generic;

namespace SiliFish.Helpers
{
    public static class ListBuilder
    {
        public static List<T> Build<T>(params object[] items)
            where T : class
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            List<T> list = [];
            foreach (object item in items)
            {
                if (item is IEnumerable<T> tList)
                    list.AddRange(tList);
                else if (typeof(T) == typeof(string))
                    list.Add(item?.ToString() as T);
                else
                    list.Add(item as T);
            }
            return list;

        }
    }
}
