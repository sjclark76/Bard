using System;
using System.Text.Json;

namespace Bard
{
    /// <summary>
    ///     Abstract base class for defining custom BadRequest Provider
    /// </summary>
    /// <typeparam name="TErrorMessage">The custom error message returned by your API</typeparam>
    public abstract class BadRequestProvider<TErrorMessage> : BadRequestProviderBase
    {
        /// <summary>
        ///     Serialize the content response to your custom error message.
        /// </summary>
        /// <returns>Custom error message</returns>
        protected TErrorMessage Content()
        {
            TErrorMessage content = default!;

            try
            {
                content = JsonSerializer.Deserialize<TErrorMessage>(StringContent);
            }
            catch (Exception)
            {
                // ok..
            }

            return content ?? throw new Exception($"Unable to serialize to {typeof(TErrorMessage).FullName}");
        }
    }
}