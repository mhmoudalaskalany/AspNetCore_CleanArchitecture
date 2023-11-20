using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Template.Api.MiddleWares.Swagger;

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
            app.UseMiddleware<SwaggerBasicAuthMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

        }
    }
}
