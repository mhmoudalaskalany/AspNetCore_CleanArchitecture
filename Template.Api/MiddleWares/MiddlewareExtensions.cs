using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;

namespace Template.Api.MiddleWares
{
    /// <summary>
    /// Middleware  Pipeline Registration
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Configure Method
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<LanguageMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
