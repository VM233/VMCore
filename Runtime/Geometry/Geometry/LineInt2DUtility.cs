using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class LineInt2DUtility
    {
        // 判断三点是否共线
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreCollinear(this Vector2Int a, Vector2Int b, Vector2Int c)
        {
            // 计算叉积，如果为0则三点共线
            int crossProduct = (b.x - a.x) * (c.y - b.y) - (b.y - a.y) * (c.x - b.x);
            return crossProduct == 0;
        }
    }
}