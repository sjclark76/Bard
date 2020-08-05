using System;

namespace Bard.Internal.Given
{
    internal class ChapterGiven<TStoryData, TStoryParams> : IChapterGiven<TStoryData, TStoryParams>
        where TStoryParams : new() where TStoryData : class, new()
    {
        private readonly ScenarioContext<TStoryData> _context;
        private readonly Func<TStoryParams> _createRequest;

        internal ChapterGiven(ScenarioContext<TStoryData> context, Func<TStoryParams> createRequest)
        {
            _context = context;
            _createRequest = createRequest;
        }

        public IChapterGivenWhen<TStoryData> When(Action<ScenarioContext<TStoryData>, TStoryParams> execute)
        {
            return new ChapterGivenWhen<TStoryData, TStoryParams>(_context, _createRequest, execute);
        }
    }
}