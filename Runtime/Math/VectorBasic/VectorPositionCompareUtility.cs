using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class VectorPositionCompareUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRightBottomOf(this Vector2 a, Vector2 b)
        {
            return a.x > b.x && a.y < b.y;
        }
    }
}