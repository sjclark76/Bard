using System;
using System.Collections.Generic;
using Bard.gRPC.Internal;
using Bard.Infrastructure;
using Bard.Internal;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;

namespace Bard.gRPC
{
    internal class GrpcClientFactory : IDisposable
    {
        private readonly Dictionary<Type, string> _grpcClients;
        private readonly BardHttpClient _bardHttpClient;
        private readonly LogWriter _logWriter;
        private readonly Dictionary<Type, GrpcChannel> _channels;
        
        internal GrpcClientFactory(Dictionary<Type, string> grpcClients, BardHttpClient bardHttpClient, LogWriter logWriter)
        {
            _grpcClients = grpcClients;
            _bardHttpClient = bardHttpClient;
            _logWriter = logWriter;
            _channels = new Dictionary<Type, GrpcChannel>();
        }

        internal object Create(Type gRpcClientType)
        {
            if (_grpcClients.ContainsKey(gRpcClientType) == false)
                throw new BardException($"gRPC client :{gRpcClientType.Name} base url not registered.");

            var channelOptions = new GrpcChannelOptions
            {
                HttpClient = _bardHttpClient
            };

            if (_channels.ContainsKey(gRpcClientType) == false)
            {
                _channels.Add(gRpcClientType, GrpcChannel.ForAddress(_grpcClients[gRpcClientType], channelOptions));
            }
            
            var channel = _channels[gRpcClientType];
            
            var grpcClient =
                Activator.CreateInstance(gRpcClientType, channel.Intercept(new BardClientInterceptor(_logWriter)));

            return grpcClient;
        }

        internal TGrpcClient Create<TGrpcClient>()
        {
            return (TGrpcClient) Create(typeof(TGrpcClient));
        }

        public void Dispose()
        {
            _bardHttpClient.Dispose();

            foreach (var grpcChannel in _channels)
            {
                grpcChannel.Value.Dispose();
                //AsyncHelper.RunSync(() => grpcChannel.Value.ShutdownAsync());
            }
        }
    }
}