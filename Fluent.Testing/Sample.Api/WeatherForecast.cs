using System;
using System.ComponentModel.DataAnnotations;

namespace Fluent.Testing.Sample.Api
{
    public class WeatherForecast
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required] public int? TemperatureC { get; set; }

        public int TemperatureF => 32 + (int) (TemperatureC.GetValueOrDefault() / 0.5556);

        public string Summary { get; set; } = "";
    }
}