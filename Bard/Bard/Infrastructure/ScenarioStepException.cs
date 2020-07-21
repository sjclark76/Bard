using System;

namespace Bard.Infrastructure
{
    public class ChapterException : Exception
    {
        public ChapterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}