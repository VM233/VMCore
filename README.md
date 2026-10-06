# VMCore

Common Unity utilities, distributed as `com.vm233.vmcore`.

Install through Unity Package Manager using `https://github.com/VM233/VMCore.git`
with `#` followed by the full 40-character commit SHA of the chosen release.
The package manifest declares Unity 2022.3 as its minimum version. The existing
tested Editor baseline is Unity 6000.0.20f1. There are no package dependencies;
the package manifest is the dependency authority.

Runtime contains the public utilities, Editor contains Editor extensions, and
Tests contains focused Editor fixtures. Consumers can enable the package's tests
through Unity's package test workflow and run `VMCore.Editor.Tests`.

Detailed documentation: [collection pools](Documentation~/CollectionPools.md),
[snake case](Documentation~/SnakeCase.md) and
[collider containment and migration](Documentation~/ColliderContainment.md).
See [CHANGELOG](CHANGELOG.md) and [LICENSE](LICENSE).
