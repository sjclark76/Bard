using System;
using Bard.Configuration;
using Bard.Infrastructure;
using Bard.Internal.Exception;
using Microsoft.Extensions.DependencyInjection;

namespace Bard
{
    public class ScenarioContext
    {
        private IServiceProvider? _services;

        internal ScenarioContext(IPipelineBuilder pipelineBuilder, IApi api, LogWriter logWriter,
            IServiceProvider? services, Func<object>? createGrpcClient = null)
        {
            Builder = pipelineBuilder;
            Api = api;
            Writer = logWriter;
            CreateGrpcClient = createGrpcClient;

            if (services != null)
                Services = services.CreateScope().ServiceProvider;
        }

        internal IPipelineBuilder Builder { get; }

        internal Func<object>? CreateGrpcClient { get; set; }

        public IServiceProvider? Services
        {
            get
            {
                if (_services == null)
                    throw new BardConfigurationException(
                        $"Error Accessing {nameof(ScenarioContext)} {nameof(Services)} property. It has not been set in {nameof(ScenarioConfiguration)} {nameof(ScenarioConfiguration.Configure)} method.");

                return _services;
            }
            set => _services = value;
        }

        public IApi Api { get; }

        public LogWriter Writer { get; }

        internal object? ExecutePipeline()
        {
            return Builder.Execute();
        }

        internal void AddPipelineStep(string stepName, Func<object?, object?> func)
        {
            Builder.AddStep(stepName, func);
        }
    }

    public class ScenarioContext<TStoryData> : ScenarioContext where TStoryData : class, new()
    {
        private TStoryData? _storyInput;

        internal ScenarioContext(ScenarioContext context) : base(context.Builder, context.Api, context.Writer,
            context.Services, context.CreateGrpcClient)
        {
        }

        public TStoryData StoryData
        {
            get
            {
                if (_storyInput == null)
                    throw new BardException($"{nameof(StoryData)} has not been set.");

                return _storyInput;
            }
        }

        internal void SetStoryData(TStoryData? input)
        {
            _storyInput = input;
        }
    }
}