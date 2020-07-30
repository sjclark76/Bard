using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class BeginWhen<TStoryOutput> : IBeginWhen<TStoryOutput> where TStoryOutput : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<ScenarioContext, TStoryOutput> _execute;

        internal BeginWhen(ScenarioContext<TStoryOutput> context, Func<ScenarioContext, TStoryOutput> execute)
        {
            _context = context;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TStoryOutput>, new()
        {
            _context.AddPipelineStep(memberName, input => _execute(_context));

            var nextContext = new ScenarioContext<TStoryOutput>(_context);
            var nextStep = new TNextStep {Context = nextContext};

            return nextStep;
        }
    }
}