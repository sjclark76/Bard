namespace Fluent.Testing.Library
{
    public interface IFluentTester
    {
        IWhen When { get; }
        IThen Then { get; }
    }
}