namespace Fluent.Testing
{
    public interface ITheResponse
    {
        IShouldBe ShouldBe { get; }

        T Content<T>();
    }
}