using System;

namespace Bard
{
    public interface IPipelineBuilder
    {
        void AddStep(string stepName);
        void AddStep(string stepName, Func<object?, object?> stepFunc);
        object? Execute();
        void Reset();
    }
}