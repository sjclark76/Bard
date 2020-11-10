using System;
using System.Net.Http;
using System.Text.Json;
using Bard.Configuration;
using Bard.Infrastructure;
using Bard.Internal.Exception;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class Scenario : IScenario
    {
        private readonly IBadRequestProvider _badRequestProvider;
        private readonly HttpClient _client;
        private readonly EventAggregator _eventAggregator;

        private readonly Then.Then _then;
        protected readonly LogWriter LogWriter;
        protected When.When InternalWhen;

        internal Scenario(ScenarioOptions options, EventAggregator eventAggregator) : this(options.Client,
            options.LogMessage,
            options.BadRequestProvider, options.Services, options.MaxApiResponseTime, eventAggregator,
            options.JsonDeserializeOptions, options.JsonSerializeOptions)
        {
        }

        protected Scenario(HttpClient? client, Action<string> logMessage, IBadRequestProvider badRequestProvider,
            IServiceProvider? services, int? maxElapsedTime, EventAggregator eventAggregator,
            JsonSerializerOptions? jsonDeserializeOptions,
            JsonSerializerOptions? jsonSerializeOptions)
        {
            _client = client ?? throw new BardConfigurationException("client not set.");

            if (badRequestProvider is BadRequestProviderBase badRequestProviderBase)
            {
                badRequestProviderBase.SetSerializer(new BardJsonSerializer(jsonDeserializeOptions,
                    jsonSerializeOptions));
            }

            _badRequestProvider = badRequestProvider;
            _eventAggregator = eventAggregator;

            LogWriter = new LogWriter(logMessage, _eventAggregator,
                new BardJsonSerializer(jsonDeserializeOptions, jsonSerializeOptions));

            var pipeline = new PipelineBuilder(LogWriter);

            Context = new ScenarioContext(pipeline, FullLoggingApi(), LogWriter, services);

            var when = new When.When(RequestLoggingApi(), _eventAggregator, LogWriter);

            InternalWhen = when;

            _then = new Then.Then(maxElapsedTime, LogWriter);

            _eventAggregator.Subscribe(_then);
            _eventAggregator.Subscribe(pipeline);
        }

        protected ScenarioContext Context { get; }

        public IWhen When => InternalWhen;

        public IThen Then => _then;

        protected Api RequestLoggingApi()
        {
            var bardClient =
                HttpClientBuilder.CreateRequestLoggingClient(_client, LogWriter, _badRequestProvider, _eventAggregator);

            return new Api(bardClient);
        }

        private IApi FullLoggingApi()
        {
            var bardClient =
                HttpClientBuilder.CreateFullLoggingClient(_client, LogWriter, _badRequestProvider, _eventAggregator);

            return new Api(bardClient);
        }
    }

    internal class Scenario<TStoryBook, TStoryData> : Scenario, IScenario<TStoryBook, TStoryData>
        where TStoryBook : StoryBook<TStoryData>, new() where TStoryData : class, new()
    {
        private readonly TStoryBook _given;

        internal Scenario(ScenarioOptions<TStoryBook, TStoryData> options, EventAggregator eventAggregator) : base(
            options.Client, options.LogMessage,
            options.BadRequestProvider, options.Services, options.MaxApiResponseTime, eventAggregator,
            options.JsonDeserializeOptions, options.JsonSerializeOptions)
        {
            var context = new ScenarioContext<TStoryData>(Context);

            var story = options.StoryBook;
            story.Context = context;

            InternalWhen = new When.When(RequestLoggingApi(), eventAggregator, LogWriter,
                () => context.ExecutePipeline());

            _given = story;
        }

        public TStoryBook Given
        {
            get
            {
                Context.ExecutePipeline();
                return _given;
            }
        }
    }
}