using System.Net;
using System.Net.Http.Json;
using CleanArchitectureExample.Application.Weather.Contracts;
using CleanArchitectureExample.Application.Weather.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;

namespace CleanArchitectureExample.Application.Weather.Clients;

public class OpenWeatherClient : IWeatherClient
{
    public const string ClientName = "weatherapi";
    private const string OpenWeatherMapApiKey = "33c14fddd26e6dd10109fd042a65c205";

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

    private readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy =
        Policy<HttpResponseMessage>
        .Handle<HttpRequestException>()
        .OrTransientHttpError()
        .AdvancedCircuitBreakerAsync(
            0.5,
            TimeSpan.FromSeconds(5),
            10,
            TimeSpan.FromSeconds(15));

    public OpenWeatherClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<WeatherResponse?> GetCurrentWeatherForCity(
        string city,
        CancellationToken cancellationToken)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(ClientName);

        if (_circuitBreakerPolicy.CircuitState is CircuitState.Open or CircuitState.Isolated)
        {
            return _cache.Get<WeatherResponse>(city);
        }

        HttpResponseMessage response = await _circuitBreakerPolicy.ExecuteAsync(() =>
        httpClient.GetAsync($"weather?q={city}&appid={OpenWeatherMapApiKey}", cancellationToken));

        if (response.StatusCode == HttpStatusCode.OK)
        {
            WeatherResponse? weatherResponse = await response.Content
                .ReadFromJsonAsync<WeatherResponse>(cancellationToken: cancellationToken);

            _cache.Set(city, weatherResponse);

            return weatherResponse;
        }

        return null;
    }
}
