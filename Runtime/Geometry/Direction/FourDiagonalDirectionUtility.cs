using System;
using System.Runtime.CompilerServices;
using EnumsNET;
using UnityEngine;

namespace VMFramework.Core
{
    public static class FourDiagonalDirectionUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourDiagonalDirection SingleFlagReversed(this FourDiagonalDirection direction)
        {
            return direction switch
            {
                FourDiagonalDirection.LeftUp => FourDiagonalDirection.RightDown,
                FourDiagonalDirection.RightDown => FourDiagonalDirection.LeftUp,
                FourDiagonalDirection.LeftDown => FourDiagonalDirection.RightUp,
                FourDiagonalDirection.RightUp => FourDiagonalDirection.LeftDown,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourDiagonalDirection Reversed(this FourDiagonalDirection direction)
        {
            var result = FourDiagonalDirection.None;

            if (direction.HasAnyFlags(FourDiagonalDirection.LeftDown))
            {
                result |= FourDiagonalDirection.RightUp;
            }

            if (direction.HasAnyFlags(FourDiagonalDirection.RightUp))
            {
                result |= FourDiagonalDirection.LeftDown;
            }

            if (direction.HasAnyFlags(FourDiagonalDirection.LeftUp))
            {
                result |= FourDiagonalDirection.RightDown;
            }

            if (direction.HasAnyFlags(FourDiagonalDirection.RightDown))
            {
                result |= FourDiagonalDirection.LeftUp;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourDiagonalDirection ToFourDiagonalDirection2D(this Vector2 vector)
        {
            return vector.x switch
            {
                > 0 when vector.y > 0 => FourDiagonalDirection.RightUp,
                > 0 when vector.y < 0 => FourDiagonalDirection.RightDown,
                < 0 when vector.y > 0 => FourDiagonalDirection.LeftUp,
                < 0 when vector.y < 0 => FourDiagonalDirection.LeftDown,
                _ => FourDiagonalDirection.None
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourDiagonalDirection GetSecondNearestDiagonalDirection(this Vector2 vector2)
        {
            switch (vector2.x)
            {
                case > 0 when vector2.y > 0:
                    if (vector2.x > vector2.y)
                    {
                        return FourDiagonalDirection.RightDown;
                    }
                    else
                    {
                        return FourDiagonalDirection.LeftUp;
                    }
                case > 0 when vector2.y < 0:
                    if (vector2.x > -vector2.y)
                    {
                        return FourDiagonalDirection.RightUp;
                    }
                    else
                    {
                        return FourDiagonalDirection.LeftDown;
                    }
                case < 0 when vector2.y > 0:
                    if (-vector2.x > vector2.y)
                    {
                        return FourDiagonalDirection.LeftDown;
                    }
                    else
                    {
                        return FourDiagonalDirection.RightUp;
                    }
                case < 0 when vector2.y < 0:
                    if (-vector2.x > -vector2.y)
                    {
                        return FourDiagonalDirection.LeftUp;
                    }
                    else
                    {
                        return FourDiagonalDirection.RightDown;
                    }
                default:
                    return FourDiagonalDirection.None;
            }
        }
    }
}