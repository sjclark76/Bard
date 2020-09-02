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

        public IChapterGivenWhen<TStoryData> When(Action<ScenarioContext<TStoryData>, TStoryParams> executeStory)
        {
            void BuildParametersAndExecuteStory(ScenarioContext<TStoryData> context, Func<TStoryData, TStoryParams> func)
            {
                var storyParams = _buildStoryParameters(context.StoryData);
                
                executeStory(context, storyParams);
            }

            return new ChapterGivenWhen<TStoryData, TStoryParams>(_context, _buildStoryParameters, BuildParametersAndExecuteStory);
        }
    }
}