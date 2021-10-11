using System;
using System.Collections.Generic;
using System.Net.Http;

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
        /// <param name="requestSetup">configure outgoing http request (optional)</param>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <returns>IResponse</returns>
        IResponse Put<TModel>(string route, TModel model, Action<HttpRequestMessage>? requestSetup = null);

        /// <summary>
        ///     Send a POST request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="requestSetup">configure outgoing http request (optional)</param>
        /// <returns>IResponse</returns>
        IResponse Post(string route, Action<HttpRequestMessage>? requestSetup = null);
        
        /// <summary>
        ///     Send a POST request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="model">the model to send</param>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <param name="requestSetup">configure outgoing http request (optional)</param>
        /// <returns>IResponse</returns>
        IResponse Post<TModel>(string route, TModel model, Action<HttpRequestMessage>? requestSetup = null);

        /// <summary>
        ///     Send a GET request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="name">name of the query string parameter</param>
        /// <param name="value">value of the query string parameters</param>
        /// <param name="requestSetup">configure outgoing http request (optional)</param>
        /// <returns>IResponse</returns>
        IResponse Get(string route, string name, string value, Action<HttpRequestMessage>? requestSetup = null);

        /// <summary>
        ///     Send a GET request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="queryParameters">A collection of name value query pairs to append.</param>
        /// <param name="requestSetup">configure outgoing http request (optional)</param>
        /// <returns>IResponse</returns>
        IResponse Get(string route, IDictionary<string, string> queryParameters, Action<HttpRequestMessage>? requestSetup = null);

        /// <summary>
        ///     Send a GET request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="requestSetup">configure outgoing http request (optional)</param>
        /// <returns>IResponse</returns>
        IResponse Get(string route, Action<HttpRequestMessage>? requestSetup = null);

        /// <summary>
        ///     Send a Delete request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="requestSetup">configure outgoing http request (optional)</param>
        /// <returns>IResponse</returns>
        IResponse Delete(string route, Action<HttpRequestMessage>? requestSetup = null);

        /// <summary>
        ///     Send a Patch request to the specified route
        /// </summary>
        /// <param name="route">the route to call</param>
        /// <param name="model">The model to patch</param>
        /// <param name="requestSetup">configure outgoing http request (optional)</param>
        /// <returns>IResponse</returns>
        IResponse Patch<TModel>(string route, TModel model, Action<HttpRequestMessage>? requestSetup = null);

        /// <summary>
        ///     Send a request
        /// </summary>
        /// <param name="requestSetup">configure outgoing http request</param>
        /// <returns>IResponse</returns>
        IResponse Send(Action<HttpRequestMessage> requestSetup);
    }
}