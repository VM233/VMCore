# Changelog

## 1.1.0 - 2026-09-11

- Replace sampled collider containment with immutable convex footprints and polygon-region
  containment and overlap. Capture includes authored offsets, hierarchy transforms, rotation,
  scale, capsule direction and box edge radius, including disabled primitive colliders.
- Preserve concave paths and Composite holes. Overlap detects crossing edges and fully enclosed
  obstacles. Queries do not move scene objects or simulate physics.
- Remove ColliderContainsUtility's position/sample-count API. Capture a ConvexColliderGeometry2D
  with an explicit local-to-query-frame matrix and query ColliderAreaGeometry2D instead.

## 1.0.2 - 2026-09-05

- Make CubeInteger a Unity-serializable value, preserving its bounds and axis directions
  when embedded in native framework settings.
- Consolidate the CubeInteger declaration into its owning file.
