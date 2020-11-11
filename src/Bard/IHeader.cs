namespace Bard
{
    /// <summary>
    /// </summary>
    public interface IHeaders
    {
        /// <summary>
        /// 
        /// </summary>
        IHeadersShould Should { get; }

        /// <summary>
        ///     Assert that the header is present
        /// </summary>
        /// <param name="headerName">The header name</param>
        /// <param name="headerValue">the header value</param>
        IHeaders ShouldInclude(string headerName, string? headerValue = null);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IHeadersShould
    {
        /// <summary>
        /// 
        /// </summary>
        IInclude Include { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IInclude
    {
        /// <summary>
        /// Assert the headers include a content type
        /// </summary>
        /// <param name="headerValue">the value (optional)</param>
        /// <returns></returns>
        IHeaders ContentType(string? headerValue = null);

        /// <summary>
        /// Assert the headers include a content length
        /// </summary>
        /// <param name="headerValue">the value (optional)</param>
        /// <returns></returns>
        IHeaders ContentLength(string? headerValue = null);

        /// <summary>
        /// Assert the headers include a location
        /// </summary>
        /// <param name="headerValue">the value (optional)</param>
        /// <returns></returns>
        IHeaders Location(string? headerValue = null);
    }
}