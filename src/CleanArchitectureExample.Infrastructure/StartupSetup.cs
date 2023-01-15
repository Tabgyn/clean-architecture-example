using CleanArchitectureExample.Infrastructure.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureExample.Infrastructure;

public static class StartupSetup
{
    public static IServiceCollection ProjectBootstrap(
        this IServiceCollection services,
        IConfiguration configuration) => services
        .AddInfrastructure(configuration)
        .AddApplication()
        .AddData();
}
