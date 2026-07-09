using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EnumsNET;

namespace VMFramework.Core
{
    public static class EightDirectionsNeighborsQuery
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TItem GetNeighbor<TItem>(this EightDirectionsNeighbors<TItem> allNeighbors,
            EightTypesDirection direction)
        {
            return direction switch
            {
                EightTypesDirection.UpLeft => allNeighbors.leftUp,
                EightTypesDirection.Up => allNeighbors.up,
                EightTypesDirection.UpRight => allNeighbors.rightUp,
                EightTypesDirection.Left => allNeighbors.left,
                EightTypesDirection.Right => allNeighbors.right,
                EightTypesDirection.DownLeft => allNeighbors.leftDown,
                EightTypesDirection.Down => allNeighbors.down,
                EightTypesDirection.DownRight => allNeighbors.rightDown,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TItem GetNeighbor<TItem>(this EightDirectionsNeighbors<TItem> allNeighbors,
            FourTypesDirection direction)
        {
            return direction switch
            {
                FourTypesDirection.Left => allNeighbors.left,
                FourTypesDirection.Right => allNeighbors.right,
                FourTypesDirection.Up => allNeighbors.up,
                FourTypesDirection.Down => allNeighbors.down,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TItem GetNeighbor<TItem>(this EightDirectionsNeighbors<TItem> allNeighbors,
            LeftRightDirection direction)
        {
            return direction switch
            {
                LeftRightDirection.Left => allNeighbors.left,
                LeftRightDirection.Right => allNeighbors.right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TItem GetNeighbor<TItem>(this EightDirectionsNeighbors<TItem> allNeighbors, int index)
        {
            return index switch
            {
                0 => allNeighbors.leftUp,
                1 => allNeighbors.up,
                2 => allNeighbors.rightUp,
                3 => allNeighbors.left,
                4 => allNeighbors.right,
                5 => allNeighbors.leftDown,
                6 => allNeighbors.down,
                7 => allNeighbors.rightDown,
                _ => throw new IndexOutOfRangeException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TItem> GetNeighbors<TItem>(this EightDirectionsNeighbors<TItem> allNeighbors,
            EightTypesDirection direction)
        {
            if (direction.HasAnyFlags(EightTypesDirection.UpLeft))
            {
                yield return allNeighbors.leftUp;
            }

            if (direction.HasAnyFlags(EightTypesDirection.Up))
            {
                yield return allNeighbors.up;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.UpRight))
            {
                yield return allNeighbors.rightUp;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.Left))
            {
                yield return allNeighbors.left;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.Right))
            {
                yield return allNeighbors.right;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.DownLeft))
            {
                yield return allNeighbors.leftDown;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.Down))
            {
                yield return allNeighbors.down;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.DownRight))
            {
                yield return allNeighbors.rightDown;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetNeighbors<TItem>(this EightDirectionsNeighbors<TItem> allNeighbors,
            EightTypesDirection direction, ref TItem[] neighbors)
        {
            8.CreateOrResizeArrayWithMinLength(ref neighbors);
            
            int count = 0;
            
            if (direction.HasAnyFlags(EightTypesDirection.UpLeft))
            {
                neighbors[count++] = allNeighbors.leftUp;
            }

            if (direction.HasAnyFlags(EightTypesDirection.Up))
            {
                neighbors[count++] = allNeighbors.up;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.UpRight))
            {
                neighbors[count++] = allNeighbors.rightUp;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.Left))
            {
                neighbors[count++] = allNeighbors.left;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.Right))
            {
                neighbors[count++] = allNeighbors.right;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.DownLeft))
            {
                neighbors[count++] = allNeighbors.leftDown;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.Down))
            {
                neighbors[count++] = allNeighbors.down;
            }
            
            if (direction.HasAnyFlags(EightTypesDirection.DownRight))
            {
                neighbors[count++] = allNeighbors.rightDown;
            }
            
            return count;
        }
    }
}