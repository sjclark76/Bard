using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class BeginWhen<TOutput> : IBeginWhen<TOutput> where TOutput : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<ScenarioContext, TOutput> _execute;

        internal BeginWhen(ScenarioContext<TOutput> context, Func<ScenarioContext, TOutput> execute)
        {
            _context = context;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TOutput>, new()
        {
            _context.AddPipelineStep(memberName, input => _execute(_context));

            var nextContext = new ScenarioContext<TOutput>(_context);
            var nextStep = new TNextStep {Context = nextContext};

            return nextStep;
        }
    }
}