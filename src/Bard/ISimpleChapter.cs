namespace Bard
{
    /// <summary>
    /// Interface used for internal use.
    /// </summary>
    /// <typeparam name="TStoryData"></typeparam>
    public interface ISimpleChapter<out TStoryData> where TStoryData : class, new()
    {
        internal void ExecutePipeline();

        internal TStoryData GetStoryData();
    }
}