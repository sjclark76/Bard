using System;
using Fluent.Testing.Library.Given;

namespace Fluent.Testing.Library.Internal.Given
{
    internal class ScenarioStepGiven<TInput, TRequest> : IScenarioStepGiven<TInput, TRequest> where TRequest : new() where TInput : new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<TRequest> _createRequest;

        public ScenarioStepGiven(ScenarioContext context, Func<TRequest> createRequest)
        {
            _context = context;
            _createRequest = createRequest;
        }

        public IScenarioStepGivenWhen<TOutput> When<TOutput>(Func<ScenarioContext, TInput, TRequest, TOutput> execute)
            where TOutput : class, new()
        {
            return new ScenarioStepGivenWhen<TInput, TRequest, TOutput>(_context, _createRequest, execute);
        }
    }
}