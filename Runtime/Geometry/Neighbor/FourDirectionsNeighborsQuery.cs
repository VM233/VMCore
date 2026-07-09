using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EnumsNET;

namespace VMFramework.Core
{
    public static class FourDirectionsNeighborsQuery
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TItem GetNeighbor<TItem>(this FourDirectionsNeighbors<TItem> neighbors,
            FourTypesDirection direction)
        {
            return direction switch
            {
                FourTypesDirection.Left => neighbors.left,
                FourTypesDirection.Right => neighbors.right,
                FourTypesDirection.Up => neighbors.up,
                FourTypesDirection.Down => neighbors.down,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TItem GetNeighbor<TItem>(this FourDirectionsNeighbors<TItem> neighbors,
            LeftRightDirection direction)
        {
            return direction switch
            {
                LeftRightDirection.Left => neighbors.left,
                LeftRightDirection.Right => neighbors.right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        /// <summary>
        /// 通过索引获取四邻域中的某个元素，
        /// 索引范围为闭区间[0, 3]，分别对应左、上、右、下
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">超出索引范围</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TItem GetNeighbor<TItem>(this FourDirectionsNeighbors<TItem> neighbors, int index)
        {
            return index switch
            {
                0 => neighbors.left,
                1 => neighbors.up,
                2 => neighbors.right,
                3 => neighbors.down,
                _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetClockwiseNeighbors<TItem, TCollection>(this FourDirectionsNeighbors<TItem> neighbors,
            FourTypesDirection startDirection, TCollection collection)
            where TCollection : ICollection<TItem>
        {
            switch (startDirection)
            {
                case FourTypesDirection.Left:
                    collection.Add(neighbors.left);
                    collection.Add(neighbors.up);
                    collection.Add(neighbors.right);
                    collection.Add(neighbors.down);
                    break;
                case FourTypesDirection.Right:
                    collection.Add(neighbors.right);
                    collection.Add(neighbors.down);
                    collection.Add(neighbors.left);
                    collection.Add(neighbors.up);
                    break;
                case FourTypesDirection.Up:
                    collection.Add(neighbors.up);
                    collection.Add(neighbors.right);
                    collection.Add(neighbors.down);
                    collection.Add(neighbors.left);
                    break;
                case FourTypesDirection.Down:
                    collection.Add(neighbors.down);
                    collection.Add(neighbors.left);
                    collection.Add(neighbors.up);
                    collection.Add(neighbors.right);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(startDirection), startDirection, null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TItem> GetNeighbors<TItem>(this FourDirectionsNeighbors<TItem> neighbors,
            FourTypesDirection direction)
        {
            if (direction.HasAnyFlags(FourTypesDirection.Left))
            {
                yield return neighbors.left;
            }

            if (direction.HasAnyFlags(FourTypesDirection.Right))
            {
                yield return neighbors.right;
            }

            if (direction.HasAnyFlags(FourTypesDirection.Up))
            {
                yield return neighbors.up;
            }

            if (direction.HasAnyFlags(FourTypesDirection.Down))
            {
                yield return neighbors.down;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetNeighbors<TItem>(this FourDirectionsNeighbors<TItem> neighbors,
            FourTypesDirection direction, ref TItem[] neighborsArray)
        {
            4.CreateOrResizeArrayWithMinLength(ref neighborsArray);

            int count = 0;

            if (direction.HasAnyFlags(FourTypesDirection.Left))
            {
                neighborsArray[count++] = neighbors.left;
            }

            if (direction.HasAnyFlags(FourTypesDirection.Right))
            {
                neighborsArray[count++] = neighbors.right;
            }

            if (direction.HasAnyFlags(FourTypesDirection.Up))
            {
                neighborsArray[count++] = neighbors.up;
            }

            if (direction.HasAnyFlags(FourTypesDirection.Down))
            {
                neighborsArray[count++] = neighbors.down;
            }

            return count;
        }
    }
}