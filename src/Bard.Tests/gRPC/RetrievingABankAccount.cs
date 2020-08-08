using Bard.gRPC;
using Bard.gRPCService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Fluent.Testing.Library.Tests.gRPC
{
    public abstract class BankingTestBase
    {
        protected readonly IScenario<BankAccountService.BankAccountServiceClient> Scenario;

        protected BankingTestBase(ITestOutputHelper output)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseStartup<Startup>()
                        .UseTestServer()
                        .UseEnvironment("development"));

            var host = hostBuilder.Start();
            var testClient = host.GetTestClient();

            Scenario = GrpcScenarioConfiguration
                .UseGrpc<BankAccountService.BankAccountServiceClient>()
                .Configure(options =>
                {
                    options.Services = host.Services;
                    options.LogMessage = output.WriteLine;
                    options.GrpcClient = c => new BankAccountService.BankAccountServiceClient(c);
                    options.Client = testClient;
                });
        }
    }
    public class RetrievingABankAccount : BankingTestBase
    {
        [Fact]
        public void Foo()
        {
            base.Scenario.When.Grpc(client => client.GetBankAccount(new BankAccountRequest()));

            Scenario.Then.Response.ShouldBe.Ok<BankAccountResponse>();
        }

        public RetrievingABankAccount(ITestOutputHelper output) : base(output)
        {
        }
    }
}