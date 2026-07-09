using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class BoolMatchUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatch(this bool value, BooleansMatchType matchType)
        {
            if (matchType is BooleansMatchType.AllTrue)
            {
                return value;
            }

            if (matchType is BooleansMatchType.AnyTrue)
            {
                return value;
            }

            if (matchType is BooleansMatchType.AllFalse)
            {
                return value == false;
            }
            
            if (matchType is BooleansMatchType.AnyFalse)
            {
                return value == false;
            }

            throw new ArgumentOutOfRangeException(nameof(matchType), matchType, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatch<TEnumerable>(this TEnumerable enumerable, BooleansMatchType matchType)
            where TEnumerable : IEnumerable<bool>
        {
            if (matchType is BooleansMatchType.AllTrue)
            {
                foreach (bool value in enumerable)
                {
                    if (value == false)
                    {
                        return false;
                    }
                }

                return true;
            }

            if (matchType is BooleansMatchType.AnyTrue)
            {
                foreach (bool value in enumerable)
                {
                    if (value)
                    {
                        return true;
                    }
                }

                return false;
            }

            if (matchType is BooleansMatchType.AllFalse)
            {
                foreach (bool value in enumerable)
                {
                    if (value)
                    {
                        return false;
                    }
                }

                return true;
            }

            if (matchType is BooleansMatchType.AnyFalse)
            {
                foreach (bool value in enumerable)
                {
                    if (value == false)
                    {
                        return true;
                    }
                }

                return false;
            }

            throw new ArgumentOutOfRangeException(nameof(matchType), matchType, null);
        }
    }
}