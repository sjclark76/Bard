using System;
using System.Net.Http;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library
{
    internal class FluentScenario<T> : IFluentScenario<T> where T : BeginAScenario, new()
    {
        private readonly Then.Then _then;

        public FluentScenario(HttpClient httpClient, LogWriter logWriter, Func<ApiResult, IResponse> responseFactory, Func<T> createScenario)
        {
            var beginningScenario = createScenario();
            
            Given = new Given<T>(createScenario());
            _then = new Then.Then(responseFactory);
            When = new When.When(httpClient, logWriter, responseFactory, _then.SetTheResponse);
        }

        public IGiven<T> Given { get; }

        public IWhen When { get; }

        public IThen Then => _then;
    }
}