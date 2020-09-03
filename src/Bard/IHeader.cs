using System;
using System.Net.Http.Headers;

namespace Bard
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHeader
    {
        IHeaderShould Should { get; }
        string ContentType { get; }
        EntityTagHeaderValue ETag { get; }
        Uri? ContentLocation { get; }
        DateTimeOffset? LastModified { get; }
        DateTimeOffset? Expires { get; }
        long? ContentLength { get; }
        void ShouldInclude(string headerName);
    }

    public interface IHeaderShould
    {
        IInclude Include { get; }
    }

    public interface IInclude
    {
        void ContentType();
    }
}