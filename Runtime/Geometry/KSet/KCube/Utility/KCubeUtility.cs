using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class KCubeUtility
    {
        #region Contains Cube

        /// <summary>
        /// 判断一个K维立方体是否包含另一个K维立方体
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<TCube, TPoint>(this IKCube<TPoint> cube, TCube smallerCube)
            where TPoint : struct, IEquatable<TPoint>
            where TCube : IKCube<TPoint>
        {
            return cube.Contains(smallerCube.Min) && cube.Contains(smallerCube.Max);
        }

        /// <summary>
        /// 判断一个K维立方体是否被另一个K维立方体包含
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsBy<TCube, TPoint>(this IKCube<TPoint> cube, TCube biggerCube)
            where TPoint : struct, IEquatable<TPoint>
            where TCube : IKCube<TPoint>
        {
            return biggerCube.Contains(cube.Min) && biggerCube.Contains(cube.Max);
        }

        #endregion

        #region Clamp

        /// <summary>
        ///     以此K维立方体为基础，截断一个点，确保这个点在K维立方体内
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPoint Clamp<TPoint, TCube>(this TCube cube, TPoint pos)
            where TPoint : struct, IEquatable<TPoint>
            where TCube : IKCube<TPoint>
        {
            return cube.ClampMin(cube.ClampMax(pos));
        }

        #endregion

        #region Geometry

        /// <summary>
        /// 返回两个K维立方体的交，或者说以一个K维立方体为基础，截断另一个K维立方体
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (TPoint min, TPoint max) IntersectsWith<TCube, TPoint>(this TCube cube, TPoint min, TPoint max)
            where TPoint : struct, IEquatable<TPoint>
            where TCube : IKCube<TPoint>
        {
            return (cube.ClampMin(min), cube.ClampMax(max));
        }

        /// <summary>
        /// 判断两个K维立方体是否相交
        /// </summary>
        public static bool Overlaps<TCube, TPoint>(this TCube cube, TPoint min, TPoint max)
            where TPoint : struct, IEquatable<TPoint>
            where TCube : IKCube<TPoint>
        {
            return cube.Contains(min) || cube.Contains(max);
        }

        #endregion

        #region Boundary

        #region Get All Boundary Points

        /// <summary>
        ///     获取K维立方体的边界上的所有点
        /// </summary>
        /// <param name="cube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllBoundaryPoints(this IKCube<Vector3Int> cube)
        {
            return cube.Min.GetAllFacePointsOfCube(cube.Max);
        }

        #endregion

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LimitTo<TCube, TPoint, TCollection>(this IEnumerable<TCube> cubes, TPoint min, TPoint max,
            TCollection collection)
            where TPoint : struct, IEquatable<TPoint>, IComparable<TPoint>
            where TCube : IKCube<TPoint>, new()
            where TCollection : ICollection<TCube>
        {
            foreach (var cube in cubes)
            {
                var (newMin, newMax) = cube.IntersectsWith(min, max);

                if (newMin.CompareTo(newMax) > 0)
                {
                    continue;
                }

                collection.Add(new TCube()
                {
                    Min = newMin,
                    Max = newMax
                });
            }
        }
    }
}