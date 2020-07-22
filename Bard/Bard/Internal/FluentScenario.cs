using System;
using System.Net.Http;
using Bard.Configuration;
using Bard.Infrastructure;
using Bard.Internal.given;
using Bard.Internal.then;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class FluentScenario : IFluentScenario
    {
        private readonly Then _then;

        public FluentScenario(ScenarioOptions options) : this(options.Client, options.LogMessage,
            options.BadRequestProvider)
        {
        }

        protected FluentScenario(HttpClient? client, Action<string> logMessage, IBadRequestProvider badRequestProvider)
        {
            if (client == null)
                throw new Exception("Use method must be called first.");

            var logWriter = new LogWriter(logMessage);
            
            Context = new ScenarioContext(new PipelineBuilder(logWriter),
                new Api(client, logWriter, badRequestProvider), logWriter);

            When = new When.When(client, logWriter, badRequestProvider,
                () => Context.ExecutePipeline(),
                response => _then.Response = response);

            _then = new Then();
        }

        protected ScenarioContext Context { get; set; }

        public IWhen When { get; }
        
        public IThen Then => _then;
    }

    internal class FluentScenario<T> : FluentScenario, IFluentScenario<T> where T : StoryBook, new()
    {
        public FluentScenario(ScenarioOptions<T> options) : base(options.Client, options.LogMessage,
            options.BadRequestProvider)
        {
            var story = options.Story;
            story.Context = Context;

            Given = new Given<T>(story);
        }

        public IGiven<T> Given { get; }
    }
}