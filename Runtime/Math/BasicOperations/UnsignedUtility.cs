using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class UnsignedUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong MinusAndClampMin(this ulong a, ulong b, ulong min)
        {
            if (a <= b)
            {
                return min;
            }
            
            var result = a - b;
            return result < min ? min : result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong MinusAndClampZero(this ulong a, ulong b)
        {
            if (a <= b)
            {
                return 0;
            }
            
            return a - b;
        }
    }
}