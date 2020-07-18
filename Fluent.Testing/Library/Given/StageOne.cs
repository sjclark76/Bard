using System;

namespace Fluent.Testing.Library.Given
{
    public class StageOne<TInput, TRequest> : IStageOne<TInput, TRequest> where TRequest : new() where TInput : new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<TRequest> _createRequest;

        public StageOne(ScenarioContext context, Func<TRequest> createRequest)
        {
            _context = context;
            _createRequest = createRequest;
        }

        public IStageTwo<TOutput> When<TOutput>(Func<ScenarioContext, TInput, TRequest, TOutput> execute)
            where TOutput : class, new()
        {
            return new StageTwo<TInput, TRequest, TOutput>(_context, _createRequest, execute);
        }
    }
}