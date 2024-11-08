using System;

namespace VMFramework.Core
{
    public interface IInfiniteKCube<TPoint> : IKCube<TPoint> where TPoint : struct, IEquatable<TPoint>
    {
        
    }
}