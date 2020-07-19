using System;

namespace Fluent.Testing.Library
{
    public interface IPipelineBuilder
    {
        void AddStep(string stepName);
        void AddStep(string stepName, Func<object?, object?> stepFunc);
        object? Execute();
    }
}