using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class BoxCollider2DUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRectangle<TCube>(this BoxCollider2D boxCollider, TCube rectangle)
            where TCube : IKCube<Vector2>
        {
            boxCollider.offset = rectangle.Pivot;
            boxCollider.size = rectangle.Size;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFloat GetRectangle(this BoxCollider2D boxCollider)
        {
            return RectangleFloat.FromPivotSize(boxCollider.offset, boxCollider.size);
        }
    }
}