using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class ChapterGivenWhen<TStoryData, TStoryParams> : IChapterGivenWhen<TStoryData> where TStoryParams : new()
        where TStoryData : class, new()
    {
        private readonly ScenarioContext<TStoryData> _context;
        private readonly Func<TStoryParams> _createRequest;
        private readonly Func<ScenarioContext<TStoryData>, TStoryParams, TStoryData> _execute;

        internal ChapterGivenWhen(ScenarioContext<TStoryData> context, Func<TStoryParams> createRequest,
            Func<ScenarioContext<TStoryData>, TStoryParams, TStoryData> execute)
        {
            _context = context;
            _createRequest = createRequest;
            _execute = execute;
        }

        public TNextChapter Then<TNextChapter>([CallerMemberName] string memberName = "")
            where TNextChapter : Chapter<TStoryData>, new()
        {
            var request = _createRequest();

            _context.AddPipelineStep(memberName, input =>
            {
                _context.SetStoryData(input as TStoryData);
                   return _execute(_context, request);
            });

            var nextContext = new ScenarioContext<TStoryData>(_context);
            var nextStep = new TNextChapter {Context = nextContext};

            return nextStep;
        }
    }
}