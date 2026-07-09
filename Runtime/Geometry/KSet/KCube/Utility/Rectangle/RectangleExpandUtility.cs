using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class RectangleExpandUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleInteger Encapsulate(this RectangleInteger rect, Vector2Int point)
        {
            rect.min = rect.min.Min(point);
            rect.max = rect.max.Max(point);
            return rect;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFloat Encapsulate(this RectangleFloat rect, RectangleFloat other)
        {
            rect.min = rect.min.Min(other.min);
            rect.max = rect.max.Max(other.max);
            return rect;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Expand(this ref RectangleFloat rect, float amount)
        {
            rect.min -= new Vector2(amount, amount);
            rect.max += new Vector2(amount, amount);
        }
    }
}