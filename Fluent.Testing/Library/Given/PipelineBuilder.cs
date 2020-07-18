using System;
using System.Collections.Generic;
using System.Text;
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

            StringBuilder stringBuilder = new StringBuilder("* Given That");

            foreach (var pipelineStep in _pipelineSteps)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(" ");
                
                stringBuilder.Append(pipelineStep.StepName);
                // stringBuilder.Append(" ");

                if (pipelineStep.StepFunc != null)
                {
                    WriteHeader(stringBuilder);

                    try
                    {
                        var output = pipelineStep.StepFunc(input);
                        input = output;
                    }
                    catch (Exception exception)
                    {
                        throw new ScenarioStepException($"Error executing scenario step {pipelineStep.StepName}", exception);
                    }
                }
            }

            Result = input;

            return Result;
        }

        private void WriteHeader(StringBuilder stringBuilder)
        {
            var astrixLine = new string('*', stringBuilder.Length + 2);
            _logWriter.WriteStringToConsole(astrixLine);
            stringBuilder.Append(" *");
            _logWriter.WriteStringToConsole(stringBuilder.ToString());
            _logWriter.WriteStringToConsole(astrixLine);
            _logWriter.WriteStringToConsole("");
            stringBuilder.Clear();
            stringBuilder.Append("* ");
        }
    }
}