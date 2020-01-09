﻿using System.Collections.Generic;

namespace OpenVIII.Fields
{
    public static class IEnumerableExtensionMethods
    {
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumerable, T prefix)
        {
            yield return prefix;
         
            foreach (var item in enumerable)
                yield return item;
        }
    }
}