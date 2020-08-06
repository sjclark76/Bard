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

        public bool HasSteps => _pipelineSteps.Any();

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

        public void AddStep(string stepName, Action stepAction)
        {
            _pipelineSteps.Add(new PipelineStep(stepName, stepAction));
        }

        public void Execute(object? storyData)
        {
            if (HasSteps == false) return;

            var initialMessage = _executionCount > 0 ? "* AND" : "* GIVEN THAT";
            StringBuilder stringBuilder = new StringBuilder(initialMessage);

            foreach (var pipelineStep in _pipelineSteps)
            {
                _apiCalled = false;
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(" ");

                stringBuilder.Append(pipelineStep.StepName);

                if (pipelineStep.StepAction == null) continue;

                WriteHeader(stringBuilder);

                try
                {
                    pipelineStep.StepAction();
                    if (_apiCalled == false)
                        // The API was not called through the context so log
                        // the output instead.
                        if (storyData != null)
                            _logWriter.LogObject(storyData);
                }
                catch (BardException exception)
                {
                    throw new ChapterException($"Error executing story {pipelineStep.StepName}", exception);
                }
            }

            Reset();

            _executionCount++;
        }

        public void Reset()
        {
            _pipelineSteps.Clear();
        }

        private void WriteHeader(StringBuilder stringBuilder)
        {
            var astrixLine = new string('*', stringBuilder.Length + 2);
            _logWriter.LogMessage(astrixLine);
            stringBuilder.Append(" *");
            _logWriter.LogMessage(stringBuilder.ToString());
            _logWriter.LogMessage(astrixLine);
            _logWriter.LogMessage("");
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