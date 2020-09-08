namespace Bard
{
    /// <summary>
    ///     Abstract StoryBook class to inherit from when creating a StoryBook
    /// </summary>
    /// <typeparam name="TStoryData">The Story Data that will flow through the stories.</typeparam>
    public abstract class StoryBook<TStoryData> : Chapter<TStoryData> where TStoryData : class, new()
    {
    }
}