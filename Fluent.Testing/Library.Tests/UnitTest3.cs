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

            Given = GivenFactory
                .StartsWith<Start>()
                .Build();

            Scenario = ScenarioHostConfiguration
                .TheApiUses(httpClient)
                .Log(output.WriteLine)
                .AndBeginsWithScenario<Start>()
                .Build();
        }

        public Given<Start> Given { get; set; }

        public IInternalFluentApiTester Scenario { get; set; }

        [Fact]
        public void Post_should_return_400_if_required_field_is_not_provided22()
        {
            Given
                .That
                .A_weather_forecast_has_been_created()
                .And()
                .The()
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
        public Start A_weather_forecast_has_been_created()
        {
            return this;
        }

        public Start Weather_forecast_has_been_deleted()
        {
            return this;
        }
    }
}