using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class ChapterGivenWhen<TStoryData, TStoryParams> : IChapterGivenWhen<TStoryData> where TStoryParams : new()
        where TStoryData : class, new()
    {
        private readonly ScenarioContext<TStoryData> _context;
        private readonly Func<TStoryParams> _createRequest;
        private readonly Action<ScenarioContext<TStoryData>, TStoryParams> _execute;

        internal ChapterGivenWhen(ScenarioContext<TStoryData> context, Func<TStoryParams> createRequest,
            Action<ScenarioContext<TStoryData>, TStoryParams> execute)
        {
            _context = context;
            _createRequest = createRequest;
            _execute = execute;
        }

        public TNextChapter Then<TNextChapter>([CallerMemberName] string memberName = "")
            where TNextChapter : Chapter<TStoryData>, new()
        {
            var request = _createRequest();

            _context.AddPipelineStep(memberName, () => { _execute(_context, request); });

            var nextStep = new TNextChapter {Context = _context};

            return nextStep;
        }
    }
}