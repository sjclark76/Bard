using System;
using Fluent.Testing.Library.Internal.Given;

namespace Fluent.Testing.Library
{
    public delegate TOutput ScenarioStepAction<in TInput, out TOutput>(ScenarioContext context, TInput input);

    public abstract class ScenarioStep<TStepInput> : ScenarioBase where TStepInput : class, new()
    {
        public void UseResult(Action<TStepInput> useResult)
        {
            var pipelineResult = Context?.ExecutePipeline();

            if (pipelineResult == null) return;

            var input = (TStepInput) pipelineResult;

            useResult(input);
        }

        protected IScenarioStepWhen<TOutput> When<TOutput>(Func<ScenarioContext, TStepInput, TOutput> execute)
            where TOutput : class, new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new ScenarioStepWhen<TStepInput, TOutput>(Context, execute);
        }

        protected IScenarioStepGiven<TStepInput, TRequest> Given<TRequest>(Func<TRequest> createRequest)
            where TRequest : new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new ScenarioStepGiven<TStepInput, TRequest>(Context, createRequest);
        }
    }
}