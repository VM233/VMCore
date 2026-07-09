using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public partial struct RangeFloat : IMinimumClampable<double>, IMaximumClampable<double>
    {
        float IMinMaxOwner<float>.Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => min = value;
        }

        float IMinMaxOwner<float>.Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => max = value;
        }

        float IReadOnlyMinMaxOwner<float>.GetMin()
        {
            return min;
        }

        float IReadOnlyMinMaxOwner<float>.GetMax()
        {
            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(float pos) => pos >= min && pos <= max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetRelativePos(float pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ClampMin(float pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ClampMax(float pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetRandomItem(Random random) => random.Range(min, max);

        public IChooser<float> GenerateNewChooser() => this;

        public IChooser GenerateNewObjectChooser() => this;

        object IRandomItemProvider.GetRandomObjectItem(Random random)
        {
            return GetRandomItem(random);
        }

        public bool TryClampByMinimum(double minimum)
        {
            min = min.ClampMin((float)minimum);
            max = max.ClampMin((float)minimum);
            return true;
        }

        public bool TryClampByMaximum(double maximum)
        {
            min = min.ClampMax((float)maximum);
            max = max.ClampMax((float)maximum);
            return true;
        }
    }
}