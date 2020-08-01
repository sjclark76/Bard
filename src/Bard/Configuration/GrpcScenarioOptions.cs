using System;
using Grpc.Core;

namespace Bard.Configuration
{
    public class GrpcScenarioOptions<TGrpcClient> : ScenarioOptions where TGrpcClient : ClientBase<TGrpcClient>
    {
        public Func<ChannelBase, TGrpcClient>? GrpcClient { get; set; }
    }
}