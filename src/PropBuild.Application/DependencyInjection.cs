using Microsoft.Extensions.DependencyInjection;

namespace PropBuild.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Application services are registered here as business modules are introduced.
        return services;
    }
}
