using System;
using Bard.Infrastructure;

namespace Bard.Internal.Then
{
    internal class Then : IThen, IObserver<IResponse>
    {
        private readonly int? _maxElapsedTime;
        private readonly LogWriter _logWriter;
        private IResponse? _response;

        public Then(int? maxElapsedTime, LogWriter logWriter)
        {
            _maxElapsedTime = maxElapsedTime;
            _logWriter = logWriter;
        }

        public void OnCompleted()
        {
        }

        public void OnError(System.Exception error)
        {
        }

        public void OnNext(IResponse value)
        {
            _response = value;
        }

        public IResponse Response
        {
            get
            {
                if (_response == null)
                    throw new BardException("The api has not been called. Call When.Get(url))");

                _response.Log = true;
                _response.MaxElapsedTime = _maxElapsedTime;
                
                return _response;
            }
        }

        public ISnapshot Snapshot(params object[] extensions)
        {
            return new BardSnapshot(_logWriter, Response.ShouldBe, extensions);
        }
    }
}