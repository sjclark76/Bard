using System;
using System.Collections.Generic;
using Fluent.Testing.Library.Infrastructure;

namespace Fluent.Testing.Library.Given
{
    public class PipelineStep
    {
        public string StepName { get; }
        public Func<object?, object?> StepFunc { get; }

        public PipelineStep(string stepName, Func<object?, object?> stepFunc)
        {
            StepName = stepName;
            StepFunc = stepFunc;
        }
    }
    
    public class PipelineBuilder
    {
        private readonly LogWriter _logWriter;
        private bool _hasBeenExecuted;
        private readonly List<PipelineStep> _pipelineSteps = new List<PipelineStep>();

        public PipelineBuilder(LogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public object? Result { get; set; }

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
                var output = pipelineStep.StepFunc(input);
                input = output;
            }

            Result = input;

            return Result;
        }
    }
}