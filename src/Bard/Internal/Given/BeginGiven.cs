using System;

namespace Bard.Internal.Given
{
    internal class BeginGiven<TRequest> : IBeginGiven<TRequest>
    {
        private readonly ScenarioContext _context;
        private readonly Func<TRequest> _createRequest;

        internal BeginGiven(ScenarioContext context, Func<TRequest> createRequest)
        {
            _context = context;
            _createRequest = createRequest;
        }

        public IBeginGivenWhen<TOutput> When<TOutput>(Func<ScenarioContext, TRequest, TOutput> execute)
            where TOutput : class, new()
        {
            var nextContext = new ScenarioContext<TOutput>(_context);
            
            return new BeginGivenWhen<TRequest, TOutput>(nextContext, _createRequest, execute);
        }
    }
}