using System.Net;
using System.Text;
using BenchmarkDotNet.Attributes;
using TheSingularityWorkshop.HeadlessAi;

namespace TheSingularityWorkshop.HeadlessAi.Benchmarks;

/// <summary>
/// Measures HeadlessAi's local invocation path with a deterministic in-memory handler.
/// This intentionally excludes network transit and provider inference.
/// </summary>
[MemoryDiagnoser]
public class HeadlessAiInvocationBenchmarks
{
    private HttpClient? _httpClient;
    private HeadlessAiAgent? _agent;
    private readonly HeadlessAiInput _input = new("Summarize this short, fixed benchmark prompt.");

    /// <summary>Builds a fixed response path before measurement begins.</summary>
    [GlobalSetup]
    public void Setup()
    {
        _httpClient = new HttpClient(new FixtureHandler());
        var profile = new HeadlessAiProfile(
            "benchmark-fixture",
            new Uri("https://benchmark.invalid/v1/responses"),
            settings: new Dictionary<string, string> { ["model"] = "fixture-model" },
            timeout: TimeSpan.FromSeconds(10),
            maxResponseBytes: 16 * 1024);
        _agent = new HeadlessAiAgentTemplate(profile, new OpenAiResponsesAdapter()).CreateAgent(_httpClient);
    }

    /// <summary>Invokes the adapter and response pipeline against the local fixture.</summary>
    [Benchmark]
    public Task<HeadlessAiOutput> InvokeLocalFakeEndpoint()
        => (_agent ?? throw new InvalidOperationException("Benchmark setup has not run.")).SendAsync(_input);

    /// <summary>Disposes the benchmark-owned transport.</summary>
    [GlobalCleanup]
    public void Cleanup()
    {
        _httpClient?.Dispose();
        _httpClient = null;
        _agent = null;
    }

    private sealed class FixtureHandler : HttpMessageHandler
    {
        private const string ResponseJson = """{"output":[{"type":"message","content":[{"type":"output_text","text":"Fixed benchmark response."}]}]}""";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ResponseJson, Encoding.UTF8, "application/json")
            });
        }
    }
}
