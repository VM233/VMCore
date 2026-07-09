using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ColliderContainsUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsFullCollider([DisallowNull] this Collider2D collider, Collider2D other,
            Vector2 position, int sampleCount)
        {
            if (other is CircleCollider2D circleCollider)
            {
                return collider.ContainsFullCircle(circle: circleCollider.SelfToCircle() + position, sampleCount);
            }

            if (other is CapsuleCollider2D capsuleCollider)
            {
                return collider.ContainsFullCapsule(offset: position + capsuleCollider.offset, capsuleCollider.size,
                    sampleCount);
            }

            throw new ArgumentOutOfRangeException(nameof(other), $"{other.GetType().Name} is not supported.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsPartialCollider([DisallowNull] this Collider2D collider, Collider2D other,
            Vector2 position, int sampleCount)
        {
            if (other is CircleCollider2D circleCollider)
            {
                return collider.ContainsPartialCircle(circle: circleCollider.SelfToCircle() + position, sampleCount);
            }

            if (other is CapsuleCollider2D capsuleCollider)
            {
                return collider.ContainsPartialCapsule(offset: position + capsuleCollider.offset, capsuleCollider.size,
                    sampleCount);
            }

            throw new ArgumentOutOfRangeException(nameof(other), $"{other.GetType().Name} is not supported.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsFullCircle([DisallowNull] this Collider2D collider, CircleFloat circle,
            int sampleCount)
        {
            if (sampleCount <= 0)
            {
                return true;
            }

            foreach (var angle in UniformlySpacedRangeFloat.Circle(0, sampleCount))
            {
                var vector = angle.ClockwiseAngleToDirection();
                var point = circle.center + vector * circle.radius;

                if (collider.OverlapPoint(point) == false)
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsPartialCircle([DisallowNull] this Collider2D collider, CircleFloat circle,
            int sampleCount)
        {
            if (sampleCount <= 0)
            {
                return true;
            }

            foreach (var angle in UniformlySpacedRangeFloat.Circle(0, sampleCount))
            {
                var vector = angle.ClockwiseAngleToDirection();
                var point = circle.center + vector * circle.radius;

                if (collider.OverlapPoint(point))
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsFullCapsule([DisallowNull] this Collider2D collider, Vector2 offset, Vector2 size,
            int sampleCount)
        {
            if (sampleCount <= 0)
            {
                return true;
            }

            // 椭圆半径
            Vector2 r = size * 0.5f;

            for (int i = 0; i < sampleCount; i++)
            {
                float t = (i / (float)sampleCount) * (Mathf.PI * 2f);
                Vector2 p = new Vector2(Mathf.Cos(t) * r.x, Mathf.Sin(t) * r.y) + offset;

                if (collider.OverlapPoint(p) == false)
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsPartialCapsule([DisallowNull] this Collider2D collider, Vector2 offset, Vector2 size,
            int sampleCount)
        {
            if (sampleCount <= 0)
            {
                return true;
            }

            // 椭圆半径
            Vector2 r = size * 0.5f;

            for (int i = 0; i < sampleCount; i++)
            {
                float t = (i / (float)sampleCount) * (Mathf.PI * 2f);
                Vector2 p = new Vector2(Mathf.Cos(t) * r.x, Mathf.Sin(t) * r.y) + offset;

                if (collider.OverlapPoint(p))
                {
                    return true;
                }
            }

            return false;
        }
    }
}