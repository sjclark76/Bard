using System.Net;
using Bard.Internal.When;

namespace Bard.Internal.Then
{
    internal class Response : IResponse
    {
        private readonly ShouldBe _shouldBe;

        internal Response(ApiResult result, IBadRequestProvider badRequestProvider)
        {
            _shouldBe = new ShouldBe(result, badRequestProvider);
        }

        public IShouldBe ShouldBe => _shouldBe;

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