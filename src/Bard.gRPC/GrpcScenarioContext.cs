using System;
using Bard.Infrastructure;
using Bard.Internal;
using Grpc.Core;

namespace Bard.gRPC
{
    public class GrpcScenarioContext<TGrpcClient> : ScenarioContext where TGrpcClient : ClientBase<TGrpcClient>
    {
        internal GrpcScenarioContext(IPipelineBuilder pipelineBuilder,
            BardHttpClient bardHttpClient, IApi api, LogWriter logWriter, IServiceProvider? services,
            Func<TGrpcClient> createGrpcClient) : base(
            pipelineBuilder, bardHttpClient, api, logWriter, services, createGrpcClient)
        {
            GrpcClient = createGrpcClient();
        }

        public ClientBase<TGrpcClient> GrpcClient { get; }
    }
}