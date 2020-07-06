using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Fluent.Testing.Sample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly List<WeatherForecast> _forecasts;

        public WeatherForecastController()
        {
            var rng = new Random();
            _forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Id = index,
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Index()
        {
            return Ok(_forecasts);
        }

        [HttpGet("{id}")]
        public ActionResult<WeatherForecast> Get([FromRoute] int id)
        {
            var forecast = _forecasts.SingleOrDefault(weatherForecast => weatherForecast.Id == id);

            if (forecast == null)
                return NotFound();

            return Ok(forecast);
        }

        [HttpPost]
        public ActionResult Create(WeatherForecast weatherForecast)
        {
            weatherForecast.Id = _forecasts.Count + 1;
            _forecasts.Add(weatherForecast);
            return CreatedAtAction(nameof(Get), new {id = weatherForecast.Id}, weatherForecast);
        }
    }
}