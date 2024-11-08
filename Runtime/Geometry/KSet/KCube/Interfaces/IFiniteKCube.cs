using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public interface IFiniteKCube<TPoint> : IKCube<TPoint> where TPoint : struct, IEquatable<TPoint>
    {
        /// <summary>
        /// 返回一个点相对于K维立方体的位置
        /// </summary>
        public TPoint GetRelativePos(TPoint pos);
    }
}