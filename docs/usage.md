# Run benchmarks

## Requirements

- .NET 8 SDK.
- NuGet access to restore BenchmarkDotNet and test dependencies.
- No provider credentials and no external AI endpoint access.
- Until the alpha package is published, clone the HeadlessAi source into the expected sibling folder; CI does this automatically.

## Build and verify

Run these commands from the **repository root** (the folder containing `TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx`).

### Windows PowerShell

```powershell
# Run once if the dependencies\HeadlessAi folder does not exist yet.
New-Item -ItemType Directory -Force dependencies | Out-Null
git clone --branch development https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi.git dependencies/HeadlessAi

dotnet --list-sdks
dotnet restore .\TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx
dotnet build .\TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx --configuration Release --no-restore
dotnet test .\TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx --configuration Release
```

If `dependencies\HeadlessAi` already exists, do not clone it again; update that checkout with `git -C dependencies\HeadlessAi pull` instead. The benchmark project conditionally references this local source checkout. Without it, the project falls back to the alpha NuGet package, which may not be available from your configured package sources.

### Visual Studio

1. Open `TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx` from the repository root, rather than opening the benchmark `.csproj` by itself.
2. In Solution Explorer, right-click the **solution** and choose **Restore NuGet Packages**.
3. Wait for restore to finish, then build the solution in **Release** configuration.
4. If Visual Studio reports `NETSDK1004` for `obj\project.assets.json`, restore did not complete for that project. Check the **Output** window's **Package Manager** / restore messages for the first underlying error; the missing assets file is a symptom, not the original cause.
5. Confirm the local HeadlessAi checkout exists at `dependencies\HeadlessAi\src\TheSingularityWorkshop.HeadlessAi\TheSingularityWorkshop.HeadlessAi.csproj`. If it does not, follow the PowerShell setup above, then restore again.

## Execute

```powershell
dotnet run --project .\src\TheSingularityWorkshop.HeadlessAi.Benchmarks --configuration Release
```

BenchmarkDotNet writes detailed reports under `BenchmarkDotNet.Artifacts`. Preserve the raw reports when sharing results. The harness is intentionally not part of routine CI because stable benchmark runs need controlled execution and can take significantly longer than unit tests.

## Run on GitHub Actions

The repository also provides a manual [Run BenchmarkDotNet Measurements workflow](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi.Benchmarks/actions/workflows/benchmark.yml). Open the workflow, choose **Run workflow** on `development`, and start it. It builds the same source-based dependency setup, runs the full parameter matrix on the GitHub-hosted runner, and uploads the BenchmarkDotNet output as a workflow artifact for 30 days. This is a convenient first measurement, not a universal performance baseline: record the runner/runtime details and preserve the raw reports before comparing results. The workflow makes no live provider calls and has no publishing step.
