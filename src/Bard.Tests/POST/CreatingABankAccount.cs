using System;
using System.Net.Http;
using Bard.Configuration;
using Bard.Sample.Api;
using Bard.Sample.Api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.POST
{
    public class CreatingABankAccount: IDisposable
    {
        private IHost _host;
        private HttpClient _httpClient;

        public CreatingABankAccount(ITestOutputHelper output)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseStartup<Startup>()
                        .UseTestServer()
                        .UseEnvironment("development"));

            _host = hostBuilder.Start();

            _httpClient = _host.GetTestClient();

            Scenario = ScenarioConfiguration
                .Configure(options =>
                {
                    options.Client = _httpClient;
                    options.LogMessage = output.WriteLine;
                    options.Services = _host.Services;
                });

            When = Scenario.When;
            Then = Scenario.Then;
        }

        private const string ApiBankaccounts = "api/bankaccounts";

        public IWhen When { get; }
        public IThen Then { get; }

        public IScenario Scenario { get; }

        [Fact]
        public void When_creating_a_bank_account()
        {
            When
                .Post(ApiBankaccounts, new BankAccount
                {
                    CustomerName = "Ranulph Fiennes"
                });

            Then
                .Response
                .ShouldBe
                .Created();
        }

        public void Dispose()
        {
            _host.Dispose();
            _httpClient.Dispose();
        }
    }
}