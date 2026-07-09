using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class RandomBoolUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NextBool(this Random random) => random.Next(2) == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NextBool(this Random random, double trueProbability) => random.NextDouble() < trueProbability;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Random01(this Random random, double trueProbability) =>
            random.NextBool(trueProbability) ? 1 : 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RandomBool(this float trueProbability) => GlobalRandom.Default.NextBool(trueProbability);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RandomBool(this double trueProbability) => GlobalRandom.Default.NextBool(trueProbability);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Random01(this float trueProbability) => GlobalRandom.Default.Random01(trueProbability);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Random01(this double trueProbability) => GlobalRandom.Default.Random01(trueProbability);
    }
}