using Grpc.Core;

namespace Bard.gRPC
{
    /// <summary>
    ///     Full Scenario with Given When and Then functionality
    /// </summary>
    public interface IScenario<out TStoryBook, TStoryData>  : IScenario
        where TStoryBook : StoryBook<TStoryData>, new()
        where TStoryData : class, new()
    {
        /// <summary>
        ///     Test Arrangement
        /// </summary>
        TStoryBook Given { get; }
    }
    
    /// <summary>
    ///     Basic Scenario with When and Then functionality
    /// </summary>
    public interface IScenario
    {
       /// <summary>
        ///     Test Act
        /// </summary>
        IWhen When { get; }

        /// <summary>
        /// Call the gRPC client during the test Act
        /// </summary>
        /// <typeparam name="TGrpcClient"></typeparam>
        /// <returns></returns>
       IGrpc<TGrpcClient> Grpc<TGrpcClient>() where TGrpcClient : ClientBase<TGrpcClient>;


       /// <summary>
        ///     Test Assertion
        /// </summary>
        IThen Then { get; }
    }
}