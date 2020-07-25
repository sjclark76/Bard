using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class ChapterGivenWhen<TInput, TRequest, TOutput> : IChapterGivenWhen<TOutput> where TRequest : new()
        where TOutput : class, new()
        where TInput : new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<TRequest> _createRequest;
        private readonly Func<ScenarioContext, TInput, TRequest, TOutput> _execute;

        internal ChapterGivenWhen(ScenarioContext context, Func<TRequest> createRequest,
            Func<ScenarioContext, TInput, TRequest, TOutput> execute)
        {
            _context = context;
            _createRequest = createRequest;
            _execute = execute;
        }

        public TNextChapter Then<TNextChapter>([CallerMemberName] string memberName = "")
            where TNextChapter : Chapter<TOutput>, new()
        {
            var request = _createRequest();

            _context.AddPipelineStep(memberName, input => input == null
                ? _execute(_context, new TInput(), request)
                : _execute(_context, (TInput) input, request));

            var nextStep = new TNextChapter {Context = _context};

            return nextStep;
        }
    }
}