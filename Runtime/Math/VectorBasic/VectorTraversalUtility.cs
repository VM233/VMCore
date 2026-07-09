using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class VectorTraversalUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetNumbers<TCollection>(this Vector2 vector, TCollection collection)
            where TCollection : ICollection<float>
        {
            collection.Add(vector.x);
            collection.Add(vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetNumbers<TCollection>(this Vector3 vector, TCollection collection)
            where TCollection : ICollection<float>
        {
            collection.Add(vector.x);
            collection.Add(vector.y);
            collection.Add(vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetNumbers<TCollection>(this Vector4 vector, TCollection collection)
            where TCollection : ICollection<float>
        {
            collection.Add(vector.x);
            collection.Add(vector.y);
            collection.Add(vector.z);
            collection.Add(vector.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetNumbers<TCollection>(this Vector2Int vector, TCollection collection)
            where TCollection : ICollection<int>
        {
            collection.Add(vector.x);
            collection.Add(vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetNumbers<TCollection>(this Vector3Int vector, TCollection collection)
            where TCollection : ICollection<int>
        {
            collection.Add(vector.x);
            collection.Add(vector.y);
            collection.Add(vector.z);
        }
    }
}