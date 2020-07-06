using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library
{
    public class FluentApiTester : IFluentApiTester
    {
        public FluentApiTester(IWhen when, IThen then)
        {
            When = when;
            Then = then;
        }
        
        public IWhen When { get; }
        
        public IThen Then { get; }
    }
}