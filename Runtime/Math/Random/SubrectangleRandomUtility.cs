using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = System.Random;

namespace VMFramework.Core
{
    public static class SubrectangleRandomUtility
    {
        /// <summary>
        /// 对矩形内进行随机采样，但是不包括<paramref name="subRect"/>的边界及内部的点。
        /// </summary>
        /// <param name="random"></param>
        /// <param name="largeRect"></param>
        /// <param name="subRect"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int PointInsideSubrectangle(this Random random, RectangleInteger largeRect,
            RectangleInteger subRect)
        {
            var leftRect = new RectangleInteger(largeRect.min, new Vector2Int(subRect.min.x - 1, largeRect.max.y));
            var rightRect = new RectangleInteger(new Vector2Int(subRect.max.x + 1, largeRect.min.y), largeRect.max);
            var topRect = new RectangleInteger(new Vector2Int(subRect.min.x, subRect.max.y + 1),
                new Vector2Int(subRect.max.x, largeRect.max.y));
            var bottomRect = new RectangleInteger(new Vector2Int(subRect.min.x, largeRect.min.y),
                new Vector2Int(subRect.max.x, subRect.min.y - 1));

            var leftCount = leftRect.Count;
            var rightCount = rightRect.Count;
            var topCount = topRect.Count;
            var bottomCount = bottomRect.Count;

            var totalCount = leftCount + rightCount + topCount + bottomCount;

            if (totalCount <= 0)
            {
                throw new ArgumentOutOfRangeException("The subrectangle is too small to generate a random point.");
            }

            var randomIndex = random.Range(totalCount);

            RectangleInteger chosenRect;
            if (randomIndex < leftCount)
            {
                chosenRect = leftRect;
                goto CONTINUE;
            }

            randomIndex -= leftCount;

            if (randomIndex < rightCount)
            {
                chosenRect = rightRect;
                goto CONTINUE;
            }

            randomIndex -= rightCount;

            if (randomIndex < topCount)
            {
                chosenRect = topRect;
                goto CONTINUE;
            }

            randomIndex -= topCount;

            if (randomIndex < bottomCount)
            {
                chosenRect = bottomRect;
                goto CONTINUE;
            }

            throw new Exception($"This should not happen. randomIndex: {randomIndex}, totalCount: {totalCount}");

            CONTINUE:
            return random.Range(chosenRect.min, chosenRect.max);
        }
    }
}