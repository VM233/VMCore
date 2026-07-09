using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core.Generic
{
    public static class GenericNumberUtility
    {
        public static readonly HashSet<Type> numberTypes = new()
        {
            typeof(int),
            typeof(float),
            typeof(double)
        };

        public static readonly HashSet<Type> vectorTypes = new()
        {
            typeof(Vector2),
            typeof(Vector3),
            typeof(Vector2Int),
            typeof(Vector3Int),
            typeof(Vector4),
            typeof(Color)
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNumber(this Type type) => numberTypes.Contains(type);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsVector(this Type type) => vectorTypes.Contains(type);

        public static T ConvertNumber<T>(object value) => (T)Convert.ChangeType(value, typeof(T));

        #region Clamp

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ClampMin<T>(this T a, T min)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => ClampUtility.ClampMin(a.ConvertTo<int>(), min.ConvertTo<int>()).ConvertTo<T>(),
                float => ClampUtility.ClampMin(a.ConvertTo<float>(), min.ConvertTo<float>()).ConvertTo<T>(),
                double => ClampUtility.ClampMin(a.ConvertTo<double>(), min.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => ClampUtility.ClampMin(a.ConvertTo<Vector2>(), min.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => ClampUtility.ClampMin(a.ConvertTo<Vector3>(), min.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => ClampUtility.ClampMin(a.ConvertTo<Vector4>(), min.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => ClampUtility.ClampMin(a.ConvertTo<Vector2Int>(), min.ConvertTo<Vector2Int>())
                    .ConvertTo<T>(),
                Vector3Int => ClampUtility.ClampMin(a.ConvertTo<Vector3Int>(), min.ConvertTo<Vector3Int>())
                    .ConvertTo<T>(),
                Color => ClampUtility.ClampMin(a.ConvertTo<Color>(), min.ConvertTo<Color>()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ClampMax<T>(this T a, T min)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => ClampUtility.ClampMax(a.ConvertTo<int>(), min.ConvertTo<int>()).ConvertTo<T>(),
                float => ClampUtility.ClampMax(a.ConvertTo<float>(), min.ConvertTo<float>()).ConvertTo<T>(),
                double => ClampUtility.ClampMax(a.ConvertTo<double>(), min.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => ClampUtility.ClampMax(a.ConvertTo<Vector2>(), min.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => ClampUtility.ClampMax(a.ConvertTo<Vector3>(), min.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => ClampUtility.ClampMax(a.ConvertTo<Vector4>(), min.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => ClampUtility.ClampMax(a.ConvertTo<Vector2Int>(), min.ConvertTo<Vector2Int>())
                    .ConvertTo<T>(),
                Vector3Int => ClampUtility.ClampMax(a.ConvertTo<Vector3Int>(), min.ConvertTo<Vector3Int>())
                    .ConvertTo<T>(),
                Color => ClampUtility.ClampMax(a.ConvertTo<Color>(), min.ConvertTo<Color>()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ClampMin<T>(this T a, double min)
        {
            return a switch
            {
                int intValue => ClampUtility.ClampMin(intValue, min).ConvertTo<T>(),
                float floatValue => ClampUtility.ClampMin(floatValue, min).ConvertTo<T>(),
                double doubleValue => ClampUtility.ClampMin(doubleValue, min).ConvertTo<T>(),
                Vector2 vector => ClampUtility.ClampMin(vector, (float)min).ConvertTo<T>(),
                Vector3 vector => ClampUtility.ClampMin(vector, (float)min).ConvertTo<T>(),
                Vector4 vector => ClampUtility.ClampMin(vector, (float)min).ConvertTo<T>(),
                Vector2Int vector => ClampUtility.ClampMin(vector, NearToIntegerUtility.Ceiling(min)).ConvertTo<T>(),
                Vector3Int vector => ClampUtility.ClampMin(vector, NearToIntegerUtility.Ceiling(min)).ConvertTo<T>(),
                Color color => ClampUtility.ClampMin(color, (float)min).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ClampMax<T>(this T a, double min)
        {
            return a switch
            {
                int intValue => ClampUtility.ClampMax(intValue, min).ConvertTo<T>(),
                float floatValue => ClampUtility.ClampMax(floatValue, min).ConvertTo<T>(),
                double doubleValue => ClampUtility.ClampMax(doubleValue, min).ConvertTo<T>(),
                Vector2 vector => ClampUtility.ClampMax(vector, (float)min).ConvertTo<T>(),
                Vector3 vector => ClampUtility.ClampMax(vector, (float)min).ConvertTo<T>(),
                Vector4 vector => ClampUtility.ClampMax(vector, (float)min).ConvertTo<T>(),
                Vector2Int vector => ClampUtility.ClampMax(vector, NearToIntegerUtility.Floor(min)).ConvertTo<T>(),
                Vector3Int vector => ClampUtility.ClampMax(vector, NearToIntegerUtility.Floor(min)).ConvertTo<T>(),
                Color color => ClampUtility.ClampMax(color, (float)min).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>(this T a, T min, T max)
            where T : IEquatable<T>
        {
            return a.ClampMin(min).ClampMax(max);
        }

        #endregion

        #region Compare

        #region AllNumber

        /// <summary>
        /// return true if num is in range [min, max]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBetweenInclusive<T>(this T num, T min, T max)
            where T : IEquatable<T>
        {
            return num.AllNumberAboveOrEqual(min) && num.AllNumberBelowOrEqual(max);
        }

        /// <summary>
        /// return true if num is in range (min, max)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBetweenExclusive<T>(this T num, T min, T max)
            where T : IEquatable<T>
        {
            return num.AllNumberAbove(min) && num.AllNumberBelow(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AllNumberBelow(a.ConvertTo<int>(), comparison.ConvertTo<int>()),
                float => VectorCompareUtility.AllNumberBelow(a.ConvertTo<float>(), comparison.ConvertTo<float>()),
                double => VectorCompareUtility.AllNumberBelow(a.ConvertTo<double>(), comparison.ConvertTo<double>()),
                Vector2 => VectorCompareUtility.AllNumberBelow(a.ConvertTo<Vector2>(), comparison.ConvertTo<Vector2>()),
                Vector3 => VectorCompareUtility.AllNumberBelow(a.ConvertTo<Vector3>(), comparison.ConvertTo<Vector3>()),
                Vector4 => VectorCompareUtility.AllNumberBelow(a.ConvertTo<Vector4>(), comparison.ConvertTo<Vector4>()),
                Vector2Int => VectorCompareUtility.AllNumberBelow(a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => VectorCompareUtility.AllNumberBelow(a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => VectorCompareUtility.AllNumberBelow(a.ConvertTo<Color>(), comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AllNumberAbove(a.ConvertTo<int>(), comparison.ConvertTo<int>()),
                float => VectorCompareUtility.AllNumberAbove(a.ConvertTo<float>(), comparison.ConvertTo<float>()),
                double => VectorCompareUtility.AllNumberAbove(a.ConvertTo<double>(), comparison.ConvertTo<double>()),
                Vector2 => VectorCompareUtility.AllNumberAbove(a.ConvertTo<Vector2>(), comparison.ConvertTo<Vector2>()),
                Vector3 => VectorCompareUtility.AllNumberAbove(a.ConvertTo<Vector3>(), comparison.ConvertTo<Vector3>()),
                Vector4 => VectorCompareUtility.AllNumberAbove(a.ConvertTo<Vector4>(), comparison.ConvertTo<Vector4>()),
                Vector2Int => VectorCompareUtility.AllNumberAbove(a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => VectorCompareUtility.AllNumberAbove(a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => VectorCompareUtility.AllNumberAbove(a.ConvertTo<Color>(), comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AllNumberBelowOrEqual(a.ConvertTo<int>(), comparison.ConvertTo<int>()),
                float => VectorCompareUtility.AllNumberBelowOrEqual(a.ConvertTo<float>(), comparison.ConvertTo<float>()),
                double => VectorCompareUtility.AllNumberBelowOrEqual(a.ConvertTo<double>(), comparison.ConvertTo<double>()),
                Vector2 => VectorCompareUtility.AllNumberBelowOrEqual(a.ConvertTo<Vector2>(), comparison.ConvertTo<Vector2>()),
                Vector3 => VectorCompareUtility.AllNumberBelowOrEqual(a.ConvertTo<Vector3>(), comparison.ConvertTo<Vector3>()),
                Vector4 => VectorCompareUtility.AllNumberBelowOrEqual(a.ConvertTo<Vector4>(), comparison.ConvertTo<Vector4>()),
                Vector2Int => VectorCompareUtility.AllNumberBelowOrEqual(a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => VectorCompareUtility.AllNumberBelowOrEqual(a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => VectorCompareUtility.AllNumberBelowOrEqual(a.ConvertTo<Color>(), comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AllNumberAboveOrEqual(a.ConvertTo<int>(), comparison.ConvertTo<int>()),
                float => VectorCompareUtility.AllNumberAboveOrEqual(a.ConvertTo<float>(), comparison.ConvertTo<float>()),
                double => VectorCompareUtility.AllNumberAboveOrEqual(a.ConvertTo<double>(), comparison.ConvertTo<double>()),
                Vector2 => VectorCompareUtility.AllNumberAboveOrEqual(a.ConvertTo<Vector2>(), comparison.ConvertTo<Vector2>()),
                Vector3 => VectorCompareUtility.AllNumberAboveOrEqual(a.ConvertTo<Vector3>(), comparison.ConvertTo<Vector3>()),
                Vector4 => VectorCompareUtility.AllNumberAboveOrEqual(a.ConvertTo<Vector4>(), comparison.ConvertTo<Vector4>()),
                Vector2Int => VectorCompareUtility.AllNumberAboveOrEqual(a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => VectorCompareUtility.AllNumberAboveOrEqual(a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => VectorCompareUtility.AllNumberAboveOrEqual(a.ConvertTo<Color>(), comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region AnyNumber

        /// <summary>
        /// return true if num is in range [min, max]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBetweenInclusive<T>(this T num, T min, T max)
            where T : IEquatable<T>
        {
            return num.AnyNumberAboveOrEqual(min) && num.AnyNumberBelowOrEqual(max);
        }

        /// <summary>
        /// return true if num is in range (min, max)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBetweenExclusive<T>(this T num, T min, T max)
            where T : IEquatable<T>
        {
            return num.AnyNumberAbove(min) && num.AnyNumberBelow(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<int>(), comparison.ConvertTo<int>()),
                uint => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<uint>(), comparison.ConvertTo<uint>()),
                float => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<float>(), comparison.ConvertTo<float>()),
                double => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<double>(), comparison.ConvertTo<double>()),
                Vector2 => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Vector2>(), comparison.ConvertTo<Vector2>()),
                Vector3 => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Vector3>(), comparison.ConvertTo<Vector3>()),
                Vector4 => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Vector4>(), comparison.ConvertTo<Vector4>()),
                Vector2Int => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Color>(), comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<int>(), comparison.ConvertTo<int>()),
                float => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<float>(), comparison.ConvertTo<float>()),
                double => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<double>(), comparison.ConvertTo<double>()),
                Vector2 => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Vector2>(), comparison.ConvertTo<Vector2>()),
                Vector3 => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Vector3>(), comparison.ConvertTo<Vector3>()),
                Vector4 => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Vector4>(), comparison.ConvertTo<Vector4>()),
                Vector2Int => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Color>(), comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<int>(), comparison.ConvertTo<int>()),
                float => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<float>(), comparison.ConvertTo<float>()),
                double => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<double>(), comparison.ConvertTo<double>()),
                Vector2 => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector2>(), comparison.ConvertTo<Vector2>()),
                Vector3 => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector3>(), comparison.ConvertTo<Vector3>()),
                Vector4 => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector4>(), comparison.ConvertTo<Vector4>()),
                Vector2Int => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Color>(), comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual<T>(this T a, T comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<int>(), comparison.ConvertTo<int>()),
                float => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<float>(), comparison.ConvertTo<float>()),
                double => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<double>(), comparison.ConvertTo<double>()),
                Vector2 => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector2>(), comparison.ConvertTo<Vector2>()),
                Vector3 => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector3>(), comparison.ConvertTo<Vector3>()),
                Vector4 => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector4>(), comparison.ConvertTo<Vector4>()),
                Vector2Int => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector2Int>(), comparison.ConvertTo<Vector2Int>()),
                Vector3Int => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector3Int>(), comparison.ConvertTo<Vector3Int>()),
                Color => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Color>(), comparison.ConvertTo<Color>()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual<T>(this T a, double comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<int>(), NearToIntegerUtility.Floor(comparison)),
                float => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<float>(), comparison.F()),
                double => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<double>(), comparison),
                Vector2 => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector2>(), comparison.F()),
                Vector3 => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector3>(), comparison.F()),
                Vector4 => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector4>(), comparison.F()),
                Vector2Int => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector2Int>(),
                    NearToIntegerUtility.Floor(comparison)),
                Vector3Int => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Vector3Int>(),
                    NearToIntegerUtility.Floor(comparison)),
                Color => VectorCompareUtility.AnyNumberBelowOrEqual(a.ConvertTo<Color>(), comparison.F()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow<T>(this T a, double comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<int>(), NearToIntegerUtility.Floor(comparison)),
                float => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<float>(), comparison.F()),
                double => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<double>(), comparison),
                Vector2 => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Vector2>(), comparison.F()),
                Vector3 => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Vector3>(), comparison.F()),
                Vector4 => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Vector4>(), comparison.F()),
                Vector2Int => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Vector2Int>(), NearToIntegerUtility.Floor(comparison)),
                Vector3Int => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Vector3Int>(), NearToIntegerUtility.Floor(comparison)),
                Color => VectorCompareUtility.AnyNumberBelow(a.ConvertTo<Color>(), comparison.F()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual<T>(this T a, double comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<int>(), NearToIntegerUtility.Ceiling(comparison)),
                float => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<float>(), comparison.F()),
                double => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<double>(), comparison),
                Vector2 => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector2>(), comparison.F()),
                Vector3 => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector3>(), comparison.F()),
                Vector4 => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector4>(), comparison.F()),
                Vector2Int => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector2Int>(),
                    NearToIntegerUtility.Ceiling(comparison)),
                Vector3Int => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Vector3Int>(),
                    NearToIntegerUtility.Ceiling(comparison)),
                Color => VectorCompareUtility.AnyNumberAboveOrEqual(a.ConvertTo<Color>(), comparison.F()),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove<T>(this T a, double comparison)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<int>(), NearToIntegerUtility.Ceiling(comparison)),
                float => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<float>(), comparison.F()),
                double => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<double>(), comparison),
                Vector2 => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Vector2>(), comparison.F()),
                Vector3 => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Vector3>(), comparison.F()),
                Vector4 => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Vector4>(), comparison.F()),
                Vector2Int => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Vector2Int>(), NearToIntegerUtility.Ceiling(comparison)),
                Vector3Int => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Vector3Int>(), NearToIntegerUtility.Ceiling(comparison)),
                Color => VectorCompareUtility.AnyNumberAbove(a.ConvertTo<Color>(), comparison.F()),
                _ => throw new ArgumentException()
            };
        }


        #endregion

        #endregion

        #region + - * / ^

        #region Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this T a, T b)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => (a.ConvertTo<int>() + b.ConvertTo<int>()).ConvertTo<T>(),
                float => (a.ConvertTo<float>() + b.ConvertTo<float>()).ConvertTo<T>(),
                double => (a.ConvertTo<double>() + b.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => (a.ConvertTo<Vector2>() + b.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => (a.ConvertTo<Vector3>() + b.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => (a.ConvertTo<Vector4>() + b.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => (a.ConvertTo<Vector2Int>() + b.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => (a.ConvertTo<Vector3Int>() + b.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                Color => (a.ConvertTo<Color>() + b.ConvertTo<Color>()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region Substract

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Subtract<T>(this T a, T b)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => (a.ConvertTo<int>() - b.ConvertTo<int>()).ConvertTo<T>(),
                float => (a.ConvertTo<float>() - b.ConvertTo<float>()).ConvertTo<T>(),
                double => (a.ConvertTo<double>() - b.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => (a.ConvertTo<Vector2>() - b.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => (a.ConvertTo<Vector3>() - b.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => (a.ConvertTo<Vector4>() - b.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => (a.ConvertTo<Vector2Int>() - b.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => (a.ConvertTo<Vector3Int>() - b.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                Color => (a.ConvertTo<Color>() - b.ConvertTo<Color>()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region Negate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Negate<T>(this T a)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => (-a.ConvertTo<int>()).ConvertTo<T>(),
                float => (-a.ConvertTo<float>()).ConvertTo<T>(),
                double => (-a.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => (-a.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => (-a.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => (-a.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => (-a.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => (-a.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                _ => throw new ArgumentException(),
            };
        }

        #endregion

        #region Multiply

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Multiply<T>(this T a, double b)
            where T : IEquatable<T>
        {
            return a switch
            {
                int num => (num * b).ConvertTo<T>(),
                float num => (num * b).ConvertTo<T>(),
                double num => (num * b).ConvertTo<T>(),
                Vector2 vector => (vector * b.F()).ConvertTo<T>(),
                Vector3 vector => (vector * b.F()).ConvertTo<T>(),
                Vector4 vector => (vector * b.F()).ConvertTo<T>(),
                Vector2Int vector => ArithmeticUtility.Multiply(vector, b.F()).Round().ConvertTo<T>(),
                Vector3Int vector => ArithmeticUtility.Multiply(vector, b.F()).Round().ConvertTo<T>(),
                Color color => (color * b.F()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Multiply<T>(this T a, T b)
            where T : IEquatable<T>
        {
            return a switch
            {
                int => (a.ConvertTo<int>() * b.ConvertTo<int>()).ConvertTo<T>(),
                float => (a.ConvertTo<float>() * b.ConvertTo<float>()).ConvertTo<T>(),
                double => (a.ConvertTo<double>() * b.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => ArithmeticUtility.Multiply(a.ConvertTo<Vector2>(), b.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => ArithmeticUtility.Multiply(a.ConvertTo<Vector3>(), b.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => ArithmeticUtility.Multiply(a.ConvertTo<Vector4>(), b.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => ArithmeticUtility.Multiply(a.ConvertTo<Vector2Int>(), b.ConvertTo<Vector2Int>())
                    .ConvertTo<T>(),
                Vector3Int => ArithmeticUtility.Multiply(a.ConvertTo<Vector3Int>(), b.ConvertTo<Vector3Int>())
                    .ConvertTo<T>(),
                Color => ArithmeticUtility.Multiply(a.ConvertTo<Color>(), b.ConvertTo<Color>()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #region Divide

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this T dividend, double divisor)
            where T : IEquatable<T>
        {
            return dividend switch
            {
                int num => (num / divisor).Round().ConvertTo<T>(),
                float num => (num / divisor).F().ConvertTo<T>(),
                double num => (num / divisor).ConvertTo<T>(),
                Vector2 vector => ArithmeticUtility.Divide(vector, divisor.F()).ConvertTo<T>(),
                Vector3 vector => ArithmeticUtility.Divide(vector, divisor.F()).ConvertTo<T>(),
                Vector4 vector => ArithmeticUtility.Divide(vector, divisor.F()).ConvertTo<T>(),
                Vector2Int vector => ArithmeticUtility.Divide(vector, NearToIntegerUtility.Round(divisor))
                    .ConvertTo<T>(),
                Vector3Int vector => ArithmeticUtility.Divide(vector, NearToIntegerUtility.Round(divisor))
                    .ConvertTo<T>(),
                Color color => ArithmeticUtility.Divide(color, divisor.F()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(this T dividend, T divisor)
            where T : IEquatable<T>
        {
            return dividend switch
            {
                int => (dividend.ConvertTo<int>() / divisor.ConvertTo<int>()).ConvertTo<T>(),
                float => (dividend.ConvertTo<float>() / divisor.ConvertTo<float>()).ConvertTo<T>(),
                double => (dividend.ConvertTo<double>() / divisor.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => ArithmeticUtility.Divide(dividend.ConvertTo<Vector2>(), divisor.ConvertTo<Vector2>())
                    .ConvertTo<T>(),
                Vector3 => ArithmeticUtility.Divide(dividend.ConvertTo<Vector3>(), divisor.ConvertTo<Vector3>())
                    .ConvertTo<T>(),
                Vector4 => ArithmeticUtility.Divide(dividend.ConvertTo<Vector4>(), divisor.ConvertTo<Vector4>())
                    .ConvertTo<T>(),
                Vector2Int => ArithmeticUtility
                    .Divide(dividend.ConvertTo<Vector2Int>(), divisor.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => ArithmeticUtility
                    .Divide(dividend.ConvertTo<Vector3Int>(), divisor.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                Color => ArithmeticUtility.Divide(dividend.ConvertTo<Color>(), divisor.ConvertTo<Color>())
                    .ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        #endregion

        #endregion

        #region Min & Max

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this T a, T min)
            where T : struct, IEquatable<T>
        {
            return a switch
            {
                int => MinMaxUtility.Min(a.ConvertTo<int>(), min.ConvertTo<int>()).ConvertTo<T>(),
                float => MinMaxUtility.Min(a.ConvertTo<float>(), min.ConvertTo<float>()).ConvertTo<T>(),
                double => MinMaxUtility.Min(a.ConvertTo<double>(), min.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => MinMaxUtility.Min(a.ConvertTo<Vector2>(), min.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => MinMaxUtility.Min(a.ConvertTo<Vector3>(), min.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => MinMaxUtility.Min(a.ConvertTo<Vector4>(), min.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => MinMaxUtility.Min(a.ConvertTo<Vector2Int>(), min.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => MinMaxUtility.Min(a.ConvertTo<Vector3Int>(), min.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                Color => MinMaxUtility.Min(a.ConvertTo<Color>(), min.ConvertTo<Color>()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this IEnumerable<T> enumerable)
            where T : struct, IEquatable<T>
        {
            return enumerable.Aggregate(Min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Min<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector)
            where TResult : struct, IEquatable<TResult>
        {
            return enumerable.Select(selector).Aggregate(Min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this IEnumerable<T> enumerable)
            where T : struct, IEquatable<T>
        {
            return MinOrDefault(enumerable, item => item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult MinOrDefault<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector)
            where TResult : struct, IEquatable<TResult>
        {
            var list = enumerable.ToList();
            return list.Count == 0 ? default : list.Select(selector).Aggregate(Min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this T a, T min)
            where T : struct, IEquatable<T>
        {
            return a switch
            {
                int => MinMaxUtility.Max(a.ConvertTo<int>(), min.ConvertTo<int>()).ConvertTo<T>(),
                float => MinMaxUtility.Max(a.ConvertTo<float>(), min.ConvertTo<float>()).ConvertTo<T>(),
                double => MinMaxUtility.Max(a.ConvertTo<double>(), min.ConvertTo<double>()).ConvertTo<T>(),
                Vector2 => MinMaxUtility.Max(a.ConvertTo<Vector2>(), min.ConvertTo<Vector2>()).ConvertTo<T>(),
                Vector3 => MinMaxUtility.Max(a.ConvertTo<Vector3>(), min.ConvertTo<Vector3>()).ConvertTo<T>(),
                Vector4 => MinMaxUtility.Max(a.ConvertTo<Vector4>(), min.ConvertTo<Vector4>()).ConvertTo<T>(),
                Vector2Int => MinMaxUtility.Max(a.ConvertTo<Vector2Int>(), min.ConvertTo<Vector2Int>()).ConvertTo<T>(),
                Vector3Int => MinMaxUtility.Max(a.ConvertTo<Vector3Int>(), min.ConvertTo<Vector3Int>()).ConvertTo<T>(),
                Color => MinMaxUtility.Max(a.ConvertTo<Color>(), min.ConvertTo<Color>()).ConvertTo<T>(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this IEnumerable<T> enumerable)
            where T : struct, IEquatable<T>
        {
            return enumerable.Aggregate(Max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Max<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector)
            where TResult : struct, IEquatable<TResult>
        {
            return enumerable.Select(selector).Aggregate(Max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this IEnumerable<T> enumerable)
            where T : struct, IEquatable<T>
        {
            return MaxOrDefault(enumerable, item => item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult MaxOrDefault<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector)
            where TResult : struct, IEquatable<TResult>
        {
            var list = enumerable.ToList();
            return list.Count == 0 ? default : list.Select(selector).Aggregate(Max);
        }

        #endregion
    }
}