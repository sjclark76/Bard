namespace Fluent.Testing.Library.Configuration
{
    public interface IStartingScenarioProvided<T> where T : StoryBook, new()

    {
        IFluentScenario<T> Build();
    }
}