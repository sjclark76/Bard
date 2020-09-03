using System;
using System.Net.Http;
using System.Text;
using Bard.Infrastructure;
using Bard.Internal.Exception;
using Bard.Internal.When;
using Microsoft.Net.Http.Headers;
using EntityTagHeaderValue = System.Net.Http.Headers.EntityTagHeaderValue;

namespace Bard.Internal.Then
{
    internal class Header : IHeader, IInclude, IHeaderShould
    {
        private readonly LogWriter _logWriter;
        private readonly HttpResponseMessage _responseMessage;
        private string _contentType;

        public Header(ApiResult apiResult, LogWriter logWriter)
        {
            _logWriter = logWriter;
            _responseMessage = apiResult.ResponseMessage;
        }

        public IHeaderShould Should => this;

        string IHeader.ContentType => _responseMessage.Content.Headers.ContentType.ToString();
         long? IHeader.ContentLength => _responseMessage.Content.Headers.ContentLength;
         DateTimeOffset? IHeader.Expires => _responseMessage.Content.Headers.Expires;
         DateTimeOffset? IHeader.LastModified => _responseMessage.Content.Headers.LastModified;
         Uri? IHeader.ContentLocation => _responseMessage.Content.Headers.ContentLocation;
        EntityTagHeaderValue IHeader.ETag => _responseMessage.Headers.ETag;


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

        public void ContentType()
        {
            var headerMessage =
                new StringBuilder($"THEN THE RESPONSE SHOULD INCLUDE THE HTTP HEADER '{nameof(_responseMessage.Content.Headers.ContentType)}'");
                
            // if (statusCode != httpStatusCode)
            //     headerMessage.Append($" BUT WAS HTTP {(int) statusCode} {statusCode}");

            _logWriter.LogHeaderMessage(headerMessage.ToString());
            
            var foo = _responseMessage.Content.Headers.ContentType.Equals(true);

            if (foo == false)
            {
                throw new BardException("blah ");
            }
            
            _logWriter.WriteHttpResponseToConsole(_responseMessage);
        }

        public IInclude Include => this;
    }
}