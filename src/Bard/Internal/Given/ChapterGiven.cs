using System;

namespace Bard.Internal.Given
{
    internal class ChapterGiven<TInput, TRequest> : IChapterGiven<TInput, TRequest>
        where TRequest : new() where TInput : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<TRequest> _createRequest;

        internal ChapterGiven(ScenarioContext context, Func<TRequest> createRequest)
        {
            _context = context;
            _createRequest = createRequest;
        }

     public IChapterGivenWhen<TOutput> When<TOutput>(Func<ScenarioContext<TInput>, TRequest, TOutput> execute) where TOutput : class, new()
        {
            var nextContext = new ScenarioContext<TInput>(_context);
            return new ChapterGivenWhen<TInput, TRequest, TOutput>(nextContext, _createRequest, execute);
            
        }
    }
}