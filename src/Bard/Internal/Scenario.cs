using System;
using System.Net.Http;
using Bard.Configuration;
using Bard.Infrastructure;
using Bard.Internal.Exception;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class Scenario : IScenario
    {
        private readonly HttpClient _client;
        private readonly IBadRequestProvider _badRequestProvider;

        private readonly Then.Then _then;
        protected readonly LogWriter LogWriter;
        protected When.When InternalWhen;
        private readonly EventAggregator _eventAggregator;

        internal Scenario(ScenarioOptions options) : this(options.Client, options.LogMessage,
            options.BadRequestProvider, options.Services, options.MaxApiResponseTime)
        {
        }

        protected Scenario(HttpClient? client, Action<string> logMessage, IBadRequestProvider badRequestProvider,
            IServiceProvider? services, int? maxElapsedTime)
        {
            _client = client ?? throw new BardConfigurationException("client not set.");
            _badRequestProvider = badRequestProvider;
            _eventAggregator = new EventAggregator();
            
            LogWriter = new LogWriter(logMessage, _eventAggregator);

            var pipeline = new PipelineBuilder(LogWriter);

            Context = new ScenarioContext(pipeline, FullLoggingApi(), LogWriter, services);

            var when = new When.When(RequestLoggingApi(), LogWriter);

            InternalWhen = when;

            _then = new Then.Then(maxElapsedTime, LogWriter);

            _eventAggregator.Subscribe(_then);
            _eventAggregator.Subscribe(pipeline);
        }

        protected Api RequestLoggingApi()
        {
            var bardClient = HttpClientBuilder.CreateRequestLoggingClient(_client, LogWriter, _badRequestProvider, _eventAggregator);
            
            return new Api(bardClient);
        }
        
        private IApi FullLoggingApi()
        {
            var bardClient = HttpClientBuilder.CreateFullLoggingClient(_client, LogWriter, _badRequestProvider, _eventAggregator);
            
            return new Api(bardClient);
        }

        protected ScenarioContext Context { get; }

        public IWhen When => InternalWhen;

        public IThen Then => _then;
    }

    internal class Scenario<TStoryBook, TStoryData> : Scenario, IScenario<TStoryBook, TStoryData>
        where TStoryBook : StoryBook<TStoryData>, new() where TStoryData : class, new()
    {
        private readonly TStoryBook _given;

        internal Scenario(ScenarioOptions<TStoryBook, TStoryData> options) : base(options.Client, options.LogMessage,
            options.BadRequestProvider, options.Services, options.MaxApiResponseTime)
        {
            var context = new ScenarioContext<TStoryData>(Context);

            var story = options.StoryBook;
            story.Context = context;

            InternalWhen = new When.When(RequestLoggingApi(), LogWriter, () => context.ExecutePipeline());

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