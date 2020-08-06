namespace Bard
{
    /// <inheritdoc />
    public interface IScenario<out TStoryBook, TStoryData> : IScenario where TStoryBook : StoryBook<TStoryData>
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
    public interface IScenario
    {
        /// <summary>
        ///     Test Actor
        /// </summary>
        IWhen When { get; }

        /// <summary>
        ///     Test Assertion
        /// </summary>
        IThen Then { get; }
    }
}