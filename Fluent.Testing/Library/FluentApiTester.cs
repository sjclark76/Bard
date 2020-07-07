using System;
using System.Net.Http;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library
{
    internal class FluentApiTester<TShouldBe> : IInternalFluentApiTester<TShouldBe> where TShouldBe : ShouldBeBase 
    {
        private readonly Then<TShouldBe> _then;

        public FluentApiTester(HttpClient httpClient, LogWriter logWriter,
            Func<ApiResult, IResponse<TShouldBe>> responseFactory)
        {
            When = new When<TShouldBe>(this, httpClient, logWriter, responseFactory);
            _then = new Then<TShouldBe>(responseFactory);
        }

        public IWhen<TShouldBe> When { get; }

        public IThen<TShouldBe> Then => _then;

        public void Publish(ApiResult apiResult)
        {
            _then.SetTheResponse(apiResult);
        }
    }
}