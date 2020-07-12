using System;
using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library.Given
{
    public abstract class BeginAScenario : ScenarioBase
    {
        protected TNextStep AddStep<TNextStep, TOutput>(Func<ScenarioContext, TOutput> stepAction,
            [CallerMemberName] string memberName = "") where TNextStep : ScenarioStep<TOutput>, new()
            where TOutput : class, new()
        {
            Context?.AddPipelineStep(memberName, o => stepAction(Context));

            var nextStep = new TNextStep {Context = Context};

            return nextStep;
        }
    }
}