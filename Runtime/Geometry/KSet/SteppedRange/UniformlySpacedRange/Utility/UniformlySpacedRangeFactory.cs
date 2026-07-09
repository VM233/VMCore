using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public partial struct UniformlySpacedRangeFloat
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniformlySpacedRangeFloat ExcludeBoundaries(float min, float max, int count)
        {
            if (count < 2)
            {
                return new(min, max, count);
            }

            float step = (max - min) / (count + 1);
            return new(min + step, max - step, count);
        }

        /// <summary>
        /// 返回一个圆形范围，起始角度为startAngle，范围为360度，步长为360/count度
        /// </summary>
        /// <param name="startAngle"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniformlySpacedRangeFloat Circle(float startAngle, int count)
        {
            var step = 360f / count;
            var endAngle = startAngle + 360f - step;
            return new(startAngle, endAngle, count);
        }
    }
}