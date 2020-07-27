using System;

namespace Bard.Internal
{
    public class BardException : Exception
    {
        public BardException(string? message) : base(message)
        {
        }
    }
}