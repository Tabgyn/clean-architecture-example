using CleanArchitectureExample.Application;
using CleanArchitectureExample.Application.Common.Behaviors;
using CleanArchitectureExample.Application.Weather.Clients;
using CleanArchitectureExample.Application.Weather.Interfaces;
using CleanArchitectureExample.Application.Weather.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureExample.Infrastructure.Modules;

internal static class ApplicationModule
{
    internal static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(ApplicationAssembly.Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        services.AddSingleton<IWeatherClient, OpenWeatherClient>();
        services.AddHttpClient(OpenWeatherClient.ClientName, client =>
        client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/"));

        services.AddValidatorsFromAssembly(ApplicationAssembly.Assembly,
            includeInternalTypes: true);

        return services;
    }
}
