using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace VMFramework.Core
{
    public static class VisualElementDisplayUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToggleDisplay(this VisualElement visualElement)
        {
            if (visualElement.style.display.value == DisplayStyle.Flex)
            {
                visualElement.style.display = DisplayStyle.None;
            }
            else
            {
                visualElement.style.display = DisplayStyle.Flex;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisplayNone(this VisualElement visualElement)
        {
            visualElement.style.display = DisplayStyle.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisplayFlex(this VisualElement visualElement)
        {
            visualElement.style.display = DisplayStyle.Flex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Display(this VisualElement visualElement, bool display)
        {
            if (display)
            {
                visualElement.style.display = DisplayStyle.Flex;
            }
            else
            {
                visualElement.style.display = DisplayStyle.None;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Display(this IEnumerable<VisualElement> visualElements, bool display)
        {
            foreach (var visualElement in visualElements)
            {
                visualElement.style.display = display ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisplayNone(this IEnumerable<VisualElement> visualElements)
        {
            foreach (var visualElement in visualElements)
            {
                visualElement.style.display = DisplayStyle.None;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisplayFlex(this IEnumerable<VisualElement> visualElements)
        {
            foreach (var visualElement in visualElements)
            {
                visualElement.style.display = DisplayStyle.Flex;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisplayNone(this IReadOnlyList<VisualElement> visualElements, int startIndex, int endIndex)
        {
            startIndex = startIndex.Max(0);
            endIndex = endIndex.Min(visualElements.Count - 1);

            for (int i = startIndex; i <= endIndex; i++)
            {
                visualElements[i].style.display = DisplayStyle.None;
            }
        }
    }
}