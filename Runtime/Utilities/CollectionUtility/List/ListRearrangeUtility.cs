using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class ListRearrangeUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterleaveByColumnGroups<T, TTarget>(this IReadOnlyList<T> source, int groupCount,
            TTarget target)
            where TTarget : ICollection<T>, new()
        {
            if (groupCount <= 0) throw new ArgumentException("groupCount must be positive");
            int total = source.Count;
            int groupSize = (int)Math.Ceiling((double)total / groupCount);

            for (int i = 0; i < groupSize; i++)
            {
                for (int j = 0; j < groupCount; j++)
                {
                    int index = j * groupSize + i;
                    if (index < total)
                    {
                        target.Add(source[index]);
                    }
                }
            }
        }
    }
}