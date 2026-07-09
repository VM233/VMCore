using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ColliderKSetUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircleFloat SelfToCircle(this CircleCollider2D collider)
        {
            return new CircleFloat(collider.offset, collider.radius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFloat SelfToRectangle(this BoxCollider2D collider)
        {
            return RectangleFloat.FromPivotSize(collider.offset, collider.size);
        }
    }
}