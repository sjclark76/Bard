using System;
using Grpc.Core;

namespace Bard.gRPC
{
    /// <summary>
    ///     Test Actor
    /// </summary>
    /// <typeparam name="TGrpcClient"></typeparam>
    public interface IWhen
    {
        /// <summary>
        ///     Call the gRPC client during the test Act
        /// </summary>
        /// <param name="grpcCall"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        TResponse Grpc<TGrpcClient, TResponse>(Func<TGrpcClient, TResponse> grpcCall)
            where TGrpcClient : ClientBase<TGrpcClient>;
    }
}