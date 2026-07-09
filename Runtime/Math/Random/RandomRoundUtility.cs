using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class RandomRoundUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomRound(this Random random, float value)
        {
            var floor = value.Floor();
            var excess = value - floor;
            var addOne = random.NextBool(excess);
            if (addOne)
            {
                return floor + 1;
            }

            return floor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomRound(this Random random, double value)
        {
            var floor = value.Floor();
            var excess = value - floor;
            var addOne = random.NextBool(excess);
            if (addOne)
            {
                return floor + 1;
            }

            return floor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomRound(this float value) => GlobalRandom.Default.RandomRound(value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomRound(this double value) => GlobalRandom.Default.RandomRound(value);
    }
}