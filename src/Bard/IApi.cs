using System.Collections.Generic;

namespace Bard
{
    /// <summary>
    ///     Bard API wrapper round an HTTPClient
    /// </summary>
    public interface IApi
    {
        /// <summary>
        ///     Send a PUT request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="model">the model to send</param>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <returns>IResponse</returns>
        IResponse Put<TModel>(string route, TModel model);

        /// <summary>
        ///     Send a POST request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="model">the model to send</param>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <returns>IResponse</returns>
        IResponse Post<TModel>(string route, TModel model);

        /// <summary>
        ///     Send a GET request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="name">name of the query string parameter</param>
        /// <param name="value">value of the query string parameters</param>
        /// <returns>IResponse</returns>
        IResponse Get(string route, string name, string value);

        /// <summary>
        ///     Send a GET request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="queryParameters">A collection of name value query pairs to append.</param>
        /// <returns>IResponse</returns>
        IResponse Get(string route, IDictionary<string, string> queryParameters);

        /// <summary>
        ///     Send a GET request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <returns>IResponse</returns>
        IResponse Get(string route);

        /// <summary>
        ///     Send a Delete request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <returns>IResponse</returns>
        IResponse Delete(string route);

        /// <summary>
        ///     Send a Patch request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="model">The model to patch</param>
        /// <returns>IResponse</returns>
        IResponse Patch<TModel>(string route, TModel model);
    }
}