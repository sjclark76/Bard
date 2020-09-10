using System;

namespace Bard
{
    /// <summary>
    /// General BardException
    /// </summary>
    public class BardException : Exception
    {
        internal BardException(string? message) : base(message)
        {
        }
    }
}