using System;
using UnityEngine;

namespace VMFramework.Core
{
    /// <summary>An immutable convex core dilated by a disk, in the caller's metric frame.</summary>
    public sealed class ConvexColliderGeometry2D
    {
        private readonly Vector2[] vertices;
        public ReadOnlySpan<Vector2> Vertices => vertices;
        public float Radius { get; }
        public int EdgeCount => vertices.Length == 1 ? 0 : vertices.Length == 2 ? 1 : vertices.Length;

        private ConvexColliderGeometry2D(Vector2[] vertices, float radius)
        {
            this.vertices = vertices;
            Radius = radius;
        }

        /// <summary>Capture authored geometry, including disabled colliders. The matrix maps collider-local coordinates.</summary>
        public static ConvexColliderGeometry2D Capture(Collider2D collider, Matrix4x4 localToFrame)
        {
            Vector2 center = localToFrame.MultiplyPoint3x4(collider.offset);
            Vector2 x = localToFrame.MultiplyVector(Vector2.right);
            Vector2 y = localToFrame.MultiplyVector(Vector2.up);
            float scale = Mathf.Max(x.magnitude, y.magnitude);
            switch (collider)
            {
                case CircleCollider2D circle:
                    return new ConvexColliderGeometry2D(new[] { center }, circle.radius * scale);
                case CapsuleCollider2D capsule:
                    bool vertical = capsule.direction == CapsuleDirection2D.Vertical;
                    float length = vertical ? capsule.size.y * y.magnitude : capsule.size.x * x.magnitude;
                    float width = vertical ? capsule.size.x * x.magnitude : capsule.size.y * y.magnitude;
                    float radius = Mathf.Min(length, width) * 0.5f;
                    Vector2 core = (vertical ? y : x).normalized * (length * 0.5f - radius);
                    return new ConvexColliderGeometry2D(new[] { center - core, center + core }, radius);
                case BoxCollider2D box:
                    Vector2 halfX = x * (box.size.x * 0.5f);
                    Vector2 halfY = y * (box.size.y * 0.5f);
                    return new ConvexColliderGeometry2D(new[]
                    {
                        center - halfX - halfY, center + halfX - halfY,
                        center + halfX + halfY, center - halfX + halfY
                    }, box.edgeRadius * scale);
                default:
                    throw new NotSupportedException($"Collider '{collider}' has no convex primitive geometry contract.");
            }
        }

        public bool Contains(ReadOnlySpan<Vector2> other, float radius)
        {
            double remainingRadius = Radius - radius;
            if (remainingRadius >= 0d)
            {
                foreach (Vector2 point in other)
                    if (PlanarCoreGeometry2D.PointDistanceSquared(point, vertices) >
                        (remainingRadius + PlanarCoreGeometry2D.RoundTripTolerance) *
                        (remainingRadius + PlanarCoreGeometry2D.RoundTripTolerance)) return false;
                return true;
            }
            if (vertices.Length < 3) return false;
            return new PolygonAreaGeometry2D(vertices).Contains(other, (float)-remainingRadius);
        }

        public bool Overlaps(ReadOnlySpan<Vector2> other, float radius) =>
            PlanarCoreGeometry2D.DistanceSquared(vertices, other) <= (double)(Radius + radius) * (Radius + radius);
    }
}
