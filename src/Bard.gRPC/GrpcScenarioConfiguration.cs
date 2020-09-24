using System;

namespace Bard.gRPC
{
    /// <summary>
    ///     Configuration helper to configure your gRPC Scenario
    /// </summary>
    public static class GrpcScenarioConfiguration
    {
        /// <summary>
        ///     Configure the Scenario to use the supplied gRPC Client
        /// </summary>
        /// <returns></returns>
        public static UseGrpcOptions UseGrpc() 
        {
            return new UseGrpcOptions();
        }

        /// <summary>
        ///     GrpcStoryBookOptions supplies all the necessary configuration
        ///     necessary to customize and bootstrap a working
        ///     gRPC Scenario with StoryBook
        /// </summary>
        /// <typeparam name="TStoryBook">The Story Book</typeparam>
        /// <typeparam name="TStoryData">The Story Data</typeparam>
        public class GrpcStoryBookOptions<TStoryBook, TStoryData>
            where TStoryBook : StoryBook<TStoryData>, new()
            where TStoryData : class, new()
        {
            /// <summary>
            ///     Supply the required configuration values for the scenario
            /// </summary>
            /// <param name="configure">The action that configures the scenario</param>
            /// <returns>The created scenario</returns>
            public IScenario<TStoryBook, TStoryData> Configure(
                Action<GrpcScenarioOptions<TStoryBook>> configure)
            {
                var options = new GrpcScenarioOptions<TStoryBook>();

                configure(options);

                return new Scenario<TStoryBook, TStoryData>(options);
            }
        }

        /// <summary>
        ///     Story Book gRPC configuration class
        /// </summary>
        public class UseGrpcOptions
        {
            /// <summary>
            /// Configure the gRPC Scenario
            /// </summary>
            /// <param name="configure"></param>
            /// <returns></returns>
            public IScenario Configure(Action<GrpcScenarioOptions> configure) 
            {
                var options = new GrpcScenarioOptions();
            
                configure(options);
            
                return new Scenario(options);
            }

            /// <summary>
            ///     Indicates to the configuration builder which StoryBook to use for the Scenario
            /// </summary>
            /// <typeparam name="TStoryBook">The Story Book</typeparam>
            /// <typeparam name="TStoryData">The Story Data</typeparam>
            /// <returns></returns>
            public GrpcStoryBookOptions<TStoryBook, TStoryData> WithStoryBook<TStoryBook, TStoryData>()
                where TStoryBook : StoryBook<TStoryData>, new()
                where TStoryData : class, new()
            {
                return new GrpcStoryBookOptions<TStoryBook, TStoryData>();
            }
        }
    }
}