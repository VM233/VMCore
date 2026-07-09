using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class ClampBySignUtility
    {
        /// <summary>
        /// 当x小于0时，返回值 ≤ <paramref name="maxWhenNegative"/>；
        /// 当x大于0时，返回值 ≥ <paramref name="minWhenPositive"/>；
        /// x==0 返回 0。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ClampBySign(this float value, float maxWhenNegative, float minWhenPositive)
        {
            if (value < 0)
            {
                return value <= maxWhenNegative ? value : maxWhenNegative;
            }

            if (value > 0)
            {
                return value >= minWhenPositive ? value : minWhenPositive;
            }

            return 0;
        }
    }
}