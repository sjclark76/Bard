using System;
using System.Net.Http;
using Bard.Configuration;
using Bard.Infrastructure;
using Bard.Internal.Given;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class FluentScenario : IFluentScenario
    {
        private readonly Then.Then _then;

        internal FluentScenario(ScenarioOptions options) : this(options.Client, options.LogMessage,
            options.BadRequestProvider, options.Services)
        {
        }

        protected FluentScenario(HttpClient? client, Action<string> logMessage, IBadRequestProvider badRequestProvider,
            IServiceProvider? services)
        {
            if (client == null)
                throw new Exception("client not set.");

            var logWriter = new LogWriter(logMessage);
            var api = new Api(client, logWriter, badRequestProvider);
            var pipeline = new PipelineBuilder(logWriter);

            Context = new ScenarioContext(pipeline, api, logWriter, services);

            var when = new When.When(api, logWriter, 
                () => Context.ExecutePipeline());
            
            When = when;
            
            _then = new Then.Then();
            
            _then.Subscribe(api);
            pipeline.Subscribe(api);
        }

        protected ScenarioContext Context { get; }

        public IWhen When { get; }

        public IThen Then => _then;
    }

    internal class FluentScenario<T> : FluentScenario, IFluentScenario<T> where T : StoryBook, new()
    {
        internal FluentScenario(ScenarioOptions<T> options) : base(options.Client, options.LogMessage,
            options.BadRequestProvider, options.Services)
        {
            var story = options.Story;
            story.Context = Context;

            Given = new Given<T>(story, () => Context.ExecutePipeline());
        }

        public IGiven<T> Given { get; }
    }
    
    
}