using System.Runtime.CompilerServices;
using EnumsNET;

namespace VMFramework.Core
{
    public static class RectangleContainsUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this RectangleFloat current, RectangleFloat other)
        {
            return current.Contains(other.min) && current.Contains(other.max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this RectangleFloat current, RectangleFloat other,
            out FourTypesDirection overflowDirections)
        {
            overflowDirections = FourTypesDirection.None;

            if (other.min.x < current.min.x)
            {
                overflowDirections |= FourTypesDirection.Left;
            }
            
            if (other.max.x > current.max.x)
            {
                overflowDirections |= FourTypesDirection.Right;
            }
            
            if (other.max.y > current.max.y)
            {
                overflowDirections |= FourTypesDirection.Up;
            }
            
            if (other.min.y < current.min.y)
            {
                overflowDirections |= FourTypesDirection.Down;
            }

            return overflowDirections == FourTypesDirection.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this RectangleFloat current, RectangleFloat other, FourTypesDirection directions)
        {
            if (directions.HasAnyFlags(FourTypesDirection.Left))
            {
                if (other.min.x < current.min.x)
                {
                    return false;
                }
            }

            if (directions.HasAnyFlags(FourTypesDirection.Right))
            {
                if (other.max.x > current.max.x)
                {
                    return false;
                }
            }

            if (directions.HasAnyFlags(FourTypesDirection.Up))
            {
                if (other.max.y > current.max.y)
                {
                    return false;
                }
            }

            if (directions.HasAnyFlags(FourTypesDirection.Down))
            {
                if (other.min.y < current.min.y)
                {
                    return false;
                }
            }

            return true;
        }
    }
}