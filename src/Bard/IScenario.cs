namespace Bard
{
    public interface IScenario<out TStoryBook, TStoryData> : IScenario where TStoryBook : StoryBook<TStoryData> where TStoryData : class, new()
    {
        IGiven<TStoryBook, TStoryData> Given { get; }
    }
    
    public interface IScenario
    {
        IWhen When { get; }

        IThen Then { get; }
    }
}