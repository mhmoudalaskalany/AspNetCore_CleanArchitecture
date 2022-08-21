using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Task = System.Threading.Tasks.Task;

namespace BackendCore.Common.MiddleWares
{
    [ExcludeFromCodeCoverage]
    public class LanguageMiddleware
    {
        private readonly ILogger<LanguageMiddleware> _logger;
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;


        public LanguageMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _logger = loggerFactory?.CreateLogger<LanguageMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            var rqf = httpContext.Features.Get<IRequestCultureFeature>();
            if (rqf == null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                await _next(httpContext);
            }
            else
            {
                // Culture contains the information of the requested culture
                var culture = rqf.RequestCulture.Culture;
                Thread.CurrentThread.CurrentCulture = culture;
                await _next(httpContext);
            }
            


        }


    }
}


