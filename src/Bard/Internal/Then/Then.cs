using System;
using Bard.Internal.Exception;

namespace Bard.Internal.Then
{
    internal class Then : IThen, IObserver<IResponse>
    {
        private IResponse? _response;

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
                return _response;
            }
        }
    }
}