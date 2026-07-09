using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ArrayUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this T[] array) => array == null || array.Length == 0;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this T[,] array) => array == null || array.Length == 0;

        /// <summary>
        /// Moves a segment of an array to a new position.
        /// For example, if the array is [1, 2, 3, 4, 5]
        /// and you want to move the segment [2, 3, 4] with an offset of 1,
        /// use array.MoveSegment(1, 3, 1) to get [1, 2, 2, 3, 4].
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="offset"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MoveSegment<T>(this T[] array, int start, int end, int offset)
        {
            if (offset == 0)
            {
                return;
            }

            if (start + offset < 0)
            {
                throw new ArgumentException($"{nameof(start)} + {nameof(offset)} < 0");
            }

            if (end + offset > array.Length)
            {
                throw new ArgumentException($"{nameof(end)} + {nameof(offset)} > {nameof(array)}.Length");
            }
            
            for (int i = start; i <= end; i++)
            {
                array[i + offset] = array[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateOrResizeArrayWithMinLength<T>(this int minLength, ref T[] array)
        {
            if (array == null)
            {
                array = new T[minLength];
            }
            else if (array.Length < minLength)
            {
                Array.Resize(ref array, minLength);
            }
        }

        #region Array 3D

        #region Get

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this T[,,] array, Vector3Int pos)
        {
            return array[pos.x, pos.y, pos.z];
        }

        #endregion

        #region Size

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int GetSize<T>(this T[,,] array)
        {
            if (array == null)
            {
                return default;
            }

            return new(array.GetLength(0),
                array.GetLength(1),
                array.GetLength(2));
        }

        #endregion

        #region Enumerate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(Vector3Int, T)> Enumerate<T>(this T[,,] array,
            Vector3Int indexOffset = default)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int x = indexOffset.x + i;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    int y = indexOffset.y + j;
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        int z = indexOffset.z + k;
                        yield return (new(x, y, z), array[i, j, k]);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Array 2D

        #region Get

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this T[,] array, Vector2Int pos)
        {
            return array[pos.x, pos.y];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetRectangle<T>(this T[,] array,
            Vector2Int from, Vector2Int to)
        {
            return from.GetRectangle(to).Select(array.Get);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetByX<T>(this T[,] array, int x)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                yield return array[x, y];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetByXRange<T>(this T[,] array, int xFrom,
            int xTo)
        {
            for (int x = xFrom; x <= xTo; x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    yield return array[x, y];
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetByY<T>(this T[,] array, int y)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                yield return array[x, y];
            }
        }

        #endregion

        #region Pop

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Pop<T>(this T[,] array, Vector2Int pos)
        {
            var popItem = array[pos.x, pos.y];
            array[pos.x, pos.y] = default;
            return popItem;
        }

        #endregion

        #region Size

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int GetSize<T>(this T[,] array)
        {
            if (array == null)
            {
                return default;
            }

            return new(array.GetLength(0), array.GetLength(1));
        }

        #endregion

        #region Enumerate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(Vector2Int, T)> Enumerate<T>(this T[,] array,
            Vector2Int indexOffset = default)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int x = indexOffset.x + i;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    int y = indexOffset.y + j;
                    yield return (new(x, y), array[i, j]);
                }
            }
        }

        public static IEnumerable<(Vector2Int pos, T)> EnumerateRectangle<T>(
            this T[,] array,
            Vector2Int from, Vector2Int to, Vector2Int indexOffset = default)
        {
            return from.GetRectangle(to)
                .Select(pos => (pos + indexOffset, array.Get(pos)));
        }

        #endregion

        #endregion
    }
}

