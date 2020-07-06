using System;

namespace Fluent.Testing.Library.Configuration
{
    public interface IStepOne
    {
        IStepTwo Log(Action<string> logMessage);
    }
}