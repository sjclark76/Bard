using System;
using System.Collections.Generic;
using System.Net.Http;
using Bard.Internal.Then;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class BardHttpClient : HttpClient, IObservable<Response>
    {
        private readonly BardApiMessageHandler _messageHandler;
        private readonly IBadRequestProvider _badRequestProvider;
        private readonly List<IObserver<Response>> _observers;

        internal BardHttpClient(BardApiMessageHandler messageHandler, IBadRequestProvider badRequestProvider) : base(messageHandler)
        {
            _messageHandler = messageHandler;
            _messageHandler.PublishApiResult = NotifyObservers;
            _observers = new List<IObserver<Response>>();

            _badRequestProvider = badRequestProvider;
        }

        private void NotifyObservers(ApiResult apiResult)
        {
            var response = new Response(apiResult, _badRequestProvider);

            foreach (var observer in _observers)
                if (response == null)
                {
                    //observer.OnError(new LocationUnknownException());
                }
                else
                {
                    observer.OnNext(response);
                }
        }

        public IDisposable Subscribe(IObserver<Response> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!_observers.Contains(observer)) _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }
        
        private class Unsubscriber : IDisposable
        {
            private readonly IObserver<Response> _observer;
            private readonly List<IObserver<Response>> _observers;

            public Unsubscriber(List<IObserver<Response>> observers, IObserver<Response> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}