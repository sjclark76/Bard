using System;

namespace Fluent.Testing.Library.Infrastructure
{
    public class ScenarioStepException : Exception 
    {
        public ScenarioStepException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}