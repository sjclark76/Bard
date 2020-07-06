using Fluent.Testing.Library.Then;

namespace Fluent.Testing.Library.Configuration
{
    public interface IStepTwo
    {
        IInternalFluentApiTester<Then.v1.IShouldBe> Build();
        
        IStepThree Use<T>() where T : IBadRequestResponse, new(); 
    }
}