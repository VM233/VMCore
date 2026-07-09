using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public partial struct RangeInteger : IMinimumClampable<double>, IMaximumClampable<double>
    {
        int IMinMaxOwner<int>.Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => min = value;
        }

        int IMinMaxOwner<int>.Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => max = value;
        }

        int IReadOnlyMinMaxOwner<int>.GetMin()
        {
            return min;
        }

        int IReadOnlyMinMaxOwner<int>.GetMax()
        {
            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(int pos) => pos >= min && pos <= max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetRelativePos(int pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ClampMin(int pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ClampMax(int pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetRandomItem(Random random) => random.Range(min, max);

        public IChooser<int> GenerateNewChooser() => this;

        public IChooser GenerateNewObjectChooser() => this;

        object IRandomItemProvider.GetRandomObjectItem(Random random)
        {
            return GetRandomItem(random);
        }

        public bool TryClampByMinimum(double minimum)
        {
            min = min.D().ClampMin(minimum).Round();
            max = max.D().ClampMin(minimum).Round();
            return true;
        }

        public bool TryClampByMaximum(double maximum)
        {
            min = min.D().ClampMax(maximum).Round();
            max = max.D().ClampMax(maximum).Round();
            return true;
        }
    }
}