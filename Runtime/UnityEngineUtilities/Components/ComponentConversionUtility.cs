using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ComponentConversionUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentOrAs<T>(this object obj, out T component)
        {
            if (obj is T targetComponent)
            {
                component = targetComponent;
                return true;
            }
            
            if (obj.TryAsGameObject(out var targetObject))
            {
                return targetObject.TryGetComponent(out component);
            }

            component = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAsGameObject(this object obj, out GameObject gameObject)
        {
            if (obj is Component targetComponent)
            {
                gameObject = targetComponent.gameObject;
                return true;
            }

            if (obj is GameObject targetGameObject)
            {
                gameObject = targetGameObject;
                return true;
            }

            gameObject = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponent<T>(this object obj, out T component)
        {
            if (obj.TryAsGameObject(out GameObject targetGameObject))
            {
                if (targetGameObject.TryGetComponent(out component))
                {
                    return true;
                }
            }

            component = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponent(this object obj, Type type, out Component component)
        {
            if (obj.TryAsGameObject(out GameObject targetGameObject))
            {
                if (targetGameObject.TryGetComponent(type, out component))
                {
                    return true;
                }
            }

            component = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponents<T>(this object obj, out T[] components)
        {
            if (obj.TryAsGameObject(out GameObject targetGameObject))
            {
                components = targetGameObject.GetComponents<T>();
                return components.Length > 0;
            }

            components = null;
            return false;
        }
    }
}