using CleanArchitectureExample.Application.Weather.Contracts;

namespace CleanArchitectureExample.Application.Weather.Interfaces;

public interface IWeatherForecastService
{
    Task<WeatherResponse?> GetCurrentWeatherForCity(string city, CancellationToken cancellationToken);
}
