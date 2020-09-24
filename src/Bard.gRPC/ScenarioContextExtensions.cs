using Bard.Internal.Exception;
using Grpc.Core;

namespace Bard.gRPC
{
    /// <summary>
    ///     Extension methods that add functionality to the base ScenarioContext
    /// </summary>
    public static class ScenarioContextExtensions
    {
        /// <summary>
        ///     Perform an action against the configured gRPC Client
        /// </summary>
        /// <param name="context">The scenario context</param>
        /// <typeparam name="TGrpcClient">The gRPC Client Type</typeparam>
        /// <exception cref="BardConfigurationException">If the gRPC Client has not been configured</exception>
        public static TGrpcClient Grpc<TGrpcClient>(this ScenarioContext context)
            where TGrpcClient : ClientBase<TGrpcClient>
        {
            if (context.CreateGrpcClient == null)
                throw new BardConfigurationException($"context {nameof(context.CreateGrpcClient)} not set.");

            
            var gRpcClient = context.CreateGrpcClient(typeof(TGrpcClient));

            return (TGrpcClient) gRpcClient;
        }
    }
}