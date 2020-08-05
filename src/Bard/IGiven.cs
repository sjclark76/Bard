namespace Bard
{
    public interface IGiven<out TStoryBook, TStoryData> where TStoryBook : StoryBook<TStoryData> where TStoryData : class, new()
    {
        TStoryBook That { get; }
    }
}