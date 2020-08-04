using System;
using Grpc.Core;

namespace Bard.gRPC
{
    public static class GrpcScenarioConfiguration
    {
        public class GrpcStoryBookOptions<TGrpcClient, TStoryBook> where TStoryBook : StoryBook, new() where TGrpcClient : ClientBase<TGrpcClient>
        {
            public GrpcFluentScenario<TGrpcClient, TStoryBook> Configure(Action<GrpcScenarioOptions<TGrpcClient, TStoryBook>> configure) 
            {
                var options = new GrpcScenarioOptions<TGrpcClient, TStoryBook>();

                configure(options);

                return new GrpcFluentScenario<TGrpcClient, TStoryBook>(options);
            }
        }
        
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
            
            public GrpcStoryBookOptions<TGrpcClient, TStoryBook> WithStoryBook<TStoryBook>() where TStoryBook : StoryBook, new()
            {
                return new GrpcStoryBookOptions<TGrpcClient, TStoryBook>();
            }
        }
        
        public static UseGrpcOptions<TGrpcClient> UseGrpc<TGrpcClient>() where TGrpcClient : ClientBase<TGrpcClient>
        {
            return new UseGrpcOptions<TGrpcClient>();
        }
        
        // public static GrpcFluentScenario<TGrpcClient> ConfigureGrpc<TGrpcClient>(Action<GrpcScenarioOptions<TGrpcClient>> configure) where TGrpcClient : ClientBase<TGrpcClient>
        // {
        //     var options = new GrpcScenarioOptions<TGrpcClient>();
        //
        //     configure(options);
        //
        //     return new GrpcFluentScenario<TGrpcClient>(options);
        // }
        
        // public static IFluentScenario Configure(Action<ScenarioOptions> configure)
        // {
        //     var options = new ScenarioOptions();
        //
        //     configure(options);
        //
        //     return new FluentScenario(options);
        // }

        // public static IFluentScenario<TStoryBook> Configure<TStoryBook>(Action<ScenarioOptions<TStoryBook>> configure)
        //     where TStoryBook : StoryBook, new()
        // {
        //     var options = new ScenarioOptions<TStoryBook>();
        //
        //     configure(options);
        //
        //     return new FluentScenario<TStoryBook>(options);
        // }
    }
}