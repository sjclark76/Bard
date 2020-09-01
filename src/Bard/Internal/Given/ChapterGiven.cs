using System;

namespace Bard.Internal.Given
{
    internal class ChapterGiven<TStoryData, TStoryParams> : IChapterGiven<TStoryData, TStoryParams>
        where TStoryParams : new() where TStoryData : class, new()
    {
        private readonly ScenarioContext<TStoryData> _context;
        private readonly Func<TStoryData, TStoryParams> _storyParameters;

        internal ChapterGiven(ScenarioContext<TStoryData> context, Func<TStoryData, TStoryParams> storyParameters)
        {
            _context = context;
            _storyParameters = storyParameters;
        }

        public IChapterGivenWhen<TStoryData> When(Action<ScenarioContext<TStoryData>, TStoryParams> execute)
        {
            return new ChapterGivenWhen<TStoryData, TStoryParams>(_context, _storyParameters, execute);
        }
    }
}