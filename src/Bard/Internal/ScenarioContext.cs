using System;
using Bard.Configuration;
using Bard.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Bard.Internal
{
    internal class ScenarioContext : IScenarioContext
    {
        private readonly IPipelineBuilder _pipelineBuilder;
        private IServiceProvider? _services;

        internal ScenarioContext(IPipelineBuilder pipelineBuilder, IApi api, LogWriter logWriter,
            IServiceProvider? services)
        {
            _pipelineBuilder = pipelineBuilder;
            Api = api;
            Writer = logWriter;
            
            if (services != null)
                Services = services.CreateScope().ServiceProvider;
        }

        public IServiceProvider? Services
        {
            get
            {
                if (_services == null)
                    throw new BardConfigurationException($"Error Accessing {nameof(ScenarioContext)} {nameof(Services)} property. It has not been set in {nameof(ScenarioConfiguration)} {nameof(ScenarioConfiguration.Configure)} method.");
                
                return _services;
            }
            set => _services = value;
        }

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

        public void ResetPipeline()
        {
            _pipelineBuilder.Reset();
        }

        public bool HasSteps => _pipelineBuilder.HasSteps;
    }
}