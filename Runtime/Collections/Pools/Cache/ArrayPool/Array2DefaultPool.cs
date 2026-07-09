using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core.Pools
{
    public static class Array2DefaultPool<T>
    {
        private static readonly Dictionary<Vector2Int, Stack<T[,]>> pools = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Get(Vector2Int size)
        {
            var pool = pools.GetOrCreate(size);

            return pool.Count > 0 ? pool.Pop() : new T[size.x, size.y];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(T[,] array)
        {
            var pool = pools.GetOrCreate(new(array.GetLength(0), array.GetLength(1)));

            pool.Push(array);
        }
    }
}