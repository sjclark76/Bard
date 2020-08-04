namespace Bard
{
    public interface ISimpleChapter<in TStoryData> where TStoryData : class, new()
    {
        internal object? ExecutePipeline();

        internal void SetStoryInput(TStoryData? input);
    }
}