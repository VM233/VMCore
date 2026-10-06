# Changelog

## 1.1.7 - 2026-10-06

- Repeat the retained-collection regression in one application domain and
  assert the lease/return contract without assuming the pool starts empty.
  The independent initialization implementation is unchanged.

## 1.1.6 - 2026-10-06

- Initialize single-thread and shared collection pools independently, so a
  caller requesting one mode does not construct the other mode's pool and
  generic concurrent queue during first use.
- Verify retained-collection isolation, factory clearing and concurrent first
  publication with focused Editor tests. Existing capacities and lease/return
  semantics remain unchanged.

## 1.1.5 - 2026-10-04

- Measure snake-case allocations with Unity's thread-local GC.Alloc recorder
  and a known positive control, so an unavailable managed byte counter cannot
  make the allocation regression pass falsely or fail without valid observation.

## 1.1.4 - 2026-10-04

- Bind the snake-case fixture's assertions to NUnit explicitly so the test
  assembly compiles alongside VMCore's assertion utility.

## 1.1.3 - 2026-10-04

- Correct the three existing runtime folder metadata records to declare folder
  ownership, preserving their GUIDs and completing the native package meta audit.

## 1.1.2 - 2026-10-04

- Give WordSegment its own declaration file, preserving its public value API and
  the snake-case allocation repair while satisfying the one-type-per-file policy.

## 1.1.1 - 2026-10-03

- Normalize canonical lowercase ASCII snake case without word, lowercase or join
  allocations, preserving all other current-culture normalization semantics.
- Add focused culture, exhaustive short-input, reference identity and allocation
  tests. Move detailed collider usage and migration out of the README.

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
