using Fluent.Testing.Library.Then.v1;

namespace Fluent.Testing.Library.Then
{
    public interface IThen<out TShouldBe> where TShouldBe : IShouldBeBase
    {
        IResponse<TShouldBe> Response { get; }
    }
}