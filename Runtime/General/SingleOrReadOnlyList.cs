using System.Collections;
using System.Collections.Generic;

namespace VMFramework.Core
{
    public readonly struct SingleOrReadOnlyList<TValue> : IEnumerable<TValue>
    {
        public readonly bool isSingle;
        public readonly TValue value;
        public readonly IReadOnlyList<TValue> list;

        public SingleOrReadOnlyList(TValue value)
        {
            isSingle = true;
            this.value = value;
            list = null;
        }

        public SingleOrReadOnlyList(IReadOnlyList<TValue> list)
        {
            isSingle = false;
            value = default;
            this.list = list;
        }

        public void AddTo<TCollection>(TCollection collection)
            where TCollection : ICollection<TValue>
        {
            if (isSingle)
            {
                collection.Add(value);
            }
            else
            {
                collection.AddRange(list);
            }
        }

        public static implicit operator SingleOrReadOnlyList<TValue>(TValue value)
        {
            return new SingleOrReadOnlyList<TValue>(value);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            if (isSingle)
            {
                return value.AsEnumerator();
            }

            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}