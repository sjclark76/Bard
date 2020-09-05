using System;
using System.Net.Http;

namespace Bard.Internal.When
{
    internal class ApiResult
    {
        internal ApiResult(HttpResponseMessage responseMessage, string responseString,
            TimeSpan? elapsedTime = null)
        {
            ResponseMessage = responseMessage;
            ResponseString = responseString;
            ElapsedTime = elapsedTime;
        }

        public TimeSpan? ElapsedTime { get; set; }

        public HttpResponseMessage ResponseMessage { get; }
        public string ResponseString { get; }
    }
}