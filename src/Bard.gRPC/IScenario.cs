using Grpc.Core;

namespace Bard.gRPC
{
    public interface IScenario<TGrpcClient, out TStoryBook, TStoryData> where TGrpcClient : ClientBase<TGrpcClient> where TStoryBook : StoryBook<TStoryData>, new() where TStoryData : class, new()
    {
        IGiven<TStoryBook, TStoryData> Given { get; }
        IGrpcWhen<TGrpcClient> When { get; set; }
        IThen Then { get; }
    }
}