using System;

namespace VMFramework.Core
{
    public interface IMinMaxOwner<TPoint> : IReadOnlyMinMaxOwner<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        public TPoint Min { get; set; }

        public TPoint Max { get; set; }

        TPoint IReadOnlyMinMaxOwner<TPoint>.GetMin()
        {
            return Min;
        }

        TPoint IReadOnlyMinMaxOwner<TPoint>.GetMax()
        {
            return Max;
        }
    }
}