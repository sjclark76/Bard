using Fluent.Testing.Library.Given;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.Then.Basic;

namespace Fluent.Testing.Library.Configuration
{
    public interface ILoggerProvided
    {
        IInternalFluentApiTester<IShouldBe> Build();
        
        ICustomErrorProviderSupplied Use<T>() where T : IBadRequestResponse, new();

        IStartingScenarioProvided AndBeginsWithScenario<TScenario>() where TScenario : IBeginAScenario, new();
    }
}