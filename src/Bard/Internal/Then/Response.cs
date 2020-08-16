using System.Net;
using Bard.Infrastructure;
using Bard.Internal.When;

namespace Bard.Internal.Then
{
    internal class Response : IResponse
    {
        private readonly ShouldBe _shouldBe;

        internal Response(EventAggregator eventAggregator, ApiResult result, IBadRequestProvider badRequestProvider, LogWriter logWriter)
        {
            _shouldBe = new ShouldBe(result, badRequestProvider, logWriter);
            eventAggregator.Subscribe(_shouldBe);
        }

        public IShouldBe ShouldBe => _shouldBe;

        bool IResponse.Log
        {
            get => _shouldBe.Log;
            set => _shouldBe.Log = value;
        }

        public void StatusCodeShouldBe(HttpStatusCode statusCode)
        {
            ShouldBe.StatusCodeShouldBe(statusCode);
        }

        public T Content<T>()
        {
            return _shouldBe.Content<T>();
        }
    }
}