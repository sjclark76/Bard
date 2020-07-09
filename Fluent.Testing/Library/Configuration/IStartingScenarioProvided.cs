using Fluent.Testing.Library.Given;

namespace Fluent.Testing.Library.Configuration
{
    public interface IStartingScenarioProvided<T> where T : IBeginAScenario, new()

    {
        IFluentScenario<T> Build();
    }
}