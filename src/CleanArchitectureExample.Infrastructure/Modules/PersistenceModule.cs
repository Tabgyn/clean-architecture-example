using CleanArchitectureExample.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureExample.Infrastructure.Modules;

internal static class PersistenceModule
{
    internal static IServiceCollection AddData(this IServiceCollection services)
    {
        services
            .Scan(
            selector => selector.FromAssemblies(
            PersistenceAssembly.Assembly)
            .AddClasses(false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("CleanArchitecture");
            options.ConfigureWarnings(config =>
            config.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });

        return services;
    }
}
