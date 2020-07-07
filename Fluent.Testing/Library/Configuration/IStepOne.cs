using System;

namespace Fluent.Testing.Library.Configuration
{
    public interface IHttpClientProvided
    {
        ILoggerProvided Log(Action<string> logMessage);
    }
}