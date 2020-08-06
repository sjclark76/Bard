namespace Bard
{
    /// <summary>
    ///     Begin the test story
    /// </summary>
    /// <typeparam name="TStoryBook">The StoryBook</typeparam>
    /// <typeparam name="TStoryData">The StoryData</typeparam>
    public interface IGiven<out TStoryBook, TStoryData> where TStoryBook : StoryBook<TStoryData>
        where TStoryData : class, new()
    {
        /// <summary>
        ///     Entry point to the StoryBook
        /// </summary>
        TStoryBook That { get; }
    }
}