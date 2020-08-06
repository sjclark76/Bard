using System;

namespace Bard.Internal
{
    internal interface IPipelineBuilder
    {
        void AddStep(string stepName, Action stepAction);
        void Execute(object? storyData = null);
    }
}