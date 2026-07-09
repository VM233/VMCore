using System.Runtime.CompilerServices;
using EnumsNET;
using UnityEngine;
using UnityEngine.UIElements;

namespace VMFramework.Core
{
    public static class StyleColorUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetColor(this IStyle style, StyleColorType type, Color color)
        {
            if (type.HasAnyFlags(StyleColorType.TextColor))
            {
                style.color = color;
            }
            else if (type.HasAnyFlags(StyleColorType.BackgroundColor))
            {
                style.backgroundColor = color;
            }
            else if (type.HasAnyFlags(StyleColorType.BorderColor))
            {
                style.SetBorderColor(color);
            }
        }
    }
}