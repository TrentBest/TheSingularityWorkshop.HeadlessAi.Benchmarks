# Repository map

- README.md: purpose, quick start, measurement scope, and reader path.
- DOCUMENTATION_STANDARD.md: local application of Workshop documentation rules.
- DOCUMENTATION_INDEX.md: guided document directory.
- TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx: benchmark and test solution.
- src/: BenchmarkDotNet workload and fixed in-memory HTTP handler.
- tests/: fixture test for the benchmark workload.
- docs/usage.md: build, run, and artifact instructions.
- docs/methodology.md: included/excluded costs and controls.
- docs/interpreting-results.md: interpretation rules.
- .github/workflows/build.yml: build, test, and coverage; long benchmark runs are not part of CI.

The harness measures local invocation overhead only, not provider/model performance.
