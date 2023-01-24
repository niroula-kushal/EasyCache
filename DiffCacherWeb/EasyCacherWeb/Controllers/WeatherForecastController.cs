using EasyCacher.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EasyCache.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IEasyCacheClient _easyCacheClient;
    private readonly IEasyCacheKeyManager _easyCacheKeyManager;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IEasyCacheClient easyCacheClient,
        IEasyCacheKeyManager easyCacheKeyManager)
    {
        _logger = logger;
        _easyCacheClient = easyCacheClient;
        _easyCacheKeyManager = easyCacheKeyManager;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        var cache = _easyCacheClient.UseCache<List<WeatherForecast>>(GetForecast, new
        {
            key = "weather_items",
            id = 45
        });
        var keys = _easyCacheKeyManager.GetKeys();
        return Ok(new
        {
            data = (await cache.GetData()).ToList(),
            keys = keys
        });
    }

    private Task<List<WeatherForecast>> GetForecast()
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToList());
    }
}