using System;

namespace Fluent.Testing.Library.Internal
{
    internal class PipelineStep
    {
        public string StepName { get; }
        
        public Func<object?, object?>? StepFunc { get; }

        public PipelineStep(string stepName)
        {
            StepName = stepName;
        }
        public PipelineStep(string stepName, Func<object?, object?> stepFunc)
        {
            StepName = stepName;
            StepFunc = stepFunc;
        }
    }
}