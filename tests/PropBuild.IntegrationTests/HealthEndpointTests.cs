using Microsoft.AspNetCore.Mvc.Testing;

namespace PropBuild.IntegrationTests;

public sealed class HealthEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HealthEndpointTests(WebApplicationFactory<Program> factory) => _client = factory.CreateClient();

    [Fact]
    public async Task LivenessHealthCheckReturnsSuccess()
    {
        using var response = await _client.GetAsync("/health");
        response.EnsureSuccessStatusCode();
    }
}
