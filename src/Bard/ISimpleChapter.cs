namespace Bard
{
    public interface ISimpleChapter<TStoryData> where TStoryData : class, new()
    {
        internal object? ExecutePipeline();

        internal void SetStoryData(TStoryData? input);
        
        internal TStoryData GetStoryData();
    }
}