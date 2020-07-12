using Fluent.Testing.Library.Configuration;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Library.Tests.Scenario;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.When;
using Fluent.Testing.Sample.Api;
using Fluent.Testing.Sample.Api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Fluent.Testing.Library.Tests.POST
{
    public class CreatingABankAccount
    {
        private const string ApiBankaccounts = "api/bankaccounts";

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
            int customerId = 0;
            
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
        
        [Fact]
        public void When_retrievinddddg_a_bank_account()
        {
            int customerId = 0;
            
            Given
                .That
                .A()
                .BankAccount_has_been_created(account => account.CustomerName = "Dougal")
                .And()
                .Deposit_has_been_made()
                .Deposit_has_been_made()
                .Deposit_has_been_made()
                .UseResult(account => customerId = account.Id.GetValueOrDefault());

            When
                .Get($"{ApiBankaccounts}/{customerId}");

            Then
                .Response
                .ShouldBe
                .Ok<BankAccount>()
                .Balance
                .ShouldBe(300);
        }
    }
}