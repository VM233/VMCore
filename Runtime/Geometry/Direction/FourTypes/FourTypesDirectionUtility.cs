using System;
using System.Runtime.CompilerServices;
using EnumsNET;
using UnityEngine;

namespace VMFramework.Core
{
    public static class FourTypesDirectionUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourTypesDirection SingleFlagReversed(this FourTypesDirection direction)
        {
            return direction switch
            {
                FourTypesDirection.Left => FourTypesDirection.Right,
                FourTypesDirection.Right => FourTypesDirection.Left,
                FourTypesDirection.Up => FourTypesDirection.Down,
                FourTypesDirection.Down => FourTypesDirection.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourTypesDirection Reversed(this FourTypesDirection direction)
        {
            var result = FourTypesDirection.None;

            if (direction.HasAnyFlags(FourTypesDirection.Up))
            {
                result |= FourTypesDirection.Down;
            }

            if (direction.HasAnyFlags(FourTypesDirection.Down))
            {
                result |= FourTypesDirection.Up;
            }

            if (direction.HasAnyFlags(FourTypesDirection.Left))
            {
                result |= FourTypesDirection.Right;
            }

            if (direction.HasAnyFlags(FourTypesDirection.Right))
            {
                result |= FourTypesDirection.Left;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourTypesDirection ToFourTypesDirection(this Vector2 vector)
        {
            if (vector.y > vector.x)
            {
                return vector.y > -vector.x ? FourTypesDirection.Up : FourTypesDirection.Left;
            }

            return vector.y > -vector.x ? FourTypesDirection.Right : FourTypesDirection.Down;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourTypesDirection ToFourTypesDirection(this Vector2Int vector)
        {
            FourTypesDirection result = FourTypesDirection.None;

            if (vector.x > 0)
            {
                result |= FourTypesDirection.Right;
            }
            else if (vector.x < 0)
            {
                result |= FourTypesDirection.Left;
            }
            
            if (vector.y > 0)
            {
                result |= FourTypesDirection.Up;
            }
            else if (vector.y < 0)
            {
                result |= FourTypesDirection.Down;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ToCardinalVector(this FourTypesDirection direction)
        {
            return direction switch
            {
                FourTypesDirection.Up => Vector2Int.up,
                FourTypesDirection.Down => Vector2Int.down,
                FourTypesDirection.Left => Vector2Int.left,
                FourTypesDirection.Right => Vector2Int.right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourTypesDirection GetOtherDirections(this FourTypesDirection direction)
        {
            return FourTypesDirection.All & ~direction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsHorizontal(this FourTypesDirection direction)
        {
            return (direction & FourTypesDirection.Horizontal) != FourTypesDirection.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsVertical(this FourTypesDirection direction)
        {
            return (direction & FourTypesDirection.Vertical) != FourTypesDirection.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EightTypesDirection ToEightTypesDirection(this FourTypesDirection direction)
        {
            return (EightTypesDirection)direction;
        }
    }
}