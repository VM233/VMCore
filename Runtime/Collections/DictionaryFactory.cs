using System.Collections.Generic;

namespace VMFramework.Core
{
    public static class DictionaryFactory<TKey, TValue>
    {
        public static IReadOnlyDictionary<TKey, TValue> Empty { get; } = new Dictionary<TKey, TValue>();
    }
}