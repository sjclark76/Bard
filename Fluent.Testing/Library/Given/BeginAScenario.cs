using System;

namespace Fluent.Testing.Library.Given
{
    public abstract class BeginAScenario : ScenarioBase
    {
        protected IBeginWhen<TOutput> When<TOutput>(Func<ScenarioContext, TOutput> execute)
            where TOutput : class, new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new BeginWhen<TOutput>(Context, execute);
        }

        protected IBeginGiven<TRequest> Given<TRequest>(Func<TRequest> createRequest)
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new BeginGiven<TRequest>(Context, createRequest);
        }
    }
}