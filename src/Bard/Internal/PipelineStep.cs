using System;

namespace Bard.Internal
{
    internal class PipelineStep
    {
        internal PipelineStep(string stepName)
        {
            StepName = stepName;
        }

        public PipelineStep(string stepName, Action<object?> stepFunc)
        {
            StepName = stepName;
            StepFunc = stepFunc;
        }

        public string StepName { get; }

        public Action<object?>? StepFunc { get; }
    }
}