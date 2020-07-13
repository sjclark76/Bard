using Fluent.Testing.Library.Configuration;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Library.Tests.Scenario;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.When;
using Fluent.Testing.Sample.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace Fluent.Testing.Library.Tests
{
    public abstract class BankingTestBase
    {
        protected BankingTestBase(ITestOutputHelper output)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseStartup<Startup>()
                        .UseTestServer()
                        .UseEnvironment("development"));

            var host = hostBuilder.Start();

            var httpClient = host.GetTestClient();

            var scenario = ScenarioHostConfiguration
                .TheApiUses(httpClient)
                .Log(output.WriteLine)
                .AndBeginsWithScenario(() => new BankingScenario())
                .Build();

            Given = scenario.Given;
            When = scenario.When;
            Then = scenario.Then;
        }

        public IThen Then { get; set; }

        public IWhen When { get; set; }

        public IGiven<BankingScenario> Given { get; set; }
    }
}