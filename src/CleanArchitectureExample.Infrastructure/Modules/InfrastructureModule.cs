using CleanArchitectureExample.Domain.Common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArchitectureExample.Infrastructure.Modules;

internal static class InfrastructureModule
{
    internal static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AnyOptions>(configuration.GetSection(AnyOptions.SectionName));
        services.AddSingleton(x => x.GetService<IOptions<AnyOptions>>()!.Value);

        return services;
    }
}
