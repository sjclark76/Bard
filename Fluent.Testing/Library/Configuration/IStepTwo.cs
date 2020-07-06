using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.Then.Basic;

namespace Fluent.Testing.Library.Configuration
{
    public interface IStepTwo
    {
        IInternalFluentApiTester<IShouldBe> Build();
        
        IStepThree Use<T>() where T : IBadRequestResponse, new(); 
    }
}