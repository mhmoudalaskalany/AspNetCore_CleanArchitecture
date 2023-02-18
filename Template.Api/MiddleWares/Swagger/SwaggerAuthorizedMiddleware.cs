using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Template.Api.MiddleWares.Swagger
{
    /// <summary>
    /// Swagger Middleware
    /// </summary>
    public class SwaggerBasicAuthMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        public SwaggerBasicAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            //Make sure we are hitting the swagger path, and not doing it locally as it just gets annoying :-)
            if (context.User.Identity != null && context.Request.Path.StartsWithSegments("/swagger") && !context.User.Identity.IsAuthenticated)
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Get the encoded username and password
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    // Decode from Base64 to string
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    // Split username and password
                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(':', 2)[1];

                    // Check if login is correct
                    if (IsAuthorized(username, password))
                    {
                        await _next.Invoke(context);
                        return;
                    }
                }

                // Return authentication type (causes browser to show login dialog)
                context.Response.Headers["WWW-Authenticate"] = "Basic";

                // Return unauthorized
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsAuthorized(string username, string password)
        {
            var user = _configuration["SwaggerAuth:Username"];
            var swaggerPassword = _configuration["SwaggerAuth:Password"];
            // Check that username and password are correct
            return username.Equals(user, StringComparison.InvariantCultureIgnoreCase)
                    && password.Equals(swaggerPassword);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool IsLocalRequest(HttpContext context)
        {
            //Handle running using the Microsoft.AspNetCore.TestHost and the site being run entirely locally in memory without an actual TCP/IP connection
            if (context.Connection.RemoteIpAddress == null && context.Connection.LocalIpAddress == null)
            {
                return true;
            }
            if (context.Connection.RemoteIpAddress != null && context.Connection.RemoteIpAddress.Equals(context.Connection.LocalIpAddress))
            {
                return true;
            }
            if (context.Connection.RemoteIpAddress != null && IPAddress.IsLoopback(context.Connection.RemoteIpAddress))
            {
                return true;
            }
            return false;
        }
    }
}