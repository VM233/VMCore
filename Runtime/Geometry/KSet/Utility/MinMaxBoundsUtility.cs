using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class MinMaxBoundsUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleInteger GetBounds<TMinMaxOwner>(this TMinMaxOwner owner)
            where TMinMaxOwner : IReadOnlyMinMaxOwner<Vector2Int>
        {
            return new RectangleInteger(owner.GetMin(), owner.GetMax());
        }
    }
}