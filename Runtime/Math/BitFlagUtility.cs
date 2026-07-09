using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class BitFlagUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetFlagsCount(this int flags)
        {
            int count = 0;
            while (flags != 0)
            {
                if ((flags & 1) != 0)
                {
                    count++;
                }

                flags >>= 1;
            }

            return count;
        }
    }
}