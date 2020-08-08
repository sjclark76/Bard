using System;
using System.Net.Http;
using Bard.Configuration;
using Bard.Infrastructure;
using Bard.Internal.Exception;
using Bard.Internal.Given;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class Scenario : IScenario
    {
        private readonly Then.Then _then;

        internal Scenario(ScenarioOptions options) : this(options.Client, options.LogMessage,
            options.BadRequestProvider, options.Services)
        {
        }

        protected Scenario(HttpClient? client, Action<string> logMessage, IBadRequestProvider badRequestProvider,
            IServiceProvider? services)
        {
            if (client == null)
                throw new BardConfigurationException("client not set.");

            var logWriter = new LogWriter(logMessage);
            var eventAggregator = new EventAggregator();
            
            var bardClient = HttpClientBuilder.GenerateBardClient(client, logWriter, badRequestProvider, eventAggregator);
            var api = new Api(bardClient, badRequestProvider, eventAggregator);
            var pipeline = new PipelineBuilder(logWriter);

            Context = new ScenarioContext(pipeline, api, logWriter, services);

            var when = new When.When(api, logWriter,
                () => Context.ExecutePipeline());

            When = when;

            _then = new Then.Then();

            eventAggregator.Subscribe(_then);
            eventAggregator.Subscribe(pipeline);
        }

        protected ScenarioContext Context { get; }

        public IWhen When { get; }

        public IThen Then => _then;
    }

    internal class Scenario<TStoryBook, TStoryData> : Scenario, IScenario<TStoryBook, TStoryData>
        where TStoryBook : StoryBook<TStoryData>, new() where TStoryData : class, new()
    {
        internal Scenario(ScenarioOptions<TStoryBook, TStoryData> options) : base(options.Client, options.LogMessage,
            options.BadRequestProvider, options.Services)
        {
            var story = options.Story;
            story.Context = new ScenarioContext<TStoryData>(Context);

            Given = new Given<TStoryBook, TStoryData>(story, () => Context.ExecutePipeline());
        }

        public IGiven<TStoryBook, TStoryData> Given { get; }
    }
}