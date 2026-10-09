# Security policy

This local performance harness should not need secrets or live provider access. Do not add API keys or provider credentials. Keep routine benchmarks and CI on deterministic in-memory fixtures. Treat captured response data as potentially sensitive if future workloads introduce real payloads. The benchmark does not measure or guarantee provider security, output safety, or production readiness.
