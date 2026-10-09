# Interpreting results without overclaiming

- **Mean / median:** describe local measured invocation time only for the configured workload.
- **Allocated bytes:** describe managed allocations on that path; this is not automatically a measurement of total memory pressure or energy.
- **Gen0 collections:** describe observed collection counts for the run; do not extrapolate them directly into a universal energy-efficiency percentage.
- **Run-to-run variation:** may reflect JIT, runtime, machine load, power policy, or measurement noise.
- **Comparison:** must use the same workload, runtime, hardware, and BenchmarkDotNet settings.

Do not claim the benchmark predicts live model response time. Provider and network effects can dominate the local path by orders of magnitude, and the benchmark intentionally does not measure them.

No baseline is currently advertised. Capture a real run, include the environment and raw artifacts, then summarize the result with its limitations.
