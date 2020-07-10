using System;
using System.Net.Http;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library
{
    internal class Orchestrator
    {
    }

    internal class FluentScenario<T> : IFluentScenario<T> where T : BeginAScenario, new()
    {
        private readonly Then.Then _then;

        public FluentScenario(HttpClient httpClient, LogWriter logWriter, IBadRequestProvider badRequestProvider,
            Func<T> createScenario)
        {
            var beginningScenario = createScenario();
         
            Given = new Given<T>(beginningScenario);

            _then = new Then.Then();

            When = new When.When(httpClient, logWriter, badRequestProvider, response => _then.Response = response);
        }

        public IGiven<T> Given { get; }

        public IWhen When { get; }

        public IThen Then => _then;
    }
}