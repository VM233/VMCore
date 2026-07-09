using System;

namespace VMFramework.Core
{
    [Flags]
    public enum FourDiagonalDirection
    {
        None = 0,
        LeftUp = 1,
        RightDown = 2,
        LeftDown = 4,
        RightUp = 8,
        Up = LeftUp | RightUp,
        Down = LeftDown | RightDown,
        Left = LeftUp | LeftDown,
        Right = RightUp | RightDown,
        All = LeftUp | RightUp | LeftDown | RightDown
    }
}