using System;
using Grpc.Core;

namespace Bard.gRPC
{
    /// <summary>
    /// Test Actor
    /// </summary>
    /// <typeparam name="TGrpcClient"></typeparam>
    public interface IWhen<out TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
        /// <summary>
        /// Call the gRPC client during the test Act
        /// </summary>
        /// <param name="grpcCall"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        TResponse Grpc<TResponse>(Func<TGrpcClient, TResponse> grpcCall);
    }
}