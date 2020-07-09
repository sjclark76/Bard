using System;
using System.Collections.Generic;

namespace Fluent.Testing.Library.Given
{
    public class ScenarioStart<TActionResult>
    {
        public ScenarioStart(Func<TActionResult> output)
        {
            PipelineBuilder = new PipelineBuilder();

            var foo = output();
            PipelineBuilder.AddStep(o => output());
        }

        public PipelineBuilder PipelineBuilder { get; set; }
    }

    public class ScenarioStep<TInput, TActionResult> where TInput : class
    {
        public ScenarioStep(Func<TInput, TActionResult> scenarioAction, PipelineBuilder pipelineBuilder)
        {
            PipelineBuilder = pipelineBuilder;
            PipelineBuilder.AddStep(o => scenarioAction(o as TInput));
        }

        public PipelineBuilder PipelineBuilder { get; }
    }

    public class ScenarioEnd<TInput>
    {
        public ScenarioEnd(Action<TInput> scenarioAction, PipelineBuilder pipelineBuilder)
        {
            PipelineBuilder = pipelineBuilder;
            PipelineBuilder.AddStep(o => scenarioAction);

            PipelineBuilder.Execute();
        }

        public PipelineBuilder PipelineBuilder { get; }
    }

    public class PipelineBuilder
    {
        private readonly List<Func<object?, object?>> _pipelineSteps = new List<Func<object?, object?>>();

        public void AddStep(Func<object?, object?> stepFunc)
        {
            _pipelineSteps.Add(stepFunc);
        }

        public void Execute()
        {
            object? input = null;

            foreach (var pipelineStep in _pipelineSteps)
            {
                var output = pipelineStep(input);
                input = output;
            }
        }
    }
}