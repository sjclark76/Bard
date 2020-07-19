using System;

namespace Bard.Internal.then
{
    internal class Then : IThen
    {
        private IResponse? _response;

        public IResponse Response
        {
            get
            {
                if (_response == null)
                    throw new Exception("The api has not been called. Call When.Get(url))");

                return _response;
            }
            set => _response = value;
        }
    }
}