# VMCore
 The Common Utilities for unity

 Have Tested on:
- Unity 6000.0.20f1

`CubeInteger` supports Unity field serialization, including both bounds and axis directions.

For collider containment, capture `ConvexColliderGeometry2D` from a circle, capsule or box using
an explicit collider-local-to-world matrix. `ColliderAreaGeometry2D.Contains` and `Overlaps`
consume its vertices and radius, with no angular samples or temporary Transform changes.
Areas support primitive colliders, closed Polygon paths, and polygon-mode Composite paths
with zero edge radius. `PolygonAreaGeometry2D` can also be retained for continuous geometric
queries. Captures are immutable, so their owner must replace them when authored geometry or
the captured coordinate frame changes. Project-specific CustomCollider2D shapes must resolve
their authored rest primitive before capture.

The old `ColliderContainsUtility` position/sample-count API is removed. To query a proposed
body position, prepend the requested world translation to each collider's local-to-world
matrix. Rotation, scale and hierarchy offsets remain part of that explicit frame.
