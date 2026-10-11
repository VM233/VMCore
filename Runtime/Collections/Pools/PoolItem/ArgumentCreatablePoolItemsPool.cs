using System;

namespace VMFramework.Core.Pools
{
    /// <inheritdoc cref="PoolItemsPool{TItem}"/>
    public sealed class CreatablePoolItemsPool<TItem, TArgument> : PoolItemsPool<TItem>
        where TItem : ICreatablePoolItem<TArgument>
    {
        private readonly TArgument argument;
        private readonly Func<TArgument, TItem> onCreateFunc;

        public CreatablePoolItemsPool(TArgument argument, Func<TArgument, TItem> onCreateFunc, int capacity) :
            base(capacity)
        {
            onCreateFunc.AssertIsNotNull(nameof(onCreateFunc));
            capacity.AssertIsAbove(0, nameof(capacity));
            this.argument = argument;
            this.onCreateFunc = onCreateFunc;
        }

        protected override TItem CreateItem()
        {
            var item = onCreateFunc(argument);
            try
            {
                item.OnCreate(argument);
                return item;
            }
            catch (Exception primary)
            {
                try { item.OnClear(); }
                catch (Exception retirement)
                {
                    throw new PoolItemAcquisitionException(this, item, "create.clear", primary, retirement);
                }
                throw;
            }
        }
    }
}
