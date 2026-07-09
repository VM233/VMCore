using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class LerpUtility
    {
        #region Lerp Unclamped

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LerpUnclamped(this float from, float to, float t)
        {
            return from + (to - from) * t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LerpUnclamped(this double from, double to, double t)
        {
            return from + (to - from) * t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 LerpUnclamped(this Vector3 a, Vector3 b, float t)
        {
            return Vector3.LerpUnclamped(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 LerpUnclamped(this Vector3 a, Vector3 b, Vector3 t)
        {
            a.x = LerpUnclamped(a.x, b.x, t.x);
            a.y = LerpUnclamped(a.y, b.y, t.y);
            a.z = LerpUnclamped(a.z, b.z, t.z);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 LerpUnclamped(this Vector2 a, Vector2 b, float t)
        {
            return Vector2.LerpUnclamped(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 LerpUnclamped(this Vector2 a, Vector2 b, Vector2 t)
        {
            a.x = LerpUnclamped(a.x, b.x, t.x);
            a.y = LerpUnclamped(a.y, b.y, t.y);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 LerpUnclamped(this Vector4 a, Vector4 b, float t)
        {
            return Vector4.LerpUnclamped(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 LerpUnclamped(this Vector4 a, Vector4 b, Vector4 t)
        {
            a.x = LerpUnclamped(a.x, b.x, t.x);
            a.y = LerpUnclamped(a.y, b.y, t.y);
            a.z = LerpUnclamped(a.z, b.z, t.z);
            a.w = LerpUnclamped(a.w, b.w, t.w);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color LerpUnclamped(this Color a, Color b, float t)
        {
            return Color.LerpUnclamped(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color LerpUnclamped(this Color a, Color b, Color t)
        {
            a.r = LerpUnclamped(a.r, b.r, t.r);
            a.g = LerpUnclamped(a.g, b.g, t.g);
            a.b = LerpUnclamped(a.b, b.b, t.b);
            a.a = LerpUnclamped(a.a, b.a, t.a);
            return a;
        }

        #endregion

        #region Linear Lerp

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(this float from, float to, float t)
        {
            return Mathf.Lerp(from, to, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Lerp(this double from, double to, double t)
        {
            return from + (to - from) * t.Clamp01();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(this Vector3 a, Vector3 b, float t)
        {
            return Vector3.Lerp(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(this Vector3 a, Vector3 b, Vector3 t)
        {
            a.x = Lerp(a.x, b.x, t.x);
            a.y = Lerp(a.y, b.y, t.y);
            a.z = Lerp(a.z, b.z, t.z);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Lerp(this Vector2 a, Vector2 b, float t)
        {
            return Vector2.Lerp(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Lerp(this Vector2 a, Vector2 b, Vector2 t)
        {
            a.x = Lerp(a.x, b.x, t.x);
            a.y = Lerp(a.y, b.y, t.y);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Lerp(this Vector4 a, Vector4 b, float t)
        {
            return Vector4.Lerp(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Lerp(this Vector4 a, Vector4 b, Vector4 t)
        {
            a.x = Lerp(a.x, b.x, t.x);
            a.y = Lerp(a.y, b.y, t.y);
            a.z = Lerp(a.z, b.z, t.z);
            a.w = Lerp(a.w, b.w, t.w);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lerp(this Color a, Color b, float t)
        {
            return Color.Lerp(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lerp(this Color a, Color b, Color t)
        {
            a.r = Lerp(a.r, b.r, t.r);
            a.g = Lerp(a.g, b.g, t.g);
            a.b = Lerp(a.b, b.b, t.b);
            a.a = Lerp(a.a, b.a, t.a);
            return a;
        }

        #endregion

        #region Lerp With Power

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(this float from, float to, float t, float power)
        {
            return Mathf.Lerp(from, to, t.Power(power));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Lerp(this double from, double to, float t, float power)
        {
            return from + (to - from) * t.Clamp01().Power(power);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Lerp(this Vector4 a, Vector4 b, float t, float power)
        {
            return Vector4.Lerp(a, b, t.Power(power));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lerp(this Color a, Color b, float t, float power)
        {
            return Color.Lerp(a, b, t.Power(power));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(this Vector3 a, Vector3 b, float t, float power)
        {
            return Vector3.Lerp(a, b, t.Power(power));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Lerp(this Vector2 a, Vector2 b, float t, float power)
        {
            return Vector2.Lerp(a, b, t.Power(power));
        }

        #endregion
    }
}