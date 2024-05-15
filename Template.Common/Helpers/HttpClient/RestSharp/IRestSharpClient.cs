using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Template.Common.Helpers.HttpClient.RestSharp
{
    public interface IRestSharpClient
    {
        Task<T> SendRequest<T>(string url, Method method, object obj = null, string urlEncoded = null,
            Dictionary<string, string> headers = null);

        Task<T> SendBasicRequest<T>(string url, Method method, string username, string password, object obj = null,
            Dictionary<string, string> headers = null);

    }
}