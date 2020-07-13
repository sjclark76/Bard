using System;
using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library.Given
{
    public delegate TOutput ScenarioStepAction<in TInput, out TOutput>(ScenarioContext context, TInput input);

    public abstract class ScenarioStep<TInput> : ScenarioBase where TInput : class, new()
    {
        public void UseResult(Action<TInput> useResult)
        {
            var pipelineResult = Context?.ExecutePipeline();

            if (pipelineResult == null) return;

            var input = (TInput) pipelineResult;

            useResult(input);
        }

        protected IStageOne<TInput, TRequest> ForRequest<TRequest>(Action<TRequest>? modifyRequest = null) where TRequest : new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");
            
            return new StageOne<TInput, TRequest>(Context, modifyRequest, "");
        }
        
        /// <summary>
        /// Blah blah blah
        /// </summary>
        /// <param name="stepAction">f</param>
        /// <param name="memberName">x</param>
        /// <typeparam name="TNextStep">y</typeparam>
        /// <typeparam name="TOutput">z</typeparam>
        /// <returns>bb</returns>
        protected TNextStep AddStep<TNextStep, TOutput>(ScenarioStepAction<TInput, TOutput> stepAction,
            [CallerMemberName] string memberName = "")
            where TNextStep : ScenarioStep<TOutput>, new() where TOutput : class, new()
        {
            Context?.AddPipelineStep(memberName, input => input == null
                ? stepAction(Context, new TInput())
                : stepAction(Context, (TInput) input));

            var nextStep = new TNextStep {Context = Context};

            return nextStep;
        }
    }
}