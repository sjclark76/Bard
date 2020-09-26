using System;
using System.Net;
using System.Net.Http;
using Bard.Configuration;
using Bard.Sample.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.GET
{
    // ReSharper disable once InconsistentNaming
    public class Calling_the_slow_running_end_point : IDisposable
    {
        public Calling_the_slow_running_end_point(ITestOutputHelper output)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseStartup<Startup>()
                        .UseTestServer()
                        .UseEnvironment("development"));

            _host = hostBuilder.Start();

            _httpClient = _host.GetTestClient();

            var scenario = ScenarioConfiguration
                .Configure(options =>
                {
                    options.MaxApiResponseTime = 2000;
                    options.Client = _httpClient;
                    options.LogMessage = output.WriteLine;
                    options.Services = _host.Services;
                    options.BadRequestProvider = new MyBadRequestProvider();
                });

            When = scenario.When;
            Then = scenario.Then;
        }

        public void Dispose()
        {
            _host.Dispose();
            _httpClient?.Dispose();
        }

        private readonly IHost _host;
        private readonly HttpClient? _httpClient;
        private IThen Then { get; }

        private IWhen When { get; }

        [Fact]
        public void ShouldBeOk_should_throw_exception()
        {
            When
                .Get("api/misc/slow");
            
            Assert.Throws<BardException>(() =>
            {
                Then.Response
                    .ShouldBe.Ok();
            });
        }
        
        [Fact]
        public void StatusCodeShouldBe_should_throw_exception()
        {
            When
                .Get("api/misc/slow");
            
            Assert.Throws<BardException>(() =>
            {
                Then.Response.StatusCodeShouldBe(HttpStatusCode.OK);
            });
        }
        
        [Fact]
        public void Headers_should_throw_exception()
        {
            When
                .Get("api/misc/slow");
            
            Assert.Throws<BardException>(() =>
            {
                Then.Response.Headers.Should.Include.ContentType();
            });
        }
    }
}