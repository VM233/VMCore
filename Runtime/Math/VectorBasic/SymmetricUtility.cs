using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class SymmetricUtility
    {
        #region Number

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PointSymmetric(this double point, double origin = 0)
        {
            return origin + origin - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float PointSymmetric(this float point, float origin = 0)
        {
            return origin + origin - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int PointSymmetric(this int point, int origin = 0)
        {
            return origin + origin - point;
        }

        #endregion

        #region Vector2Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int XAxisymmetric(this Vector2Int point, int xAxis = 0)
        {
            return new(xAxis + xAxis - point.x, point.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int YAxisymmetric(this Vector2Int point, int yAxis = 0)
        {
            return new(point.x, yAxis + yAxis - point.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int PointSymmetric(this Vector2Int point, Vector2Int origin = default)
        {
            point.x = origin.x + origin.x - point.x;
            point.y = origin.y + origin.y - point.y;
            return point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> Symmetric(this Vector2Int point, Vector2Int origin = default,
            bool includingSelf = true)
        {
            if (includingSelf)
            {
                yield return point;
            }

            if (point.x != origin.x)
            {
                yield return point.XAxisymmetric(origin.x);
            }

            if (point.y != origin.y)
            {
                yield return point.YAxisymmetric(origin.y);
            }

            if (point.x != origin.x && point.y != origin.y)
            {
                yield return point.PointSymmetric(origin);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Symmetric<TPoints>(this Vector2Int point, Vector2Int origin, TPoints points,
            bool includingSelf = true)
            where TPoints : ICollection<Vector2Int>
        {
            if (includingSelf)
            {
                points.Add(point);
            }

            if (point.x != origin.x)
            {
                points.Add(point.XAxisymmetric(origin.x));
            }

            if (point.y != origin.y)
            {
                points.Add(point.YAxisymmetric(origin.y));
            }

            if (point.x != origin.x && point.y != origin.y)
            {
                points.Add(point.PointSymmetric(origin));
            }
        }

        #endregion

        #region Vector3Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int XAxisymmetric(this Vector3Int a, int xAxis = 0)
        {
            return new(xAxis + xAxis - a.x, a.y, a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int YAxisymmetric(this Vector3Int a, int yAxis = 0)
        {
            return new(a.x, yAxis + yAxis - a.y, a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ZAxisymmetric(this Vector3Int a, int zAxis = 0)
        {
            return new(a.x, a.y, zAxis + zAxis - a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int XYAxisymmetric(this Vector3Int a, int xAxis = 0, int yAxis = 0)
        {
            return new(xAxis + xAxis - a.x, yAxis + yAxis - a.y, a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int XZAxisymmetric(this Vector3Int a, int xAxis = 0, int zAxis = 0)
        {
            return new(xAxis + xAxis - a.x, a.y, zAxis + zAxis - a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int YZAxisymmetric(this Vector3Int a, int yAxis = 0, int zAxis = 0)
        {
            return new(a.x, yAxis + yAxis - a.y, zAxis + zAxis - a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int PointSymmetric(this Vector3Int a, Vector3Int point = default)
        {
            a.x = point.x + point.x - a.x;
            a.y = point.y + point.y - a.y;
            a.z = point.z + point.z - a.z;
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> Symmetric(this Vector3Int a, Vector3Int point = default,
            bool includingSelf = true)
        {
            if (includingSelf)
            {
                yield return a;
            }

            if (a.x != point.x)
            {
                yield return a.XAxisymmetric(point.x);
            }

            if (a.y != point.y)
            {
                yield return a.YAxisymmetric(point.y);
            }

            if (a.z != point.z)
            {
                yield return a.ZAxisymmetric(point.z);
            }

            if (a.x != point.x && a.y != point.y)
            {
                yield return a.XYAxisymmetric(point.x, point.y);
            }

            if (a.x != point.x && a.z != point.z)
            {
                yield return a.XZAxisymmetric(point.x, point.z);
            }

            if (a.y != point.y && a.z != point.z)
            {
                yield return a.YZAxisymmetric(point.y, point.z);
            }

            if (a.x != point.x && a.y != point.y && a.z != point.z)
            {
                yield return a.PointSymmetric(point);
            }
        }

        #endregion

        #region Vector2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XAxisymmetric(this Vector2 point, float xAxis = 0)
        {
            return new(xAxis + xAxis - point.x, point.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 YAxisymmetric(this Vector2 point, float yAxis = 0)
        {
            return new(point.x, yAxis + yAxis - point.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 PointSymmetric(this Vector2 point, Vector2 origin = default)
        {
            point.x = origin.x + origin.x - point.x;
            point.y = origin.y + origin.y - point.y;
            return point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2> Symmetric(this Vector2 a, Vector2 point = default, bool includingSelf = true)
        {
            if (includingSelf)
            {
                yield return a;
            }

            yield return a.XAxisymmetric(point.x);
            yield return a.YAxisymmetric(point.y);
            yield return a.PointSymmetric(point);
        }

        #endregion

        #region Vector3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XAxisymmetric(this Vector3 a, float xAxis = 0)
        {
            return new(xAxis + xAxis - a.x, a.y, a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YAxisymmetric(this Vector3 a, float yAxis = 0)
        {
            return new(a.x, yAxis + yAxis - a.y, a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ZAxisymmetric(this Vector3 a, float zAxis = 0)
        {
            return new(a.x, a.y, zAxis + zAxis - a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XYAxisymmetric(this Vector3 a, float xAxis = 0, float yAxis = 0)
        {
            return new(xAxis + xAxis - a.x, yAxis + yAxis - a.y, a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XZAxisymmetric(this Vector3 a, float xAxis = 0, float zAxis = 0)
        {
            return new(xAxis + xAxis - a.x, a.y, zAxis + zAxis - a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YZAxisymmetric(this Vector3 a, float yAxis = 0, float zAxis = 0)
        {
            return new(a.x, yAxis + yAxis - a.y, zAxis + zAxis - a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 PointSymmetric(this Vector3 a, Vector3 point = default)
        {
            a.x = point.x + point.x - a.x;
            a.y = point.y + point.y - a.y;
            a.z = point.z + point.z - a.z;
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3> Symmetric(this Vector3 a, Vector3 point = default, bool includingSelf = true)
        {
            if (includingSelf)
            {
                yield return a;
            }

            yield return a.XAxisymmetric(point.x);
            yield return a.YAxisymmetric(point.y);
            yield return a.ZAxisymmetric(point.z);

            yield return a.XYAxisymmetric(point.x, point.y);
            yield return a.XZAxisymmetric(point.x, point.z);
            yield return a.YZAxisymmetric(point.y, point.z);

            yield return a.PointSymmetric(point);
        }

        #endregion

        #region Vector4

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 PointSymmetric(this Vector4 point, Vector4 origin = default)
        {
            point.x = origin.x + origin.x - point.x;
            point.y = origin.y + origin.y - point.y;
            point.z = origin.z + origin.z - point.z;
            point.w = origin.w + origin.w - point.w;
            return point;
        }

        #endregion
    }
}