using System.Net;
using Bard.Infrastructure;
using Bard.Internal.When;

namespace Bard.Internal.Then
{
    internal class Response : IResponse, ITime
    {
        private readonly ApiResult _apiResult;
        private readonly LogWriter _logWriter;
        private readonly ShouldBe _shouldBe;

        internal Response(EventAggregator eventAggregator, ApiResult apiResult, IBadRequestProvider badRequestProvider,
            LogWriter logWriter)
        {
            _apiResult = apiResult;
            _logWriter = logWriter;
            _shouldBe = new ShouldBe(apiResult, badRequestProvider, logWriter);
            eventAggregator.Subscribe(_shouldBe);
            Headers = new Headers(apiResult, logWriter);
        }

        public IShouldBe ShouldBe => _shouldBe;

        public IHeaders Headers { get; }

        bool IResponse.Log
        {
            get => _shouldBe.Log;
            set => _shouldBe.Log = value;
        }

        public void StatusCodeShouldBe(HttpStatusCode statusCode)
        {
            ShouldBe.StatusCodeShouldBe(statusCode);
        }

        public T Content<T>()
        {
            return _shouldBe.Content<T>();
        }

        public void WriteResponse()
        {
            _logWriter.WriteHttpResponseToConsole(_apiResult.ResponseMessage);
        }

        public ITime Time => this;

        public void LessThan(int milliseconds)
        {
            _logWriter.LogHeaderMessage($"THEN THE RESPONSE SHOULD BE LESS THAN {milliseconds} MILLISECONDS");
            _logWriter.WriteHttpResponseToConsole(_apiResult);
            
            if (_apiResult.ElapsedTime != null && _apiResult.ElapsedTime.Value.TotalMilliseconds > milliseconds)
            {
                throw new BardException($"The API response took longer than {milliseconds} milliseconds. ({milliseconds})");
            }
        }
    }
}