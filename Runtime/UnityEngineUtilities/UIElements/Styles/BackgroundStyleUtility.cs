using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace VMFramework.Core
{
    public static class BackgroundStyleUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCoverBackgroundImage(this VisualElement element, Sprite sprite)
        {
            if (sprite == null)
            {
                element.style.backgroundImage = new StyleBackground(StyleKeyword.Null);
                return;
            }

            element.style.backgroundImage = new StyleBackground(sprite);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBackgroundImage(this VisualElement element, Sprite sprite)
        {
            element.style.backgroundImage = new StyleBackground(sprite);
        }
    }
}