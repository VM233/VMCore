using UnityEngine;

namespace VMFramework.Core
{
    public interface IController
    {
        public Transform transform { get; }
        
        public GameObject gameObject { get; }
        
        public T GetComponent<T>();
        
        public T[] GetComponents<T>();
        
        public bool TryGetComponent<T>(out T component);

        public T[] GetComponentsInChildren<T>();

        public T GetComponentInChildren<T>();
    }
}