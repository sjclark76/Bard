using System;
using System.Collections.Generic;

namespace Fluent.Testing.Library.Given
{
    public class PipelineBuilder
    {
        private bool _hasBeenExecuted;
        private readonly List<Func<object?, object?>> _pipelineSteps = new List<Func<object?, object?>>();

        public object? Result { get; set; }

        public void AddStep(Func<object?, object?> stepFunc)
        {
            _pipelineSteps.Add(stepFunc);
        }

        public object? Execute()
        {
            if (_hasBeenExecuted) return Result;
            
            _hasBeenExecuted = true;
            
            object? input = null;

            foreach (var pipelineStep in _pipelineSteps)
            {
                var output = pipelineStep(input);
                input = output;
            }

            Result = input;

            return Result;
        }
    }
}