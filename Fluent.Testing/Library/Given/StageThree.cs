using System;
using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library.Given
{
    public class StageThree<TInput, TOutput> : IStage3<TOutput> where TOutput : class, new() where TInput : class, new()
    {
        public StageThree(ScenarioContext context, Func<ScenarioContext, TInput, TOutput> execute)
        {
            Context = context;
            Execute = execute;
        }

        public ScenarioContext Context { get; set; }

        public Func<ScenarioContext, TInput, TOutput> Execute { get; set; }
        
        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "") where TNextStep : ScenarioStep<TOutput>, new()
        {
            Context.AddPipelineStep(memberName, input => input == null
                ? Execute(Context, new TInput())
                : Execute(Context, (TInput) input));

            var nextStep = new TNextStep {Context = Context};

            return nextStep;
        }
    }
}