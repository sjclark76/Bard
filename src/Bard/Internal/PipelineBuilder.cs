using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bard.Infrastructure;
using Bard.Internal.Exception;

namespace Bard.Internal
{
    internal class PipelineBuilder : IPipelineBuilder, IObserver<MessageLogged>
    {
        private readonly LogWriter _logWriter;
        private readonly List<PipelineStep> _pipelineSteps = new List<PipelineStep>();
        private bool _messageLogged;
        private int _executionCount;

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

        public void OnNext(MessageLogged value)
        {
            _messageLogged = true;
        }

        public void AddStep(string stepName, Action stepAction)
        {
            _pipelineSteps.Add(new PipelineStep(stepName, stepAction));
        }

        public void Execute(object? storyData)
        {
            if (HasSteps == false) return;

            var initialMessage = _executionCount > 0 ? "AND" : "GIVEN THAT";
            StringBuilder stringBuilder = new StringBuilder(initialMessage);

            foreach (var pipelineStep in _pipelineSteps)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(" ");

                stringBuilder.Append(Sanitize(pipelineStep.StepName));

                if (pipelineStep.StepAction == null) continue;

                _logWriter.LogHeaderMessage(stringBuilder.ToString());

                try
                {
                    _messageLogged = false;
                    pipelineStep.StepAction();
                    if (_messageLogged == false)
                        // The API was not called through the context so log
                        // the output instead.
                        if (storyData != null)
                            _logWriter.LogObject(storyData);

                    stringBuilder.Clear();
                }
                catch (BardException exception)
                {
                    throw new ChapterException($"Error executing story {pipelineStep.StepName}", exception);
                }
            }

            Reset();

            _executionCount++;
        }

        private string Sanitize(string methodName)
        {
            StringBuilder humanizedMethodName = new StringBuilder();
            foreach (var character in methodName)
            {
                if (character == '_') 
                {
                    humanizedMethodName.Append(" ");
                }
                else if (char.IsUpper(character))
                {
                    humanizedMethodName.Append(" ");
                    humanizedMethodName.Append(char.ToUpper(character));
                }
                else
                {
                    humanizedMethodName.Append(char.ToUpper(character));
                }
            }

            return humanizedMethodName.ToString();
        }

        private void Reset()
        {
            _pipelineSteps.Clear();
        }
    }
}