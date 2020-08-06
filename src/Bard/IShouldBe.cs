using System.Net;

namespace Bard
{
    /// <summary>
    /// Gateway fluent interface for API test response assertion
    /// </summary>
    public interface IShouldBe
    {
        /// <summary>
        /// Verify that the response was a bad request.
        /// </summary>
        IBadRequestProvider BadRequest { get; }
        
        /// <summary>
        /// Assert the response was a HTTP 200 Ok Response
        /// </summary>
        void Ok();

        /// <summary>
        /// Assert the response was a HTTP 204 No Content Response
        /// </summary>
        void NoContent();

        /// <summary>
        /// Assert the response was a HTTP 200 Ok Response
        /// </summary>
        T Ok<T>();

        /// <summary>
        /// Assert the response was a HTTP 201 Created Response
        /// </summary>
        void Created();

        /// <summary>
        /// Assert the response was a HTTP 201 Created Response
        /// </summary>
        T Created<T>();

        /// <summary>
        /// Assert the response was a HTTP 403 Forbidden Response
        /// </summary>
        void Forbidden();

        /// <summary>
        /// Assert the response was a HTTP 404 Not Found Response
        /// </summary>
        void NotFound();

        /// <summary>
        /// Assert the response matches the provided value
        /// </summary>
        void StatusCodeShouldBe(HttpStatusCode statusCode);
    }
}