using System;
using Grpc.Core;

namespace Bard.gRPC
{
    public interface IGrpc<out TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
        /// <summary>
        ///     Call the gRPC client during the test Act
        /// </summary>
        /// <param name="grpcCall"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        TResponse When<TResponse>(Func<TGrpcClient, TResponse> grpcCall);
    }
}