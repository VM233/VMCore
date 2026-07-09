using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ClampUtility
    {
        #region Clamp

        #region Uint
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Clamp(this uint num, uint min, uint max)
        {
            return num.ClampMin(min).ClampMax(max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ClampMax(this uint num, uint max)
        {
            if (num > max)
            {
                num = max;
            }

            return num;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ClampMin(this uint num, uint min)
        {
            if (num < min)
            {
                num = min;
            }
            
            return num;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Clamp(this uint num, uint length)
        {
            return num.Clamp(0, length - 1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Clamp01(this uint num)
        {
            return Clamp(num, 0, 1);
        }

        #endregion
        
        #region Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(this int num, int min, int max)
        {
            return num.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ClampMax(this int num, int max)
        {
            if (num > max)
            {
                num = max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ClampMin(this int num, int min)
        {
            if (num < min)
            {
                num = min;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(this int num, int length)
        {
            return num.Clamp(0, length - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp01(this int num)
        {
            return Clamp(num, 0, 1);
        }


        #endregion

        #region Long
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Clamp(this long num, long min, long max)
        {
            return num.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ClampMax(this long num, long max)
        {
            if (num > max)
            {
                num = max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ClampMin(this long num, long min)
        {
            if (num < min)
            {
                num = min;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Clamp(this long num, long length)
        {
            return num.Clamp(0, length - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Clamp01(this long num)
        {
            return Clamp(num, 0, 1);
        }

        #endregion

        #region Float

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(this float num, float min, float max)
        {
            if (num < min)
            {
                num = min;
            }
            else if (num > max)
            {
                num = max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ClampMax(this float num, float max)
        {
            if (num > max)
            {
                num = max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ClampMin(this float num, float min)
        {
            if (num < min)
            {
                num = min;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(this float num, float length)
        {
            return num.Clamp(0, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp01(this float num)
        {
            return Clamp(num, 0, 1);
        }

        #endregion

        #region Double

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this double num, double min, double max)
        {
            if (num < min)
            {
                num = min;
            }
            else if (num > max)
            {
                num = max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClampMax(this double num, double max)
        {
            if (num > max)
            {
                num = max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClampMin(this double num, double min)
        {
            if (num < min)
            {
                num = min;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this double num, double length)
        {
            return num.Clamp(0, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp01(this double num)
        {
            return Clamp(num, 0, 1);
        }

        #endregion

        #region Vector3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Clamp(this Vector3 a, Vector3 min, Vector3 max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Clamp(this Vector3 a, Vector3 size)
        {
            a.x = a.x.Clamp(0, size.x);
            a.y = a.y.Clamp(0, size.y);
            a.z = a.z.Clamp(0, size.z);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMin(this Vector3 a, Vector3 min)
        {
            a.x = a.x.ClampMin(min.x);
            a.y = a.y.ClampMin(min.y);
            a.z = a.z.ClampMin(min.z);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMax(this Vector3 a, Vector3 max)
        {
            a.x = a.x.ClampMax(max.x);
            a.y = a.y.ClampMax(max.y);
            a.z = a.z.ClampMax(max.z);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Clamp(this Vector3 a, float min, float max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMin(this Vector3 a, float min)
        {
            a.x = a.x.ClampMin(min);
            a.y = a.y.ClampMin(min);
            a.z = a.z.ClampMin(min);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMax(this Vector3 a, float max)
        {
            a.x = a.x.ClampMax(max);
            a.y = a.y.ClampMax(max);
            a.z = a.z.ClampMax(max);
            return a;
        }

        #endregion

        #region Vector2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Clamp(this Vector2 a, Vector2 min, Vector2 max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Clamp(this Vector2 a, float min, float max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Clamp(this Vector2 a, Vector2 size)
        {
            a.x = a.x.Clamp(0, size.x);
            a.y = a.y.Clamp(0, size.y);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMin(this Vector2 a, Vector2 min)
        {
            a.x = a.x.ClampMin(min.x);
            a.y = a.y.ClampMin(min.y);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMax(this Vector2 a, Vector2 max)
        {
            a.x = a.x.ClampMax(max.x);
            a.y = a.y.ClampMax(max.y);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMin(this Vector2 a, float min)
        {
            a.x = a.x.ClampMin(min);
            a.y = a.y.ClampMin(min);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMax(this Vector2 a, float max)
        {
            a.x = a.x.ClampMax(max);
            a.y = a.y.ClampMax(max);
            return a;
        }

        #endregion

        #region Vector3Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Clamp(this Vector3Int a, Vector3Int min,
            Vector3Int max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Clamp(this Vector3Int a, int min, int max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Clamp(this Vector3Int a, Vector3Int size)
        {
            a.x = a.x.Clamp(0, size.x);
            a.y = a.y.Clamp(0, size.y);
            a.z = a.z.Clamp(0, size.z);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ClampMin(this Vector3Int a, Vector3Int min)
        {
            a.x = a.x.ClampMin(min.x);
            a.y = a.y.ClampMin(min.y);
            a.z = a.z.ClampMin(min.z);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ClampMax(this Vector3Int a, Vector3Int max)
        {
            a.x = a.x.ClampMax(max.x);
            a.y = a.y.ClampMax(max.y);
            a.z = a.z.ClampMax(max.z);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ClampMin(this Vector3Int a, int min)
        {
            a.x = a.x.ClampMin(min);
            a.y = a.y.ClampMin(min);
            a.z = a.z.ClampMin(min);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ClampMax(this Vector3Int a, int max)
        {
            a.x = a.x.ClampMax(max);
            a.y = a.y.ClampMax(max);
            a.z = a.z.ClampMax(max);
            return a;
        }

        #endregion

        #region Vector2Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Clamp(this Vector2Int a, Vector2Int min,
            Vector2Int max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Clamp(this Vector2Int a, int min, int max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Clamp(this Vector2Int a, Vector2Int size)
        {
            a.x = a.x.Clamp(0, size.x);
            a.y = a.y.Clamp(0, size.y);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ClampMin(this Vector2Int a, Vector2Int min)
        {
            a.x = a.x.ClampMin(min.x);
            a.y = a.y.ClampMin(min.y);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ClampMax(this Vector2Int a, Vector2Int max)
        {
            a.x = a.x.ClampMax(max.x);
            a.y = a.y.ClampMax(max.y);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ClampMin(this Vector2Int a, int min)
        {
            a.x = a.x.ClampMin(min);
            a.y = a.y.ClampMin(min);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ClampMax(this Vector2Int a, int max)
        {
            a.x = a.x.ClampMax(max);
            a.y = a.y.ClampMax(max);
            return a;
        }

        #endregion

        #region Vector4

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Clamp(this Vector4 a, Vector4 min, Vector4 max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Clamp(this Vector4 a, Vector4 size)
        {
            a.x = a.x.Clamp(0, size.x);
            a.y = a.y.Clamp(0, size.y);
            a.z = a.z.Clamp(0, size.z);
            a.w = a.w.Clamp(0, size.w);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMin(this Vector4 a, Vector4 min)
        {
            a.x = a.x.ClampMin(min.x);
            a.y = a.y.ClampMin(min.y);
            a.z = a.z.ClampMin(min.z);
            a.w = a.w.ClampMin(min.w);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMax(this Vector4 a, Vector4 max)
        {
            a.x = a.x.ClampMax(max.x);
            a.y = a.y.ClampMax(max.y);
            a.z = a.z.ClampMax(max.z);
            a.w = a.w.ClampMax(max.w);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMin(this Vector4 a, float min)
        {
            a.x = a.x.ClampMin(min);
            a.y = a.y.ClampMin(min);
            a.z = a.z.ClampMin(min);
            a.w = a.w.ClampMin(min);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMax(this Vector4 a, float max)
        {
            a.x = a.x.ClampMax(max);
            a.y = a.y.ClampMax(max);
            a.z = a.z.ClampMax(max);
            a.w = a.w.ClampMax(max);
            return a;
        }

        #endregion

        #region Color

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Clamp(this Color a, Color min, Color max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Clamp(this Color a, Color size)
        {
            a.r = a.r.Clamp(0, size.r);
            a.g = a.g.Clamp(0, size.g);
            a.b = a.b.Clamp(0, size.b);
            a.a = a.a.Clamp(0, size.a);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ClampMin(this Color a, Color min)
        {
            a.r = a.r.ClampMin(min.r);
            a.g = a.g.ClampMin(min.g);
            a.b = a.b.ClampMin(min.b);
            a.a = a.a.ClampMin(min.a);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ClampMax(this Color a, Color max)
        {
            a.r = a.r.ClampMax(max.r);
            a.g = a.g.ClampMax(max.g);
            a.b = a.b.ClampMax(max.b);
            a.a = a.a.ClampMax(max.a);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ClampMin(this Color a, float min)
        {
            a.r = a.r.ClampMin(min);
            a.g = a.g.ClampMin(min);
            a.b = a.b.ClampMin(min);
            a.a = a.a.ClampMin(min);
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ClampMax(this Color a, float min)
        {
            a.r = a.r.ClampMax(min);
            a.g = a.g.ClampMax(min);
            a.b = a.b.ClampMax(min);
            a.a = a.a.ClampMax(min);
            return a;
        }

        #endregion

        #endregion

        #region Clamp Magnitude

        #region Clamp Min Magnitude

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMinMagnitude(this Vector2 vector, float min)
        {
            if (min <= 0)
            {
                return vector;
            }
            
            float sqrMagnitude = vector.sqrMagnitude;

            if (sqrMagnitude < min * min)
            {
                if (sqrMagnitude <= float.Epsilon)
                {
                    return Vector2.zero;
                }
                
                float magnitude = sqrMagnitude.Sqrt();
                
                float scale = min / magnitude;
                
                return vector * scale;
            }

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMinMagnitude(this Vector3 vector, float min)
        {
            if (min <= 0)
            {
                return vector;
            }
            
            float sqrMagnitude = vector.sqrMagnitude;

            if (sqrMagnitude < min * min)
            {
                if (sqrMagnitude <= float.Epsilon)
                {
                    return Vector3.zero;
                }
                
                float magnitude = sqrMagnitude.Sqrt();
                
                float scale = min / magnitude;
                
                return vector * scale;
            }

            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMinMagnitude(this Vector4 vector, float min)
        {
            if (min <= 0)
            {
                return vector;
            }
            
            float sqrMagnitude = vector.sqrMagnitude;

            if (sqrMagnitude < min * min)
            {
                if (sqrMagnitude <= float.Epsilon)
                {
                    return Vector4.zero;
                }
                
                float magnitude = sqrMagnitude.Sqrt();
                
                float scale = min / magnitude;
                
                return vector * scale;
            }

            return vector;
        }

        #endregion

        #region Clamp Max Magnitude
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMaxMagnitude(this Vector2 vector, float max)
        {
            return Vector2.ClampMagnitude(vector, max);
            // if (max <= 0)
            // {
            //     return Vector2.zero;
            // }
            //
            // float sqrMagnitude = vector.sqrMagnitude;
            //
            // if (sqrMagnitude > max * max)
            // {
            //     float magnitude = sqrMagnitude.Sqrt();
            //     
            //     float scale = max / magnitude;
            //     
            //     return vector * scale;
            // }
            //
            // return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMaxMagnitude(this Vector3 vector, float max)
        {
            return Vector3.ClampMagnitude(vector, max);
            
            // if (max <= 0)
            // {
            //     return Vector3.zero;
            // }
            //
            // float sqrMagnitude = vector.sqrMagnitude;
            //
            // if (sqrMagnitude > max * max)
            // {
            //     float magnitude = sqrMagnitude.Sqrt();
            //     
            //     float scale = max / magnitude;
            //     
            //     return vector * scale;
            // }
            //
            // return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMaxMagnitude(this Vector4 vector, float max)
        {
            if (max <= 0)
            {
                return Vector4.zero;
            }
            
            float sqrMagnitude = vector.sqrMagnitude;

            if (sqrMagnitude > max * max)
            {
                float magnitude = sqrMagnitude.Sqrt();
                
                float scale = max / magnitude;
                
                return vector * scale;
            }

            return vector;
        }

        #endregion

        #endregion
    }
}