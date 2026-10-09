using TheSingularityWorkshop.HeadlessAi.Benchmarks;
using Xunit;

namespace TheSingularityWorkshop.HeadlessAi.Benchmarks.Tests;

public sealed class BenchmarkFixtureTests
{
    [Fact]
    public async Task BenchmarkWorkloadReturnsConfiguredFixtureOutput()
    {
        var benchmark = new HeadlessAiInvocationBenchmarks
        {
            PromptLength = 32,
            ResponseLength = 64
        };
        benchmark.Setup();
        try
        {
            var output = await benchmark.InvokeLocalFakeEndpoint();
            Assert.Equal(64, output.Content.Length);
            Assert.All(output.Content, character => Assert.Equal('r', character));
        }
        finally
        {
            benchmark.Cleanup();
        }
    }
}
