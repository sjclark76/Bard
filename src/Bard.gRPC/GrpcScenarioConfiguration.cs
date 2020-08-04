using System;
using Grpc.Core;

namespace Bard.gRPC
{
    public static class GrpcScenarioConfiguration
    {
        public class GrpcStoryBookOptions<TGrpcClient, TStoryBook> where TStoryBook : StoryBook, new() where TGrpcClient : ClientBase<TGrpcClient>
        {
            public IScenario<TGrpcClient, TStoryBook> Configure(Action<GrpcScenarioOptions<TGrpcClient, TStoryBook>> configure) 
            {
                var options = new GrpcScenarioOptions<TGrpcClient, TStoryBook>();

                configure(options);

                return new Scenario<TGrpcClient, TStoryBook>(options);
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
    }
}