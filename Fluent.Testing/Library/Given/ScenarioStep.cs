using System;

namespace Fluent.Testing.Library.Given
{
    public class ScenarioStep<TInput> where TInput : class
    {
        public ScenarioContext? Context { get; set; }

        public void UseResult(Action<TInput> useResult)
        {
            var input = Context.ExecutePipeline() as TInput;
            useResult(input);
        }

        protected TNextStep AddStep<TNextStep, TOutput>(Func<TInput, TOutput> stepAction)
            where TNextStep : ScenarioStep<TOutput>, new() where TOutput : class
        {
            Context.AddPipelineStep(o => stepAction((TInput) o));

            var nextStep = new TNextStep {Context = Context};

            return nextStep;
        }
    }
}