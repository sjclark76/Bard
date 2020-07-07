using System;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library.Then
{
    public class Then : IThen
    {
        private readonly Func<ApiResult, IResponse> _responseFactory;
        private IResponse? _response;

        public Then(Func<ApiResult, IResponse> responseFactory)
        {
            _responseFactory = responseFactory;
        }

        public IResponse Response
        {
            get
            {
                if (_response == null)
                    throw new Exception("The api has not been called. Call When.Get(url))");

                return _response;
            }
        }

        public void SetTheResponse(ApiResult result)
        {
            _response = _responseFactory(result);
        }
    }
}