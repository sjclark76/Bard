using System;
using Fluent.Testing.Library.Infrastructure;
using Newtonsoft.Json;

namespace Fluent.Testing.Library
{
    public abstract class BadRequestProvider<TErrorMessage> : BadRequestProviderBase
    {
        protected TErrorMessage Content()
        {
            TErrorMessage content = default!;

            try
            {
                if (StringContent != null)
                    content = JsonConvert.DeserializeObject<TErrorMessage>(StringContent,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new ResolvePrivateSetters()
                        });
            }
            catch (Exception)
            {
                // ok..
            }

            return content ?? throw new Exception($"Unable to serialize to {typeof(TErrorMessage).FullName}");
        }
    }
}