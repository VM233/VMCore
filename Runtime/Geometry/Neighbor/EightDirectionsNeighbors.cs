using System;
using System.Collections;
using System.Collections.Generic;

namespace VMFramework.Core
{
    public readonly struct EightDirectionsNeighbors<TItem> : IEnumerable<TItem>
    {
        public readonly TItem leftUp, up, rightUp;
        public readonly TItem left, right;
        public readonly TItem leftDown, down, rightDown;

        public EightDirectionsNeighbors(TItem left, TItem right, TItem up, TItem down, TItem leftUp, TItem rightUp,
            TItem leftDown, TItem rightDown)
        {
            this.left = left;
            this.right = right;
            this.up = up;
            this.down = down;
            this.leftUp = leftUp;
            this.rightUp = rightUp;
            this.leftDown = leftDown;
            this.rightDown = rightDown;
        }

        public EightDirectionsNeighbors(TItem item)
        {
            left = item;
            right = item;
            up = item;
            down = item;
            leftUp = item;
            rightUp = item;
            leftDown = item;
            rightDown = item;
        }

        #region Operators

        public TItem this[EightTypesDirection direction] => this.GetNeighbor(direction);

        public TItem this[FourTypesDirection direction] => this.GetNeighbor(direction);

        public TItem this[LeftRightDirection direction] => this.GetNeighbor(direction);

        public TItem this[int index] => this.GetNeighbor(index);

        public static implicit operator EightDirectionsNeighbors<TItem>(TItem item) => new(item);

        #endregion

        #region Enumerator

        public Enumerator GetEnumerator() => new(this);

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<TItem>
        {
            private readonly EightDirectionsNeighbors<TItem> neighbors;
            private int index;

            public Enumerator(EightDirectionsNeighbors<TItem> neighbors)
            {
                this.neighbors = neighbors;
                index = -1;
            }

            public TItem Current => neighbors.GetNeighbor(index);

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                index++;
                return index < 8;
            }

            public void Reset()
            {
                index = -1;
            }

            public void Dispose()
            {
            }
        }

        #endregion
    }
}