using System;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library
{
    public class ScenarioContext
    {
        private readonly PipelineBuilder _pipelineBuilder;
        public Api Api { get; }
        public LogWriter Writer { get; }

        public ScenarioContext(PipelineBuilder pipelineBuilder, Api api, LogWriter logWriter)
        {
            _pipelineBuilder = pipelineBuilder;
            Api = api;
            Writer = logWriter;
        }

        public object? ExecutePipeline()
        {
            return _pipelineBuilder.Execute();
        }

        public void AddPipelineStep(string stepName, Func<object?, object?> func)
        {
            _pipelineBuilder.AddStep(stepName, func);
        }
        
        public void AddPipelineStep(string message)
        {
            _pipelineBuilder.AddStep(message);
        }
    }
}