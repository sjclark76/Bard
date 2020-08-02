using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bard.Infrastructure;
using Bard.Internal.Exception;
using Bard.Internal.Then;

namespace Bard.Internal
{
    internal class PipelineBuilder : IPipelineBuilder, IObserver<Response>
    {
        private readonly LogWriter _logWriter;
        private readonly List<PipelineStep> _pipelineSteps = new List<PipelineStep>();
        private bool _apiCalled;
        private int _executionCount;
        private IDisposable? _unSubscriber;

        internal PipelineBuilder(LogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public object? Result { get; set; }

        public void OnCompleted()
        {
        }

        public void OnError(System.Exception error)
        {
        }

        public void OnNext(Response value)
        {
            _apiCalled = true;
        }

        public void AddStep(string stepName, Func<object?, object?> stepFunc)
        {
            _pipelineSteps.Add(new PipelineStep(stepName, stepFunc));
        }

        private object? Input { get; set; }
        
        public object? Execute()
        {
            if (HasSteps == false) return Result;

            var initialMessage = _executionCount > 0 ? "* AND" : "* GIVEN THAT";
            StringBuilder stringBuilder = new StringBuilder(initialMessage);

            foreach (var pipelineStep in _pipelineSteps)
            {
                _apiCalled = false;
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(" ");

                stringBuilder.Append(pipelineStep.StepName);

                if (pipelineStep.StepFunc == null) continue;

                WriteHeader(stringBuilder);

                try
                {
                    var output = pipelineStep.StepFunc(Input);
                    if (_apiCalled == false)
                        // The API was not called through the context so log
                        // the output instead.
                        _logWriter.WriteObjectToConsole(output);

                    Input = output;
                }
                catch (BardException exception)
                {
                    throw new ChapterException($"Error executing story {pipelineStep.StepName}", exception);
                }
            }
            Reset();
            
            _executionCount++;

            Result = Input;

            return Result;
        }

        public void Reset()
        {
            _pipelineSteps.Clear();
        }

        public bool HasSteps => _pipelineSteps.Any();

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

        public void Subscribe(IObservable<Response> provider)
        {
            if (provider != null)
                _unSubscriber = provider.Subscribe(this);
        }

        public void UnSubscribe()
        {
            _unSubscriber?.Dispose();
        }
    }
}