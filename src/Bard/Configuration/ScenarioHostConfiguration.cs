using System;
using Bard.Internal;
using Grpc.Core;

namespace Bard.Configuration
{
    public class ScenarioConfiguration
    {
        public static GrpcFluentScenario<TGrpcClient> ConfigureGrpc<TGrpcClient>(
            Action<GrpcScenarioOptions<TGrpcClient>> configure) where TGrpcClient : ClientBase<TGrpcClient>
        {
            var options = new GrpcScenarioOptions<TGrpcClient>();

            configure(options);

            return new GrpcFluentScenario<TGrpcClient>(options);
        }

        public static IFluentScenario Configure(Action<ScenarioOptions> configure)
        {
            var options = new ScenarioOptions();

            configure(options);

            return new FluentScenario(options);
        }

        public static IFluentScenario<TStoryBook> Configure<TStoryBook>(Action<ScenarioOptions<TStoryBook>> configure)
            where TStoryBook : StoryBook, new()
        {
            var options = new ScenarioOptions<TStoryBook>();

            configure(options);

            return new FluentScenario<TStoryBook>(options);
        }
    }
}