using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bard.gRPCService;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Shouldly;
using Xunit;
using Startup = Bard.gRPCService.Startup;

namespace Fluent.Testing.Library.Tests.gRPC
{
   
    public class ResponseVersionHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            response.Version = request.Version;

            return response;
        }
    }
    
    public class gRPCTests
    {
        private HttpClient _httpClient;

        public gRPCTests()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseStartup<Startup>()
                        .UseTestServer()
                        .UseEnvironment("development"));

            var host = hostBuilder.Start();
            var server = host.GetTestServer();
            // Need to set the response version to 2.0.
            // Required because of this TestServer issue - https://github.com/aspnet/AspNetCore/issues/16940
            var responseVersionHandler = new ResponseVersionHandler();
            responseVersionHandler.InnerHandler = server.CreateHandler();

            var client = new HttpClient(responseVersionHandler);
            client.BaseAddress = new Uri("http://localhost");

            _httpClient = client; //host.GetTestClient();
        }

        [Fact]
        public async Task Foo()
        {
           // Bard.Configuration.ScenarioConfiguration.Configure()
            GrpcChannelOptions options = new GrpcChannelOptions()
            {
                HttpClient = _httpClient
            };
            var channel = GrpcChannel.ForAddress(_httpClient.BaseAddress, options);
            
            var client = new CreditRatingCheck.CreditRatingCheckClient(channel);
            var creditRequest = new CreditRequest { CustomerId = "id0201", Credit = 7000};
            var reply = await client.CheckCreditRequestAsync(creditRequest);
            
            reply.IsAccepted.ShouldBe(true);
            
        }
    }
}