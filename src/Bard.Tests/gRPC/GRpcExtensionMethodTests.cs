using System;
using System.Net.Http;
using Bard.gRPC;
using Bard.gRPCService;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Shouldly;
using Xunit;

namespace Bard.Tests.gRPC
{
    public class GRpcExtensionMethodTests : IDisposable
    {
        private readonly IHost _host;
        private readonly HttpClient _httpClient;

        public GRpcExtensionMethodTests()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseStartup<Startup>()
                        .UseTestServer()
                        .UseEnvironment("development"));

            _host = hostBuilder.Start();
            
            var testClient = _host
                .GetTestClient()
                .ForGrpc();
            
            _httpClient = testClient;
        }
        
        [Fact(Skip = "skipping for now")]
        public void Call_grpc_with_story_book()
        {
            GrpcChannelOptions channelOptions = new GrpcChannelOptions
            {
                HttpClient = _httpClient
            };

            var channel = GrpcChannel.ForAddress(_httpClient.BaseAddress, channelOptions);

            var client = new CreditRatingCheck.CreditRatingCheckClient(channel);
            
            var creditRequest = new CreditRequest {CustomerId = "id0201", Credit = 7000};

            var response = client.CheckCreditRequest(creditRequest);

            response.ShouldNotBeNull();
        }
        
        public void Dispose()
        {
            _host.Dispose();
            _httpClient.Dispose();
        }
    }
}