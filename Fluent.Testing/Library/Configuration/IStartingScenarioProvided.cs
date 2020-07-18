namespace Fluent.Testing.Library.Configuration
{
    public interface IStartingScenarioProvided<T> where T : BeginAScenario, new()

    {
        IFluentScenario<T> Build();
    }
}