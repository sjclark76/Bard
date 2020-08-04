using System;

namespace Bard.Internal.Given
{
    internal class ChapterGiven<TStoryData, TStoryParams> : IChapterGiven<TStoryData, TStoryParams>
        where TStoryParams : new() where TStoryData : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<TStoryParams> _createRequest;

        internal ChapterGiven(ScenarioContext context, Func<TStoryParams> createRequest)
        {
            _context = context;
            _createRequest = createRequest;
        }

     public IChapterGivenWhen<TStoryData> When(Func<ScenarioContext<TStoryData>, TStoryParams, TStoryData> execute)
        {
            var nextContext = new ScenarioContext<TStoryData>(_context);
            return new ChapterGivenWhen<TStoryData, TStoryParams>(nextContext, _createRequest, execute);
            
        }
    }
}