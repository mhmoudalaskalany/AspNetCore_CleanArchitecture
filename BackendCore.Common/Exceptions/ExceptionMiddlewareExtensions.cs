using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;

namespace BackendCore.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
