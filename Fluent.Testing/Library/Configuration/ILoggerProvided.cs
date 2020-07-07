using Fluent.Testing.Library.Given;
using Fluent.Testing.Library.Then;

namespace Fluent.Testing.Library.Configuration
{
    public interface ILoggerProvided
    {
        IInternalFluentApiTester Build();
        
        ICustomErrorProviderSupplied Use<T>() where T : IBadRequestProvider, new();

        IStartingScenarioProvided AndBeginsWithScenario<TScenario>() where TScenario : IBeginAScenario, new();
    }
}