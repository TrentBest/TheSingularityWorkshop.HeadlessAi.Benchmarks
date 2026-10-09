# Measurement methodology

## Question being measured

How does local HeadlessAi invocation overhead change as the request prompt and response text grow, when HTTP responses are deterministic and delivered by an in-memory handler?

## Workload matrix

The current benchmark varies two independent inputs:

| Parameter | Values | What it helps reveal |
|---|---|---|
| Prompt length | 32 and 1,024 characters | Request construction and serialization costs as input grows |
| Response text length | 64 and 4,096 characters | Response reading, JSON parsing, and normalized extraction costs as output grows |

BenchmarkDotNet runs each combination as a separately named case. These are controlled payload-size probes, not a claim that character count is equivalent to provider tokens, semantic complexity, or production traffic. The fixed ASCII characters keep the fixture repeatable.

## Included

- A preconfigured profile and adapter.
- A normalized input constructed during global setup.
- Request creation and serialization.
- The injected `HttpClient` pipeline and fake handler dispatch.
- Response-body reading, JSON parsing, and normalized output extraction.
- Per-invocation allocations visible to BenchmarkDotNet's memory diagnoser.

## Excluded

- DNS, socket setup, TLS, internet transit, and provider latency.
- Provider queues, rate limits, retries performed outside this harness, inference, and token generation.
- User interface work, prompt construction outside the benchmark input, orchestration, and application policy.
- Energy consumption and end-to-end production throughput.

## Controls

Use Release builds, retain BenchmarkDotNet's environment header and generated artifacts, avoid unrelated CPU-heavy work during measurement, and compare like-for-like runtime/OS/architecture configurations. Repeat measurements before describing small differences as meaningful.

Do not collapse the workload matrix into one opaque score. Report the parameter values, runtime, operating system, architecture, build configuration, and raw artifacts alongside any published baseline. No performance number is considered verified until a run has actually produced it.
