using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Linq
{
    public static class LINQConditionUtility
    {
        #region Is Null Or Empty

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.Any() == false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty([NotNullWhen(false)] this IEnumerable enumerable)
        {
            return enumerable == null || enumerable.Cast<object>().Any() == false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IReadOnlyCollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        #endregion
        
        #region Is All Null

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAllNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.All(item => item == null);
        }

        #endregion

        #region Is Any Null

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAnyNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any(item => item == null);
        }

        #endregion

        #region Is Null Or Empty Or All Null

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmptyOrAllNull<T>([NotNullWhen(false)] this IEnumerable<T> enumerable)
            where T : class
        {
            if (enumerable == null)
            {
                return true;
            }

            foreach (var item in enumerable)
            {
                if (item != null)
                {
                    return false;
                }
            }
            
            return true;
        }

        #endregion

        #region Any & All

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyIs<TValue>(this IEnumerable<object> enumerable, out TValue value)
        {
            foreach (var obj in enumerable)
            {
                if (obj is TValue val)
                {
                    value = val;
                    return true;
                }
            }
            
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyIs<T>(this IEnumerable<object> enumerable)
        {
            return enumerable.Any(item => item is T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllIs<T>(this IEnumerable<object> enumerable)
        {
            return enumerable.All(item => item is T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this IEnumerable<bool> enumerable)
        {
            var result = false;
            foreach (var item in enumerable)
            {
                result |= item;
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this IEnumerable<bool> enumerable)
        {
            var result = true;
            foreach (var item in enumerable)
            {
                result &= item;
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<T>(this IEnumerable<T> enumerable,
            Func<int, T, bool> predicate)
        {
            foreach (var (i, item) in enumerable.Enumerate())
            {
                if (predicate(i, item))
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All<T>(this IEnumerable<T> enumerable,
            Func<int, T, bool> predicate)
        {
            foreach (var (i, item) in enumerable.Enumerate())
            {
                if (predicate(i, item) == false)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
        
        #region Where

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> enumerable)
            where T : class
        {
            return enumerable.Where(item => item != null);
        }

        #endregion
    }
}