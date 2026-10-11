using System;
using System.Collections.Generic;

namespace VMFramework.Core.Pools
{
    public sealed class PoolItemAcquisitionException : AggregateException
    {
        public object Pool { get; }
        public IPoolItem Item { get; }
        public string Phase { get; }

        public PoolItemAcquisitionException(object pool, IPoolItem item, string phase,
            Exception primary, Exception retirement)
            : base($"Pool item acquisition and retirement failed during {phase}.", primary, retirement)
        {
            Pool = pool;
            Item = item;
            Phase = phase;
        }

        public PoolItemAcquisitionException(object pool, IPoolItem item, string phase, Exception retirement)
            : base($"Pool item retirement failed during {phase}.", retirement)
        {
            Pool = pool;
            Item = item;
            Phase = phase;
        }
        public PoolItemAcquisitionException(object pool, IPoolItem item, string phase,
            IEnumerable<Exception> failures)
            : base($"Pool item retirement failed during {phase}.", failures)
        {
            Pool = pool;
            Item = item;
            Phase = phase;
        }
    }
}
