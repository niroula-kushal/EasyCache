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
    private readonly IEasyCache _easyCache;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IEasyCache easyCache)
    {
        _logger = logger;
        _easyCache = easyCache;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var cache = _easyCache.UseCache<List<WeatherForecast>>(GetForecast, "WeatherItems");
        return await cache.GetData();
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