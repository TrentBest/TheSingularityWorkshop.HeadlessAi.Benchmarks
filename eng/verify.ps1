# Verify the HeadlessAi benchmark harness from a Windows checkout.
# Run from any directory: .\eng\verify.ps1
[CmdletBinding()]
param(
    [switch]$UpdateDependency,
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release"
)

$ErrorActionPreference = "Stop"
$repoRoot = Split-Path -Parent $PSScriptRoot
$solution = Join-Path $repoRoot "TheSingularityWorkshop.HeadlessAi.Benchmarks.slnx"
$dependencyRoot = Join-Path $repoRoot "dependencies\HeadlessAi"
$dependencyProject = Join-Path $dependencyRoot "src\TheSingularityWorkshop.HeadlessAi\TheSingularityWorkshop.HeadlessAi.csproj"

if (-not (Test-Path $solution)) {
    throw "Could not find the solution at '$solution'. The repository checkout may be incomplete."
}

if (-not (Test-Path $dependencyProject)) {
    if (Test-Path $dependencyRoot) {
        throw "The dependency folder exists but does not contain the expected HeadlessAi project: '$dependencyProject'. Inspect that checkout; the script will not delete or replace it."
    }

    New-Item -ItemType Directory -Force (Split-Path -Parent $dependencyRoot) | Out-Null
    Write-Host "Fetching the HeadlessAi development source required by this benchmark harness..."
    & git clone --branch development https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi.git $dependencyRoot
    if ($LASTEXITCODE -ne 0) { throw "git clone failed with exit code $LASTEXITCODE." }
}
elseif ($UpdateDependency) {
    Write-Host "Updating the existing HeadlessAi source checkout..."
    & git -C $dependencyRoot pull --ff-only
    if ($LASTEXITCODE -ne 0) { throw "Updating HeadlessAi failed with exit code $LASTEXITCODE." }
}

Push-Location $repoRoot
try {
    Write-Host "Restoring solution..."
    & dotnet restore $solution
    if ($LASTEXITCODE -ne 0) { throw "dotnet restore failed with exit code $LASTEXITCODE." }

    Write-Host "Building solution ($Configuration)..."
    & dotnet build $solution --configuration $Configuration --no-restore
    if ($LASTEXITCODE -ne 0) { throw "dotnet build failed with exit code $LASTEXITCODE." }

    Write-Host "Running tests ($Configuration)..."
    & dotnet test $solution --configuration $Configuration --no-build
    if ($LASTEXITCODE -ne 0) { throw "dotnet test failed with exit code $LASTEXITCODE." }

    Write-Host "Verification completed successfully."
}
finally {
    Pop-Location
}
