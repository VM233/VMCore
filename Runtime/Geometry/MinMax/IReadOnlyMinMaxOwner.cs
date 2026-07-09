using System;

namespace VMFramework.Core
{
    public interface IReadOnlyMinMaxOwner<out TPoint> where TPoint : struct, IEquatable<TPoint>
    {
        public TPoint GetMin();
        
        public TPoint GetMax();
    }
}