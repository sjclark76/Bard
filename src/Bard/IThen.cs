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
        
        /// <summary>
        /// Fluent interface for performing Snapshot testing.
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        ISnapshot Snapshot(params object[] suffix);
    }
}