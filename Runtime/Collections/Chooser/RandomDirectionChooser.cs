using System;
using UnityEngine;
using Random = System.Random;

namespace VMFramework.Core
{
    /// <summary>
    /// 随机方向选择器，返回一个随机的方向。
    /// 角度与方向的关系参考：相对于Vector2.right即(1, 0)，顺时针旋转一定角度后的方向
    /// </summary>
    [Serializable]
    public class RandomDirectionChooser : IChooser<Vector2>
    {
        public RangeFloat angleRange = new(0, 360);

        public RandomDirectionChooser()
        {
            
        }

        public RandomDirectionChooser(RangeFloat angleRange)
        {
            this.angleRange = angleRange;
        }

        public Vector2 GetRandomItem(Random random)
        {
            var angle = angleRange.GetRandomItem(random);
            var direction = angle.ClockwiseAngleToDirection();
            return direction;
        }

        public object GetRandomObjectItem(Random random)
        {
            return GetRandomItem(random);
        }

        public IChooser<Vector2> GenerateNewChooser()
        {
            return new RandomDirectionChooser(angleRange);
        }

        public IChooser GenerateNewObjectChooser()
        {
            return new RandomDirectionChooser(angleRange);
        }
    }
}