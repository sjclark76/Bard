using System;
using Bard.Configuration;
using Grpc.Core;

namespace Bard.gRPC
{
    /// <summary>
    ///     ScenarioOptions supplies all the necessary configuration
    ///     necessary to customize and bootstrap a working
    ///     gRPC Scenario
    /// </summary>
    public class GrpcScenarioOptions<TGrpcClient> : ScenarioOptions where TGrpcClient : ClientBase<TGrpcClient>
    {
        /// <summary>
        ///     The function to create a gRPC client
        /// </summary>
        public Func<ChannelBase, TGrpcClient>? GrpcClient { get; set; }
    }

    /// <summary>
    ///     ScenarioOptions supplies all the necessary configuration
    ///     necessary to customize and bootstrap a working
    ///     gRPC Scenario with StoryBook
    /// </summary>
    public class GrpcScenarioOptions<TGrpcClient, TStoryBook> : GrpcScenarioOptions<TGrpcClient>
        where TGrpcClient : ClientBase<TGrpcClient> where TStoryBook : new()
    {
        internal GrpcScenarioOptions()
        {
            Story = new TStoryBook();
        }

        internal TStoryBook Story { get; }
    }
}