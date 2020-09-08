using System;
using System.Net;
using Bard.Infrastructure;
using Bard.Internal.When;
using Snapshooter;
using Snapshooter.Core;

namespace Bard.Internal.Then
{
    internal class Response : IResponse, ITime
    {
        private readonly ApiResult _apiResult;
        private readonly Headers _headers;
        private readonly LogWriter _logWriter;
        private readonly ShouldBe _shouldBe;
        private int? _maxElapsedTime;

        internal Response(EventAggregator eventAggregator, ApiResult apiResult, IBadRequestProvider badRequestProvider,
            LogWriter logWriter)
        {
            _apiResult = apiResult;
            _logWriter = logWriter;
            _shouldBe = new ShouldBe(apiResult, badRequestProvider, logWriter);
            _headers = new Headers(apiResult, logWriter);
            eventAggregator.Subscribe(_shouldBe);
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

        public void Snapshot<T>(Func<MatchOptions, MatchOptions>? matchOptions = null)
        {
            var snapShooter = new Snapshooter.Snapshooter(
                new SnapshotAssert(
                    new SnapshotSerializer(),
                    new SnapshotFileHandler(),
                    new SnapshotEnvironmentCleaner(
                        new SnapshotFileHandler()),
                    new JsonSnapshotComparer(
                        new BardAssert(),
                        new SnapshotSerializer())),
                new SnapshotFullNameResolver(
                    new BardSnapshotFullNameReader()));

            var content = _shouldBe.Content<T>();
            snapShooter.AssertSnapshot(content, snapShooter.ResolveSnapshotFullName(), matchOptions);
        }

        public ITime Time => this;

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
            _logWriter.LogHeaderMessage($"THEN THE RESPONSE SHOULD BE LESS THAN {milliseconds} MILLISECONDS");
            _logWriter.WriteHttpResponseToConsole(_apiResult);

            _apiResult.AssertElapsedTime(milliseconds);
        }
    }
}