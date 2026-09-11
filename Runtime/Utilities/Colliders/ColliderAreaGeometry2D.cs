using System;
using UnityEngine;

namespace VMFramework.Core
{
    /// <summary>Captures a filled collider region for repeated containment and overlap queries.</summary>
    public sealed class ColliderAreaGeometry2D
    {
        private readonly PolygonAreaGeometry2D polygon;
        private readonly ConvexColliderGeometry2D primitive;

        public ColliderAreaGeometry2D(Collider2D collider)
        {
            if (collider is PolygonCollider2D polygonCollider) polygon = new PolygonAreaGeometry2D(polygonCollider);
            else if (collider is CompositeCollider2D composite) polygon = new PolygonAreaGeometry2D(composite);
            else primitive = ConvexColliderGeometry2D.Capture(collider, collider.transform.localToWorldMatrix);
        }

        public bool Contains(ReadOnlySpan<Vector2> core, float radius) =>
            polygon != null ? polygon.Contains(core, radius) : primitive.Contains(core, radius);

        public bool Overlaps(ReadOnlySpan<Vector2> core, float radius) =>
            polygon != null ? polygon.Overlaps(core, radius) : primitive.Overlaps(core, radius);
    }
}
