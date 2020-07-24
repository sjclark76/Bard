namespace Bard
{
    public interface IFluentScenario
    {
        IWhen When { get; }

        IThen Then { get; }
    }

    public interface IFluentScenario<out TIBeginAScenario> : IFluentScenario where TIBeginAScenario : StoryBook
    {
        IGiven<TIBeginAScenario> Given { get; }
    }
}