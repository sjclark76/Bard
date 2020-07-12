using System;
using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library.Given
{
    public abstract class BeginAScenario : ScenarioBase
    {
        protected TNextStep AddStep<TNextStep, TOutput>(Func<TOutput> stepAction,
            [CallerMemberName] string memberName = "") where TNextStep : ScenarioStep<TOutput>, new()
            where TOutput : class, new()
        {
            Context.AddPipelineStep(memberName, o => stepAction());

            var nextStep = new TNextStep();

            nextStep.SetContext(Context);

            return nextStep;
        }
    }
}