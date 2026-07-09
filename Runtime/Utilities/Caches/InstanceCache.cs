using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class InstanceCache
    {
        private static readonly Dictionary<Type, object> cacheByType = new();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Get([DisallowNull] Type type)
        {
            if (cacheByType.TryGetValue(type, out var instance))
            {
                return instance;
            }

            instance = type.CreateInstance();

            if (instance == null)
            {
                return null;
            }
            
            cacheByType.Add(type, instance);
            return instance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>()
            where T : class
        {
            return (T)Get(typeof(T));
        }
    }
}