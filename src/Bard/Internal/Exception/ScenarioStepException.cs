namespace Bard.Internal.Exception
{
    internal class ChapterException : System.Exception
    {
        public ChapterException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}