namespace Bard
{
    public interface IScenario<out TIBeginAScenario> : IScenario where TIBeginAScenario : StoryBook
    {
        IGiven<TIBeginAScenario> Given { get; }
    }
    
    public interface IScenario
    {
        IWhen When { get; }

        IThen Then { get; }
    }
}