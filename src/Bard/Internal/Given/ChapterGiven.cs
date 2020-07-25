using System;

namespace Bard.Internal.Given
{
    internal class ChapterGiven<TInput, TRequest> : IChapterGiven<TInput, TRequest>
        where TRequest : new() where TInput : new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<TRequest> _createRequest;

        internal ChapterGiven(ScenarioContext context, Func<TRequest> createRequest)
        {
            _context = context;
            _createRequest = createRequest;
        }

        public IChapterGivenWhen<TOutput> When<TOutput>(Func<IScenarioContext, TInput, TRequest, TOutput> execute)
            where TOutput : class, new()
        {
            return new ChapterGivenWhen<TInput, TRequest, TOutput>(_context, _createRequest, execute);
        }
    }
}