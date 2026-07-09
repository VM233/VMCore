using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Linq
{
    public static class BoundFindingUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFindBoundingElements<TItem, TTarget>(this IEnumerable<TItem> sortedEnumerable,
            TTarget target, out (TItem left, TItem right) bound, [DisallowNull] Func<TItem, TTarget> targetGetter)
            where TTarget : IComparable<TTarget>
        {
            TItem lastElement = default;
            bool hasLastElement = false;

            foreach (var item in sortedEnumerable)
            {
                var currentTarget = targetGetter(item);

                if (target.Below(currentTarget))
                {
                    if (hasLastElement == false)
                    {
                        bound = (default, item);
                        return false;
                    }
                    
                    bound = (lastElement, item);
                    return true;
                }
                
                lastElement = item;
                hasLastElement = true;
            }
            
            bound = (lastElement, default);
            return false;
        }

        /// <summary>
        /// 从一个排序好的数组中找到两个元素，使得目标值在这两个元素之间
        /// </summary>
        public static (T, T) FindBoundingElements<T, TTarget>(this TTarget target,
            IEnumerable<T> sortedEnumerable, Func<T, TTarget> extractBound)
            where TTarget : IComparable<TTarget>
            where T : class
        {
            T lastElement = null;

            foreach (var item in sortedEnumerable)
            {
                if (target.Below(extractBound(item)))
                {
                    return (lastElement, item);
                }

                lastElement = item;
            }

            return (lastElement, null);
        }

        /// <summary>
        /// 从一个排序好的数组中找到两个元素，使得目标值在这两个元素之间
        /// </summary>
        public static (TTarget, TTarget) FindBoundingElements<TTarget>(
            this TTarget target, IEnumerable<TTarget> sortedEnumerable, 
            TTarget firstBoundary, TTarget lastBoundary)
            where TTarget : IComparable<TTarget>
        {
            TTarget lastElement = firstBoundary;

            foreach (var item in sortedEnumerable)
            {
                if (target.Below(item))
                {
                    return (lastElement, item);
                }

                lastElement = item;
            }

            return (lastElement, lastBoundary);
        }

        /// <summary>
        /// 从一个排序好的数组中找到两个Index，使得目标值在这两个Index对应的值之间
        /// </summary>
        public static (int, int) FindBoundingIndices<T, TTarget>(this TTarget target,
            IEnumerable<T> sortedEnumerable, Func<T, TTarget> extractBound)
            where TTarget : IComparable<TTarget>
        {
            int lastIndex = 0;
            foreach (var (index, item) in sortedEnumerable.Enumerate())
            {
                if (target.Below(extractBound(item)))
                {
                    return (index - 1, index);
                }

                lastIndex = index;
            }

            return (lastIndex, lastIndex + 1);
        }

        /// <summary>
        /// 从一个排序好的数组中找到两个Index，使得目标值在这两个Index对应的值之间
        /// </summary>
        public static (int, int) FindBoundingIndices<TTarget>(this TTarget target,
            IEnumerable<TTarget> sortedEnumerable)
            where TTarget : IComparable<TTarget>
        {
            int lastIndex = 0;
            foreach (var (index, item) in sortedEnumerable.Enumerate())
            {
                if (target.Below(item))
                {
                    return (index - 1, index);
                }

                lastIndex = index;
            }

            return (lastIndex, lastIndex + 1);
        }
    }
}