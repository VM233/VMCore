using System;
using System.Collections;
using System.Collections.Generic;

namespace VMFramework.Core
{
    public partial struct UniformlySpacedRangeFloat : ISteppedRange<float>
    {
        public float min;
        public float max;
        public int count;
        
        public float Step => count > 1 ? (max - min) / (count - 1) : 0;
        
        public UniformlySpacedRangeFloat(float min, float max, int count)
        {
            this.min = min;
            this.max = max;
            this.count = count;
        }
        
        float IMinMaxOwner<float>.Min
        {
            get => min;
            set => min = value;
        }

        float IMinMaxOwner<float>.Max
        {
            get => max;
            set => max = value;
        }

        int IReadOnlyCollection<float>.Count => count;

        public bool Contains(float pos)
        {
            if (count <= 0)
            {
                return false;
            }

            if (count == 1)
            {
                return pos == (max + min) / 2;
            }

            if (count == 2)
            {
                return pos == min || pos == max;
            }
            
            return (pos - min) % Step == 0;
        }
        
        public float GetRandomItem(Random random)
        {
            if (count <= 0)
            {
                throw new InvalidOperationException($"{nameof(UniformlySpacedRangeFloat)} is empty.");
            }

            if (count == 1)
            {
                return (max + min) / 2;
            }

            if (count == 2)
            {
                return random.NextBool() ? min : max;
            }

            var index = random.Next(count);
            return min + index * Step;
        }

        object IRandomItemProvider.GetRandomObjectItem(Random random)
        {
            return GetRandomItem(random);
        }
        
        #region Enumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<float> IEnumerable<float>.GetEnumerator()
        {
            return new Enumerator(this);
        }
        
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
        
        public struct Enumerator : IEnumerator<float>
        {
            private readonly UniformlySpacedRangeFloat range;
            private readonly float step;
            private float x;
            private int index;

            public Enumerator(UniformlySpacedRangeFloat range)
            {
                this.range = range;
                step = range.Step;
                x = range.min - step;
                index = -1;
            }

            public float Current => x;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (range.count <= 0)
                {
                    return false;
                }
                
                index++;

                if (range.count == 1)
                {
                    x = (range.max + range.min) / 2;
                    return index < 1;
                }
                
                x += step;
                return index < range.count;
            }

            public void Reset()
            {
                x = range.min - range.Step;
            }

            public void Dispose() { }
        }

        #endregion
    }
}