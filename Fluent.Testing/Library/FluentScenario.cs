using System;
using System.Net.Http;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library
{
    public class ScenarioContext
    {
        private readonly PipelineBuilder _pipelineBuilder;
        public Api Api { get; }
        public LogWriter Writer { get; }

        public ScenarioContext(PipelineBuilder pipelineBuilder, Api api, LogWriter logWriter)
        {
            _pipelineBuilder = pipelineBuilder;
            Api = api;
            Writer = logWriter;
        }

        public object? ExecutePipeline()
        {
           return _pipelineBuilder.Execute();
        }

        public void AddPipelineStep(Func<object, object> func)
        {
            _pipelineBuilder.AddStep(func);
        }
    }

    internal class FluentScenario<T> : IFluentScenario<T> where T : BeginAScenario, new()
    {
        private readonly Then.Then _then;

        public FluentScenario(HttpClient httpClient, LogWriter logWriter, IBadRequestProvider badRequestProvider,
            Func<T> createScenario)
        {
            var beginningScenario = createScenario();
            var context = new ScenarioContext(new PipelineBuilder(), new Api(httpClient, logWriter), logWriter);
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