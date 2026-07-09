using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace VMFramework.Core
{
    public static class StyleSetUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBorderColor(this IStyle style, Color color)
        {
            style.borderTopColor = color;
            style.borderRightColor = color;
            style.borderBottomColor = color;
            style.borderLeftColor = color;
        }
    }
}