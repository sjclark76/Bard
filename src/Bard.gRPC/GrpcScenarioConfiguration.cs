using System;
using Grpc.Core;

namespace Bard.gRPC
{
    public static class GrpcScenarioConfiguration
    {
        public class GrpcStoryBookOptions<TGrpcClient, TStoryBook, TStoryData> where TStoryBook : StoryBook<TStoryData>, new() where TGrpcClient : ClientBase<TGrpcClient> where TStoryData : class, new()
        {
            public IScenario<TGrpcClient, TStoryBook, TStoryData> Configure(Action<GrpcScenarioOptions<TGrpcClient, TStoryBook>> configure) 
            {
                var options = new GrpcScenarioOptions<TGrpcClient, TStoryBook>();

                configure(options);

                return new Scenario<TGrpcClient, TStoryBook, TStoryData>(options);
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
            
            public GrpcStoryBookOptions<TGrpcClient, TStoryBook, TStoryData> WithStoryBook<TStoryBook, TStoryData>() where TStoryBook : StoryBook<TStoryData>, new() where TStoryData : class, new()
            {
                return new GrpcStoryBookOptions<TGrpcClient, TStoryBook, TStoryData>();
            }
        }
        
        public static UseGrpcOptions<TGrpcClient> UseGrpc<TGrpcClient>() where TGrpcClient : ClientBase<TGrpcClient>
        {
            return new UseGrpcOptions<TGrpcClient>();
        }
    }
}