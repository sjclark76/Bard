using System;
using Bard.Infrastructure;
using Bard.Internal;
using Grpc.Core;

namespace Bard.gRPC
{
    /// <summary>
    ///     Scenario Context allows state to be passed between stories.
    /// </summary>
    public class GrpcScenarioContext<TGrpcClient> : ScenarioContext where TGrpcClient : ClientBase<TGrpcClient>
    {
        internal GrpcScenarioContext(IPipelineBuilder pipelineBuilder,
            IApi api, LogWriter logWriter, IServiceProvider? services,
            GrpcClientFactory grpcClientFactory) : base(
            pipelineBuilder, api, logWriter, services, grpcClientFactory.Create)
        {
        }
    }
}