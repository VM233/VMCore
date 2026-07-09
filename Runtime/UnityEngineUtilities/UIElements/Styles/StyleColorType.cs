using System;

namespace VMFramework.Core
{
    [Flags]
    public enum StyleColorType
    {
        None = 0,
        TextColor = 1,
        BackgroundColor = 2,
        BorderColor = 4,
    }
}