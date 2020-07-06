using System.Net;
using Fluent.Testing.Library.Then.v1;

namespace Fluent.Testing.Library.Then
{
    public class EmptyResponse<TShouldBe> : IResponse<TShouldBe> where TShouldBe : ShouldBeBase, new()
    {
        public EmptyResponse()
        {
            ShouldBe = new TShouldBe();
        }

        public TShouldBe ShouldBe { get; }
        public void StatusCodeShouldBe(HttpStatusCode statusCode)
        {
            throw new System.NotImplementedException();
        }

        public T Content<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}