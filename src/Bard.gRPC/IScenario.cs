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
        Bard.gRPC.IWhen  When { get; }

        /// <summary>
        ///     Test Assertion
        /// </summary>
        IThen Then { get; }
    }
}