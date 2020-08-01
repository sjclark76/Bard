using System.Net.Http;
using System.Threading.Tasks;
using Bard.Configuration;
using Bard.gRPCService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using static Bard.gRPCService.CreditRatingCheck;

namespace Fluent.Testing.Library.Tests.gRPC
{
    public class gRPCTests
    {
        public gRPCTests(ITestOutputHelper output)
        {
            _output = output;
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseStartup<Startup>()
                        .UseTestServer()
                        .UseEnvironment("development"));

            var host = hostBuilder.Start();
            var testClient = host.GetTestClient();
            _httpClient = testClient;
        }

        private readonly ITestOutputHelper _output;

        private readonly HttpClient _httpClient;

        [Fact]
        public  void Foo()
        {
            var scenario = ScenarioConfiguration.ConfigureGrpc<CreditRatingCheckClient>(scenarioOptions =>
            {
                scenarioOptions.LogMessage = s => _output.WriteLine(s);
                scenarioOptions.GrpcClient = c => new CreditRatingCheckClient(c);
                scenarioOptions.Client = _httpClient;
            });

            var creditRequest = new CreditRequest {CustomerId = "id0201", Credit = 7000};

            var reply = scenario.When.Grpc(client => client.CheckCreditRequest(creditRequest));

            scenario.Then.Response.ShouldBe.Ok();
            
            //reply.IsAccepted.ShouldBe(true);
        }
    }
}