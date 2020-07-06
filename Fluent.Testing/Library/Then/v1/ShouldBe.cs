using System.Net;

namespace Fluent.Testing.Library.Then.v1
{
    public class ShouldBe : ShouldBeBase, IShouldBe 
    {
        public void BadRequest()
        {
            StatusCodeShouldBe(HttpStatusCode.BadRequest);
        }
    }
}