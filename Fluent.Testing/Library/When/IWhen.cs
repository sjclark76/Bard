using System.Collections.Generic;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.Then.v1;

namespace Fluent.Testing.Library.When
{
    public interface IWhen<out TShouldBe> where TShouldBe : IShouldBeBase
    {
        IResponse<TShouldBe> Put<TModel>(string route, TModel model);
        IResponse<TShouldBe> Post<TModel>(string route, TModel model);
        IResponse<TShouldBe> Get(string uri, string name, string value);
        IResponse<TShouldBe> Get(string uri, IDictionary<string, string> queryParameters);
        IResponse<TShouldBe> Get(string route);
    }
}