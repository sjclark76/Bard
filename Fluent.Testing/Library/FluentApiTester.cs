using System;
using System.Net.Http;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library
{
    internal class ScenarioHost : IInternalFluentApiTester
    {
        private readonly Then.Then _then;

        public ScenarioHost(HttpClient httpClient, LogWriter logWriter, Func<ApiResult, IResponse> responseFactory)
        {
            When = new When.When(this, httpClient, logWriter, responseFactory);
            _then = new Then.Then(responseFactory);
        }

        public IWhen When { get; }

        public IThen Then => _then;

        public void Publish(ApiResult apiResult)
        {
            _then.SetTheResponse(apiResult);
        }
    }
}