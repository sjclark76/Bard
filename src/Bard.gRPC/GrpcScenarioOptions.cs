using System;
using System.Collections.Generic;
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
        internal Dictionary<Type, string> GrpcClients { get; }

        internal GrpcScenarioOptions()
        {
            GrpcClients = new Dictionary<Type, string>();
        }

        /// <summary>
        ///     The function to create a gRPC client
        /// </summary>
        public Func<CallInvoker, TGrpcClient>? GrpcClient { get; set; }
        
        public void AddGrpcClient<T>(string address) where  T : ClientBase<T>
        {
            GrpcClients.Add(typeof(T), address);
        }
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