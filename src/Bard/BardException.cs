namespace Bard
{
    /// <summary>
    /// General BardException
    /// </summary>
    public class BardException : System.Exception
    {
        internal BardException(string? message) : base(message)
        {
        }
    }
}