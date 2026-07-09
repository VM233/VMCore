using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace VMFramework.Core
{
    public static class SizeStyleUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetResolvedSize(this VisualElement visualElement)
        {
            return new(visualElement.resolvedStyle.width, visualElement.resolvedStyle.height);
        }
    }
}