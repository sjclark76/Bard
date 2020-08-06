namespace Bard
{
    /// <summary>
    ///     Implementation of a chapter to signify the story is over
    /// </summary>
    /// <typeparam name="TStoryData">The Story Data</typeparam>
    public class EndChapter<TStoryData> : Chapter<TStoryData> where TStoryData : class, new()
    {
    }
}