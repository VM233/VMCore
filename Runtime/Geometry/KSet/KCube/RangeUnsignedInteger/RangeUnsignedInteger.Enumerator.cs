using System.Collections;
using System.Collections.Generic;

namespace VMFramework.Core
{
    public partial struct RangeUnsignedInteger
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<uint> IEnumerable<uint>.GetEnumerator()
        {
            return new Enumerator(this);
        }
        
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
        
        public struct Enumerator : IEnumerator<uint>
        {
            private readonly RangeUnsignedInteger range;
            private uint x;

            public Enumerator(RangeUnsignedInteger range)
            {
                this.range = range;
                x = range.min - 1;
            }

            public uint Current => x;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                x++;
                
                return x <= range.max;
            }

            public void Reset()
            {
                x = range.min - 1;
            }

            public void Dispose() { }
        }
    }
}