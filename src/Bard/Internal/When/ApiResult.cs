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

        internal void AssertElapsedTime(TimeSpan? elapsedTime, int? milliseconds)
        {
            if (ExceededElapsedTime(elapsedTime, milliseconds))
                throw new BardException(
                    $"The API response took longer than {milliseconds} milliseconds. ({elapsedTime?.TotalMilliseconds})");
        }

        private static bool ExceededElapsedTime(TimeSpan? elapsedTime, int? milliseconds)
        {
            return milliseconds.HasValue && elapsedTime != null && elapsedTime.Value.TotalMilliseconds > milliseconds;
        }
        
        internal bool ExceededElapsedTime(int? milliseconds)
        {
            return ExceededElapsedTime(ElapsedTime, milliseconds);
        }
    }
}