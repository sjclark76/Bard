using System.Net;

namespace Fluent.Testing.Library.Then
{
    public abstract class ShouldBeBase : IShouldBeBase
    {
        public void StatusCodeShouldBe(HttpStatusCode statusCode)
        {
            // _httpResponse?.StatusCode.ShouldBe(statusCode,
            //     $"Status code mismatch, response was {_httpResponse.StatusCode}");
        }
        
        public void Ok()
        {
            StatusCodeShouldBe(HttpStatusCode.OK);
        }

        public void NoContent()
        {
            throw new System.NotImplementedException();
        }

        public T Ok<T>()
        {
            throw new System.NotImplementedException();
        }

        public T Created<T>()
        {
            throw new System.NotImplementedException();
        }

        public void Forbidden()
        {
            throw new System.NotImplementedException();
        }

        public void NotFound()
        {
            throw new System.NotImplementedException();
        }
    }
}