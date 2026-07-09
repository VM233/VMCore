using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class ArrayResetUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(this T[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(this T[,] array)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = default;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(this T[,,] array)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    for (var k = 0; k < array.GetLength(2); k++)
                    {
                        array[i, j, k] = default;
                    }
                }
            }
        }
    }
}