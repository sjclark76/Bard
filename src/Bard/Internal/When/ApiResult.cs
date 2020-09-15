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

        public TimeSpan? ElapsedTime { get; }
        public HttpResponseMessage ResponseMessage { get; }
        public string ResponseString { get; }

        public void AssertElapsedTime(TimeSpan? elapsedTime, int? milliseconds)
        {
            if (ExceededElapsedTime(milliseconds))
                throw new BardException(
                    $"The API response took longer than {milliseconds} milliseconds. ({elapsedTime?.TotalMilliseconds})");
        }
        
        public void AssertElapsedTime(int? milliseconds)
        {
            AssertElapsedTime(ElapsedTime, milliseconds);
        }

        public bool ExceededElapsedTime(int? milliseconds)
        {
            return milliseconds.HasValue && ElapsedTime != null && ElapsedTime.Value.TotalMilliseconds > milliseconds;
        }
    }
}