using System;
using Bard.Infrastructure;
using Grpc.Core;

namespace Bard.gRPC
{
    public class GrpcScenarioContext<TGrpcClient> : ScenarioContext where TGrpcClient : ClientBase<TGrpcClient>
    {
        public ClientBase<TGrpcClient> GrpcClient { get; }

        internal GrpcScenarioContext(ClientBase<TGrpcClient> grpcClient, IPipelineBuilder pipelineBuilder, IApi api, LogWriter logWriter, IServiceProvider? services) : base(pipelineBuilder, api, logWriter, services)
        {
            GrpcClient = grpcClient;
        }
    }
}