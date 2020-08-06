using System;
using Grpc.Core;

namespace Bard.gRPC
{
    /// <summary>
    ///     Configuration helper to configure your gRPC Scenario
    /// </summary>
    public static class GrpcScenarioConfiguration
    {
        /// <summary>
        /// Configure the Scenario to use the supplied gRPC Client
        /// </summary>
        /// <typeparam name="TGrpcClient"></typeparam>
        /// <returns></returns>
        public static UseGrpcOptions<TGrpcClient> UseGrpc<TGrpcClient>() where TGrpcClient : ClientBase<TGrpcClient>
        {
            return new UseGrpcOptions<TGrpcClient>();
        }

        /// <summary>
        ///     GrpcStoryBookOptions supplies all the necessary configuration
        ///     necessary to customize and bootstrap a working
        ///     gRPC Scenario with StoryBook
        /// </summary>
        /// <typeparam name="TGrpcClient">The gRPC Client</typeparam>
        /// <typeparam name="TStoryBook">The Story Book</typeparam>
        /// <typeparam name="TStoryData">The Story Data</typeparam>
        public class GrpcStoryBookOptions<TGrpcClient, TStoryBook, TStoryData>
            where TStoryBook : StoryBook<TStoryData>, new()
            where TGrpcClient : ClientBase<TGrpcClient>
            where TStoryData : class, new()
        {
            /// <summary>
            /// Supply the required configuration values for the scenario
            /// </summary>
            /// <param name="configure">The action that configures the scenario</param>
            /// <returns>The created scenario</returns>
            public IScenario<TGrpcClient, TStoryBook, TStoryData> Configure(
                Action<GrpcScenarioOptions<TGrpcClient, TStoryBook>> configure)
            {
                var options = new GrpcScenarioOptions<TGrpcClient, TStoryBook>();

                configure(options);

                return new Scenario<TGrpcClient, TStoryBook, TStoryData>(options);
            }
        }

        /// <summary>
        /// Story Book gRPC configuration class
        /// </summary>
        /// <typeparam name="TGrpcClient"></typeparam>
        public class UseGrpcOptions<TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
        {
            // public GrpcFluentScenario<TGrpcClient> Configure(Action<GrpcScenarioOptions<TGrpcClient>> configure) 
            // {
            //     var options = new GrpcScenarioOptions<TGrpcClient>();
            //
            //     configure(options);
            //
            //     return new GrpcFluentScenario<TGrpcClient>(options);
            // }

            /// <summary>
            /// Indicates to the configuration builder which StoryBook to use for the Scenario
            /// </summary>
            /// <typeparam name="TStoryBook">The Story Book</typeparam>
            /// <typeparam name="TStoryData">The Story Data</typeparam>
            /// <returns></returns>
            public GrpcStoryBookOptions<TGrpcClient, TStoryBook, TStoryData> WithStoryBook<TStoryBook, TStoryData>()
                where TStoryBook : StoryBook<TStoryData>, new() where TStoryData : class, new()
            {
                return new GrpcStoryBookOptions<TGrpcClient, TStoryBook, TStoryData>();
            }
        }
    }
}