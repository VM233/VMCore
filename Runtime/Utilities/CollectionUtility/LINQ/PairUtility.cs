using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Linq
{
    public static partial class PairUtility
    {
        #region Pair With Next

        /// <summary>
        /// 将一个IEnumerable中的元素两两相邻地组合起来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(T, T)> PairWithNext<T>(this IEnumerable<T> source)
        {
            var items = source.ToList();
            var previousItem = items.First();

            foreach (var currentItem in items.Skip(1))
            {
                yield return (previousItem, currentItem);
                previousItem = currentItem;
            }
        }

        /// <summary>
        /// 将一个IEnumerable中的元素两两相邻地组合起来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="firstBoundary"></param>
        /// <param name="lastBoundary"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(T, T)> PairWithNext<T>(this IEnumerable<T> source, T firstBoundary, T lastBoundary)
        {
            var items = source.ToList();
            var previousItem = items.First();

            yield return (firstBoundary, previousItem);

            foreach (var currentItem in items.Skip(1))
            {
                yield return (previousItem, currentItem);
                previousItem = currentItem;
            }

            yield return (previousItem, lastBoundary);
        }

        #endregion

        #region Zip

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(T1, T2)> Zip<T1, T2>(this IEnumerable<T1> enumerableOne,
            IEnumerable<T2> enumerableTwo)
        {
            return enumerableOne.Zip(enumerableTwo, (t1, t2) => (t1, t2));
        }

        #endregion
    }
}