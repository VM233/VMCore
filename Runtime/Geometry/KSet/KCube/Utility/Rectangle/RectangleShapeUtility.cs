using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EnumsNET;
using UnityEngine;

namespace VMFramework.Core
{
    public static class RectangleShapeUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GenerateCrossShapeWithin<TMainRect, TBoundaryRect, TCollection>(this TMainRect mainRect,
            TBoundaryRect boundaryRect, FourTypesDirection availableDirections, TCollection shapePoints)
            where TMainRect : IKCubeFloat<Vector2>
            where TBoundaryRect : IKCubeFloat<Vector2>
            where TCollection : ICollection<Vector2>
        {
            var leftBottom = mainRect.Min;
            var rightTop = mainRect.Max;
            var leftTop = new Vector2(leftBottom.x, rightTop.y);
            var rightBottom = new Vector2(rightTop.x, leftBottom.y);

            var boundaryMax = boundaryRect.Max;
            var boundaryMin = boundaryRect.Min;

            shapePoints.Add(rightTop);

            if (availableDirections.HasAnyFlags(FourTypesDirection.Right))
            {
                shapePoints.Add(new(boundaryMax.x, rightTop.y));
                shapePoints.Add(new(boundaryMax.x, rightBottom.y));
            }

            shapePoints.Add(rightBottom);

            if (availableDirections.HasAnyFlags(FourTypesDirection.Down))
            {
                shapePoints.Add(new(rightBottom.x, boundaryMin.y));
                shapePoints.Add(new(leftBottom.x, boundaryMin.y));
            }

            shapePoints.Add(leftBottom);

            if (availableDirections.HasAnyFlags(FourTypesDirection.Left))
            {
                shapePoints.Add(new(boundaryMin.x, leftBottom.y));
                shapePoints.Add(new(boundaryMin.x, leftTop.y));
            }

            shapePoints.Add(leftTop);

            if (availableDirections.HasAnyFlags(FourTypesDirection.Up))
            {
                shapePoints.Add(new(leftTop.x, boundaryMax.y));
                shapePoints.Add(new(rightTop.x, boundaryMax.y));
            }
        }
    }
}