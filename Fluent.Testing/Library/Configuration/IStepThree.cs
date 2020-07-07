using Fluent.Testing.Library.Then.Advanced;

namespace Fluent.Testing.Library.Configuration
{
    public interface ICustomErrorProviderSupplied
    {
        IInternalFluentApiTester<IShouldBe> Build();
    }
    
    public interface IStartingScenarioProvided
    {
        IInternalFluentApiTester<IShouldBe> Build();
    }
}