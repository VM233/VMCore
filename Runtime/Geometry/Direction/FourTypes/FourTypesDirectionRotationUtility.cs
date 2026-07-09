using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class FourTypesDirectionRotationUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourTypesDirection ClockwiseRotate(this FourTypesDirection direction)
        {
            var result = FourTypesDirection.None;

            if (direction.HasFlag(FourTypesDirection.Up))
            {
                result |= FourTypesDirection.Right;
            }

            if (direction.HasFlag(FourTypesDirection.Right))
            {
                result |= FourTypesDirection.Down;
            }

            if (direction.HasFlag(FourTypesDirection.Down))
            {
                result |= FourTypesDirection.Left;
            }

            if (direction.HasFlag(FourTypesDirection.Left))
            {
                result |= FourTypesDirection.Up;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourTypesDirection CounterclockwiseRotate(this FourTypesDirection direction)
        {
            var result = FourTypesDirection.None;

            if (direction.HasFlag(FourTypesDirection.Up))
            {
                result |= FourTypesDirection.Left;
            }

            if (direction.HasFlag(FourTypesDirection.Left))
            {
                result |= FourTypesDirection.Down;
            }

            if (direction.HasFlag(FourTypesDirection.Down))
            {
                result |= FourTypesDirection.Right;
            }

            if (direction.HasFlag(FourTypesDirection.Right))
            {
                result |= FourTypesDirection.Up;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetClockwiseRotation(this FourTypesDirection from, FourTypesDirection to)
        {
            return from switch
            {
                FourTypesDirection.Left => to switch
                {
                    FourTypesDirection.Left => 0,
                    FourTypesDirection.Right => 2,
                    FourTypesDirection.Up => 1,
                    FourTypesDirection.Down => 3,
                    _ => throw new ArgumentOutOfRangeException(nameof(to), to, null)
                },
                FourTypesDirection.Right => to switch
                {
                    FourTypesDirection.Left => 2,
                    FourTypesDirection.Right => 0,
                    FourTypesDirection.Up => 3,
                    FourTypesDirection.Down => 1,
                    _ => throw new ArgumentOutOfRangeException(nameof(to), to, null)
                },
                FourTypesDirection.Up => to switch
                {
                    FourTypesDirection.Left => 3,
                    FourTypesDirection.Right => 1,
                    FourTypesDirection.Up => 0,
                    FourTypesDirection.Down => 2,
                    _ => throw new ArgumentOutOfRangeException(nameof(to), to, null)
                },
                FourTypesDirection.Down => to switch
                {
                    FourTypesDirection.Left => 1,
                    FourTypesDirection.Right => 3,
                    FourTypesDirection.Up => 2,
                    FourTypesDirection.Down => 0,
                    _ => throw new ArgumentOutOfRangeException(nameof(to), to, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int RotateTo(this Vector2Int vector, FourTypesDirection from, FourTypesDirection to)
        {
            var rotation = from.GetClockwiseRotation(to);
            return rotation switch
            {
                0 => vector,
                1 => new(vector.y, -vector.x),
                2 => new(-vector.x, -vector.y),
                3 => new(-vector.y, vector.x),
                _ => throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RotateTo(this Vector2 vector, FourTypesDirection from, FourTypesDirection to)
        {
            var rotation = from.GetClockwiseRotation(to);
            return rotation switch
            {
                0 => vector,
                1 => new(vector.y, -vector.x),
                2 => new(-vector.x, -vector.y),
                3 => new(-vector.y, vector.x),
                _ => throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFloat RotateTo<TRectangle>(this TRectangle rect, FourTypesDirection from,
            FourTypesDirection to)
            where TRectangle : IKCubeFloat<Vector2>
        {
            var rotatedMin = rect.Min.RotateTo(from, to);
            var rotatedMax = rect.Max.RotateTo(from, to);

            return RectangleFloat.FromCorners(rotatedMin, rotatedMax);
        }
    }
}