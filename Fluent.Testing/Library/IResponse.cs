using System.Net;

namespace Fluent.Testing.Library
{
    public interface IResponse
    {
        IShouldBe ShouldBe { get; }

        void StatusCodeShouldBe(HttpStatusCode statusCode);

        T Content<T>();
    }
}