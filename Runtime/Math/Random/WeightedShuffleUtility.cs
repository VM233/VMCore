using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core.Pools;

namespace VMFramework.Core
{
    public static class WeightedShuffleUtility
    {
        /// <summary>
        /// 按权重“无放回”随机打乱：权重越高，越有可能排在前面。
        /// 使用 Efraimidis–Spirakis 的加权随机排列方法：key = -ln(U) / weight，按 key 升序排序。
        /// weight 小于等于 0 的元素会被放到末尾（末尾内部仍随机）。
        /// </summary>
        public static void WeightedShuffleInPlace<T>(this Random random, IList<T> items, Func<T, double> weightSelector)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (weightSelector == null)
            {
                throw new ArgumentNullException(nameof(weightSelector));
            }

            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            int count = items.Count;
            if (count <= 1)
            {
                return;
            }

            var entries = ListPool<Entry<T>>.Shared.Get();
            entries.Clear();

            for (int index = 0; index < count; index++)
            {
                T item = items[index];
                double weight = weightSelector(item);

                if (double.IsNaN(weight) || double.IsInfinity(weight))
                {
                    throw new ArgumentException("Weight must be a finite number.", nameof(weightSelector));
                }

                if (weight < 0.0)
                {
                    throw new ArgumentException("Weight must be >= 0.", nameof(weightSelector));
                }

                double tieBreaker = NextPositiveOpenInterval(random);

                if (weight > 0.0)
                {
                    double u = NextPositiveOpenInterval(random);
                    double sortKey = -Math.Log(u) / weight;
                    entries.Add(new Entry<T>(item, sortKey, tieBreaker, hasPositiveWeight: true));
                }
                else
                {
                    // 权重为 0：统一放后面，但后面内部随机
                    entries.Add(new Entry<T>(item, sortKey: double.PositiveInfinity, tieBreaker,
                        hasPositiveWeight: false));
                }
            }

            entries.Sort(CompareEntries);

            for (int index = 0; index < count; index++)
            {
                items[index] = entries[index].Item;
            }

            entries.ReturnToSharedPool();
        }

        /// <inheritdoc cref="WeightedShuffleInPlace{T}(Random, IList{T}, Func{T, double})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WeightedShuffleInPlace<T>(this Random random, IList<T> items,
            IReadOnlyDictionary<T, double> weightsByItem)
        {
            if (weightsByItem == null)
            {
                throw new ArgumentNullException(nameof(weightsByItem));
            }

            random.WeightedShuffleInPlace(items, item => weightsByItem.GetValueOrDefault(item, 0.0));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CompareEntries<T>(Entry<T> left, Entry<T> right)
        {
            int keyComparison = left.SortKey.CompareTo(right.SortKey);
            if (keyComparison != 0)
            {
                return keyComparison;
            }

            // 末尾（SortKey=+inf）内部也要随机，用 tieBreaker 打散
            return left.TieBreaker.CompareTo(right.TieBreaker);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double NextPositiveOpenInterval(Random random)
        {
            // 需要 (0, 1) 避免 Log(0)
            double value = random.NextDouble();
            while (value <= 0.0 || value >= 1.0)
            {
                value = random.NextDouble();
            }

            return value;
        }

        private readonly struct Entry<T>
        {
            public Entry(T item, double sortKey, double tieBreaker, bool hasPositiveWeight)
            {
                Item = item;
                SortKey = sortKey;
                TieBreaker = tieBreaker;
                HasPositiveWeight = hasPositiveWeight;
            }

            public T Item { get; }
            public double SortKey { get; }
            public double TieBreaker { get; }
            public bool HasPositiveWeight { get; }
        }
    }
}