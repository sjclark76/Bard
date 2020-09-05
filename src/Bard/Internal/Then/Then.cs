using System;

namespace Bard.Internal.Then
{
    internal class Then : IThen, IObserver<IResponse>
    {
        private readonly int? _maxElapsedTime;
        private IResponse? _response;

        public Then(int? maxElapsedTime)
        {
            _maxElapsedTime = maxElapsedTime;
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
    }
}