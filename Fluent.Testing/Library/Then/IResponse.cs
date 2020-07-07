using System.Net;
using Fluent.Testing.Library.Then.Advanced;

namespace Fluent.Testing.Library.Then
{
    public interface IResponse
    {
        IShouldBe ShouldBe { get; }

        void StatusCodeShouldBe(HttpStatusCode statusCode);

        T Content<T>();
    }
}