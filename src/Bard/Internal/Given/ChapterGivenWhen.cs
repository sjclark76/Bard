using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class ChapterGivenWhen<TInput, TRequest, TOutput> : IChapterGivenWhen<TOutput> where TRequest : new()
        where TOutput : class, new()
        where TInput : class, new()
    {
        private readonly ScenarioContext<TInput> _context;
        private readonly Func<TRequest> _createRequest;
        private readonly Func<ScenarioContext<TInput>, TRequest, TOutput> _execute;

        internal ChapterGivenWhen(ScenarioContext<TInput> context, Func<TRequest> createRequest,
            Func<ScenarioContext<TInput>, TRequest, TOutput> execute)
        {
            _context = context;
            _createRequest = createRequest;
            _execute = execute;
        }

        public TNextChapter Then<TNextChapter>([CallerMemberName] string memberName = "")
            where TNextChapter : Chapter<TOutput>, new()
        {
            var request = _createRequest();

            _context.AddPipelineStep(memberName, input =>
            {
                _context.SetStoryInput(input as TInput);
                return _execute(_context, request);
            });

            var nextContext = new ScenarioContext<TOutput>(_context);
            var nextStep = new TNextChapter {Context = nextContext};

            return nextStep;
        }
    }
}