using System;
using System.Collections.Generic;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Collections
{
    public static class EnumerableExtensions
    {
        public static KeyValueCollection<TKey, TSource> ToKeyValueCollection<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return EnumerableExtensions.ToKeyValueCollection<TSource, TKey, TSource>(source, keySelector, IdentityFunction<TSource>.Instance, (IEqualityComparer<TKey>)null);
        }

        public static KeyValueCollection<TKey, TSource> ToKeyValueCollection<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return EnumerableExtensions.ToKeyValueCollection<TSource, TKey, TSource>(source, keySelector, IdentityFunction<TSource>.Instance, comparer);
        }

        public static KeyValueCollection<TKey, TElement> ToKeyValueCollection<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            return EnumerableExtensions.ToKeyValueCollection<TSource, TKey, TElement>(source, keySelector, elementSelector, (IEqualityComparer<TKey>)null);
        }

        public static KeyValueCollection<TKey, TElement> ToKeyValueCollection<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            Guard.ArgumentNotNull(source, "source");
            Guard.ArgumentNotNull(keySelector, "keySelector");
            Guard.ArgumentNotNull(elementSelector, "elementSelector");

            KeyValueCollection<TKey, TElement> keyValues = new KeyValueCollection<TKey, TElement>(comparer);
            foreach (TSource source1 in source)
                keyValues.Add(keySelector(source1), elementSelector(source1));
            return keyValues;
        }
    }

    internal class IdentityFunction<TElement>
    {
        public static Func<TElement, TElement> Instance
        {
            get
            {
                return (Func<TElement, TElement>)(x => x);
            }
        }

        public IdentityFunction()
        {
        }
    }
}