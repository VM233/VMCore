using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class ListMatcher
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountOverlappingElements<THeadList, TTailList>(this THeadList head, TTailList tail)
            where THeadList : IReadOnlyList<string>
            where TTailList : IReadOnlyList<string>
        {
            var minLength = head.Count.Min(tail.Count);

            var headStartIndex = head.Count - minLength;

            for (var matchStart = headStartIndex; matchStart < head.Count; matchStart++)
            {
                bool matchFound = true;
                for (var i = matchStart; i < head.Count; i++)
                {
                    if (head[i].Equals(tail[i - matchStart]) == false)
                    {
                        matchFound = false;
                        break;
                    }
                }

                if (matchFound)
                {
                    return head.Count - matchStart;
                }
            }

            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountOverlappingElements<THeadList, TTailList>(this THeadList head, TTailList tail,
            StringComparison comparison)
            where THeadList : IReadOnlyList<string>
            where TTailList : IReadOnlyList<string>
        {
            var minLength = head.Count.Min(tail.Count);

            var headStartIndex = head.Count - minLength;

            for (var matchStart = headStartIndex; matchStart < head.Count; matchStart++)
            {
                bool matchFound = true;
                for (var i = matchStart; i < head.Count; i++)
                {
                    if (head[i].Equals(tail[i - matchStart], comparison) == false)
                    {
                        matchFound = false;
                        break;
                    }
                }

                if (matchFound)
                {
                    return head.Count - matchStart;
                }
            }

            return 0;
        }
    }
}