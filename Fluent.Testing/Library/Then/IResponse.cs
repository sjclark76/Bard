using System.Net;

namespace Fluent.Testing.Library.Then
{
    public interface IResponse<out TShouldBe> where TShouldBe : IShouldBeBase
    {
        TShouldBe ShouldBe { get; }

        void StatusCodeShouldBe(HttpStatusCode statusCode);

        T Content<T>();
    }
}