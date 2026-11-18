using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropBuild.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using PropBuild.Domain.Entities;

namespace PropBuild.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres")
            ?? throw new InvalidOperationException("Connection string 'Postgres' is not configured.");

        services.AddDbContext<PropBuildDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();
        services.AddHealthChecks().AddDbContextCheck<PropBuildDbContext>("postgresql");

        return services;
    }
}
