using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial struct RangeUnsignedInteger
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out uint min, out uint max)
        {
            min = this.min;
            max = this.max;
        }

        public static RangeUnsignedInteger operator +(RangeUnsignedInteger a, uint b) => new(a.min + b, a.max + b);

        public static RangeUnsignedInteger operator -(RangeUnsignedInteger a, uint b) => new(a.min - b, a.max - b);

        public static RangeUnsignedInteger operator *(RangeUnsignedInteger a, uint b)
        {
            return new(a.min * b, a.max * b);
        }

        public static RangeUnsignedInteger operator /(RangeUnsignedInteger a, uint b)
        {
            return new(a.min / b, a.max / b);
        }

        public static bool operator ==(RangeUnsignedInteger a, RangeUnsignedInteger b) => a.Equals(b);

        public static bool operator !=(RangeUnsignedInteger a, RangeUnsignedInteger b) => !a.Equals(b);

        public static implicit operator RangeUnsignedInteger(Vector2Int range) => new(range);

        public static implicit operator (uint min, uint max)(RangeUnsignedInteger range) => (range.min, range.max);

        public static implicit operator RangeUnsignedInteger((uint min, uint max) range) => new(range.min, range.max);
    }
}