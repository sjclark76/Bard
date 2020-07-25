using System;
using System.Collections.Generic;
using System.Text;
using Bard.Infrastructure;

namespace Bard.Internal
{
    internal class PipelineBuilder : IPipelineBuilder
    {
        private readonly LogWriter _logWriter;
        private readonly List<PipelineStep> _pipelineSteps = new List<PipelineStep>();
        private int _executionCount;
        private bool _hasBeenExecuted;

        internal PipelineBuilder(LogWriter logWriter)
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

            var initialMessage = _executionCount > 0 ? "* AND" : "* GIVEN THAT";
            StringBuilder stringBuilder = new StringBuilder(initialMessage);

            foreach (var pipelineStep in _pipelineSteps)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(" ");

                stringBuilder.Append(pipelineStep.StepName);

                if (pipelineStep.StepFunc == null) continue;

                WriteHeader(stringBuilder);

                try
                {
                    var output = pipelineStep.StepFunc(input);
                    input = output;
                }
                catch (Exception exception)
                {
                    throw new ChapterException($"Error executing story {pipelineStep.StepName}", exception);
                }
            }

            _executionCount++;

            Result = input;

            return Result;
        }

        public void Reset()
        {
            _hasBeenExecuted = false;
            _pipelineSteps.Clear();
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