using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.given
{
    internal class ChapterWhen<TInput, TOutput> : IChapterWhen<TOutput> where TOutput : class, new() where TInput : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<ScenarioContext, TInput, TOutput> _execute;

        public ChapterWhen(ScenarioContext context, Func<ScenarioContext, TInput, TOutput> execute)
        {
            _context = context;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "") where TNextStep : Chapter<TOutput>, new()
        {
            _context.AddPipelineStep(memberName, input => input == null
                ? _execute(_context, new TInput())
                : _execute(_context, (TInput) input));

            var nextStep = new TNextStep {Context = _context};

            return nextStep;
        }
    }
}