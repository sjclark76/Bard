using System;
using Bard.Internal;
using Bard.Internal.Given;

namespace Bard
{
    public abstract class StoryBook : ChapterBase
    {
        protected IBeginWhen<TOutput> When<TOutput>(Func<IScenarioContext, TOutput> execute)
            where TOutput : class, new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new BeginWhen<TOutput>((ScenarioContext) Context, execute);
        }

        protected IBeginGiven<TRequest> Given<TRequest>(Func<TRequest> createRequest)
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new BeginGiven<TRequest>((ScenarioContext) Context, createRequest);
        }
    }
}