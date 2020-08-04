using System;
using Bard.Infrastructure;
using Grpc.Core;

namespace Bard.gRPC
{
    public class GrpcScenarioContext<TGrpcClient> : ScenarioContext where TGrpcClient : ClientBase<TGrpcClient>
    {
        internal GrpcScenarioContext(IPipelineBuilder pipelineBuilder,
            IApi api, LogWriter logWriter, IServiceProvider? services,
            Func<TGrpcClient> createGrpcClient) : base(
            pipelineBuilder, api, logWriter, services, createGrpcClient)
        {
        }
    }
}