using System;

namespace Bard.Internal
{
    internal class PipelineStep
    {
        public PipelineStep(string stepName, Action stepAction)
        {
            StepName = stepName;
            StepAction = stepAction;
        }

        public string StepName { get; }

        public Action? StepAction { get; }
    }
}