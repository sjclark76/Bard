using System.Net.Http;
using System.Text;
using Bard.Infrastructure;
using Bard.Internal.Exception;
using Bard.Internal.When;

namespace Bard.Internal.Then
{
    internal class Header : IHeader
    {
        private readonly LogWriter _logWriter;
        private readonly HttpResponseMessage _responseMessage;

        public Header(ApiResult apiResult, LogWriter logWriter)
        {
            _logWriter = logWriter;
            _responseMessage = apiResult.ResponseMessage;
        }

        public void ShouldInclude(string headerName)
        {
            var headerMessage =
                new StringBuilder($"THEN THE RESPONSE SHOULD INCLUDE THE HTTP HEADER '{headerName}'");
                
            // if (statusCode != httpStatusCode)
            //     headerMessage.Append($" BUT WAS HTTP {(int) statusCode} {statusCode}");

            _logWriter.LogHeaderMessage(headerMessage.ToString());
            
            var foo = _responseMessage.Headers.Contains(headerName);

            if (foo == false)
            {
                throw new BardException("blah ");
            }
            
            _logWriter.WriteHttpResponseToConsole(_responseMessage);
        }
    }
}