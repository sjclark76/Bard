using System;

namespace Bard.Internal.given
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