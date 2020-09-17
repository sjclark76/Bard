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
        protected Bard.gRPC.IScenario Scenario{ get; set; }
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
                .UseGrpc<BankAccountService.BankAccountServiceClient>()
                .Configure(options =>
                {
                    options.Services = _host.Services;
                    options.LogMessage = output.WriteLine;
                    //options.GrpcClient = c => new BankAccountService.BankAccountServiceClient(c);
                    options.Client = _httpClient;
                });
        }

       

        public void Dispose()
        {
            _host.Dispose();
            _httpClient.Dispose();
        }
    }
    public class RetrievingABankAccount : BankingTestBase
    {
        [Fact]
        public void Foo()
        {
            Scenario.When.Grpc<BankAccountService.BankAccountServiceClient, BankAccountResponse>(client => client.GetBankAccount(new BankAccountRequest()));

            Scenario.Then.Response.ShouldBe.Ok<BankAccountResponse>();
        }

        public RetrievingABankAccount(ITestOutputHelper output) : base(output)
        {
        }
    }
}