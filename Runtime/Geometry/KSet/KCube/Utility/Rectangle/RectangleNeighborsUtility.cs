using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class RectangleNeighborsUtility
    {
        /// <summary>
        /// 返回四边形的四个顶点，返回值里的left、right、up、down分别代表左上、右下、右上、左下四个顶点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FourDirectionsNeighbors<Vector2> GetVertices<TRectangle>(this TRectangle rectangle)
            where TRectangle : IKCubeFloat<Vector2>
        {
            var min = rectangle.Min;
            var max = rectangle.Max;
            var leftTop = new Vector2(min.x, max.y);
            var rightBottom = new Vector2(max.x, min.y);

            return new(left: leftTop, right: rightBottom, up: max, down: min);
        }
    }
}