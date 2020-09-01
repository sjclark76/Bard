using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class ChapterGivenWhen<TStoryData, TStoryParams> : IChapterGivenWhen<TStoryData> where TStoryParams : new()
        where TStoryData : class, new()
    {
        private readonly ScenarioContext<TStoryData> _context;
        private readonly Func<TStoryData, TStoryParams> _storyParameters;
        private readonly Action<ScenarioContext<TStoryData>, TStoryParams> _execute;

        internal ChapterGivenWhen(ScenarioContext<TStoryData> context, Func<TStoryData, TStoryParams> storyParameters,
            Action<ScenarioContext<TStoryData>, TStoryParams> execute)
        {
            _context = context;
            _storyParameters = storyParameters;
            _execute = execute;
        }

        public TNextChapter ProceedToChapter<TNextChapter>([CallerMemberName] string memberName = "")
            where TNextChapter : Chapter<TStoryData>, new()
        {
            var request = _storyParameters(_context.StoryData);

            _context.AddPipelineStep(memberName, () => { _execute(_context, request); });

            var nextStep = new TNextChapter {Context = _context};

            return nextStep;
        }
    }
}