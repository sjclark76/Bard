using System;
using Bard.Infrastructure;
using Bard.Internal;
using Grpc.Core;

namespace Bard.gRPC
{
    public class GrpcScenarioContext<TGrpcClient> : ScenarioContext where TGrpcClient : ClientBase<TGrpcClient>
    {
        internal GrpcScenarioContext(ClientBase<TGrpcClient> grpcClient, IPipelineBuilder pipelineBuilder,
            BardHttpClient bardHttpClient, IApi api, LogWriter logWriter, IServiceProvider? services) : base(
            pipelineBuilder, bardHttpClient, api, logWriter, services)
        {
            GrpcClient = grpcClient;
        }

        public ClientBase<TGrpcClient> GrpcClient { get; }
    }
}