namespace Fluent.Testing.Library.Then.Advanced
{
    public interface IShouldBe : IShouldBeBase
    {
        IBadRequestResponse BadRequest { get; }
    }
}