using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class FourTypesDirectionVectorCompareUtility
    {
        /// <summary>
        /// 获得<paramref name="vector"/>相对于<paramref name="origin"/>的方向。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourTypesDirection GetRelativeDirectionTo(this Vector2Int vector, Vector2Int origin)
        {
            FourTypesDirection direction = FourTypesDirection.None;

            if (vector.x > origin.x)
            {
                direction |= FourTypesDirection.Right;
            }
            else if (vector.x < origin.x)
            {
                direction |= FourTypesDirection.Left;
            }

            if (vector.y > origin.y)
            {
                direction |= FourTypesDirection.Up;
            }
            else if (vector.y < origin.y)
            {
                direction |= FourTypesDirection.Down;
            }

            return direction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourTypesDirection GetRelativeDirectionTo(this Vector2Int vector, RectangleInteger rectangle)
        {
            if (rectangle.GetInnerRectangle().Contains(vector))
            {
                throw new ArgumentException("The vector is inside the rectangle.");
            }

            FourTypesDirection direction = FourTypesDirection.None;

            if (vector.x >= rectangle.max.x)
            {
                direction |= FourTypesDirection.Right;
            }
            else if (vector.x <= rectangle.min.x)
            {
                direction |= FourTypesDirection.Left;
            }

            if (vector.y >= rectangle.max.y)
            {
                direction |= FourTypesDirection.Up;
            }
            else if (vector.y <= rectangle.min.y)
            {
                direction |= FourTypesDirection.Down;
            }

            return direction;
        }
    }
}