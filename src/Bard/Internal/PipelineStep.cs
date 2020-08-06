using System;

namespace Bard.Internal
{
    internal class PipelineStep
    {
        internal PipelineStep(string stepName)
        {
            StepName = stepName;
        }

        public PipelineStep(string stepName, Action stepAction)
        {
            StepName = stepName;
            StepAction = stepAction;
        }

        public string StepName { get; }

        public Action? StepAction { get; }
    }
}