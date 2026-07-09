using System.Collections;
using System.Collections.Generic;

namespace VMFramework.Core
{
    public struct FourDiagonalDirectionsNeighbors<TItem> : IEnumerable<TItem>
    {
        public TItem leftUp, rightDown, leftDown, rightUp;

        public FourDiagonalDirectionsNeighbors(TItem leftUp, TItem rightDown, TItem leftDown, TItem rightUp)
        {
            this.leftUp = leftUp;
            this.rightDown = rightDown;
            this.leftDown = leftDown;
            this.rightUp = rightUp;
        }

        public FourDiagonalDirectionsNeighbors(TItem item)
        {
            this.leftUp = item;
            this.rightDown = item;
            this.leftDown = item;
            this.rightUp = item;
        }

        #region Operators

        public TItem this[int index] => this.GetNeighbor(index);

        public static implicit operator FourDiagonalDirectionsNeighbors<TItem>(TItem item) => new(item);

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
            private readonly FourDiagonalDirectionsNeighbors<TItem> neighbors;
            private int index;

            public Enumerator(FourDiagonalDirectionsNeighbors<TItem> neighbors)
            {
                this.neighbors = neighbors;
                index = -1;
            }

            public TItem Current => neighbors.GetNeighbor(index);

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                index++;
                return index < 4;
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