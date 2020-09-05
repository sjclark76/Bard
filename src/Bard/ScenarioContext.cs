using System;
using Bard.Configuration;
using Bard.Infrastructure;
using Bard.Internal;
using Bard.Internal.Exception;
using Microsoft.Extensions.DependencyInjection;

namespace Bard
{
    /// <summary>
    ///     Scenario Context allows state to be passed between stories.
    /// </summary>
    public class ScenarioContext
    {
        internal IServiceProvider? ServicesInternal;

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

        /// <summary>
        ///     Provides access to the instance of IServiceProvider to use Dependency Injection from within a story.
        /// </summary>
        /// <exception cref="BardConfigurationException">Throws if the Services has not been set during configuration.</exception>
        public IServiceProvider? Services
        {
            get
            {
                if (ServicesInternal == null)
                    throw new BardConfigurationException(
                        $"Error Accessing {nameof(ScenarioContext)} {nameof(Services)} property. It has not been set in {nameof(ScenarioConfiguration)} {nameof(ScenarioConfiguration.Configure)} method.");

                return ServicesInternal;
            }
            set => ServicesInternal = value;
        }

        /// <summary>
        ///     Provides access to your API client
        /// </summary>
        public IApi Api { get; }

        /// <summary>
        ///     Provides access to the LogWriter to output to the console window.
        /// </summary>
        public LogWriter Writer { get; }

        internal virtual void ExecutePipeline()
        {
            Builder.Execute();
        }

        internal void AddPipelineStep(string stepName, Action stepAction)
        {
            Builder.AddStep(stepName, stepAction);
        }
    }

    /// <summary>
    ///     Scenario Context allows state to be passed between stories.
    /// </summary>
    public class ScenarioContext<TStoryData> : ScenarioContext where TStoryData : class, new()
    {
        private readonly TStoryData? _storyData;

        internal ScenarioContext(ScenarioContext context) : base(context.Builder, context.Api, context.Writer,
            context.ServicesInternal, context.CreateGrpcClient)
        {
            _storyData = new TStoryData();
        }

        /// <summary>
        ///     The story data that is passed from story to story
        /// </summary>
        /// <exception cref="BardException">
        ///     If something has gone horribly wrong internally.+
        /// </exception>
        public TStoryData StoryData
        {
            get
            {
                if (_storyData == null)
                    throw new BardException($"{nameof(StoryData)} has not been set.");

                return _storyData;
            }
        }

        internal override void ExecutePipeline()
        {
            Builder.Execute(_storyData);
        }
    }
}