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
        /// <param name="extensions">Creates a new snapshot name extension from the given objects.</param>
        /// <returns></returns>
        ISnapshot Snapshot(params object[] extensions);
    }
}