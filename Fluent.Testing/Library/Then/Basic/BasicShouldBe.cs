using System.Net;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library.Then.Basic
{
    public class BasicShouldBe : ShouldBeBase, IShouldBe
    {
        public BasicShouldBe(ApiResult apiResult) : base(apiResult)
        {
        }

        public void BadRequest()
        {
            StatusCodeShouldBe(HttpStatusCode.BadRequest);
        }
    }
}