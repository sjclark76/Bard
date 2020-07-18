using System;
using Fluent.Testing.Library.Given;

namespace Fluent.Testing.Library.Internal.Given
{
    internal class BeginGiven<TRequest> : IBeginGiven<TRequest>
    {
        private readonly ScenarioContext _context;
        private readonly Func<TRequest> _createRequest;

        public BeginGiven(ScenarioContext context, Func<TRequest> createRequest)
        {
            _context = context;
            _createRequest = createRequest;
        }
        
        public IBeginGivenWhen<TOutput> When<TOutput>(Func<ScenarioContext, TRequest, TOutput> execute) where TOutput : class, new()
        {
            return new BeginGivenWhen<TRequest, TOutput>(_context, _createRequest, execute);
        }
    }
}