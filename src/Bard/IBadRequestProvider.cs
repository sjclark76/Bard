using System;
using System.Linq.Expressions;

namespace Bard
{
    /// <summary>
    /// Interface for defining custom BadRequest Provider
    /// </summary>
    public interface IBadRequestProvider
    {
        /// <summary>
        /// Return the bad request as a string
        /// </summary>
        /// <returns>string</returns>
        string StringContent { get; set; }

        /// <summary>
        /// Should contain the property name.
        /// </summary>
        /// <param name="expression">define the property</param>
        /// <returns>IBadRequestProvider</returns>
        IBadRequestProvider ForProperty<TCommand>(Expression<Func<TCommand, object?>> expression);

        /// <summary>
        /// Should contain the property name.
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <returns>IBadRequestProvider</returns>
        IBadRequestProvider ForProperty(string propertyName);

        /// <summary>
        /// Should contain the message.
        /// </summary>
        /// <param name="message">The message value</param>
        /// <returns>IBadRequestProvider</returns>
        IBadRequestProvider WithMessage(string message);

        /// <summary>
        /// Should contain the errorCode.
        /// </summary>
        /// <param name="errorCode">The errorCode value</param>
        /// <returns>IBadRequestProvider</returns>
        IBadRequestProvider WithErrorCode(string errorCode);

        /// <summary>
        /// Should start with the message.
        /// </summary>
        /// <param name="message">The message value</param>
        /// <returns>IBadRequestProvider</returns>
        IBadRequestProvider StartsWithMessage(string message);

        /// <summary>
        /// Should end with the message.
        /// </summary>
        /// <param name="message">The message value</param>
        /// <returns>IBadRequestProvider</returns>
        IBadRequestProvider EndsWithMessage(string message);
    }
}