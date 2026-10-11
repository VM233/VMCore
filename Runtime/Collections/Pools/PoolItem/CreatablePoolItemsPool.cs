using System;

namespace VMFramework.Core.Pools
{
    /// <inheritdoc cref="PoolItemsPool{TPoolItem}"/>
    public sealed class CreatablePoolItemsPool<TPoolItem> : PoolItemsPool<TPoolItem>
        where TPoolItem : ICreatablePoolItem
    {
        private readonly Func<TPoolItem> onCreateFunc;

        public CreatablePoolItemsPool(Func<TPoolItem> onCreateFunc, int capacity) : base(capacity)
        {
            onCreateFunc.AssertIsNotNull(nameof(onCreateFunc));
            this.onCreateFunc = onCreateFunc;
        }

        protected override TPoolItem CreateItem()
        {
            var item = onCreateFunc();
            try
            {
                item.OnCreate();
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
