using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class GradientUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Gradient CreateGradient(this Color color)
        {
            return new Gradient
            {
                colorKeys = new[]
                {
                    new GradientColorKey(color, 0f), new GradientColorKey(color, 1f)
                },
                alphaKeys = new[]
                {
                    new GradientAlphaKey(color.a, 0f), new GradientAlphaKey(color.a, 1f)
                }
            };
        }
    }
}