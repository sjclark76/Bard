using Fluent.Testing.Library.Configuration;
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

        public IFluentTester Api { get; set; }

        [Fact]
        public void Test1()
        {
            Api
                .When
                .Get("WeatherForecast");

            Api
                .Then
                .TheResponse
                .ShouldBe
                .Ok();
        }
    }
}