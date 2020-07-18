using System.Net.Http;

namespace Fluent.Testing.Library.Internal.When
{
    internal class ApiResult
    {
        public ApiResult(HttpResponseMessage responseMessage, string responseString)
        {
            ResponseMessage = responseMessage;
            ResponseString = responseString;
        }

        public HttpResponseMessage ResponseMessage { get; }
        public string ResponseString { get; }
    }
}