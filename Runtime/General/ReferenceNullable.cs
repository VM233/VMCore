using System;
using System.Data.SqlTypes;

namespace VMFramework.Core
{
    public readonly struct ReferenceNullable<T> : INullable
        where T : class
    {
        public static ReferenceNullable<T> Empty => new ReferenceNullable<T>(null, false);
        
        public readonly T Value;
        public readonly bool HasValue;

        public ReferenceNullable(T value, bool hasValue)
        {
            Value = value;
            HasValue = hasValue;
        }

        public bool IsNull => HasValue == false;
        
        public static implicit operator ReferenceNullable<T>(T value)
        {
            return new ReferenceNullable<T>(value, value != null);
        }

        public static implicit operator T(ReferenceNullable<T> nullable)
        {
            return nullable.HasValue ? nullable.Value : null;
        }
    }
}