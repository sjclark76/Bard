using System;
using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library.Given
{
    public class ScenarioStep<TInput> where TInput : class
    {
        public ScenarioContext? Context { get; set; }

        public void UseResult(Action<TInput> useResult)
        {
            var input = (TInput) Context.ExecutePipeline();
            
            useResult(input);
        }

        protected TNextStep AddStep<TNextStep, TOutput>(Func<TInput, TOutput> stepAction,
            [CallerMemberName] string memberName = "")
            where TNextStep : ScenarioStep<TOutput>, new() where TOutput : class
        {
            Context.AddPipelineStep(memberName, o => stepAction((TInput) o));

            var nextStep = new TNextStep {Context = Context};

            return nextStep;
        }
    }
}