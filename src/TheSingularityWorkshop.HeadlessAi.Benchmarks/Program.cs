using BenchmarkDotNet.Running;

namespace TheSingularityWorkshop.HeadlessAi.Benchmarks;

internal static class Program
{
    private static void Main(string[] args)
        => BenchmarkRunner.Run<HeadlessAiInvocationBenchmarks>(args);
}
