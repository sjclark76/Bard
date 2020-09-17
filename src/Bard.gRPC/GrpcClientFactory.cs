using System;
using Bard.gRPC.Internal;
using Bard.Infrastructure;
using Bard.Internal;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;

namespace Bard.gRPC
{
    internal class GrpcClientFactory
    {
        private readonly BardHttpClient _httpClient;
        private readonly LogWriter _logWriter;

        internal GrpcClientFactory(BardHttpClient httpClient, LogWriter logWriter)
        {
            _httpClient = httpClient;
            _logWriter = logWriter;
        }

        internal object Create(Type gRpcClientType)
        {
            GrpcChannelOptions channelOptions = new GrpcChannelOptions
            {
                HttpClient = _httpClient
            };
        
            var channel = GrpcChannel.ForAddress(_httpClient.BaseAddress, channelOptions);

            var grpcClient = Activator.CreateInstance(gRpcClientType, channel.Intercept(new BardClientInterceptor(_logWriter)));
        
            return grpcClient;
        }
        
        internal TGrpcClient Create<TGrpcClient>()
        {
            GrpcChannelOptions channelOptions = new GrpcChannelOptions
            {
                HttpClient = _httpClient
            };
        
            var channel = GrpcChannel.ForAddress(_httpClient.BaseAddress, channelOptions);
        
            var grpcClient = (TGrpcClient) Activator.CreateInstance(typeof(TGrpcClient),
                channel.Intercept(new BardClientInterceptor(_logWriter)));
        
            return grpcClient;
        }
    }
}