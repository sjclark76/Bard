using System.Net.Http;
using Bard.Internal;

namespace Bard.gRPC
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Make your http test client gRPC compatible
        /// </summary>
        /// <param name="httpClient"></param>
        /// <returns></returns>
        public static HttpClient ForGrpc(this HttpClient httpClient)
        {
            return HttpClientBuilder.CreateGrpcClient(httpClient);
        }
    }
}