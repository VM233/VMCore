using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ColliderCloneUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo(this PolygonCollider2D source, PolygonCollider2D destination)
        {
            var pathCount = source.pathCount;
            destination.pathCount = pathCount;
            for (var i = 0; i < pathCount; i++)
            {
                var path = source.GetPath(i);
                destination.SetPath(i, path);
            }
        }
    }
}