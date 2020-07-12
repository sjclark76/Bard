using System;

namespace Fluent.Testing.Library.Given
{
    public abstract class BeginAScenario
    {
        protected BeginAScenario()
        {
            
        }

        protected TNextStep AddStep<TNextStep, TOutput>(Func<TOutput> stepAction) where TNextStep : ScenarioStep<TOutput>, new() where TOutput : class
        {
            Context.AddPipelineStep(o => stepAction());

            var nextStep = new TNextStep {Context = Context};

            return nextStep;
        }

        public ScenarioContext? Context { get; set; }
    }
  
}