using System;
using System.Collections.Generic;
using Fluent.Testing.Library.Configuration;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Sample.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Fluent.Testing.Library.Tests
{
    public class UnitTest3
    {
        public UnitTest3(ITestOutputHelper output)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseStartup<Startup>()
                        .UseTestServer()
                        .UseEnvironment("development"));

            var host = hostBuilder.Start();

            var httpClient = host.GetTestClient();

            Scenario = ScenarioHostConfiguration
                .TheApiUses(httpClient)
                .Log(output.WriteLine)
                .AndBeginsWithScenario(() => new Start())
                .Build();
        }

        public IFluentScenario<Start> Scenario { get; set; }

        [Fact]
        public void Post_should_return_400_if_required_field_is_not_provided22()
        {
            Scenario.Given
                .That
                .Weather_forecast_has_been_created()
                .Weather_forecast_has_been_updated()
                .Weather_forecast_has_been_deleted();

            Scenario
                .When
                .Get("WeatherForecast/5");

            Scenario
                .Then
                .Response
                .ShouldBe
                .BadRequest.ForProperty("asdf");
        }
    }

    public class Start : IBeginAScenario
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