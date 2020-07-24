using System;

namespace Bard.Internal
{
    internal class PipelineStep
    {
        public PipelineStep(string stepName)
        {
            StepName = stepName;
        }

        public PipelineStep(string stepName, Func<object?, object?> stepFunc)
        {
            StepName = stepName;
            StepFunc = stepFunc;
        }

        public string StepName { get; }

        public Func<object?, object?>? StepFunc { get; }
    }
}