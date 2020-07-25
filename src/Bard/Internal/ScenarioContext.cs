using System;
using Bard.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Bard.Internal
{
    internal class ScenarioContext : IScenarioContext
    {
        private readonly IPipelineBuilder _pipelineBuilder;

        public ScenarioContext(IPipelineBuilder pipelineBuilder, IApi api, LogWriter logWriter,
            IServiceProvider? services)
        {
            _pipelineBuilder = pipelineBuilder;
            Api = api;
            Writer = logWriter;

            if (services != null)
                Services = services.CreateScope().ServiceProvider;
        }

        public IServiceProvider? Services { get; set; }

        public IApi Api { get; }
        public LogWriter Writer { get; }

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