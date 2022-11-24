using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Common.Helpers.HttpClient.RestSharp
{
    public interface IRestSharpContainer
    {
        Task<T> SendRequest<T>(string url, Method method, object obj = null, string urlEncoded = null,
            Dictionary<string, string> headers = null);

        Task<T> SendBasicRequest<T>(string url, Method method, string username, string password, object obj = null,
            Dictionary<string, string> headers = null);

    }
}