using Grpc.Core;

namespace Bard.gRPC
{
    public interface IScenario<TGrpcClient, out TStoryBook> where TGrpcClient : ClientBase<TGrpcClient> where TStoryBook : StoryBook, new()
    {
        IGiven<TStoryBook> Given { get; }
        IGrpcWhen<TGrpcClient> When { get; set; }
        IThen Then { get; }
    }
}