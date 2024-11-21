using System;
using System.Collections.Generic;

namespace SpaceStellar.Utility
{
    public static class Extensions
    {
        public static TCollection ForEach<TCollection, TElement>(this TCollection collection, Action<TElement> action)
            where TCollection : ICollection<TElement>, IEnumerable<TElement>
            where TElement : notnull
        {
            if (collection is not { Count: not 0 })
            {
                return collection;
            }

            foreach (var element in collection)
                action(element);
            return collection;
        }
    }
}