using System;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Sample.Api;

namespace Fluent.Testing.Library.Tests
{
    public class Start : BeginAScenario
    {
        public WeatherForecastCreated Weather_forecast_has_been_created()
        {
            return new WeatherForecastCreated(() =>
            {
                // API Call
                return new WeatherForecast
                {
                    Id = 1234,
                    Summary = "its hot and sunny",
                    TemperatureC = 21
                };
            });
        }
    }
    
    public class WeatherForecastCreated : ScenarioStart<WeatherForecast>
    {
        public WeatherForecastCreated(Func<WeatherForecast> output) : base(output)
        {
        }

        public WeatherForecastUpdated Weather_forecast_has_been_updated()
        {
            return new WeatherForecastUpdated(forecast =>
            {
                // API Call UPdate
                return new WeatherForecast
                {
                    Id = forecast.Id,
                    Summary = "its now cold.",
                    TemperatureC = 12
                };
            }, PipelineBuilder);
        }
    }

    public class WeatherForecastUpdated : ScenarioStep<WeatherForecast, WeatherForecast>
    {
        public WeatherForecastUpdated(Func<WeatherForecast, WeatherForecast> scenarioAction,
            PipelineBuilder pipelineBuilder) : base(scenarioAction, pipelineBuilder)
        {
        }

        public WeatherForecastDeleted Weather_forecast_has_been_deleted()
        {
            return new WeatherForecastDeleted(forecast =>
            {
                // Api Delete 
                var idToDelete = forecast.Id;
            }, PipelineBuilder);
        }
    }

    public class WeatherForecastDeleted : ScenarioEnd<WeatherForecast>
    {
        public WeatherForecastDeleted(Action<WeatherForecast> scenarioAction, PipelineBuilder pipelineBuilder) : base(
            scenarioAction, pipelineBuilder)
        {
        }
    }
}