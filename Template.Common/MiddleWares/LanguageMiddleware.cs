using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Task = System.Threading.Tasks.Task;

namespace Template.Common.MiddleWares
{
    [ExcludeFromCodeCoverage]
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;


        public LanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            var languageHeader = httpContext.Request.Headers["Accept-Language"];
            if (string.IsNullOrEmpty(languageHeader))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            }
            else if(languageHeader == "ar")
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-OM");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            }

            await _next(httpContext);





        }


    }
}


