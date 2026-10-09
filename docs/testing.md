# Test strategy

The test project calls the benchmark workload once against its in-memory handler and checks the fixed normalized response. This protects the harness from silently changing into a live-network benchmark or breaking the response fixture.

The test does not validate performance. Timing assertions are intentionally absent because shared CI runners are noisy. Performance is evaluated through explicit BenchmarkDotNet runs and saved artifacts, not pass/fail thresholds in unit tests.
