using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class ChapterWhen<TStoryInput, TStoryOutput> : IChapterWhen<TStoryOutput>
        where TStoryOutput : class, new() where TStoryInput : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<ScenarioContext<TStoryInput>, TStoryOutput> _execute;

        internal ChapterWhen(ScenarioContext<TStoryOutput> context,
            Func<ScenarioContext<TStoryInput>, TStoryOutput> execute)
        {
            _context = context;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TStoryOutput>, new()
        {
            var nextContext = new ScenarioContext<TStoryInput>(_context);

            _context.AddPipelineStep(memberName, input =>
            {
                nextContext.SetStoryInput(input as TStoryInput);
                return _execute(nextContext);
            });

            var nextStep = new TNextStep {Context = new ScenarioContext<TStoryOutput>(_context)};

            return nextStep;
        }

        public EndChapter<TStoryOutput> End(string memberName = "")
        {
            var nextContext = new ScenarioContext<TStoryInput>(_context);

            _context.AddPipelineStep(memberName, input =>
            {
                nextContext.SetStoryInput(input as TStoryInput);
                return _execute(nextContext);
            });

            var nextStep = new EndChapter<TStoryOutput> {Context = new ScenarioContext<TStoryOutput>(_context)};

            return nextStep;
        }
    }
}