using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace VMFramework.Core
{
    public static class VisualElementPositionUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetPositionFromScreenPosition(this VisualElement rootVisualElement,
            Vector2 screenPosition, out Vector2 position)
        {
            var boundsSize = rootVisualElement.GetResolvedSize();

            if (boundsSize.AnyNumberBelowOrEqual(0) || boundsSize.IsNaN())
            {
                position = default;
                return false;
            }
            
            Vector2 screenSize = new(Screen.width, Screen.height);
            position = screenPosition.Divide(screenSize).Multiply(boundsSize);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetPositionFromScreenPosition(this Vector2 boundsSize, Vector2 screenPosition,
            out Vector2 position)
        {
            if (boundsSize.AnyNumberBelowOrEqual(0) || boundsSize.IsNaN())
            {
                position = default;
                return false;
            }
            
            Vector2 screenSize = new(Screen.width, Screen.height);
            position = screenPosition.Divide(screenSize).Multiply(boundsSize);
            return true;
        }
    }
}