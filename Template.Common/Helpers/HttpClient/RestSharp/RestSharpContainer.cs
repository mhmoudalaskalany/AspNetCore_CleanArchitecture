using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace Template.Common.Helpers.HttpClient.RestSharp
{
    public class RestSharpContainer : IRestSharpContainer
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RestSharpContainer> _logger;
        private readonly RestClient _client;
        public RestSharpContainer(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<RestSharpContainer> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _logger = logger;
            _client = new RestClient();
        }
        public async Task<T> SendRequest<T>(string url, Method method, object obj = null, string urlEncoded = null, Dictionary<string, string> headers = null)
        {
            var request = new RestRequest(url, method);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }
            if (method == Method.Post || method == Method.Put)
            {
                if (urlEncoded != null)
                {
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddParameter("application/x-www-form-urlencoded", urlEncoded, ParameterType.RequestBody);
                }
                else
                {
                    SetJsonContent(request, obj);
                }
            }
            _logger.LogInformation($"Rest-Sharp: Url  {url}");
            if (_httpContextAccessor.HttpContext != null)
            {
                var accessToken = await _httpContextAccessor?.HttpContext?.GetTokenAsync("access_token");
                if (accessToken != null) request.AddHeader("Authorization", "Bearer " + accessToken);
            }

            var response = await _client.ExecuteAsync<T>(request);
            T data;
            try
            {
                data = JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception) { data = default(T); }
            return data == null ? response.Data : data;
        }

        public async Task<T> SendBasicRequest<T>(string url, Method method, string username, string password, object obj = null, Dictionary<string, string> headers = null)
        {
            try
            {
                _client.Authenticator =
                    new HttpBasicAuthenticator(username, password);

                var request = new RestRequest(url, method);


                request.AddHeader("Accept", "application/json");
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }

                if (method == Method.Post || method == Method.Put)
                {
                    SetJsonContent(request, obj);
                }
                _logger.LogInformation($"Rest-Sharp: URL {url}");
                var response = await _client.ExecuteAsync<T>(request);
                T data;
                try
                {
                    data = JsonConvert.DeserializeObject<T>(response.Content);

                }
                catch (Exception e)
                {
                    data = default(T);
                    _logger.LogInformation($"Rest-Sharp: Error At Exception Data  {JsonConvert.SerializeObject(response.StatusCode)}");
                }
                return data == null ? response.Data : data;
            }
            catch (Exception e)
            {
                _logger.LogError("Error At Rest Sharp Container" + JsonConvert.SerializeObject(e.Message));
                throw;
            }

        }


        private void SetJsonContent(RestRequest request, object obj)
        {
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(obj);
        }



    }
}
