using CleanArchitectureExample.Application.Weather.Contracts;
using CleanArchitectureExample.Application.Weather.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureExample.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet("weather/{city}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Forecast(
        string city,
        CancellationToken cancellationToken = default)
    {
        WeatherResponse? response = await _weatherForecastService.GetCurrentWeatherForCity(city, cancellationToken);

        return response is not null ? Ok(response) : NotFound();
    }
}
