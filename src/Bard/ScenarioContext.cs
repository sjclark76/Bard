using System;
using Bard.Configuration;
using Bard.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Bard.Internal
{
    public class ScenarioContext : IScenarioContext
    {
        internal IPipelineBuilder Builder { get; }
        private IServiceProvider? _services;

        internal ScenarioContext(IPipelineBuilder pipelineBuilder, IApi api, LogWriter logWriter,
            IServiceProvider? services)
        {
            Builder = pipelineBuilder;
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
            return Builder.Execute();
        }

        public void AddPipelineStep(string stepName, Func<object?, object?> func)
        {
            Builder.AddStep(stepName, func);
        }

        public void AddPipelineStep(string message)
        {
            Builder.AddStep(message);
        }

        public void ResetPipeline()
        {
            Builder.Reset();
        }

        public bool HasSteps => Builder.HasSteps;
    }

    public class ScenarioContext<TStoryInput> : ScenarioContext where TStoryInput : class, new()
    {
        public TStoryInput? StoryInput { get; set; }
        
        internal ScenarioContext(ScenarioContext context) : base(context.Builder, context.Api, context.Writer, context.Services)
        {
        }
    }
        
}