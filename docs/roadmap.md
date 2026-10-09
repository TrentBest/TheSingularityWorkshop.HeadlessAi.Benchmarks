# Roadmap

## Initial baseline
- [x] Dedicated BenchmarkDotNet project.
- [x] Fixed in-memory HTTP fixture; no live provider dependency.
- [x] Memory diagnoser enabled.
- [x] Separate fixture contract tests.
- [x] Methodology and interpretation guidance.
- [x] CI builds and tests without running long benchmark jobs.

## Next measurements
- [ ] Record an initial verified baseline with environment details and raw artifacts.
- [x] Add separately reported prompt-size and response-size workload variants; see [Measurement methodology](methodology.md).
- [ ] Compare adapter invocation paths only where the workloads are genuinely equivalent.
- [ ] Add a separate live endpoint measurement guide only if it can be done safely and without mixing provider latency into library overhead.
