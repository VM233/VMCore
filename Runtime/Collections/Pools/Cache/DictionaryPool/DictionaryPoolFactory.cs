using System.Collections.Generic;

namespace VMFramework.Core.Pools
{
    public class DictionaryPoolFactory<TKey, TValue>
        : CollectionPoolFactory<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>
    {

    }
}