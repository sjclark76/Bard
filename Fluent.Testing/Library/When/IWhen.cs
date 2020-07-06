using System.Collections.Generic;
using Fluent.Testing.Library.Then;

namespace Fluent.Testing.Library.When
{
    public interface IWhen
    {
        ITheResponse Put<TModel>(string route, TModel model);
        ITheResponse Post<TModel>(string route, TModel model);
        ITheResponse Get(string uri, string name, string value);
        ITheResponse Get(string uri, IDictionary<string, string> queryParameters);
        ITheResponse Get(string route);
    }
}