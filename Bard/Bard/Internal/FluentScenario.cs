using System;
using Bard.Configuration;
using Bard.Infrastructure;
using Bard.Internal.given;
using Bard.Internal.then;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class FluentScenario<T> : IFluentScenario<T> where T : StoryBook, new()
    {
        private readonly Then _then;

        public FluentScenario(ScenarioOptions<T> options)
        {
            if (options.Client == null)
                throw new Exception("Use method must be called first.");

            var logWriter = new LogWriter(options.LogMessage);
            var context = new ScenarioContext(new PipelineBuilder(logWriter),
                new Api(options.Client, logWriter, options.BadRequestProvider), logWriter);

            var story = options.Story;
            story.Context = context;

            Given = new Given<T>(story);

            When = new When.When(options.Client, logWriter, options.BadRequestProvider,
                () => context.ExecutePipeline(),
                response => _then.Response = response);

            _then = new Then();
        }

        public IGiven<T> Given { get; }

        public IWhen When { get; }

        public IThen Then => _then;
    }
}