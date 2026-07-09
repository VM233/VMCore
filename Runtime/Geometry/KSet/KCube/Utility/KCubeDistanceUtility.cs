using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class KCubeDistanceUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetNearestPointInRange(this RangeInteger range, int point)
        {
            if (point < range.min)
            {
                return range.min;
            }

            if (point > range.max)
            {
                return range.max;
            }

            return point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int GetNearestPointInRect(this RectangleInteger rect, Vector2Int point)
        {
            if (rect.Contains(point))
            {
                throw new ArgumentException($"Point {point} is already inside the rectangle {rect}.");
            }

            if (point.x < rect.min.x)
            {
                var yRange = rect.YRange;
                var nearestY = yRange.GetNearestPointInRange(point.y);
                return new Vector2Int(rect.min.x, nearestY);
            }

            if (point.x > rect.max.x)
            {
                var yRange = rect.YRange;
                var nearestY = yRange.GetNearestPointInRange(point.y);
                return new Vector2Int(rect.max.x, nearestY);
            }

            if (point.y < rect.min.y)
            {
                return new Vector2Int(point.x, rect.min.y);
            }

            if (point.y > rect.max.y)
            {
                return new Vector2Int(point.x, rect.max.y);
            }

            throw new InvalidOperationException($"This should never happen.");
        }
    }
}