using Fluent.Testing.Library.Configuration;
using Fluent.Testing.Library.Then.Basic;
using Fluent.Testing.Sample.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Fluent.Testing.Library.Tests
{
    public class UnitTest1
    {
        public UnitTest1(ITestOutputHelper output)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseStartup<Startup>()
                        .UseTestServer()
                        .UseEnvironment("development"));

            var host = hostBuilder.Start();

            var httpClient = host.GetTestClient();

            Api = FluentApiRegistry.For(httpClient)
                .Log(output.WriteLine)
                .Build();
        }

        public IInternalFluentApiTester<IShouldBe> Api { get; set; }

        [Fact]
        public void List_should_return_ok()
        {
            Api
                .When
                .Get("WeatherForecast");

            Api
                .Then
                .Response
                .ShouldBe
                .Ok();
        }

        [Fact]
        public void Get_should_return_ok()
        {
            Api
                .When
                .Get("WeatherForecast/5");

            Api
                .Then
                .Response
                .ShouldBe
                .Ok();
        }

        [Fact]
        public void If_the_weather_forecast_does_not_exist_then_a_404_should_be_returned()
        {
            Api
                .When
                .Get("WeatherForecast/99"); // This id does not exist

            Api
                .Then
                .Response
                .ShouldBe
                .NotFound();
        }

        [Fact]
        public void Post_should_return_201()
        {
            Api
                .When
                .Post("WeatherForecast", new WeatherForecast{TemperatureC = 21});

            Api
                .Then
                .Response
                .ShouldBe
                .Created<WeatherForecast>();
        }
        
        [Fact]
        public void Post_should_return_400_if_required_field_is_not_provided()
        {
            Api
                .When
                .Post("WeatherForecast", new WeatherForecast());

            Api
                .Then
                .Response
                .ShouldBe
                .BadRequest();
        }
    }
}
