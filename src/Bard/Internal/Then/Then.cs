using System;

namespace Bard.Internal.Then
{
    internal class Then : IThen, IObserver<Response>
    {
        private IResponse? _response;
        private IDisposable? _unsubscriber;

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(Response value)
        {
            _response = value;
        }

        public IResponse Response
        {
            get
            {
                if (_response == null)
                    throw new BardException("The api has not been called. Call When.Get(url))");

                return _response;
            }
            set => _response = value;
        }

        public void Subscribe(IObservable<Response> provider)
        {
            if (provider != null)
                _unsubscriber = provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            _unsubscriber?.Dispose();
        }
    }
}