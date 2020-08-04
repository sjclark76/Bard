using System.Net.Http;
using Bard;
using Bard.gRPC;
using Bard.gRPCService;
using Fluent.Testing.Library.Tests.Scenario;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

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

            _host = hostBuilder.Start();
            var testClient = _host.GetTestClient();
            _httpClient = testClient;
        }

        private readonly ITestOutputHelper _output;

        private readonly HttpClient _httpClient;
        private readonly IHost _host;

        [Fact]
        public void Foo()
        {
            var scenario = GrpcScenarioConfiguration
                 .UseGrpc<CreditRatingCheck.CreditRatingCheckClient>()
                 .WithStoryBook<CreditCheckStoryBook>()
                 .Configure(options =>
                 {
                     options.Services = _host.Services;
                     options.LogMessage = s => _output.WriteLine(s);
                     options.GrpcClient = c => new CreditRatingCheck.CreditRatingCheckClient(c);
                     options.Client = _httpClient;
             });
            
             scenario.Given.That
                 .Nothing_much_happens()
                 .GetResult(out object foo);
            
             var creditRequest = new CreditRequest {CustomerId = "id0201", Credit = 7000};
            
             scenario.When.Grpc(client => client.CheckCreditRequest(creditRequest));
            
             scenario.Then.Response.ShouldBe.Ok();
        }
    }
}