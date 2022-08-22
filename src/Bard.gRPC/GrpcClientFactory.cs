using System;
using System.Collections.Generic;
using Bard.gRPC.Internal;
using Bard.Infrastructure;
using Bard.Internal;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;

namespace Bard.gRPC
{
    internal class GrpcClientFactory
    {
        private readonly Dictionary<Type, string> _grpcClients;
        private readonly BardHttpClient _bardHttpClient;
        private readonly LogWriter _logWriter;

        internal GrpcClientFactory(Dictionary<Type, string> grpcClients, BardHttpClient bardHttpClient, LogWriter logWriter)
        {
            _grpcClients = grpcClients;
            _bardHttpClient = bardHttpClient;
            _logWriter = logWriter;
        }

        internal object Create(Type gRpcClientType)
        {
            if (_grpcClients.ContainsKey(gRpcClientType) == false)
                throw new BardException($"gRPC client :{gRpcClientType.Name} base url not registered.");

            var channelOptions = new GrpcChannelOptions
            {
                HttpClient = _bardHttpClient
            };
            
            var channel = GrpcChannel.ForAddress(_grpcClients[gRpcClientType], channelOptions);

            var grpcClient =
                Activator.CreateInstance(gRpcClientType, channel.Intercept(new BardClientInterceptor(_logWriter)));

            if (grpcClient == null)
                throw new BardException("unable to create grpClient instance");
            return grpcClient;
        }

        internal TGrpcClient Create<TGrpcClient>()
        {
            return (TGrpcClient)Create(typeof(TGrpcClient));
        }
    }
}