using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bananva.Utilities.Extensions
{
    public static class Extensions
    {
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<TElement> ForEach<TElement>(this IEnumerable<TElement> collection,
            Action<TElement> action)
            where TElement : notnull
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (var element in collection)
            {
                action(element);
            }

            return collection;
        }

        public static T ThrowIfArgumentNull<T>(this T? value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value;
        }
    }
}