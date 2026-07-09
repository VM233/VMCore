using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public partial struct RangeUnsignedInteger
    {
        uint IMinMaxOwner<uint>.Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => min = value;
        }

        uint IMinMaxOwner<uint>.Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => max = value;
        }

        public uint GetMin() => min;
        
        public uint GetMax() => max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(uint pos) => pos >= min && pos <= max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetRelativePos(uint pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint ClampMin(uint pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint ClampMax(uint pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetRandomItem(Random random) => (uint)random.Range((int)min, (int)max);
        
        public IChooser<uint> GenerateNewChooser() => this;

        public IChooser GenerateNewObjectChooser() => this;

        object IRandomItemProvider.GetRandomObjectItem(Random random)
        {
            return GetRandomItem(random);
        }
    }
}