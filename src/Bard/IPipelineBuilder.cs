using System;

namespace Bard
{
    public interface IPipelineBuilder
    {
        void AddStep(string stepName, Action stepAction);
        void Execute(object? storyData = null);
    }
}