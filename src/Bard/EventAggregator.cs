using System;
using System.Collections.Generic;

namespace Bard
{
    internal class EventAggregator : IObservable<IResponse>
    {
        private readonly List<IObserver<IResponse>> _observers;

        internal EventAggregator()
        {
            _observers = new List<IObserver<IResponse>>();
        }

        public IDisposable Subscribe(IObserver<IResponse> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!_observers.Contains(observer)) _observers.Add(observer);
            return new UnSubscriber(_observers, observer);
        }

        public void PublishResponse(IResponse response)
        {
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

        private class UnSubscriber : IDisposable
        {
            private readonly IObserver<IResponse> _observer;
            private readonly List<IObserver<IResponse>> _observers;

            public UnSubscriber(List<IObserver<IResponse>> observers, IObserver<IResponse> observer)
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