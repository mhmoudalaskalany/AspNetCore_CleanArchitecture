using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;

namespace Common.MiddleWares
{
    [ExcludeFromCodeCoverage]
    public static class MiddlewareExtensions
    {
        public static void ConfigureCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<LanguageMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
