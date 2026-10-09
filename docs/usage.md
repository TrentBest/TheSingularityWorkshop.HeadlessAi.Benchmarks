# Run benchmarks

## Requirements

- .NET 8 SDK.
- NuGet access to restore BenchmarkDotNet and test dependencies.
- No provider credentials and no external AI endpoint access.
- Until the alpha package is published, clone the HeadlessAi source into the expected sibling folder; CI does this automatically.

## Build and verify

```bash
mkdir -p dependencies
git clone --branch development https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi.git dependencies/HeadlessAi
dotnet restore TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx
dotnet build TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx --configuration Release --no-restore
dotnet test TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx --configuration Release
```

## Execute

```bash
dotnet run --project src/TheSingularityWorkshop.HeadlessAi.Benchmarks --configuration Release
```

BenchmarkDotNet writes detailed reports under `BenchmarkDotNet.Artifacts`. Preserve the raw reports when sharing results. The harness is intentionally not part of routine CI because stable benchmark runs need controlled execution and can take significantly longer than unit tests.
