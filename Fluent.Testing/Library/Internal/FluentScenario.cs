using System;
using System.Net.Http;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Internal.Given;
using Fluent.Testing.Library.Internal.When;

namespace Fluent.Testing.Library.Internal
{
    internal class FluentScenario<T> : IFluentScenario<T> where T : BeginAScenario, new()
    {
        private readonly Then.Then _then;

        public FluentScenario(HttpClient httpClient, LogWriter logWriter, IBadRequestProvider badRequestProvider,
            Func<T> createScenario)
        {
            var beginningScenario = createScenario();
            var context = new ScenarioContext(new PipelineBuilder(logWriter), new Api(httpClient, logWriter, badRequestProvider), logWriter);
            beginningScenario.Context = context;
            
            Given = new Given<T>(beginningScenario);
            
            When = new When.When(httpClient, logWriter, badRequestProvider,
                () => context.ExecutePipeline(), 
                response => _then.Response = response);

            _then = new Then.Then();
        }

        public IGiven<T> Given { get; }

        public IWhen When { get; }

        public IThen Then => _then;
    }
}