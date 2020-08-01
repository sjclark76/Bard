using System;

namespace Bard.Internal.Given
{
    internal class BeginGiven<TStoryParams> : IBeginGiven<TStoryParams>
    {
        private readonly ScenarioContext _context;
        private readonly Func<TStoryParams> _createRequest;

        internal BeginGiven(ScenarioContext context, Func<TStoryParams> createRequest)
        {
            _context = context;
            _createRequest = createRequest;
        }

        public IBeginGivenWhen<TOutput> When<TOutput>(Func<ScenarioContext, TStoryParams, TOutput> execute)
            where TOutput : class, new()
        {
            var nextContext = new ScenarioContext<TOutput>(_context);

            return new BeginGivenWhen<TStoryParams, TOutput>(nextContext, _createRequest, execute);
        }
    }
}