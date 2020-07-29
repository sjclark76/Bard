using System;

namespace Bard.Internal
{
    public class BardConfigurationException : Exception
    {
        public BardConfigurationException(string? message) : base(message)
        {
        }
    }
}