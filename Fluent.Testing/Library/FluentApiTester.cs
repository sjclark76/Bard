using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.Then.v1;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library
{
    internal class FluentApiTester<TShouldBe> : IInternalFluentApiTester<TShouldBe> where TShouldBe : IShouldBeBase, new()
    {
        public FluentApiTester(IWhen<TShouldBe> when, IThen<TShouldBe> then)
        {
            When = when;
            Then = then;
        }
        
        public IWhen<TShouldBe> When { get; }
        
        public IThen<TShouldBe> Then { get; }
    }
}