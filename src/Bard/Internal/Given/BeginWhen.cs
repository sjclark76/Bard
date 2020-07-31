using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class BeginWhen<TStoryInput> : IBeginWhen<TStoryInput> where TStoryInput : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<ScenarioContext, TStoryInput> _execute;

        internal BeginWhen(ScenarioContext<TStoryInput> context, Func<ScenarioContext, TStoryInput> execute)
        {
            _context = context;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TStoryInput>, new()
        {
            _context.AddPipelineStep(memberName, input => _execute(_context));

            var nextContext = new ScenarioContext<TStoryInput>(_context);
            var nextStep = new TNextStep {Context = nextContext};

            return nextStep;
        }
    }
}