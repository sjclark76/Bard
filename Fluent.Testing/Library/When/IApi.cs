using System.Collections.Generic;
using Fluent.Testing.Library.Then;

namespace Fluent.Testing.Library.When
{
    public interface IApi
    {
        IResponse Put<TModel>(string route, TModel model);
        IResponse Post<TModel>(string route, TModel model);
        IResponse Get(string uri, string name, string value);
        IResponse Get(string uri, IDictionary<string, string> queryParameters);
        IResponse Get(string route);
        IResponse Delete(string route);
    }
}