# Contributing

Contributions should improve reproducibility, measurement clarity, or responsible interpretation. Keep routine workloads independent of live providers, credentials, and billing. Do not add timing thresholds to unit tests. Name workloads clearly, retain raw BenchmarkDotNet output, and record runtime, OS, architecture, CPU, configuration, and measurement settings.

## Verify locally

Before restoring, ensure the HeadlessAi source checkout exists at `dependencies/HeadlessAi`; see [Run benchmarks](docs/usage.md) for the one-time setup.

```bash
dotnet restore TheSingularityWorkshop.HeadlessAi.Benchmarks.sln
dotnet build TheSingularityWorkshop.HeadlessAi.Benchmarks.sln --configuration Release --no-restore
dotnet test TheSingularityWorkshop.HeadlessAi.Benchmarks.sln --configuration Release
```

Run the actual benchmark explicitly when ready:

```bash
dotnet run --project src/TheSingularityWorkshop.HeadlessAi.Benchmarks --configuration Release
```

Follow the [FSM_COS documentation standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/development/DOCUMENTATION_STANDARD.md). Distinguish measurements, interpretations, hypotheses, and plans.
