using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ApproximateUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(this float a, float b)
        {
            return Mathf.Approximately(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(this Vector2 a, Vector2 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(this Vector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(this Vector4 a, Vector4 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z) &&
                   Mathf.Approximately(a.w, b.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(this float a, float b, float tolerance)
        {
            return Mathf.Abs(a - b) <= tolerance;
        }
    }
}