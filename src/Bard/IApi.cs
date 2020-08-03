using System.Collections.Generic;

namespace Bard
{
    public interface IApi
    {
        IResponse Put<TModel>(string route, TModel model);
        IResponse Post<TModel>(string route, TModel model);
        IResponse Get(string uri, string name, string value);
        IResponse Get(string uri, IDictionary<string, string> queryParameters);
        IResponse Get(string route);
        IResponse Delete(string route);
        IResponse Patch<TModel>(string route, TModel model);
    }
}