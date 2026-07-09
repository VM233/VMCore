using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class SplitUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RangeInteger> SpiltToRanges(this RangeInteger range, int step, StepSplitMode mode)
        {
            switch (mode)
            {
                case StepSplitMode.Forwards:
                    for (int i = range.min; i <= range.max; i += step)
                    {
                        yield return new RangeInteger(i, i + step - 1);
                    }
                    break;
                case StepSplitMode.Backwards:
                    for (int i = range.max; i >= range.min; i -= step)
                    {
                        yield return new RangeInteger(i - step + 1, i);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}