using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace VMFramework.Core
{
    public static class PickingModeUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPickingMode(this VisualElement element, PickingMode pickingMode)
        {
            foreach (var visualElement in element.QueryAll<VisualElement>())
            {
                visualElement.pickingMode = pickingMode;
            }
        }
    }
}