using System;

namespace Bard
{
    public interface IPipelineBuilder
    {
        void AddStep(string stepName, Action<object?> stepFunc);
        object? Execute();
    }
}