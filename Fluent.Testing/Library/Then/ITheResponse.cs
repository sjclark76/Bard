namespace Fluent.Testing.Library.Then
{
    public interface ITheResponse
    {
        IShouldBe ShouldBe { get; }

        T Content<T>();
    }
}