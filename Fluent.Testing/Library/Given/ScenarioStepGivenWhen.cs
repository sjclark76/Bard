using System;
using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library.Given
{
    public class ScenarioStepGivenWhen<TInput, TRequest, TOutput> : IScenarioStepGivenWhen<TOutput> where TRequest : new()
        where TOutput : class, new()
        where TInput : new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<TRequest> _createRequest;
        private readonly Func<ScenarioContext, TInput, TRequest, TOutput> _execute;

        public ScenarioStepGivenWhen(ScenarioContext context, Func<TRequest> createRequest,
            Func<ScenarioContext, TInput, TRequest, TOutput> execute)
        {
            _context = context;
            _createRequest = createRequest;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : ScenarioStep<TOutput>, new()
        {
            var request = _createRequest();

            _context.AddPipelineStep(memberName, input => input == null
                ? _execute(_context, new TInput(), request)
                : _execute(_context, (TInput) input, request));

            var nextStep = new TNextStep {Context = _context};

            return nextStep;
        }
    }
}