using System;
using Bard.Internal.Exception;
using Grpc.Core;

namespace Bard.gRPC
{
    public static class ScenarioContextExtensions
    {
        public static void Grpc<TGrpcClient>(this ScenarioContext context, Func<TGrpcClient, object?> execute)
            where TGrpcClient : ClientBase<TGrpcClient>
        {
            if (context.CreateGrpcClient == null)
                throw new BardException($"context {nameof(context.CreateGrpcClient)} not set.");
            
            var gRpcClient = (TGrpcClient) context.CreateGrpcClient();

            execute(gRpcClient);
        }
    }
}