using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library
{
    public interface IInternalFluentApiTester<out TShouldBe> where TShouldBe : IShouldBeBase
    {
        IWhen<TShouldBe> When { get; }
        
        IThen<TShouldBe> Then { get; }
    }
}