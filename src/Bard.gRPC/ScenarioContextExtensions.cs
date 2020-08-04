using System;
using Bard.Internal.Exception;
using Grpc.Core;
using Grpc.Net.Client;

namespace Bard.gRPC
{
    public static class ScenarioContextExtensions
    {
        public static void Grpc<TGrpcClient>(this ScenarioContext context, Func<TGrpcClient, object?> execute) where TGrpcClient : ClientBase<TGrpcClient>
        {
            GrpcChannelOptions channelOptions = new GrpcChannelOptions
            {
                HttpClient = context.BardHttpClient
            };

            var channel = GrpcChannel.ForAddress(context.BardHttpClient.BaseAddress, channelOptions);

            var gRpcClient = Activator.CreateInstance(typeof(TGrpcClient), channel) as TGrpcClient;

            if (gRpcClient == null)
                throw new BardException($"Error creating instance of gRPC client {typeof(TGrpcClient).Name}");
            
            execute(gRpcClient);
        }  
    }
}