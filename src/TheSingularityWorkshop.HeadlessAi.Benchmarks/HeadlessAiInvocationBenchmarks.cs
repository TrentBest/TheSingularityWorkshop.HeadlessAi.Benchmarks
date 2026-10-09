using System.Net;
using System.Text;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using TheSingularityWorkshop.HeadlessAi;

namespace TheSingularityWorkshop.HeadlessAi.Benchmarks;

/// <summary>
/// Measures HeadlessAi's local invocation path with deterministic in-memory responses.
/// This intentionally excludes network transit and provider inference.
/// </summary>
[MemoryDiagnoser]
public class HeadlessAiInvocationBenchmarks
{
    private HttpClient? _httpClient;
    private HeadlessAiAgent? _agent;
    private HeadlessAiInput? _input;

    /// <summary>Length of the fixed prompt payload in characters.</summary>
    [Params(32, 1024)]
    public int PromptLength { get; set; }

    /// <summary>Length of the fixed response text in characters.</summary>
    [Params(64, 4096)]
    public int ResponseLength { get; set; }

    /// <summary>Builds a fixed request and response path before measurement begins.</summary>
    [GlobalSetup]
    public void Setup()
    {
        var responseText = new string('r', ResponseLength);
        var responseJson = JsonSerializer.Serialize(new
        {
            output = new[]
            {
                new
                {
                    type = "message",
                    content = new[] { new { type = "output_text", text = responseText } }
                }
            }
        });

        _httpClient = new HttpClient(new FixtureHandler(responseJson));
        var profile = new HeadlessAiProfile(
            "benchmark-fixture",
            new Uri("https://benchmark.invalid/v1/responses"),
            settings: new Dictionary<string, string> { ["model"] = "fixture-model" },
            timeout: TimeSpan.FromSeconds(10),
            maxResponseBytes: 16 * 1024);
        _agent = new HeadlessAiAgentTemplate(profile, new OpenAiResponsesAdapter()).CreateAgent(_httpClient);
        _input = new HeadlessAiInput(new string('p', PromptLength));
    }

    /// <summary>Invokes the adapter and response pipeline against the local fixture.</summary>
    [Benchmark]
    public Task<HeadlessAiOutput> InvokeLocalFakeEndpoint()
        => (_agent ?? throw new InvalidOperationException("Benchmark setup has not run."))
            .SendAsync(_input ?? throw new InvalidOperationException("Benchmark setup has not run."));

    /// <summary>Disposes the benchmark-owned transport.</summary>
    [GlobalCleanup]
    public void Cleanup()
    {
        _httpClient?.Dispose();
        _httpClient = null;
        _agent = null;
        _input = null;
    }

    private sealed class FixtureHandler(string responseJson) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
            });
        }
    }
}
