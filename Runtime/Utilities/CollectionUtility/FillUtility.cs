using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class FillUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }
    }
}