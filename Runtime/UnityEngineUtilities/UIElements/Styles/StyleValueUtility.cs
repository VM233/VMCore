using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace VMFramework.Core
{
    public static class StyleValueUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StyleColor Styled(this Color? color)
        {
            if (color.HasValue)
            {
                return color.Value;
            }

            return new StyleColor(StyleKeyword.Null);
        }
    }
}