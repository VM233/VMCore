using System.Runtime.CompilerServices;
using UnityEngine;
using Random = System.Random;

namespace VMFramework.Core
{
    public partial struct ColorRange
    {
        Color IMinMaxOwner<Color>.Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => min = value;
        }

        Color IMinMaxOwner<Color>.Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => max = value;
        }

        public Color GetMin() => min;

        public Color GetMax() => max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Color pos) =>
            pos.r >= min.r && pos.r <= max.r && pos.g >= min.g && pos.g <= max.g && pos.b >= min.b && pos.b <= max.b &&
            pos.a >= min.a && pos.a <= max.a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color GetRelativePos(Color pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color ClampMin(Color pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color ClampMax(Color pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color GetRandomItem(Random random) => random.Range(min, max);

        public IChooser<Color> GenerateNewChooser() => this;

        public IChooser GenerateNewObjectChooser() => this;

        object IRandomItemProvider.GetRandomObjectItem(Random random)
        {
            return GetRandomItem(random);
        }
    }
}