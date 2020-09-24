using System;
using System.Net.Http;
using Bard.gRPC;
using Bard.gRPCService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.gRPC
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

            Scenario = GrpcScenarioConfiguration
                .UseGrpc()
                .Configure(options =>
                {
                    options.Services = _host.Services;
                    options.LogMessage = output.WriteLine;
                    options.AddGrpcClient<BankAccountService.BankAccountServiceClient>("http://localhost");
                    options.Client = _httpClient;
                });
        }

        protected Bard.gRPC.IScenario Scenario { get; set; }

        public void Dispose()
        {
            _host.Dispose();
            _httpClient.Dispose();
        }
    }

    public class RetrievingABankAccount : BankingTestBase
    {
        public RetrievingABankAccount(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Foo()
        {
            Scenario
                .Grpc<BankAccountService.BankAccountServiceClient>()
                .When(client => client.GetBankAccount(new BankAccountRequest()));

            Scenario.Then.Response.ShouldBe.Ok<BankAccountResponse>();
        }
    }
}