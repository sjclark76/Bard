using System;
using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library.Given
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

        protected IStageOne<TStepInput, TRequest> CreateRequest<TRequest>(Action<TRequest> createRequest) where TRequest : new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");
            
            return new StageOne<TStepInput, TRequest>(Context, createRequest, "");
        }
        
        /// <summary>
        /// Blah blah blah
        /// </summary>
        /// <param name="stepAction">f</param>
        /// <param name="memberName">x</param>
        /// <typeparam name="TNextStep">y</typeparam>
        /// <typeparam name="TOutput">z</typeparam>
        /// <returns>bb</returns>
        protected TNextStep AddStep<TNextStep, TOutput>(ScenarioStepAction<TStepInput, TOutput> stepAction,
            [CallerMemberName] string memberName = "")
            where TNextStep : ScenarioStep<TOutput>, new() where TOutput : class, new()
        {
            Context?.AddPipelineStep(memberName, input => input == null
                ? stepAction(Context, new TStepInput())
                : stepAction(Context, (TStepInput) input));

            var nextStep = new TNextStep {Context = Context};

            return nextStep;
        }
    }
}