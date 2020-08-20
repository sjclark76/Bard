using System;
using System.Net.Http;
using Bard.gRPC;
using Bard.gRPCService;
using Bard.Tests.Scenario;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.gRPC
{
    public class GRpcTests : IDisposable
    {
        public GRpcTests(ITestOutputHelper output)
        {
            _output = output;
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseStartup<Startup>()
                        .UseTestServer()
                        .UseEnvironment("development"));

            _host = hostBuilder.Start();
            var testClient = _host.GetTestClient();
            _httpClient = testClient;
        }

        private readonly ITestOutputHelper _output;

        private readonly HttpClient _httpClient;
        private readonly IHost _host;

        [Fact]
        public void Call_grpc_with_story_book()
        {
            var scenario = GrpcScenarioConfiguration
                .UseGrpc<CreditRatingCheck.CreditRatingCheckClient>()
                .WithStoryBook<CreditCheckStoryBook, CreditCheckData>()
                .Configure(options =>
                {
                    options.Services = _host.Services;
                    options.LogMessage = s => _output.WriteLine(s);
                    options.GrpcClient = c => new CreditRatingCheck.CreditRatingCheckClient(c);
                    options.Client = _httpClient;
                });

            scenario.Given
                .Nothing_much_happens();

            var creditRequest = new CreditRequest {CustomerId = "id0201", Credit = 7000};

            scenario.When.Grpc(client => client.CheckCreditRequest(creditRequest));

            scenario.Then.Response.ShouldBe.Ok();
        }

        [Fact]
        public void Call_grpc_without_story_book()
        {
            var scenario = GrpcScenarioConfiguration
                .UseGrpc<CreditRatingCheck.CreditRatingCheckClient>()
                .Configure(options =>
                {
                    options.Services = _host.Services;
                    options.LogMessage = s => _output.WriteLine(s);
                    options.GrpcClient = c => new CreditRatingCheck.CreditRatingCheckClient(c);
                    options.Client = _httpClient;
                });

            var creditRequest = new CreditRequest {CustomerId = "id0201", Credit = 7000};

            scenario.When.Grpc(client => client.CheckCreditRequest(creditRequest));

            scenario.Then.Response.ShouldBe.Ok();
        }

        public void Dispose()
        {
            _host.Dispose();
            _httpClient.Dispose();
        }
    }
}