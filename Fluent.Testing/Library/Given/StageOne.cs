using System;

namespace Fluent.Testing.Library.Given
{
    public interface IStageOne<out TInput, out TRequest> where TInput : new() where TRequest : new()
    {
        IStageTwo<TOutput> CallApi<TOutput>(Func<ScenarioContext, TInput, TRequest, TOutput> execute) where TOutput : class, new();
    }

    public class StageOne<TInput, TRequest> : IStageOne<TInput, TRequest> where TRequest : new() where TInput : new()
    {
        public ScenarioContext Context { get; }
        public Action<TRequest>? ModifyRequest { get; }
        public string MemberName { get; }

        public StageOne(ScenarioContext context, Action<TRequest> createRequest, string memberName)
        {
            Context = context;
            ModifyRequest = createRequest;
            MemberName = memberName;
        }

        public IStageTwo<TOutput> CallApi<TOutput>(Func<ScenarioContext, TInput, TRequest, TOutput> execute) where TOutput : class, new()
        {
            return new StageTwo<TInput, TRequest, TOutput>(this, execute);
        }
    }
}