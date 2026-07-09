using System;

namespace VMFramework.Core.Pools
{
    public sealed class DefaultPoolPolicy<TItem> : IPoolPolicy<TItem>
        where TItem : class, new()
    {
        public Func<TItem, TItem> PreGet { get; } = item => item;

        public Func<TItem> Create { get; } = () => new TItem();

        public Func<TItem, bool> Return { get; } = item =>
        {
            if (item is IResettable resettable)
            {
                return resettable.TryReset();
            }

            return true;
        };

        public Action<TItem> Clear { get; } = item => { };
    }
}