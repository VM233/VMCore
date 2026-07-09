using System.Collections.Generic;

namespace VMFramework.Core
{
    public interface IReadOnlyKeysOwner<TKey>
    {
        public delegate void KeyAddedHandler(TKey key);
        public delegate void KeyRemovedHandler(TKey key);
        
        public IReadOnlyCollection<TKey> Keys { get; }
        
        public event KeyAddedHandler OnKeyAdded;
        public event KeyRemovedHandler OnKeyRemoved;
    }
}