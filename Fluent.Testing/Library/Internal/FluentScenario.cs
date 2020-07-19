using System;
using Fluent.Testing.Library.Configuration;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Internal.Given;
using Fluent.Testing.Library.Internal.When;

namespace Fluent.Testing.Library.Internal
{
    internal class FluentScenario<T> : IFluentScenario<T> where T : StoryBook, new()
    {
        private readonly Then.Then _then;

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

            _then = new Then.Then();
        }

        public IGiven<T> Given { get; }

        public IWhen When { get; }

        public IThen Then => _then;
    }
}