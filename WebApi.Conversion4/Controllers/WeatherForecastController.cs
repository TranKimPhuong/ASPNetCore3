using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog.Sinks.AzureTableStorage.KeyGenerator;

namespace WebApi.Conversion4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            try
            {
                _logger.LogInformation("Hello WeatherForecast!!!!");
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Cannot get wetherForecast");
                return null;
            }
        }
    }

    #region for test log on azure 
    public class App
    {
        private readonly ILogger<App> _logger;

        public App(ILogger<App> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogDebug("Testing 123");
            _logger.LogDebug("Testing 456");
            _logger.LogDebug("Testing 789");
            System.Console.ReadKey();
        }
    }

    public class MyKeyGenerator : IKeyGenerator
    {
        string IKeyGenerator.GeneratePartitionKey(LogEvent logEvent)
        {
            return (DateTime.MaxValue.Ticks - logEvent.Timestamp.Ticks).ToString().Substring(0, 9);
        }

        string IKeyGenerator.GenerateRowKey(LogEvent logEvent, string suffix)
        {
            return (DateTime.MaxValue.Ticks - logEvent.Timestamp.Ticks).ToString() + "." + Guid.NewGuid().ToString();
        }
    }
    #endregion
}
