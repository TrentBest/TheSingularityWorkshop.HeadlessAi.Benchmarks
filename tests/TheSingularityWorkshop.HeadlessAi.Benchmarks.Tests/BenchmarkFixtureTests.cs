using TheSingularityWorkshop.HeadlessAi.Benchmarks;

namespace TheSingularityWorkshop.HeadlessAi.Benchmarks.Tests;

public sealed class BenchmarkFixtureTests
{
    [Fact]
    public async Task BenchmarkWorkloadReturnsStableFixtureOutput()
    {
        var benchmark = new HeadlessAiInvocationBenchmarks();
        benchmark.Setup();
        try
        {
            var output = await benchmark.InvokeLocalFakeEndpoint();
            Assert.Equal("Fixed benchmark response.", output.Content);
        }
        finally
        {
            benchmark.Cleanup();
        }
    }
}
