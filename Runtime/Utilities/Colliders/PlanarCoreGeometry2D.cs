using System;
using UnityEngine;

namespace VMFramework.Core
{
    internal static class PlanarCoreGeometry2D
    {
        internal const double RoundTripTolerance = 0.000001d;
        internal static int EdgeCount(int count) => count == 1 ? 0 : count == 2 ? 1 : count;
        internal static double Cross(Vector2 a, Vector2 b) => (double)a.x * b.y - (double)a.y * b.x;

        internal static bool StrictlyInside(Vector2 point, ReadOnlySpan<Vector2> polygon)
        {
            if (polygon.Length < 3) return false;
            int sign = 0;
            for (int index = 0; index < polygon.Length; index++)
            {
                Vector2 edge = polygon[(index + 1) % polygon.Length] - polygon[index];
                double cross = Cross(edge, point - polygon[index]);
                if (Math.Abs(cross) <= RoundTripTolerance * edge.magnitude) return false;
                int current = Math.Sign(cross);
                if (sign != 0 && sign != current) return false;
                sign = current;
            }
            return true;
        }

        internal static double PointDistanceSquared(Vector2 point, ReadOnlySpan<Vector2> core)
        {
            if (StrictlyInside(point, core)) return 0d;
            if (core.Length == 1) return PointDistanceSquared(point, core[0], core[0]);
            double result = double.PositiveInfinity;
            for (int index = 0; index < EdgeCount(core.Length); index++)
                result = Math.Min(result, PointDistanceSquared(point, core[index], core[(index + 1) % core.Length]));
            return result;
        }

        internal static double DistanceSquared(ReadOnlySpan<Vector2> first, ReadOnlySpan<Vector2> second)
        {
            double distance = double.PositiveInfinity;
            foreach (Vector2 point in first) distance = Math.Min(distance, PointDistanceSquared(point, second));
            foreach (Vector2 point in second) distance = Math.Min(distance, PointDistanceSquared(point, first));
            for (int a = 0; a < EdgeCount(first.Length); a++)
                for (int b = 0; b < EdgeCount(second.Length); b++)
                    distance = Math.Min(distance, SegmentDistanceSquared(first[a], first[(a + 1) % first.Length],
                        second[b], second[(b + 1) % second.Length]));
            return distance;
        }

        internal static double SegmentDistanceSquared(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            Vector2 first = b - a, second = d - c;
            double denominator = Cross(first, second);
            if (denominator != 0d)
            {
                double t = Cross(c - a, second) / denominator;
                double u = Cross(c - a, first) / denominator;
                if (t >= 0d && t <= 1d && u >= 0d && u <= 1d) return 0d;
            }
            return Math.Min(Math.Min(PointDistanceSquared(a, c, d), PointDistanceSquared(b, c, d)),
                Math.Min(PointDistanceSquared(c, a, b), PointDistanceSquared(d, a, b)));
        }

        internal static double PointDistanceSquared(Vector2 point, Vector2 a, Vector2 b)
        {
            double x = (double)b.x - a.x, y = (double)b.y - a.y;
            double px = (double)point.x - a.x, py = (double)point.y - a.y;
            double lengthSquared = x * x + y * y;
            double t = lengthSquared == 0d ? 0d : Math.Max(0d, Math.Min(1d, (px * x + py * y) / lengthSquared));
            double dx = px - t * x, dy = py - t * y;
            return dx * dx + dy * dy;
        }
    }
}
