using Grpc.Core;

namespace Bard.gRPC
{
    /// <summary>
    ///     Full Scenario with Given When and Then functionality
    /// </summary>
    public interface IScenario<out TGrpcClient, out TStoryBook, TStoryData> 
        : IScenario<TGrpcClient>
        where TGrpcClient : ClientBase<TGrpcClient>
        where TStoryBook : StoryBook<TStoryData>, new()
        where TStoryData : class, new()
    {
        /// <summary>
        ///     Test Arrangement
        /// </summary>
        IGiven<TStoryBook, TStoryData> Given { get; }
    }
    
    /// <summary>
    ///     Basic Scenario with When and Then functionality
    /// </summary>
    public interface IScenario<out TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
       /// <summary>
        ///     Test Act
        /// </summary>
        IWhen<TGrpcClient> When { get; }

        /// <summary>
        ///     Test Assertion
        /// </summary>
        IThen Then { get; }
    }
}