namespace Bard
{
    public class BardException : System.Exception
    {
        internal BardException(string? message) : base(message)
        {
        }
    }
}