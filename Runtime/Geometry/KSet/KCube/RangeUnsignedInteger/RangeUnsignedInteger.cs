using System;
using UnityEngine;

namespace VMFramework.Core
{
    public partial struct RangeUnsignedInteger : IKCubeInteger<uint>, IEquatable<RangeUnsignedInteger>, IFormattable
    {
        public static RangeUnsignedInteger Zero { get; } = new(0, 0);

        public static RangeUnsignedInteger One { get; } = new(1, 1);

        public static RangeUnsignedInteger Unit { get; } = new(0, 1);
        
        public static RangeUnsignedInteger Infinite { get; } = new(uint.MinValue, uint.MaxValue);

        public uint Size => max - min + 1;

        public int Count => (int)Size;

        public uint Pivot => (max + min) / 2;

        public uint min, max;

        #region Constructor

        public RangeUnsignedInteger(uint min, uint max)
        {
            this.min = min;
            this.max = max;
        }

        public RangeUnsignedInteger(uint length)
        {
            min = 0;
            max = length - 1;
        }

        public RangeUnsignedInteger(RangeUnsignedInteger source)
        {
            min = source.min;
            max = source.max;
        }

        public RangeUnsignedInteger(IMinMaxOwner<uint> config)
        {
            if (config is null)
            {
                min = 0;
                max = 0;
                return;
            }

            min = config.Min;
            max = config.Max;
        }

        public RangeUnsignedInteger(Vector2Int range)
        {
            min = (uint)range.x;
            max = (uint)range.y;
        }

        #endregion

        #region Equatable

        public bool Equals(RangeUnsignedInteger other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            return obj is RangeUnsignedInteger other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(min, max);
        }

        #endregion

        #region To String

        public override string ToString() => $"[{min}, {max}]";

        public string ToString(string format, IFormatProvider formatProvider) =>
            $"[{min.ToString(format, formatProvider)}, {max.ToString(format, formatProvider)}]";

        #endregion
    }
}