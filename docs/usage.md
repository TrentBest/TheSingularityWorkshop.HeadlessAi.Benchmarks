# Run benchmarks

## Requirements

- .NET 8 SDK.
- Git.
- NuGet access to restore BenchmarkDotNet and test dependencies.
- No provider credentials; the benchmark uses an in-memory HTTP handler.

HeadlessAi's alpha package is not yet available from NuGet, so this benchmark references the source project. GitHub Actions checks out that source automatically. For a local checkout, run this once from the benchmark repository root if `dependencies\HeadlessAi` does not already exist:

```powershell
git clone --branch development https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi.git .\dependencies\HeadlessAi
```

Do not clone over an existing folder.

## Restore, build, and test

From the repository root:

```powershell
dotnet restore .\TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx
dotnet build .\TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx --configuration Release --no-restore
dotnet test .\TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx --configuration Release --no-build
```

The same solution can be opened in Visual Studio. Restore NuGet packages, then build the solution in **Release** configuration. If Visual Studio reports `NETSDK1004` for `obj\project.assets.json`, restore did not complete; check the first restore error in the Output window.

## Run measurements

```powershell
dotnet run --project .\src\TheSingularityWorkshop.HeadlessAi.Benchmarks --configuration Release
```

BenchmarkDotNet writes its detailed reports under `BenchmarkDotNet.Artifacts`. Preserve the raw reports when sharing results. The benchmark measures local HeadlessAi invocation overhead against a deterministic in-memory HTTP handler; it does not measure live model inference, network latency, energy consumption, or end-to-end user latency.

## GitHub Actions

The [build workflow](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi.Benchmarks/actions/workflows/build.yml) restores, builds, and tests the solution. The separate [measurement workflow](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi.Benchmarks/actions/workflows/benchmark.yml) runs BenchmarkDotNet on demand and uploads the raw output as an artifact. Hosted-runner results are useful for an initial check, not as a replacement for a recorded baseline from a controlled machine.

No workflow publishes a NuGet package.
