using System;
using Bard.Infrastructure;
using Newtonsoft.Json;

namespace Bard
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