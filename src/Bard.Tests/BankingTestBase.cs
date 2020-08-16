using System;
using System.Net.Http;
using Bard.Configuration;
using Bard.Sample.Api;
using Bard.Tests.Scenario;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace Bard.Tests
{
    public abstract class BankingTestBase : IDisposable
    {
        private readonly IHost _host;
        private readonly HttpClient _httpClient;

        protected BankingTestBase(ITestOutputHelper output)
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
                .WithStoryBook<BankingStory, BankingStoryData>()
                .Configure(options =>
                {
                    options.Client = _httpClient;
                    options.LogMessage = output.WriteLine;
                    options.Services = _host.Services;
                    options.BadRequestProvider = new MyBadRequestProvider();
                });

            Given = scenario.Given;
            When = scenario.When;
            Then = scenario.Then;
        }

        protected BankingStory Given { get; set; }

        protected IThen Then { get; }

        protected IWhen When { get; }

        public void Dispose()
        {
            _host.Dispose();
            _httpClient.Dispose();
        }
    }
}