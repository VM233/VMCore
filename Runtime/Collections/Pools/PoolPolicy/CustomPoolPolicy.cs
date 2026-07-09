using System;

namespace VMFramework.Core.Pools
{
    public class CustomPoolPolicy<TItem> : IPoolPolicy<TItem>
    {
        public Func<TItem, TItem> PreGet { get; set; } = item => item;
        public Func<TItem> Create { get; set; }
        public Func<TItem, bool> Return { get; set; } = item => true;
        public Action<TItem> Clear { get; set; } = item => { };
    }
}