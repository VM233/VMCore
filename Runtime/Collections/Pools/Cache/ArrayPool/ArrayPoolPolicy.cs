using System;

namespace VMFramework.Core.Pools
{
    public sealed class ArrayPoolPolicy<TItem> : IPoolPolicy<TItem[]>
    {
        private readonly int arrayLength;

        public ArrayPoolPolicy(int arrayLength)
        {
            this.arrayLength = arrayLength;
        }

        public Func<TItem[], TItem[]> PreGet { get; } = item => item;

        Func<TItem[]> IPoolPolicy<TItem[]>.Create => Create;

        public TItem[] Create() => new TItem[arrayLength];

        Func<TItem[], bool> IPoolPolicy<TItem[]>.Return => Return;

        public bool Return(TItem[] item)
        {
            if (item == null)
            {
                return false;
            }

            if (item.Length != arrayLength)
            {
                return false;
            }

            return true;
        }

        public Action<TItem[]> Clear { get; } = array => { };
    }
}