using System;
using System.Collections.Generic;
using Bard.Internal.Then;

namespace Bard
{
    internal class EventAggregator : IObservable<IResponse>, IObservable<GrpcResponse>, IObservable<MessageLogged>
    {
        private readonly List<IObserver<GrpcResponse>> _grpcObservers;
        private readonly List<IObserver<MessageLogged>> _messageLoggedObservers;
        private readonly List<IObserver<IResponse>> _observers;

        internal EventAggregator()
        {
            _grpcObservers = new List<IObserver<GrpcResponse>>();
            _observers = new List<IObserver<IResponse>>();
            _messageLoggedObservers = new List<IObserver<MessageLogged>>();
        }

        public IDisposable Subscribe(IObserver<GrpcResponse> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!_grpcObservers.Contains(observer)) _grpcObservers.Add(observer);
            return new UnSubscriber<GrpcResponse>(_grpcObservers, observer);
        }

        public IDisposable Subscribe(IObserver<IResponse> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!_observers.Contains(observer)) _observers.Add(observer);
            return new UnSubscriber<IResponse>(_observers, observer);
        }

        public IDisposable Subscribe(IObserver<MessageLogged> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!_messageLoggedObservers.Contains(observer)) _messageLoggedObservers.Add(observer);
            return new UnSubscriber<MessageLogged>(_messageLoggedObservers, observer);
        }

        public void PublishResponse(IResponse response)
        {
            foreach (var observer in _observers)
                observer.OnNext(response);
        }

        public void PublishGrpcResponse(GrpcResponse response)
        {
            foreach (var observer in _grpcObservers)
                observer.OnNext(response);
        }

        public void PublishMessageLogged(MessageLogged messageLogged)
        {
            foreach (var observer in _messageLoggedObservers)
                observer.OnNext(messageLogged);
        }

        private class UnSubscriber<TResponse> : IDisposable
        {
            private readonly IObserver<TResponse> _observer;
            private readonly List<IObserver<TResponse>> _observers;

            public UnSubscriber(List<IObserver<TResponse>> observers, IObserver<TResponse> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observers .Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }

    internal class MessageLogged
    {
        public MessageLogged(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}