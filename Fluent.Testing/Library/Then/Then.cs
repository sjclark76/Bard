using System;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library.Then
{
    public class Then<TShouldBe> : IThen<TShouldBe> where TShouldBe : ShouldBeBase
    {
        private readonly Func<ApiResult, IResponse<TShouldBe>> _responseFactory;
        private IResponse<TShouldBe>? _response;

        public Then(Func<ApiResult, IResponse<TShouldBe>> responseFactory)
        {
            _responseFactory = responseFactory;
        }

        public IResponse<TShouldBe> Response
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