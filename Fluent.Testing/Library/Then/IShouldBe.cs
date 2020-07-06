namespace Fluent.Testing.Library.Then
{
    public interface IShouldBe
    {
        void Ok();

        void NoContent();

        T Ok<T>();

        T Created<T>();

        IBadRequestResponse BadRequest { get; }

        void Forbidden();

        void NotFound();
    }
}