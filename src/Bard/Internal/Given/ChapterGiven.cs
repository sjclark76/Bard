using System;

namespace Bard.Internal.Given
{
    internal class ChapterGiven<TStoryData, TStoryParams> : IChapterGiven<TStoryData, TStoryParams>
        where TStoryParams : new() where TStoryData : class, new()
    {
        private readonly ScenarioContext<TStoryData> _context;
        private readonly Func<TStoryData, TStoryParams> _buildStoryParameters;

        internal ChapterGiven(ScenarioContext<TStoryData> context, Func<TStoryData, TStoryParams> buildStoryParameters)
        {
            _context = context;
            _buildStoryParameters = buildStoryParameters;
        }

        public IChapterWhen<TStoryData> When(Action<ScenarioContext<TStoryData>, TStoryParams> executeStory)
        {
            return new ChapterWhen<TStoryData>(_context, 
                context =>
                {
                    var storyParams = _buildStoryParameters(context.StoryData);
                
                    executeStory(context, storyParams);
                });
        }
    }
}