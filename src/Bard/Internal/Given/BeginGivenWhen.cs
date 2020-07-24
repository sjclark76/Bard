using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class BeginGivenWhen<TRequest, TOutput> : IBeginGivenWhen<TOutput> where TOutput : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<TRequest> _createRequest;
        private readonly Func<ScenarioContext, TRequest, TOutput> _execute;

        public BeginGivenWhen(ScenarioContext context, Func<TRequest> createRequest,
            Func<ScenarioContext, TRequest, TOutput> execute)
        {
            _context = context;
            _createRequest = createRequest;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TOutput>, new()
        {
            var request = _createRequest();
            _context.AddPipelineStep(memberName, input => _execute(_context, request));

            var nextStep = new TNextStep {Context = _context};

            return nextStep;
        }
    }
}