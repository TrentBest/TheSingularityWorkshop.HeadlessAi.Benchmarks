# Continuation brief

## Purpose

Create reliable evidence about HeadlessAi's local invocation overhead without confusing local library costs with provider or model performance.

## Current baseline

- .NET 8 BenchmarkDotNet console project.
- Fixed in-memory HTTP handler and fixed Responses-shaped JSON output.
- MemoryDiagnoser enabled.
- Separate test project verifies the fixture response.
- CI restores, builds, tests, and uploads coverage; it does not execute long benchmark runs.
- Pull request #1 is open against master; do not merge without explicit approval.

## Boundaries

No live endpoint calls, credentials, timing thresholds in unit tests, energy claims, or published baseline numbers without measurement evidence.

## Next work

1. Confirm CI remains green.
2. Run the benchmark on a documented machine and retain raw artifacts.
3. Add payload-size variants as separately named workloads.
4. Write a results note only after the run can be reproduced.
