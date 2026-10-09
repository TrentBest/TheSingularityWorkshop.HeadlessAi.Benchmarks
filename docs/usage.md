# Run benchmarks

## Requirements

- .NET 8 SDK.
- Git installed and available on PATH.
- NuGet access to restore BenchmarkDotNet and test dependencies.
- No provider credentials and no external AI endpoint access.

The benchmark harness uses the HeadlessAi development source as a project dependency until the alpha package is available. GitHub Actions checks out that dependency automatically. On Windows, the repository's verification script bootstraps it locally.

## Build and verify

### Windows PowerShell — one command

From the repository root, run:

```powershell
.\eng\verify.ps1
```

The script safely clones the HeadlessAi development source into `dependencies\HeadlessAi` only when that expected checkout is absent, then restores, builds, and tests the solution. It does not delete or replace an unexpected existing dependency folder. To explicitly update an existing dependency checkout before verification, run:

```powershell
.\eng\verify.ps1 -UpdateDependency
```

Use `-Configuration Debug` if needed; the default is `Release`.

### Visual Studio

1. Open `TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx` from the repository root, rather than opening the benchmark `.csproj` by itself.
2. If the local dependency has not been bootstrapped, run `eng\verify.ps1` once from PowerShell. This ensures the source dependency exists and performs restore/build/test together.
3. In Solution Explorer, right-click the **solution** and choose **Restore NuGet Packages**; then build the solution in **Release** configuration.
4. If Visual Studio still reports `NETSDK1004` for `obj\project.assets.json`, restore did not complete for that project. Check the **Output** window's **Package Manager** / restore messages for the first underlying error; the missing assets file is a symptom, not the original cause.

## Execute

```powershell
dotnet run --project .\src\TheSingularityWorkshop.HeadlessAi.Benchmarks --configuration Release
```

BenchmarkDotNet writes detailed reports under `BenchmarkDotNet.Artifacts`. Preserve the raw reports when sharing results. The harness is intentionally not part of routine CI because stable benchmark runs need controlled execution and can take significantly longer than unit tests.

## Run on GitHub Actions

The repository also provides a manual [Run BenchmarkDotNet Measurements workflow](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi.Benchmarks/actions/workflows/benchmark.yml). Open the workflow, choose **Run workflow** on `development`, and start it. It builds the same source-based dependency setup, runs the full parameter matrix on the GitHub-hosted runner, and uploads the BenchmarkDotNet output as a workflow artifact for 30 days. This is a convenient first measurement, not a universal performance baseline: record the runner/runtime details and preserve the raw reports before comparing results. The workflow makes no live provider calls and has no publishing step.
