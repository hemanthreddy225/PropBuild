using PropBuild.Application;

namespace PropBuild.UnitTests;

public sealed class ArchitectureTests
{
    [Fact]
    public void ApplicationDependencyRegistrationReturnsServiceCollection()
    {
        var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
        var result = services.AddApplication();
        Assert.Same(services, result);
    }
}
