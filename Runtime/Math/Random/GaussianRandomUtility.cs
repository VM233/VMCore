using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class GaussianRandomUtility
    {
        /// <summary>
        /// 返回一个符合高斯分布的随机数
        /// </summary>
        /// <param name="random">随机数生成器</param>
        /// <param name="mean">平均值</param>
        /// <param name="sigma">标准差</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NextGaussian(this Random random, double mean = 0f, double sigma = 1f)
        {
            // 使用 Box-Muller 变换
            double u1 = random.NextDouble(); // (0,1)
            double u2 = random.NextDouble();

            // 避免 log(0)
            u1 = Math.Max(u1, 1e-10);

            double z0 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            // 缩放至指定均值和标准差
            return z0 * sigma + mean;
        }
    }
}