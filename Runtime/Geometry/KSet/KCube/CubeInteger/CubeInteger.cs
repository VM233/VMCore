using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Random = System.Random;

namespace VMFramework.Core
{
    [Serializable]
    public struct CubeInteger : IKCubeInteger<Vector3Int>, IEquatable<CubeInteger>, IFormattable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubeInteger FromPivotExtents(Vector3Int pivot, Vector3Int extents) =>
            new(pivot - extents, pivot + extents);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubeInteger FromPivotExtents(Vector3Int pivot, int extents)
        {
            var extentsVector = new Vector3Int(extents, extents, extents);
            return new(pivot - extentsVector, pivot + extentsVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubeInteger FromCorners(Vector3Int corner1, Vector3Int corner2) =>
            new(corner1.Min(corner2), corner1.Max(corner2));

        public static CubeInteger Max { get; } = new(CommonVector3Int.minValue, CommonVector3Int.maxValue);

        public static CubeInteger Zero { get; } = new(Vector3Int.zero, Vector3Int.zero);

        public static CubeInteger One { get; } = new(Vector3Int.one, Vector3Int.one);

        public static CubeInteger Unit { get; } = new(Vector3Int.zero, Vector3Int.one);

        public Vector3Int Size => max - min + Vector3Int.one;

        public Vector3Int Pivot => (max + min) / 2;

        public Vector3Int min, max;
        public bool inverseX, inverseY, inverseZ;

        public int Count => Size.Products();

        public RangeInteger XRange => new(min.x, max.x);

        public RangeInteger YRange => new(min.y, max.y);

        public RangeInteger ZRange => new(min.z, max.z);

        public RectangleInteger XYRectangle => new(min.x, min.y, max.x, max.y);

        public RectangleInteger XZRectangle => new(min.x, min.z, max.x, max.z);

        public RectangleInteger YZRectangle => new(min.y, min.z, max.y, max.z);

        #region Constructor

        public CubeInteger(RangeInteger xRange, RangeInteger yRange, RangeInteger zRange) : this(
            new Vector3Int(xRange.min, yRange.min, zRange.min), new Vector3Int(xRange.max, yRange.max, zRange.max))
        {

        }

        public CubeInteger(RectangleInteger xyRectangle, RangeInteger zRange) : this(
            new Vector3Int(xyRectangle.min.x, xyRectangle.min.y, zRange.min),
            new Vector3Int(xyRectangle.max.x, xyRectangle.max.y, zRange.max))
        {

        }

        public CubeInteger(RangeInteger xRange, RectangleInteger yzRectangle) : this(
            new Vector3Int(xRange.min, yzRectangle.min.x, yzRectangle.min.y),
            new Vector3Int(xRange.max, yzRectangle.max.x, yzRectangle.max.y))
        {

        }

        public CubeInteger(int xMin, int yMin, int zMin, int xMax, int yMax, int zMax) : this(
            new Vector3Int(xMin, yMin, zMin), new Vector3Int(xMax, yMax, zMax))
        {

        }

        public CubeInteger(Vector3Int min, Vector3Int max)
        {
            this.min = min;
            this.max = max;
            inverseX = false;
            inverseY = false;
            inverseZ = false;
        }

        public CubeInteger(int width, int length, int height) : this(Vector3Int.zero,
            new Vector3Int(width - 1, length - 1, height - 1))
        {

        }

        public CubeInteger(Vector3Int size) : this(Vector3Int.zero, size - Vector3Int.one)
        {

        }

        public CubeInteger(CubeInteger source) : this(source.min, source.max)
        {

        }

        public CubeInteger([DisallowNull] IMinMaxOwner<Vector3Int> config) : this(config.Min, config.Max)
        {

        }

        #endregion

        #region Equatable

        public bool Equals(CubeInteger other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            return obj is CubeInteger other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(min, max);
        }

        #endregion

        #region To String

        public override string ToString() => $"[{min}, {max}]";

        public string ToString(string format, IFormatProvider formatProvider) =>
            $"[{min.ToString(format, formatProvider)},{max.ToString(format, formatProvider)}]";

        #endregion
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<Vector3Int> IEnumerable<Vector3Int>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<Vector3Int>
        {
            private readonly CubeInteger cube;
            private readonly bool isValid;
            private int x, y, z;

            public Enumerator(CubeInteger cube)
            {
                if (cube.min.x > cube.max.x || cube.min.y > cube.max.y || cube.min.z > cube.max.z)
                {
                    isValid = false;
                    x = y = z = 0;
                }
                else
                {
                    isValid = true;

                    if (cube.inverseX)
                    {
                        (cube.min.x, cube.max.x) = (-cube.max.x, -cube.min.x);
                    }

                    if (cube.inverseY)
                    {
                        (cube.min.y, cube.max.y) = (-cube.max.y, -cube.min.y);
                    }

                    if (cube.inverseZ)
                    {
                        (cube.min.z, cube.max.z) = (-cube.max.z, -cube.min.z);
                    }

                    x = cube.min.x;
                    y = cube.min.y;
                    z = cube.min.z - 1;
                }

                this.cube = cube;
            }

            public Vector3Int Current
            {
                get
                {
                    var currentX = cube.inverseX? -x : x;
                    var currentY = cube.inverseY? -y : y;
                    var currentZ = cube.inverseZ? -z : z;
                    return new(currentX, currentY, currentZ);
                }
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (isValid == false)
                {
                    return false;
                }

                z++;

                if (z > cube.max.z)
                {
                    z = cube.min.z;
                    y++;

                    if (y > cube.max.y)
                    {
                        y = cube.min.y;
                        x++;

                        if (x > cube.max.x)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            public void Reset()
            {
                x = cube.min.x;
                y = cube.min.y;
                z = cube.min.z - 1;
            }

            public void Dispose() { }
        }
        Vector3Int IMinMaxOwner<Vector3Int>.Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => min = value;
        }

        Vector3Int IMinMaxOwner<Vector3Int>.Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => max = value;
        }

        Vector3Int IReadOnlyMinMaxOwner<Vector3Int>.GetMin()
        {
            return min;
        }

        Vector3Int IReadOnlyMinMaxOwner<Vector3Int>.GetMax()
        {
            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Vector3Int pos) => pos.x >= min.x && pos.x <= max.x &&
                                                pos.y >= min.y && pos.y <= max.y &&
                                                pos.z >= min.z && pos.z <= max.z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Int GetRelativePos(Vector3Int pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Int ClampMin(Vector3Int pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Int ClampMax(Vector3Int pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Int GetRandomItem(Random random) => random.Range(min, max);

        public IChooser<Vector3Int> GenerateNewChooser() => this;

        public IChooser GenerateNewObjectChooser() => this;

        object IRandomItemProvider.GetRandomObjectItem(Random random)
        {
            return GetRandomItem(random);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Vector3Int min, out Vector3Int max)
        {
            min = this.min;
            max = this.max;
        }

        public static CubeInteger operator +(CubeInteger a, Vector3Int b) =>
            new(a.min + b, a.max + b);

        public static CubeInteger operator -(CubeInteger a, Vector3Int b) =>
            new(a.min - b, a.max - b);

        public static CubeInteger operator *(CubeInteger a, Vector3Int b)
        {
            var xMin = a.min.x;
            var xMax = a.max.x;

            if (b.x >= 0)
            {
                xMin *= b.x;
                xMax *= b.x;
            }
            else
            {
                (xMin, xMax) = (xMax * b.x, xMin * b.x);
            }

            var yMin = a.min.y;
            var yMax = a.max.y;

            if (b.y >= 0)
            {
                yMin *= b.y;
                yMax *= b.y;
            }
            else
            {
                (yMin, yMax) = (yMax * b.y, yMin * b.y);
            }

            var zMin = a.min.z;
            var zMax = a.max.z;

            if (b.z >= 0)
            {
                zMin *= b.z;
                zMax *= b.z;
            }
            else
            {
                (zMin, zMax) = (zMax * b.z, zMin * b.z);
            }

            return new(xMin, yMin, zMin, xMax, yMax, zMax);
        }

        public static CubeInteger operator *(CubeInteger a, int b)
        {
            if (b >= 0)
            {
                return new(a.min * b, a.max * b);
            }

            return new(a.max * b, a.min * b);
        }

        public static CubeInteger operator /(CubeInteger a, Vector3Int b)
        {
            var xMin = a.min.x;
            var xMax = a.max.x;

            if (b.x >= 0)
            {
                xMin /= b.x;
                xMax /= b.x;
            }
            else
            {
                (xMin, xMax) = (xMax / b.x, xMin / b.x);
            }

            var yMin = a.min.y;
            var yMax = a.max.y;

            if (b.y >= 0)
            {
                yMin /= b.y;
                yMax /= b.y;
            }
            else
            {
                (yMin, yMax) = (yMax / b.y, yMin / b.y);
            }

            var zMin = a.min.z;
            var zMax = a.max.z;

            if (b.z >= 0)
            {
                zMin /= b.z;
                zMax /= b.z;
            }
            else
            {
                (zMin, zMax) = (zMax / b.z, zMin / b.z);
            }

            return new(xMin, yMin, zMin, xMax, yMax, zMax);
        }

        public static CubeInteger operator /(CubeInteger a, int b)
        {
            if (b >= 0)
            {
                return new(a.min / b, a.max / b);
            }

            return new(a.max / b, a.min / b);
        }

        public static CubeInteger operator -(CubeInteger a) =>
            new(-a.max, -a.min);

        public static bool operator ==(CubeInteger a, CubeInteger b) =>
            a.Equals(b);

        public static bool operator !=(CubeInteger a, CubeInteger b) =>
            !a.Equals(b);

        public static implicit operator (Vector3Int min, Vector3Int max)(CubeInteger cube) => (cube.min, cube.max);

        public static implicit operator CubeInteger((Vector3Int min, Vector3Int max) tuple) => new(tuple.min, tuple.max);

        public static implicit operator BoundsInt(CubeInteger cube) =>
            new(cube.min, cube.max - cube.min + Vector3Int.one);
    }
}
