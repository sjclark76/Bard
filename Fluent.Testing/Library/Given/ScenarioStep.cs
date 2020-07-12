using System;
using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library.Given
{
    public class ScenarioStep<TInput> : ScenarioBase where TInput : class, new()
    {
        public void UseResult(Action<TInput> useResult)
        {
            var pipelineResult = Context?.ExecutePipeline();

            if (pipelineResult == null) return;

            var input = (TInput) pipelineResult;

            useResult(input);
        }

        protected TNextStep AddStep<TNextStep, TOutput>(Func<TInput, TOutput> stepAction,
            [CallerMemberName] string memberName = "")
            where TNextStep : ScenarioStep<TOutput>, new() where TOutput : class, new()
        {
            Context.AddPipelineStep(memberName, input => input == null
                ? stepAction(new TInput())
                : stepAction((TInput) input));

            var nextStep = new TNextStep();

            nextStep.SetContext(Context);

            return nextStep;
        }
    }
}