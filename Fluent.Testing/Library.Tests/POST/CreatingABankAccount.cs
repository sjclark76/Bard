using Fluent.Testing.Library.Configuration;
using Fluent.Testing.Library.Tests.Scenario;
using Fluent.Testing.Sample.Api;
using Fluent.Testing.Sample.Api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Fluent.Testing.Library.Tests.POST
{
    public class CreatingABankAccount
    {
        public CreatingABankAccount(ITestOutputHelper output)
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
                .AndBeginsWithScenario(() => new BankingScenario())
                .Build();

            Given = Scenario.Given;
            When = Scenario.When;
            Then = Scenario.Then;
        }

        private const string ApiBankaccounts = "api/bankaccounts";

        public IGiven<BankingScenario> Given { get; set; }
        public IWhen When { get; set; }
        public IThen Then { get; set; }

        public IFluentScenario<BankingScenario> Scenario { get; set; }

        [Fact]
        public void When_creating_a_bank_account()
        {
            Scenario
                .When
                .Post(ApiBankaccounts, new BankAccount
                {
                    CustomerName = "Ranulph Fiennes"
                });

            Scenario
                .Then
                .Response
                .ShouldBe
                .Created();
        }

        [Fact]
        public void When_retrieving_a_bank_account()
        {
            var customerId = 0;

            Given
                .That
                .A()
                .BankAccount_has_been_created(account => account.CustomerName = "Dougal")
                .UseResult(account => customerId = account.Id.GetValueOrDefault());

            When
                .Get($"{ApiBankaccounts}/{customerId}");

            Then
                .Response
                .ShouldBe
                .Ok<BankAccount>();
        }
    }
}