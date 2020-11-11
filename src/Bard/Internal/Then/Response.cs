using System;
using System.Net;
using Bard.Infrastructure;
using Bard.Internal.When;

namespace Bard.Internal.Then
{
    internal class Response : IResponse, ITime, IObserver<Func<IResponse>>
    {
        private readonly ApiResult _apiResult;
        private readonly Headers _headers;
        private readonly LogWriter _logWriter;
        private readonly ShouldBe _shouldBe;
        private Func<IResponse>? _apiRequest;
        private int? _maxElapsedTime;

        internal Response(EventAggregator eventAggregator, ApiResult? apiResult, IBadRequestProvider badRequestProvider,
            LogWriter logWriter)
        {
            _apiResult = apiResult ?? throw new BardException("apiResult cannot be null");
            _logWriter = logWriter;
            _shouldBe = new ShouldBe(apiResult, badRequestProvider, logWriter, logWriter.Serializer);
            _headers = new Headers(apiResult, logWriter);
            eventAggregator.Subscribe(_shouldBe);
            eventAggregator.SubscribeToApiRequests(_shouldBe);
            eventAggregator.SubscribeToApiRequests(_headers);
            eventAggregator.SubscribeToApiRequests(this);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(System.Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Func<IResponse> apiRequest)
        {
            _apiRequest = apiRequest;
        }

        public IShouldBe ShouldBe => _shouldBe;

        public IHeaders Headers => _headers;

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
            _logWriter.WriteHttpResponseToConsole(_apiResult);
        }

        bool IResponse.ExceededElapsedTime(int? milliseconds)
        {
            return _apiResult.ExceededElapsedTime(milliseconds);
        }

        public ITime Time => this;

        TimeSpan? IResponse.ElapsedTime => _apiResult.ElapsedTime;

        int? IResponse.MaxElapsedTime
        {
            get => _maxElapsedTime;
            set
            {
                _maxElapsedTime = value;
                _headers.MaxElapsedTime = value;
                _shouldBe.MaxElapsedTime = value;
            }
        }

        public void LessThan(int milliseconds)
        {
            var performanceMonitor = new PerformanceMonitor(_logWriter);

            performanceMonitor.AssertElapsedTime(_apiRequest, _apiResult, milliseconds);
        }
    }
}