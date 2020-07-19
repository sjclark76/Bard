using System;

namespace Bard.Infrastructure
{
    public class ScenarioStepException : Exception 
    {
        public ScenarioStepException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}