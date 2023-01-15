using CleanArchitectureExample.Application.Weather.Contracts;

namespace CleanArchitectureExample.Application.Weather.Interfaces;

public interface IWeatherClient
{
    Task<WeatherResponse?> GetCurrentWeatherForCity(string city, CancellationToken cancellationToken);
}
