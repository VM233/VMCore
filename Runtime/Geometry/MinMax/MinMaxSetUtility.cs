using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class MinMaxSetUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector2Int min, Vector2Int max) Intersect<TMinMaxOwner>(
            this IEnumerable<TMinMaxOwner> enumerable)
            where TMinMaxOwner : IReadOnlyMinMaxOwner<Vector2Int>
        {
            Vector2Int min = new Vector2Int(int.MinValue, int.MinValue);
            Vector2Int max = new Vector2Int(int.MaxValue, int.MaxValue);
            Intersect(enumerable, ref min, ref max);
            return (min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Intersect<TMinMaxOwner>(this IEnumerable<TMinMaxOwner> enumerable, ref Vector2Int min,
            ref Vector2Int max)
            where TMinMaxOwner : IReadOnlyMinMaxOwner<Vector2Int>
        {
            foreach (var item in enumerable)
            {
                min = min.Max(item.GetMin());
                max = max.Min(item.GetMax());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector2Int min, Vector2Int max) Union<TMinMaxOwner>(this IEnumerable<TMinMaxOwner> enumerable)
            where TMinMaxOwner : IReadOnlyMinMaxOwner<Vector2Int>
        {
            Vector2Int min = new Vector2Int(int.MaxValue, int.MaxValue);
            Vector2Int max = new Vector2Int(int.MinValue, int.MinValue);
            Union(enumerable, ref min, ref max);
            return (min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Union<TMinMaxOwner>(this IEnumerable<TMinMaxOwner> enumerable, ref Vector2Int min,
            ref Vector2Int max)
            where TMinMaxOwner : IReadOnlyMinMaxOwner<Vector2Int>
        {
            foreach (var item in enumerable)
            {
                min = min.Min(item.GetMin());
                max = max.Max(item.GetMax());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int UnionMin<TMinMaxOwner>(this IEnumerable<TMinMaxOwner> enumerable)
            where TMinMaxOwner : IReadOnlyMinMaxOwner<Vector2Int>
        {
            Vector2Int min = new Vector2Int(int.MaxValue, int.MaxValue);
            foreach (var item in enumerable)
            {
                min = min.Min(item.GetMin());
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int UnionMax<TMinMaxOwner>(this IEnumerable<TMinMaxOwner> enumerable)
            where TMinMaxOwner : IReadOnlyMinMaxOwner<Vector2Int>
        {
            Vector2Int max = new Vector2Int(int.MinValue, int.MinValue);
            foreach (var item in enumerable)
            {
                max = max.Max(item.GetMax());
            }

            return max;
        }
    }
}