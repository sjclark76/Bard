using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Bard.Infrastructure;

namespace Bard.Internal.When
{
    internal class ApiRequest
    {
        public ApiRequest(string route, HttpContent httpContent)
        {
            Route = route;
            HttpContent = httpContent;
        }

        public ApiRequest(string route)
        {
            Route = route;
        }

        public ApiRequest(HttpRequestMessage request)
        {
            Route = request.RequestUri.AbsoluteUri;

            HttpContent = request.Content;
        }

        public string Route { get; }

        public HttpContent? HttpContent { get; }

        public string GenerateHash()
        {
            string input = Route;

            if (HttpContent != null)
                input = Route + AsyncHelper.RunSync(() => HttpContent.ReadAsStringAsync());

            MD5 md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (var t in hashBytes) sb.Append(t.ToString("X2"));
            return sb.ToString();
        }
    }
}