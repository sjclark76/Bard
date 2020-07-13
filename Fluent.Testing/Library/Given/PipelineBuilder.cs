using System;
using System.Collections.Generic;
using Fluent.Testing.Library.Infrastructure;

namespace Fluent.Testing.Library.Given
{
    public class PipelineBuilder
    {
        private readonly LogWriter _logWriter;
        private readonly List<PipelineStep> _pipelineSteps = new List<PipelineStep>();
        private bool _hasBeenExecuted;

        public PipelineBuilder(LogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public object? Result { get; set; }

        public void AddStep(string stepName)
        {
            _pipelineSteps.Add(new PipelineStep(stepName));
        }

        public void AddStep(string stepName, Func<object?, object?> stepFunc)
        {
            _pipelineSteps.Add(new PipelineStep(stepName, stepFunc));
        }

        public object? Execute()
        {
            if (_hasBeenExecuted) return Result;

            _hasBeenExecuted = true;

            object? input = null;

            foreach (var pipelineStep in _pipelineSteps)
            {
                _logWriter.WriteStringToConsole(pipelineStep.StepName);
                if (pipelineStep.StepFunc != null)
                {
                    var output = pipelineStep.StepFunc(input);
                    input = output;
                }
            }

            Result = input;

            return Result;
        }
    }
}