using System.Net.Http;

namespace Fluent.Testing.Library.When
{
    public class ApiResult
    {
        public HttpResponseMessage ResponseMessage { get; }
        public string ResponseString { get; }

        public ApiResult(HttpResponseMessage responseMessage, string responseString)
        {
            ResponseMessage = responseMessage;
            ResponseString = responseString;
        }
    }
}