using System;
using System.Net.Http;
using Bard.Configuration;
using Bard.gRPCService;
using Fluent.Testing.Library.Tests.Scenario;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;
using static Bard.gRPCService.CreditRatingCheck;

namespace Fluent.Testing.Library.Tests.gRPC
{
    public class GRpcTests
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

            var host = hostBuilder.Start();
            _services = host.Services;
            var testClient = host.GetTestClient();
            _httpClient = testClient;
        }

        private readonly ITestOutputHelper _output;

        private readonly HttpClient _httpClient;
        private IServiceProvider _services;

        [Fact]
        public void Foo()
        {
            var scenario = ScenarioConfiguration
                .UseGrpc<CreditRatingCheckClient>()
                .WithStoryBook<CreditCheckStoryBook>()
                .Configure(options =>
                {
                    options.Services = _services;
                    options.LogMessage = s => _output.WriteLine(s);
                    options.GrpcClient = c => new CreditRatingCheckClient(c);
                    options.Client = _httpClient;
            });

            scenario.Given.That
                .Nothing_much_happens();
            
            var creditRequest = new CreditRequest {CustomerId = "id0201", Credit = 7000};

            scenario.When.Grpc(client => client.CheckCreditRequest(creditRequest));

            scenario.Then.Response.ShouldBe.Ok();

            //reply.IsAccepted.ShouldBe(true);
        }
    }
}