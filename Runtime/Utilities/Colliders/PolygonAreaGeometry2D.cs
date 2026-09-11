using System;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core
{
    /// <summary>Immutable world-space closed polygon paths. Even-odd topology preserves Composite holes.</summary>
    public sealed class PolygonAreaGeometry2D
    {
        private readonly Vector2[] vertices;
        private readonly int[] next;
        public ReadOnlySpan<Vector2> Vertices => vertices;
        public int NextVertex(int index) => next[index];

        public PolygonAreaGeometry2D(PolygonCollider2D collider) : this(CapturePaths(collider)) { }
        public PolygonAreaGeometry2D(CompositeCollider2D collider) : this(CapturePaths(collider)) { }
        public PolygonAreaGeometry2D(Vector2[] worldPolygon) : this(new[] { worldPolygon }) { }

        public PolygonAreaGeometry2D(IReadOnlyList<Vector2[]> worldPaths)
        {
            int count = 0;
            foreach (Vector2[] path in worldPaths)
            {
                if (path.Length < 3) throw new ArgumentException("A closed area path requires at least three vertices.");
                count += path.Length;
            }
            if (count == 0) throw new ArgumentException("A polygon area requires a closed path.");
            vertices = new Vector2[count];
            next = new int[count];
            int offset = 0;
            foreach (Vector2[] path in worldPaths)
            {
                for (int index = 0; index < path.Length; index++)
                {
                    int successor = (index + 1) % path.Length;
                    if (path[index] == path[successor]) throw new ArgumentException("An area edge has zero length.");
                    vertices[offset + index] = path[index];
                    next[offset + index] = offset + successor;
                }
                offset += path.Length;
            }
        }

        private static Vector2[][] CapturePaths(PolygonCollider2D collider)
        {
            var paths = new Vector2[collider.pathCount][];
            for (int index = 0; index < paths.Length; index++)
            {
                paths[index] = collider.GetPath(index);
                TransformPath(paths[index], collider.transform, collider.offset);
            }
            return paths;
        }

        private static Vector2[][] CapturePaths(CompositeCollider2D collider)
        {
            if (collider.geometryType != CompositeCollider2D.GeometryType.Polygons || collider.edgeRadius != 0f)
                throw new NotSupportedException("A Composite area requires polygon geometry without an edge radius.");
            var paths = new Vector2[collider.pathCount][];
            for (int index = 0; index < paths.Length; index++)
            {
                paths[index] = new Vector2[collider.GetPathPointCount(index)];
                collider.GetPath(index, paths[index]);
                TransformPath(paths[index], collider.transform, collider.offset);
            }
            return paths;
        }

        private static void TransformPath(Vector2[] path, Transform transform, Vector2 offset)
        {
            for (int index = 0; index < path.Length; index++) path[index] = transform.TransformPoint(path[index] + offset);
        }

        public bool Contains(ReadOnlySpan<Vector2> core, float radius)
        {
            double clearance = Math.Max(0d, radius - PlanarCoreGeometry2D.RoundTripTolerance);
            double clearanceSquared = clearance * clearance;
            foreach (Vector2 point in core)
            {
                if (!ContainsPoint(point)) return false;
                for (int edge = 0; edge < vertices.Length; edge++)
                    if (PlanarCoreGeometry2D.PointDistanceSquared(point, vertices[edge], vertices[next[edge]]) < clearanceSquared)
                        return false;
            }
            for (int index = 0; index < PlanarCoreGeometry2D.EdgeCount(core.Length); index++)
            {
                Vector2 start = core[index], end = core[(index + 1) % core.Length];
                // Positive clearance excludes every crossing. A zero-radius core also needs
                // interval classification to distinguish a concave crossing from a tangent.
                if (clearance == 0d && !ContainsSegment(start, end)) return false;
                for (int edge = 0; edge < vertices.Length; edge++)
                    if (PlanarCoreGeometry2D.SegmentDistanceSquared(start, end, vertices[edge], vertices[next[edge]]) < clearanceSquared)
                        return false;
            }
            // A convex footprint can surround an entire hole without crossing its outline.
            foreach (Vector2 point in vertices)
                if (PlanarCoreGeometry2D.StrictlyInside(point, core)) return false;
            return true;
        }

        public bool Overlaps(ReadOnlySpan<Vector2> core, float radius)
        {
            foreach (Vector2 point in core)
            {
                if (ContainsPoint(point)) return true;
                for (int edge = 0; edge < vertices.Length; edge++)
                    if (PlanarCoreGeometry2D.PointDistanceSquared(point, vertices[edge], vertices[next[edge]]) <= (double)radius * radius)
                        return true;
            }
            for (int index = 0; index < PlanarCoreGeometry2D.EdgeCount(core.Length); index++)
                for (int edge = 0; edge < vertices.Length; edge++)
                    if (PlanarCoreGeometry2D.SegmentDistanceSquared(core[index], core[(index + 1) % core.Length],
                            vertices[edge], vertices[next[edge]]) <= (double)radius * radius) return true;
            foreach (Vector2 point in vertices)
                if (PlanarCoreGeometry2D.StrictlyInside(point, core)) return true;
            return false;
        }

        private bool ContainsPoint(Vector2 point)
        {
            bool inside = false;
            for (int index = 0; index < vertices.Length; index++)
            {
                Vector2 a = vertices[index], b = vertices[next[index]];
                if (PlanarCoreGeometry2D.PointDistanceSquared(point, a, b) <=
                    PlanarCoreGeometry2D.RoundTripTolerance * PlanarCoreGeometry2D.RoundTripTolerance) return true;
                if ((a.y > point.y) != (b.y > point.y) &&
                    point.x < (double)(b.x - a.x) * (point.y - a.y) / (b.y - a.y) + a.x) inside = !inside;
            }
            return inside;
        }

        private bool ContainsSegment(Vector2 start, Vector2 end)
        {
            Span<double> parameters = stackalloc double[vertices.Length + 2];
            int count = 2;
            parameters[0] = 0d;
            parameters[1] = 1d;
            Vector2 delta = end - start;
            for (int edge = 0; edge < vertices.Length; edge++)
            {
                Vector2 a = vertices[edge], span = vertices[next[edge]] - a;
                double denominator = PlanarCoreGeometry2D.Cross(delta, span);
                if (denominator == 0d) continue;
                double t = PlanarCoreGeometry2D.Cross(a - start, span) / denominator;
                double u = PlanarCoreGeometry2D.Cross(a - start, delta) / denominator;
                if (t > 0d && t < 1d && u >= 0d && u <= 1d) parameters[count++] = t;
            }
            for (int index = 1; index < count; index++)
            {
                double value = parameters[index];
                int previous = index - 1;
                while (previous >= 0 && parameters[previous] > value)
                {
                    parameters[previous + 1] = parameters[previous];
                    previous--;
                }
                parameters[previous + 1] = value;
            }
            for (int index = 1; index < count; index++)
                if (!ContainsPoint(start + delta * (float)((parameters[index - 1] + parameters[index]) * 0.5d))) return false;
            return true;
        }
    }
}
