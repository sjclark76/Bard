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
            WeatherForecast foo = new WeatherForecast();
            Scenario
                .Given
                .That
                .A()
                .Weather_forecast_has_been_created()
                .UseResult(forecast => foo = forecast);
            
                //.Weather_forecast_has_been_updated()
                //.Weather_forecast_has_been_deleted();

            Scenario
                .When
                .Get($"WeatherForecast/{foo.Id}");

            Scenario
                .Then
                .Response
                .ShouldBe
                .Ok();
        }
    }

   

    
}