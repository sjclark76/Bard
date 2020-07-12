using Fluent.Testing.Library.Given;
using Fluent.Testing.Sample.Api;

namespace Fluent.Testing.Library.Tests
{
    public class Start : BeginAScenario
    {
        public WeatherForecastCreated Weather_forecast_has_been_created()
        {
            return AddStep<WeatherForecastCreated, WeatherForecast>((context) =>
            {
                var weatherForecast = new WeatherForecast
                {
                    Id = 1234,
                    Summary = "its hot and sunny",
                    TemperatureC = 21
                };
                // API Call
                var response = context.Api.Post("WeatherForecast", weatherForecast);

                return response.Content<WeatherForecast>();
            });
        }
    }

    public class WeatherForecastCreated : ScenarioStep<WeatherForecast> 
    {
        public WeatherForecastUpdated Weather_forecast_has_been_updated()
        {
            return AddStep<WeatherForecastUpdated, WeatherForecast>((context, forecast) =>
            {
                // API Call Update
                forecast.Summary = "its now cold.";
                forecast.TemperatureC = 12;

                context.Api.Put($"WeatherForecast/{forecast.Id}", forecast);

                return forecast;
            });
        }
    }

    public class WeatherForecastUpdated : ScenarioStep<WeatherForecast>
    {
        public WeatherForecastDeleted Weather_forecast_has_been_deleted()
        {
            return AddStep<WeatherForecastDeleted, WeatherForecast>((context, forecast) =>
            {
                context.Api.Delete($"WeatherForecast/{forecast.Id}");

                return forecast;
            });
        }
    }

    public class WeatherForecastDeleted : ScenarioStep<WeatherForecast>
    {
      
    }
}