using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class FourDiagonalDirectionsNeighborsQuery
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TItem GetNeighbor<TItem>(this FourDiagonalDirectionsNeighbors<TItem> neighbors, int index)
        {
            return index switch
            {
                0 => neighbors.leftUp,
                1 => neighbors.rightDown,
                2 => neighbors.leftDown,
                3 => neighbors.rightUp,
                _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
            };
        }
    }
}