using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class ChapterWhen<TInput, TOutput> : IChapterWhen<TOutput>
        where TOutput : class, new() where TInput : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<ScenarioContext<TInput>, TOutput> _execute;

        internal ChapterWhen(ScenarioContext<TOutput> context, Func<ScenarioContext<TInput>, TOutput> execute)
        {
            _context = context;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TOutput>, new()
        {
            var nextContext = new ScenarioContext<TInput>(_context);
            
            _context.AddPipelineStep(memberName, input =>
            {
                nextContext.StoryInput = input as TInput;
                return _execute(nextContext);
            });

            var nextStep = new TNextStep {Context = new ScenarioContext<TOutput>(_context)};

            return nextStep;
        }
    }
}