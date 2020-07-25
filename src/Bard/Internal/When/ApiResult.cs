using System.Net.Http;

namespace Bard.Internal.When
{
    internal class ApiResult
    {
        internal ApiResult(HttpResponseMessage responseMessage, string responseString)
        {
            ResponseMessage = responseMessage;
            ResponseString = responseString;
        }

        public HttpResponseMessage ResponseMessage { get; }
        public string ResponseString { get; }
    }
}