using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class NormalizeUtility
    {
        #region Normalize01

        /// <summary>
        /// 归一化到0和1之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Normalize01(this double num, double min, double max)
        {
            return (min - max).Abs() > double.Epsilon ? ((num - min) / (max - min)).Clamp01() : 0.0f;
        }

        /// <summary>
        /// 归一化到0和1之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Normalize01(this float num, float min, float max)
        {
            return Mathf.InverseLerp(min, max, num);
        }

        /// <summary>
        /// 归一化到0和1之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Normalize01(this int num, int min, int max)
        {
            return max != min ? ((num - min).F() / (max - min)).Clamp01() : 0.0f;
        }

        /// <summary>
        /// 归一化到零向量和单位向量之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Normalize01(this Vector2 num, Vector2 min, Vector2 max)
        {
            num.x = num.x.Normalize01(min.x, max.x);
            num.y = num.y.Normalize01(min.y, max.y);
            return num;
        }

        /// <summary>
        /// 归一化到零向量和单位向量之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize01(this Vector3 num, Vector3 min, Vector3 max)
        {
            num.x = num.x.Normalize01(min.x, max.x);
            num.y = num.y.Normalize01(min.y, max.y);
            num.z = num.z.Normalize01(min.z, max.z);
            return num;
        }

        /// <summary>
        /// 归一化到零向量和单位向量之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Normalize01(this Vector4 num, Vector4 min, Vector4 max)
        {
            num.x = num.x.Normalize01(min.x, max.x);
            num.y = num.y.Normalize01(min.y, max.y);
            num.z = num.z.Normalize01(min.z, max.z);
            num.w = num.w.Normalize01(min.w, max.w);
            return num;
        }

        /// <summary>
        /// 归一化到<see cref="Color.clear"/>和<see cref="Color.white"/>之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Normalize01(this Color num, Color min, Color max)
        {
            num.r = num.r.Normalize01(min.r, max.r);
            num.g = num.g.Normalize01(min.g, max.g);
            num.b = num.b.Normalize01(min.b, max.b);
            num.a = num.a.Normalize01(min.a, max.a);
            return num;
        }

        #endregion

        #region Normalize To

        /// <summary>
        /// 从旧的最小最大值范围归一化到新的最小和最大值之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NormalizeTo(this float t, float min, float max, float newMin, float newMax)
        {
            return newMin.LerpUnclamped(newMax, t.Normalize01(min, max));
        }

        /// <summary>
        /// 从旧的最小最大值范围归一化到新的最小和最大值之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormalizeTo(this double t, double min, double max, double newMin, double newMax)
        {
            return newMin.LerpUnclamped(newMax, t.Normalize01(min, max));
        }

        /// <summary>
        /// 从旧的最小最大向量范围归一化到新的最小和最大向量之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 NormalizeTo(this Vector2 t, Vector2 min, Vector2 max, Vector2 newMin,
            Vector2 newMax)
        {
            return newMin.LerpUnclamped(newMax, t.Normalize01(min, max));
        }

        /// <summary>
        /// 从旧的最小最大向量范围归一化到新的最小和最大向量之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 NormalizeTo(this Vector3 t, Vector3 min, Vector3 max, Vector3 newMin,
            Vector3 newMax)
        {
            return newMin.LerpUnclamped(newMax, t.Normalize01(min, max));
        }

        /// <summary>
        /// 从旧的最小最大向量范围归一化到新的最小和最大向量之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 NormalizeTo(this Vector4 t, Vector4 min, Vector4 max, Vector4 newMin,
            Vector4 newMax)
        {
            return newMin.LerpUnclamped(newMax, t.Normalize01(min, max));
        }

        /// <summary>
        /// 从旧的最小最大颜色范围归一化到新的最小和最大颜色之间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color NormalizeTo(this Color t, Color min, Color max, Color newMin, Color newMax)
        {
            return newMin.LerpUnclamped(newMax, t.Normalize01(min, max));
        }

        #endregion
    }
}