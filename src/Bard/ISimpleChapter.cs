namespace Bard
{
    public interface ISimpleChapter<out TStoryData> where TStoryData : class, new()
    {
        internal void ExecutePipeline();

        internal TStoryData GetStoryData();
    }
}