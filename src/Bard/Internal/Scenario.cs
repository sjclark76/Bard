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
        private readonly Then.Then _then;
        protected Api _api;
        protected LogWriter _logWriter;
        protected When.When _when;

        internal Scenario(ScenarioOptions options) : this(options.Client, options.LogMessage,
            options.BadRequestProvider, options.Services)
        {
        }

        protected Scenario(HttpClient? client, Action<string> logMessage, IBadRequestProvider badRequestProvider,
            IServiceProvider? services)
        {
            if (client == null)
                throw new BardConfigurationException("client not set.");

            var eventAggregator = new EventAggregator();
            
            _logWriter = new LogWriter(logMessage, eventAggregator);

            var bardClient =
                HttpClientBuilder.GenerateBardClient(client, _logWriter, badRequestProvider, eventAggregator);
            _api = new Api(bardClient, badRequestProvider, eventAggregator);
            var pipeline = new PipelineBuilder(_logWriter);

            Context = new ScenarioContext(pipeline, _api, _logWriter, services);

            var when = new When.When(_api, _logWriter, () => { });

            _when = when;

            _then = new Then.Then();

            eventAggregator.Subscribe(_then);
            eventAggregator.Subscribe(pipeline);
        }

        protected ScenarioContext Context { get; }

        public IWhen When => _when;

        public IThen Then => _then;
    }

    internal class Scenario<TStoryBook, TStoryData> : Scenario, IScenario<TStoryBook, TStoryData>
        where TStoryBook : StoryBook<TStoryData>, new() where TStoryData : class, new()
    {
        private readonly TStoryBook _given;

        internal Scenario(ScenarioOptions<TStoryBook, TStoryData> options) : base(options.Client, options.LogMessage,
            options.BadRequestProvider, options.Services)
        {
            var context = new ScenarioContext<TStoryData>(Context);

            var story = options.StoryBook;
            story.Context = context;

            _when = new When.When(_api, _logWriter, () => context.ExecutePipeline());

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