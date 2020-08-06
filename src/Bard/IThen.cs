namespace Bard
{
    /// <summary>
    ///     Fluent Interface for Test Assertion
    /// </summary>
    public interface IThen
    {
        /// <summary>
        ///     Fluent interface for the API Response
        /// </summary>
        IResponse Response { get; }
    }
}