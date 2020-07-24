using System.Net;

namespace Bard
{
    public interface IResponse
    {
        IShouldBe ShouldBe { get; }

        void StatusCodeShouldBe(HttpStatusCode statusCode);

        T Content<T>();
    }
}