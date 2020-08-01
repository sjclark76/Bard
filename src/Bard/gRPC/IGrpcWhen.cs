using System;
using Grpc.Core;

namespace Bard.gRPC
{
    public interface IGrpcWhen<out TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
        TGrpcClient GrpcClient { get; }
        TResponse Grpc<TResponse>(Func<TGrpcClient, TResponse> grpcCall);
    }
}