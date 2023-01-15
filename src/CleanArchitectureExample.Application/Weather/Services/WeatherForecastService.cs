using CleanArchitectureExample.Application.Weather.Contracts;
using CleanArchitectureExample.Application.Weather.Interfaces;

namespace CleanArchitectureExample.Application.Weather.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherClient _weatherClient;

    public WeatherForecastService(IWeatherClient weatherClient)
    {
        _weatherClient = weatherClient;
    }

    public async Task<WeatherResponse?> GetCurrentWeatherForCity(
        string city,
        CancellationToken cancellationToken)
    {
        WeatherResponse? response = await _weatherClient.GetCurrentWeatherForCity(city, cancellationToken);

        return response;
    }
}
