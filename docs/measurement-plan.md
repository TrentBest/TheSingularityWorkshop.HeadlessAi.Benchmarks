# Measurement plan

## Current question

Measure the local HeadlessAi request/response path using a fixed input and fixed in-memory response.

## Evidence required for a baseline

Preserve raw BenchmarkDotNet output, generated reports, commit SHA, working-tree state, .NET runtime, OS, architecture, CPU model, Release configuration, benchmark settings, workload description, response size, and memory-diagnostic status.

## Planned dimensions

1. Payload size: small, medium, and large fixed prompt content.
2. Response size: multiple fixed response lengths to expose parsing and allocation costs.
3. Adapter work: compare only equivalent protocol paths and report them independently.
4. Cold start versus steady state: separate startup from repeated invocation.
5. Concurrency: add only with a defined workload and repeatable scheduling.

## Reporting gate

Do not publish numerical conclusions until a run exists and raw output and environment details are available. Do not infer total energy use from allocations or GC counts alone. Local fake-handler time is not live provider latency.
