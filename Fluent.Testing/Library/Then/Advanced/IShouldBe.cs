namespace Fluent.Testing.Library.Then.Advanced
{
    public interface IShouldBe : IShouldBeBase
    {
        IBadRequestProvider BadRequest { get; }
    }
}