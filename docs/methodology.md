# Measurement methodology

## Question being measured

How much local overhead does the HeadlessAi invocation path add when the HTTP response is deterministic and delivered by an in-memory handler?

## Included

- A preconfigured profile and adapter.
- Normalized input already allocated before invocation.
- Request creation and serialization.
- The injected HttpClient pipeline and fake handler dispatch.
- Response-body reading, JSON parsing, and normalized output extraction.
- Per-invocation allocations visible to BenchmarkDotNet's memory diagnoser.

## Excluded

- DNS, socket setup, TLS, internet transit, and provider latency.
- Provider queues, rate limits, retries performed outside this harness, inference, and token generation.
- User interface work, prompt construction outside the benchmark input, orchestration, and application policy.
- Energy consumption and end-to-end production throughput.

## Controls

Use Release builds, retain BenchmarkDotNet's environment header and generated artifacts, avoid unrelated CPU-heavy work during measurement, and compare like-for-like runtime/OS/architecture configurations. Repeat measurements before describing small differences as meaningful.

A future benchmark may add explicit payload-size and response-size parameters, but each workload must be named and reported separately rather than averaged into an opaque score.
