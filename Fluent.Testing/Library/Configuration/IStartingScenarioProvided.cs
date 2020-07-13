using Fluent.Testing.Library.Given;

namespace Fluent.Testing.Library.Configuration
{
    public interface IStartingScenarioProvided<T> where T : BeginAScenario, new()

    {
        IFluentScenario<T> Build();
    }
}