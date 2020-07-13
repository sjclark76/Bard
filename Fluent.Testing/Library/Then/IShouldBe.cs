using System.Net;

namespace Fluent.Testing.Library.Then
{
    public interface IShouldBe
    {
        IBadRequestProvider BadRequest { get; }
        void Ok();

        void NoContent();

        T Ok<T>();

        void Created();

        T Created<T>();

        void Forbidden();

        void NotFound();

        void StatusCodeShouldBe(HttpStatusCode statusCode);
    }
}