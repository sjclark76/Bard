using System;
using Grpc.Core;

namespace Bard.Configuration
{
    public class GrpcScenarioOptions<TGrpcClient> : ScenarioOptions where TGrpcClient : ClientBase<TGrpcClient>
    {
        public Func<ChannelBase, TGrpcClient>? GrpcClient { get; set; }
    }

    public class GrpcScenarioOptions<TGrpcClient, TStoryBook> : GrpcScenarioOptions<TGrpcClient>
        where TGrpcClient : ClientBase<TGrpcClient> where TStoryBook : new()
    {
        public GrpcScenarioOptions()
        {
            Story = new TStoryBook();
        }

        internal TStoryBook Story { get; }
    }
}