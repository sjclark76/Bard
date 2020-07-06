namespace Fluent.Testing.Library.Then.v2
{
    public interface IShouldBe : IShouldBeBase
    {
        IBadRequestResponse? BadRequest { get; }
    }
}