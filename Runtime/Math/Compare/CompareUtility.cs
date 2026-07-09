using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class CompareUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SatisfyCompareResult<TComparable, TValue>(this TComparable num, TValue comparison,
            CompareResult targetResult)
            where TComparable : IComparable<TValue>
        {
            var compareResult = num.CompareTo(comparison);
            return compareResult.SatisfyCompareResult(targetResult);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SatisfyCompareResult(this int result, CompareResult targetResult)
        {
            if (targetResult == CompareResult.Less)
            {
                return result < 0;
            }

            if (targetResult == CompareResult.Equal)
            {
                return result == 0;
            }

            if (targetResult == CompareResult.Greater)
            {
                return result > 0;
            }

            if (targetResult == CompareResult.LessOrEqual)
            {
                return result <= 0;
            }

            if (targetResult == CompareResult.GreaterOrEqual)
            {
                return result >= 0;
            }

            throw new ArgumentOutOfRangeException(nameof(targetResult), targetResult, null);
        }
    }
}