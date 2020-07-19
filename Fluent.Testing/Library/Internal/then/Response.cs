using System.Net;
using Fluent.Testing.Library.Internal.When;

namespace Fluent.Testing.Library.Internal.Then
{
    internal class Response : IResponse
    {
        private readonly ShouldBe _shouldBe;

        public Response(ApiResult result, IBadRequestProvider badRequestProvider)
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